using System;
using CreativProp.Utilities.Enums;

namespace CreativProp.Utilities.SQLHelpers
{
    public struct Order
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public SQLOrderingEnum Direction { get; set; }
    }
}

