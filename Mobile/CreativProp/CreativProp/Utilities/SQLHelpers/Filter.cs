using CreativProp.Utilities.Enums;

namespace CreativProp.Utilities.SQLHelpers
{
	public struct Filter<T>
	{
		public string TableName { get; set; }
		public string ColumnName { get; set; }
		public SQLOperators Operators { get; set; }
		public T Value { get; set; }
	}
}

