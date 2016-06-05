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
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NALOrder.Model
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        /// <summary>
        /// The _log service
        /// </summary>
        private ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IOrderDetailRepository"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public OrderDetailRepository(ILogService logService)
        {
            this._logService = logService;
        }

        public System.Collections.Generic.IEnumerable<OrderDetailDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<OrderDetailDto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Add(OrderDetailDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    OrderDetail add = context.OrderDetails.Create();

                    add.OrderId = entity.OrderId;
                    add.ProductId = entity.ProductId;
                    add.Quantity = entity.Quantity;
                    add.UnitPrice = entity.UnitPrice;

                    context.Entry<OrderDetail>(add).State = System.Data.Entity.EntityState.Added;
                    return context.SaveChanges() > 0 ? result = SaveResult.SUCCESS : result = SaveResult.FAILURE;

                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = SaveResult.FAILURE;
            }

            return result;
        }

        public System.Threading.Tasks.Task<SaveResult> AddAsync(OrderDetailDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Update(OrderDetailDto entity)
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task<SaveResult> UpdateAsync(OrderDetailDto entity)
        {
            throw new System.NotImplementedException();
        }

        public SaveResult Delete(OrderDetailDto entity)
        {
            throw new System.NotImplementedException();
        }

        public System.Threading.Tasks.Task<SaveResult> DeleteAsync(OrderDetailDto entity)
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

        public decimal TotalOrder(int orderId)
        {
            decimal results = 0;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = (from item in context.OrderDetails
                               where item.IsDeleted == false && item.OrderId == orderId
                               select new
                               {
                                   Price = item.UnitPrice,
                                   Qty = item.Quantity,
                               }).Sum(x => (x.Price * x.Qty));
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                results = 0;
            }
            return results;
        }

        public IEnumerable<OrderDetailDto> GetByOrder(int id)
        {
            IEnumerable<OrderDetailDto> results = null;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = (from item in context.OrderDetails
                               where item.IsDeleted == false && item.OrderId == id
                               select new OrderDetailDto
                               {
                                   ID = item.Id,
                                   Product = new ProductDto
                                   {
                                       ID = item.ProductId,
                                       ProductName = item.Product.ProductName
                                   },
                                   Quantity = item.Quantity,
                                   UnitPrice = item.UnitPrice,

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
    }
}
