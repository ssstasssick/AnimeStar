
namespace AnimeStar
{
    public static class GetConnString
    {
        public static string GetString()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return connectionString;
        }
    }
}
