using System;
using System.ComponentModel;

namespace CreativProp.Utilities.Enums
{
	public enum SQLJoinType
	{
		[Description("INNER JOIN")]
		INNER = 1,
		[Description("RIGHT OUTER JOIN")]
		RIGHT_OUTER = 2,
		[Description("LEFT OUTER JOIN")]
		LEFT_OUTER = 3,
		[Description("SELF JOIN")]
		SELF = 4,
		[Description("CROSS JOIN")]
		CROSS = 5
	}
}

