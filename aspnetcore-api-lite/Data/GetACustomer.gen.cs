namespace aspnetcore_api_lite.Data
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using static GetACustomer;
    public interface IGetACustomer
    {

        List<CustomerFull> Execute(string custId);
        IEnumerable<CustomerFull> Execute(string custId, IDbConnection conn, IDbTransaction tx = null);
        System.String ExecuteScalar(string custId);
        System.String ExecuteScalar(string custId, IDbConnection conn, IDbTransaction tx = null);

        List<CustomerFull> Execute();
        IEnumerable<CustomerFull> Execute(IDbConnection conn, IDbTransaction tx = null);
        System.String ExecuteScalar();
        System.String ExecuteScalar(IDbConnection conn, IDbTransaction tx = null);
        CustomerFull Create(IDataRecord record);

        CustomerFull GetOne(string custId);
        CustomerFull GetOne(string custId, IDbConnection conn, IDbTransaction tx = null);
        CustomerFull GetOne();
        CustomerFull GetOne(IDbConnection conn, IDbTransaction tx = null);
        int ExecuteNonQuery(string custId);
        int ExecuteNonQuery(string custId, IDbConnection conn, IDbTransaction tx = null);
        int ExecuteNonQuery();
        int ExecuteNonQuery(IDbConnection conn, IDbTransaction tx = null);
        string CustId { get; set; }

        string ExecutionMessages { get; }
    }

    public partial class GetACustomer : IGetACustomer
    {// props for params
        public string CustId { get; set; }
        void AppendExececutionMessage(string msg) { ExecutionMessages += msg + Environment.NewLine; }
        public string ExecutionMessages { get; protected set; }
        public virtual int ExecuteNonQuery(string custId)
        {
            CustId = custId;

            var returnVal = ExecuteNonQuery();
            ;
            return returnVal;
        }

        public virtual int ExecuteNonQuery()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return ExecuteNonQuery(conn);
            }
        }
        public virtual int ExecuteNonQuery(string custId, IDbConnection conn, IDbTransaction tx = null)
        {
            CustId = custId;

            var returnVal = ExecuteNonQuery(conn, tx);
            ;
            return returnVal;
        }

        public virtual int ExecuteNonQuery(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            using (IDbCommand cmd = conn.CreateCommand())
            {
                if (tx != null)
                    cmd.Transaction = tx;
                cmd.CommandText = getCommandText();


                {
                    var myParam = cmd.CreateParameter();
                    myParam.Direction = ParameterDirection.Input;
                    myParam.ParameterName = "@custId";
                    myParam.DbType = (DbType)Enum.Parse(typeof(DbType), "StringFixedLength");
                    myParam.Value = (object)CustId ?? DBNull.Value;
                    cmd.Parameters.Add(myParam);
                }
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
declare @custId nchar(5);
endDesignTime*/
select * from Customers C 
where C.CustomerID = @custId

";
        }
        public virtual List<CustomerFull> Execute(string custId)
        {
            CustId = custId;

            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                var returnVal = Execute(conn).ToList();
                ;
                return returnVal;
            }
        }

        public virtual List<CustomerFull> Execute()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                var returnVal = Execute(conn).ToList();
                return returnVal;
            }
        }

        public virtual IEnumerable<CustomerFull> Execute(string custId, IDbConnection conn, IDbTransaction tx = null)
        {
            CustId = custId;

            var returnVal = Execute(conn);
            return returnVal;
        }
        public virtual IEnumerable<CustomerFull> Execute(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            using (IDbCommand cmd = conn.CreateCommand())
            {
                if (tx != null)
                    cmd.Transaction = tx;
                cmd.CommandText = getCommandText();

                {
                    var myParam = cmd.CreateParameter();
                    myParam.Direction = ParameterDirection.Input;
                    myParam.ParameterName = "@custId";
                    myParam.DbType = (DbType)Enum.Parse(typeof(DbType), "StringFixedLength");
                    myParam.Value = (object)CustId ?? DBNull.Value;
                    cmd.Parameters.Add(myParam);
                }
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
        public virtual CustomerFull GetOne(string custId)
        {
            CustId = custId;

            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                var returnVal = GetOne(conn);
                return returnVal;
            }
        }
        public virtual CustomerFull GetOne()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return GetOne(conn);
            }
        }
        public virtual CustomerFull GetOne(string custId, IDbConnection conn, IDbTransaction tx = null)
        {
            CustId = custId;

            {
                var returnVal = GetOne(conn);
                return returnVal;
            }
        }
        public virtual CustomerFull GetOne(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            {
                var all = Execute(conn, tx);
                CustomerFull returnVal;
                using (IEnumerator<CustomerFull> iter = all.GetEnumerator())
                {
                    iter.MoveNext();
                    returnVal = iter.Current;
                }
                return returnVal;
            }
        }
        public virtual System.String ExecuteScalar(string custId)
        {
            CustId = custId;

            var returnVal = ExecuteScalar();
            ;
            return returnVal;
        }

        public virtual System.String ExecuteScalar()
        {
            using (IDbConnection conn = QfRuntimeConnection.GetConnection())
            {
                conn.Open();
                return ExecuteScalar(conn);
            }
        }

        public virtual System.String ExecuteScalar(string custId, IDbConnection conn, IDbTransaction tx = null)
        {
            CustId = custId;

            var returnVal = ExecuteScalar(conn, tx);
            ;
            return returnVal;
        }
        public virtual System.String ExecuteScalar(IDbConnection conn, IDbTransaction tx = null)
        {
            ((SqlConnection)conn).InfoMessage += new SqlInfoMessageEventHandler(delegate (object sender, SqlInfoMessageEventArgs e) { AppendExececutionMessage(e.Message); });
            using (IDbCommand cmd = conn.CreateCommand())
            {
                if (tx != null)
                    cmd.Transaction = tx;
                cmd.CommandText = getCommandText();


                {
                    var myParam = cmd.CreateParameter();
                    myParam.Direction = ParameterDirection.Input;
                    myParam.ParameterName = "@custId";
                    myParam.DbType = (DbType)Enum.Parse(typeof(DbType), "StringFixedLength");
                    myParam.Value = (object)CustId ?? DBNull.Value;
                    cmd.Parameters.Add(myParam);
                }
                var result = cmd.ExecuteScalar();

                // only convert dbnull if nullable
                // Assign output parameters to instance properties. 

                if (result == null || result == DBNull.Value)
                    return null;
                else
                    return (System.String)result;
            }
        }

        public virtual CustomerFull Create(IDataRecord record)
        {
            var returnVal = CreatePoco(record);

            if (record[0] != null && record[0] != DBNull.Value)
                returnVal.CustomerID = (string)record[0];

            if (record[1] != null && record[1] != DBNull.Value)
                returnVal.CompanyName = (string)record[1];

            if (record[2] != null && record[2] != DBNull.Value)
                returnVal.ContactName = (string)record[2];

            if (record[3] != null && record[3] != DBNull.Value)
                returnVal.ContactTitle = (string)record[3];

            if (record[4] != null && record[4] != DBNull.Value)
                returnVal.Address = (string)record[4];

            if (record[5] != null && record[5] != DBNull.Value)
                returnVal.City = (string)record[5];

            if (record[6] != null && record[6] != DBNull.Value)
                returnVal.Region = (string)record[6];

            if (record[7] != null && record[7] != DBNull.Value)
                returnVal.PostalCode = (string)record[7];

            if (record[8] != null && record[8] != DBNull.Value)
                returnVal.Country = (string)record[8];

            if (record[9] != null && record[9] != DBNull.Value)
                returnVal.Phone = (string)record[9];

            if (record[10] != null && record[10] != DBNull.Value)
                returnVal.Fax = (string)record[10];

            returnVal.OnLoad();
            return returnVal;
        }
    }
    public partial class CustomerFull
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
        protected string _ContactName; //(nvarchar null)
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        protected string _ContactTitle; //(nvarchar null)
        public string ContactTitle
        {
            get { return _ContactTitle; }
            set { _ContactTitle = value; }
        }
        protected string _Address; //(nvarchar null)
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        protected string _City; //(nvarchar null)
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        protected string _Region; //(nvarchar null)
        public string Region
        {
            get { return _Region; }
            set { _Region = value; }
        }
        protected string _PostalCode; //(nvarchar null)
        public string PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }
        protected string _Country; //(nvarchar null)
        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        protected string _Phone; //(nvarchar null)
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        protected string _Fax; //(nvarchar null)
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
    }
}
