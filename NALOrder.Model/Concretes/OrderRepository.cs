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
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace NALOrder.Model
{
    public class OrderRepository : IOrderRepository
    {

        /// <summary>
        /// The _log service
        /// </summary>
        private ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IOrderRepository"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public OrderRepository(ILogService logService)
        {
            this._logService = logService;
        }

        public IEnumerable<OrderDto> GetAll()
        {
            List<OrderDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = (from item in context.Orders
                               where item.IsDeleted == false
                               orderby item.OrderDate
                               select new OrderDto()
                               {
                                   ID = item.Id,
                                   Status = item.Status,
                                   OrderDate = item.OrderDate,
                                   Customer = new CustomerDto
                                   {
                                       ID = item.Customer.Id,
                                       Name = item.Customer.Name,
                                       Address = item.Customer.Address,
                                       Country = new CountryDto
                                       {
                                           ID = item.Customer.CountryId ?? 0,
                                           Name = item.Customer.Country.Name,
                                       },
                                   },
                               }).ToList();

                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                results = null;
            }
            return results;
        }

        public Task<System.Collections.Generic.IEnumerable<OrderDto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Add(OrderDto entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResult> AddAsync(OrderDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Update(OrderDto entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResult> UpdateAsync(OrderDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Delete(OrderDto entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResult> DeleteAsync(OrderDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult DeleteBy(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<SaveResult> DeleteByAsync(int Id)
        {
            throw new System.NotImplementedException();
        }

        public OrderDto AddOrder(OrderDto order)
        {
            OrderDto result = order;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Order add = context.Orders.Create();

                    add.CustomerId = order.CustomerId;
                    add.OrderDate = order.OrderDate;
                    add.Status = order.Status;

                    context.Entry<Order>(add).State = System.Data.Entity.EntityState.Added;
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

        public OrderDto Single(int id)
        {
            OrderDto result = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = (from item in context.Orders
                              where item.IsDeleted == false && item.Id == id
                              select new OrderDto()
                              {
                                  ID = item.Id,
                                  Status = item.Status,
                                  OrderDate = item.OrderDate,
                                  Customer = new CustomerDto
                                  {
                                      ID = item.Customer.Id,
                                      Name = item.Customer.Name,
                                      Address = item.Customer.Address,
                                      Country = new CountryDto
                                      {
                                          ID = item.Customer.CountryId ?? 0,
                                          Name = item.Customer.Country.Name,
                                      },
                                  },
                              }).Single();

                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = null;
            }
            return result;
        }

        public async Task<OrderDto> SingleAsync(int id)
        {
            OrderDto result = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = await (from item in context.Orders
                                    where item.IsDeleted == false && item.Id == id
                                    select new OrderDto()
                                    {
                                        ID = item.Id,
                                        Status = item.Status,
                                        OrderDate = item.OrderDate,
                                        Customer = new CustomerDto
                                        {
                                            ID = item.Customer.Id,
                                            Name = item.Customer.Name,
                                            Address = item.Customer.Address,
                                            Country = new CountryDto
                                            {
                                                ID = item.Customer.CountryId ?? 0,
                                                Name = item.Customer.Country.Name,
                                            },
                                        },
                                    }).SingleAsync();

                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = null;
            }
            return result;
        }

        public SaveResult UpdateStatus(int id, string status)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var order = context.Orders.Single(x => x.Id == id && x.IsDeleted == false);
                    order.Status = status;

                    context.Entry<Order>(order).State = System.Data.Entity.EntityState.Modified;
                    result = context.SaveChanges() > 0 ? SaveResult.SUCCESS : SaveResult.FAILURE;
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = SaveResult.FAILURE;
            }

            return result;
        }
    }
}
