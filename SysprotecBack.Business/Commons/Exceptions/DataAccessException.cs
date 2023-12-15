namespace SysprotecBack.Business.Commons.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception innerException) : base($"DataAccess: {message}", innerException)
        {
        }
    }
}
