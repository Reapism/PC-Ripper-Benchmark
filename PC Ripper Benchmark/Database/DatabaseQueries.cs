using System.Data;
using System.Data.SqlClient;

namespace PC_Ripper_Benchmark.database {
    public class DatabaseQueries {
        private SqlConnection sqlConnection;

        public DatabaseQueries() {
            this.sqlConnection = new SqlConnection(DatabaseConnection.GetConnectionString());
        }

        public DataSet RunCPUQueries(int clockSpeed) {
            this.sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT Model, Series, Codename, Clock_Speed FROM dbo.[CPU] WHERE Clock_Speed >= @clockSpeed", this.sqlConnection);
            cmd.Parameters.AddWithValue("@clockSpeed", clockSpeed);

            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            this.sqlConnection.Close();

            return ds;
        }

        public DataSet RunDISKQueries(int size) {
            this.sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SSD WHERE Size_In_GB >= @size;", this.sqlConnection);
            cmd.Parameters.AddWithValue("@size", size);

            cmd.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            this.sqlConnection.Close();

            return ds;
        }
    }
}
