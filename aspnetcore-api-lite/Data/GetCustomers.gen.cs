namespace aspnetcore_api_lite.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using static GetCustomers;
    public interface IGetCustomers
    {

        List<CustomerLite> Execute();
        IEnumerable<CustomerLite> Execute(IDbConnection conn, IDbTransaction tx = null);
        System.String ExecuteScalar();
        System.String ExecuteScalar(IDbConnection conn, IDbTransaction tx = null);
        CustomerLite Create(IDataRecord record);

        CustomerLite GetOne();
        CustomerLite GetOne(IDbConnection conn, IDbTransaction tx = null);
        int ExecuteNonQuery();
        int ExecuteNonQuery(IDbConnection conn, IDbTransaction tx = null);

        string ExecutionMessages { get; }
    }

    public partial class GetCustomers : IGetCustomers
    {// props for params

        void AppendExececutionMessage(string msg) { ExecutionMessages += msg + Environment.NewLine; }
        public string ExecutionMessages { get; protected set; }

        public virtual int ExecuteNonQuery()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return ExecuteNonQuery(conn);
            }
        }
        public virtual int ExecuteNonQuery(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            using (IDbCommand cmd = conn.CreateCommand())
            {
                if (tx != null)
                    cmd.Transaction = tx;
                cmd.CommandText = getCommandText();

                var result = cmd.ExecuteNonQuery();

                // Assign output parameters to instance properties. 

                // only convert dbnull if nullable
                return result;
            }
        }

        public string getCommandText()
        {
            return @"
/* .sql query managed by QueryFirst add-in */
/*designTime - put parameter declarations and design time initialization here
endDesignTime*/

select C.CustomerID, C.CompanyName from Customers C
";
        }
        public virtual List<CustomerLite> Execute()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                var returnVal = Execute(conn).ToList();
                return returnVal;
            }
        }

        public virtual IEnumerable<CustomerLite> Execute(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            using (IDbCommand cmd = conn.CreateCommand())
            {
                if (tx != null)
                    cmd.Transaction = tx;
                cmd.CommandText = getCommandText();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return Create(reader);
                    }
                }

                // Assign output parameters to instance properties. These will be available after this method returns.

            }
        }
        public virtual CustomerLite GetOne()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return GetOne(conn);
            }
        }
        public virtual CustomerLite GetOne(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            {
                var all = Execute(conn, tx);
                CustomerLite returnVal;
                using (IEnumerator<CustomerLite> iter = all.GetEnumerator())
                {
                    iter.MoveNext();
                    returnVal = iter.Current;
                }
                return returnVal;
            }
        }
        public virtual System.String ExecuteScalar()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return ExecuteScalar(conn);
            }
        }

        public virtual System.String ExecuteScalar(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            using (IDbCommand cmd = conn.CreateCommand())
            {
                if (tx != null)
                    cmd.Transaction = tx;
                cmd.CommandText = getCommandText();

                var result = cmd.ExecuteScalar();

                // only convert dbnull if nullable
                // Assign output parameters to instance properties. 

                if (result == null || result == DBNull.Value)
                    return null;
                else
                    return (System.String)result;
            }
        }

        public virtual CustomerLite Create(IDataRecord record)
        {
            var returnVal = CreatePoco(record);

            if (record[0] != null && record[0] != DBNull.Value)
                returnVal.CustomerID = (string)record[0];

            if (record[1] != null && record[1] != DBNull.Value)
                returnVal.CompanyName = (string)record[1];

            returnVal.OnLoad();
            return returnVal;
        }
    }
    public partial class CustomerLite
    {
        protected string _CustomerID; //(nchar not null)
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        protected string _CompanyName; //(nvarchar not null)
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
    }
}
