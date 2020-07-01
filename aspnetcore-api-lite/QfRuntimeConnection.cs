using System.Data;
using System.Data.SqlClient;

/* What provider are you using? For SqlClient, you will need to add a project reference (.net framework) or 
the System.Data.SqlClient nuget package (.net core). */


namespace aspnetcore_api_lite
{
    class QfRuntimeConnection
    {
        public static IDbConnection GetConnection()
        {
            return new SqlConnection("Server=localhost\\SQLEXPRESS;Database=NORTHWND;Trusted_Connection=True;");
        }
    }
}