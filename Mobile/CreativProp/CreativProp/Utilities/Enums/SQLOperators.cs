using System;
using System.ComponentModel;

namespace CreativProp.Utilities.Enums
{
	public enum SQLOperators
	{
		[Description(">")]
		GT = 1,
		[Description(">=")]
		GTE = 2,
		[Description("<")]
		LT = 3,
		[Description("<=")]
		LTE = 4,
		[Description("=")]
		EQ = 5,
		[Description("<>")]
		NEQ = 6,
		[Description("BETWEEN")]
		BETWEEN = 7,
		[Description("IN")]
		IN = 8,
		[Description("NOT IN")]
		NOT_IN = 9,
		[Description("IS NULL")]
		NULL = 10,
		[Description("IS NOT NULL")]
		NOT_NULL
	}
}

