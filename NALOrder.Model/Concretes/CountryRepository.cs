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
    public class CountryRepository : ICountryRepository
    {
        /// <summary>
        /// The _log service
        /// </summary>
        private ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepository"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public CountryRepository(ILogService logService)
        {
            this._logService = logService;
        }

        public IEnumerable<CountryDto> GetAll()
        {
            List<CountryDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = (from item in context.Countries
                               where item.IsDeleted == false
                               select new CountryDto()
                               {
                                   ID = item.Id,
                                   Name = item.Name,
                                   Description = item.Description
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

        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {

            List<CountryDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = await (from item in context.Countries
                                     where item.IsDeleted == false
                                     select new CountryDto()
                                     {
                                         ID = item.Id,
                                         Name = item.Name,
                                         Description = item.Description,
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

        public SaveResult Add(CountryDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Country add = context.Countries.Create();

                    add.Description = entity.Description;
                    add.Name = entity.Name;
                    context.Entry<Country>(add).State = System.Data.Entity.EntityState.Added;
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

        public async Task<SaveResult> AddAsync(CountryDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Country add = context.Countries.Create();

                    add.Description = entity.Description;
                    add.Name = entity.Name;
                    context.Entry<Country>(add).State = System.Data.Entity.EntityState.Added;
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

        public SaveResult Update(CountryDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var Country = context.Countries.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    Country.Name = entity.Name;
                    Country.Description = entity.Description;

                    context.Entry<Country>(Country).State = System.Data.Entity.EntityState.Modified;
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

        public async Task<SaveResult> UpdateAsync(CountryDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var Country = context.Countries.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    Country.Name = entity.Name;
                    Country.Description = entity.Description;

                    context.Entry<Country>(Country).State = System.Data.Entity.EntityState.Modified;
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

        public SaveResult Delete(CountryDto entity)
        {
            return DeleteBy(entity.ID);
        }

        public async Task<SaveResult> DeleteAsync(CountryDto entity)
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
                    var assembly = context.Countries.Single(x => x.Id == Id && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<Country>(assembly).State = System.Data.Entity.EntityState.Modified;
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
                    var assembly = context.Countries.Single(x => x.Id == Id && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<Country>(assembly).State = System.Data.Entity.EntityState.Modified;
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
    }
}
