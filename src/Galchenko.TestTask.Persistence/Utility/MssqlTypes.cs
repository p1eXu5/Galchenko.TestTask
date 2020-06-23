
namespace Galchenko.TestTask.Persistence.Utility
{
    public static class MssqlTypes
    {
        public const string INT = "int";
        public const string DATE = "date";
        public const string DATE_TIME_OFFSET = "datetimeoffset";

        public static string NVARCHAR( int? max = null ) => $"nvarchar({max?.ToString() ?? "max"})";
        public static string NCHAR( int? max = null ) => $"nchar({max?.ToString() ?? "max"})";
    }
}
