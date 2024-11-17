using System;
using System.ComponentModel;

namespace CreativProp.Utilities.Enums
{
	public enum CommissionCalculatorOptions
	{
		[Description("Calculate Sell Price")]
		CALCULATE_SELL_PRICE = 1,

		[Description("Calculate Proceeds To Seller")]
		CALCULATE_PROCEEDS_TO_SELLER = 2,

		[Description("Calculate Commission With VAT")]
		CALCULATE_COMMISSION_AMOUNT_WITH_VAT = 3,

		[Description("Calculate Commission Without VAT")]
		CALCULATE_COMMISSION_AMOUNT_WITHOUT_VAT = 4,

		[Description("Calculate Commission Percentage")]
		CALCULATE_COMMISSION_PERCENTAGE = 5,

		[Description("Calculate VAT Amount")]
		CALCULATE_VAT_AMOUNT = 6
	}
}

