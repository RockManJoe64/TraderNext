﻿namespace TraderNext.Data.Relational.EntityConfiguration
{
    public static class SqlDataTypes
    {
        public static readonly string AmountType = "number(12,4)";

        public static readonly string ComplexIdType = "varchar(16)";

        public static readonly string DateType = "varchar(16)";

        public static readonly string TimestampType = "varchar(16)";

        public static readonly string PriceType = "number(8,2)";

        public static readonly string QuantityType = "number(12,0)";

        public static readonly string ShortCodeType = "varchar(6)";
    }
}
