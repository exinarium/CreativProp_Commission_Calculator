using System;
using CreativProp.Utilities.Enums;

namespace CreativProp.Utilities.SQLHelpers
{
	public struct Join
	{
		public string TableName { get; set; }
		public string SourceProperty { get; set; }
		public string TargetProperty { get; set; }
		public SQLJoinType JoinType { get; set; }
	}
}

