using System;
using SQLite;

namespace CreativProp.Models
{
    public class ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}

