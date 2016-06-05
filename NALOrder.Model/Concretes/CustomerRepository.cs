// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using NALOrder.Utilities;
using System;
namespace NALOrder.Model
{
    public class CustomerRepository : ICustomerRepository
    {

        /// <summary>
        /// The _log service
        /// </summary>
        private ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ICustomerRepository"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public CustomerRepository(ILogService logService)
        {
            this._logService = logService;
        }

        public System.Collections.Generic.IEnumerable<CustomerDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<CustomerDto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Add(CustomerDto entity)
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task<SaveResult> AddAsync(CustomerDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Update(CustomerDto entity)
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task<SaveResult> UpdateAsync(CustomerDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Delete(CustomerDto entity)
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task<SaveResult> DeleteAsync(CustomerDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult DeleteBy(int Id)
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task<SaveResult> DeleteByAsync(int Id)
        {
            throw new System.NotImplementedException();
        }

        public CustomerDto AddCustomer(CustomerDto customer)
        {
            CustomerDto result = customer;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Customer add = context.Customers.Create();

                    add.Name = customer.Name;
                    add.Phone = customer.Phone;
                    add.Email = customer.Email;
                    add.CountryId = customer.CountryId;
                    customer.Address = customer.Address;

                    context.Entry<Customer>(add).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();

                    result.ID = add.Id;
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = null;
            }

            return result;
        }
    }
}
