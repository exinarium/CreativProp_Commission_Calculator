using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativProp.Models;
using CreativProp.Constants;
using CreativProp.Utilities.Enums;
using CreativProp.Utilities.SQLHelpers;
using CreativProp.Utilities.Extensions;

namespace CreativProp.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : ModelBase, new()
    {
        public Repository()
        {
            if (Database.Connection == null)
            {
                throw new Exception(DatabaseConstants.DATABASE_INITIALIZED_ERROR);
            }

            Database.Connection.CreateTableAsync<T>().Wait();
        }

        public async Task<bool> AddAsync(T item)
        {
            return (await Database.Connection.InsertAsync(item) > 0);
        }

        public async Task<bool> UpdateAsync(T item)
        {
            return (await Database.Connection.UpdateAsync(item) > 0);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await Database.Connection.Table<T>()?.DeleteAsync(x => x.ID == id) > 0;
        }

        public async Task<T> GetAsync(int id)
        {
            return await Database.Connection.Table<T>().Where(x => x.ID == id)?.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ListAsync(Order? order = null, List<Search> searches = null, int pageNumber = 1,
            int itemsPerPage = 10, List<Filter<T>> filters = null, List<Join> joins = null)
        {
            //Create Query
            var query = $"SELECT * FROM {typeof(T).Name} ";

            //Join tables
            if (joins != null)
            {
                foreach (var join in joins)
                {
                    query += $"{join.JoinType.GetDescription()} {join.TableName} ON {typeof(T).Name}.{join.SourceProperty} = {join.TableName}.{join.TargetProperty} ";
                }
            }

            //Check for Where clause
            if (searches != null || filters != null)
            {
                query += "WHERE ";

                //Include optional search string check
                if (searches != null)
                {
                    for (int i = 0; i < searches.Count; i++)
                    {
                        if (i == 0)
                        {
                            query += "(";
                        }

                        query += $"{searches[i].TableName}.{searches[i].ColumnName} LIKE {searches[i].SearchString}%";

                        if (i != searches.Count - 1)
                        {
                            query += " OR ";
                        }
                        else
                        {
                            query += ") ";
                        }
                    }
                }

                //Include optional filter check
                if (filters != null)
                {
                    for (int i = 0; i < filters.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (searches != null)
                            {
                                query += "AND (";
                            }
                            else
                            {
                                query += "(";
                            }
                        }

                        var value = $"{filters[i].Value}";

                        if (filters[i].Value.GetType() == typeof(string) || filters[i].Value.GetType() == typeof(DateTime))
                        {
                            value = $"'{filters[i].Value}'";
                        }

                        query += $"{filters[i].TableName}.{filters[i].ColumnName} {filters[i].Operators.GetDescription()} {value}";

                        if (i != searches.Count - 1)
                        {
                            query += " AND ";
                        }
                        else
                        {
                            query += ") ";
                        }
                    }
                }
            }

            //Order results
            if (order != null)
            {
                query += $"ORDER BY {order?.TableName}.{order?.ColumnName} {order?.Direction.GetDescription()} ";
            }
            else
            {
                query += $"ORDER BY {typeof(T).Name}.ID ASC ";
            }

            //Limit items per page
            query += $"LIMIT {itemsPerPage} ";

            //Limit page number
            query += $"OFFSET {(pageNumber) * itemsPerPage}";

            return (await Database.Connection.QueryAsync<T>(query));
        }
    }
}

