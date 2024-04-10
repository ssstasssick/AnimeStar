
namespace AnimeStar
{
    public static class GetConnString
    {
        public static string GetString()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return connectionString;
        }
        public static string GetRootString()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RootConnection"].ConnectionString;
            return connectionString;
        }
    }
}
