using System.Data;
using System.Data.SqlClient;

namespace PC_Ripper_Benchmark.database
{
    public class DatabaseQueries
    {
        SqlConnection sqlConnection;

        public DatabaseQueries()
        {
            sqlConnection = new SqlConnection(DatabaseConnection.GetConnectionString());
        }

        public DataSet RunCPUQueries(int clockSpeed)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT Model, Series, Codename, Clock_Speed FROM dbo.[CPU] WHERE Clock_Speed >= @clockSpeed", sqlConnection);
            cmd.Parameters.AddWithValue("@clockSpeed", clockSpeed);

            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            sqlConnection.Close();

            return ds;
        }

        public DataSet RunDISKQueries(int size)
        {
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SSD WHERE Size_In_GB >= @size;", sqlConnection);
            cmd.Parameters.AddWithValue("@size", size);

            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            sqlConnection.Close();

            return ds;
        }
    }
}
