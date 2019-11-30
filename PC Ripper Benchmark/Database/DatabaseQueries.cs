using System.Data;
using System.Data.SqlClient;

namespace PC_Ripper_Benchmark.Database
{

    /// <summary>
    /// The <see cref="DatabaseQueries"/> class.
    /// <para></para>Contains queries.
    /// <para>Author: David Hartglass, (c) all rights reserved.</para>
    /// </summary>

    public class DatabaseQueries
    {
        private SqlConnection sqlConnection;

        /// <summary>
        /// Default constructor.
        /// </summary>

        public DatabaseQueries()
        {
            this.sqlConnection = new SqlConnection(DatabaseConnection.GetConnectionString());
        }

        /// <summary>
        /// Returns a dataset containing the queries output.
        /// </summary>
        /// <param name="clockSpeed">The clockspeed for the query.</param>
        /// <returns></returns>

        public DataSet RunCPUQueries(int clockSpeed)
        {
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

        /// <summary>
        /// Returns a dataset containing the queries output.
        /// </summary>
        /// <param name="size">The size for the query.</param>
        /// <returns></returns>

        public DataSet RunDISKQueries(int size)
        {
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
