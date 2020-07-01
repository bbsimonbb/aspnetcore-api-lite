using aspnetcore_api_lite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_api_lite.Services
{
    public class CustomerService
    {
        private readonly List<CustomerLite> _countries;
        private IGetCustomers _listQuery;
        private IGetACustomer _detailQuery;

        public CustomerService():this(new GetCustomers(), new GetACustomer()){}
        public CustomerService(IGetCustomers listQuery, IGetACustomer detailQuery)
        {
            _listQuery = listQuery;
            _detailQuery = detailQuery;
        }

        public CustomerFull Get(string id)
        {
            return _detailQuery.GetOne(id);
        }

        public List<CustomerLite> Get()
        {
            return _listQuery.Execute();
        }
    }
}