// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************


using NALOrder.Utilities;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NALOrder.Model
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// The _log service
        /// </summary>
        private ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="IProductRepository"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public ProductRepository(ILogService logService)
        {
            this._logService = logService;
        }

        public ProductDto Single(int id)
        {
            ProductDto result = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = (from item in context.Products
                              where item.IsDeleted == false && item.Id == id
                              select new ProductDto()
                              {
                                  ID = item.Id,
                                  UnitPrice = item.UnitPrice,
                                  ProductName = item.ProductName,
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

        public async Task<ProductDto> SingleAsync(int id)
        {
            ProductDto result = null;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = await (from item in context.Products
                                    where item.IsDeleted == false && item.Id == id
                                    select new ProductDto()
                                    {
                                        ID = item.Id,
                                        UnitPrice = item.UnitPrice,
                                        ProductName = item.ProductName,
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

        public IEnumerable<ProductDto> GetAll()
        {
            List<ProductDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = (from item in context.Products
                               where item.IsDeleted == false
                               select new ProductDto()
                               {
                                   ID = item.Id,
                                   UnitPrice = item.UnitPrice,
                                   ProductName = item.ProductName,
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

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {

            List<ProductDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = await (from item in context.Products
                                     where item.IsDeleted == false
                                     select new ProductDto()
                                     {
                                         ID = item.Id,
                                         UnitPrice = item.UnitPrice,
                                         ProductName = item.ProductName,
                                     }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                results = null;
            }
            return results;
        }

        public SaveResult Add(ProductDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Product add = context.Products.Create();

                    add.ProductName = entity.ProductName;
                    add.UnitPrice = entity.UnitPrice;
                    context.Entry<Product>(add).State = System.Data.Entity.EntityState.Added;
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

        public async Task<SaveResult> AddAsync(ProductDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Product add = context.Products.Create();

                    add.ProductName = entity.ProductName;
                    add.UnitPrice = entity.UnitPrice;
                    context.Entry<Product>(add).State = System.Data.Entity.EntityState.Added;
                    result = await context.SaveChangesAsync() > 0 ? SaveResult.SUCCESS : SaveResult.FAILURE;
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = SaveResult.FAILURE;
            }
            return result;
        }

        public SaveResult Update(ProductDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var product = context.Products.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    product.UnitPrice = entity.UnitPrice;
                    product.ProductName = entity.ProductName;

                    context.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
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

        public async Task<SaveResult> UpdateAsync(ProductDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var product = context.Products.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    product.UnitPrice = entity.UnitPrice;
                    product.ProductName = entity.ProductName;

                    context.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
                    result = await context.SaveChangesAsync() > 0 ? SaveResult.SUCCESS : SaveResult.FAILURE;
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = SaveResult.FAILURE;
            }

            return result;
        }

        public SaveResult Delete(ProductDto entity)
        {
            return DeleteBy(entity.ID);
        }

        public async Task<SaveResult> DeleteAsync(ProductDto entity)
        {
            return await DeleteByAsync(entity.ID);
        }

        public SaveResult DeleteBy(int Id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Products.Single(x => x.Id == Id && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<Product>(assembly).State = System.Data.Entity.EntityState.Modified;
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

        public async Task<SaveResult> DeleteByAsync(int Id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Products.Single(x => x.Id == Id && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<Product>(assembly).State = System.Data.Entity.EntityState.Modified;
                    result = await context.SaveChangesAsync() > 0 ? SaveResult.SUCCESS : SaveResult.FAILURE;
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = SaveResult.FAILURE;
            }

            return result;
        }

        public decimal GetPrice(int id)
        {
            decimal? result = 0;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = (from item in context.Products
                              where item.IsDeleted == false && item.Id == id
                              select item.UnitPrice).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
                result = 0;
            }
            return result.HasValue ? result.Value : 0;
        }
    }
}
