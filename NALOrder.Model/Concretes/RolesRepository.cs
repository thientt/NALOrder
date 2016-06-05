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
    public class RolesRepository : IRolesRepository
    {
        /// <summary>
        /// The _log service
        /// </summary>
        private ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SYSRolesRepository"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public RolesRepository(ILogService logService)
        {
            this._logService = logService;
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public RoleDto Single(int id)
        {
            RoleDto result = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = (from item in context.Roles
                              where item.IsDeleted == false && item.Id == id
                              select new RoleDto()
                              {
                                  ID = item.Id,
                                  Name = item.Name,
                                  Description = item.Description,
                                  IsDeleted = item.IsDeleted,
                                  LastUpdatedBy = item.LastUpdatedBy,
                                  LastUpdate = item.LastUpdate,
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

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<RoleDto> SingleAsync(int id)
        {
            RoleDto result = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = await (from item in context.Roles
                                    where item.IsDeleted == false && item.Id == id
                                    select new RoleDto()
                                    {
                                        ID = item.Id,
                                        Name = item.Name,
                                        Description = item.Description,
                                        IsDeleted = item.IsDeleted,
                                        LastUpdatedBy = item.LastUpdatedBy,
                                        LastUpdate = item.LastUpdate,
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

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleDto> GetAll()
        {
            IEnumerable<RoleDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = (from item in context.Roles
                               where item.IsDeleted == false
                               select new RoleDto()
                               {
                                   ID = item.Id,
                                   Name = item.Name,
                                   Description = item.Description,
                                   IsDeleted = item.IsDeleted,
                                   LastUpdatedBy = item.LastUpdatedBy,
                                   LastUpdate = item.LastUpdate,
                               }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
            }
            return results;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            IEnumerable<RoleDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = await (from item in context.Roles
                                     where item.IsDeleted == false
                                     select new RoleDto()
                                     {
                                         ID = item.Id,
                                         Name = item.Name,
                                         Description = item.Description,
                                         IsDeleted = item.IsDeleted,
                                         LastUpdatedBy = item.LastUpdatedBy,
                                         LastUpdate = item.LastUpdate,
                                     }).ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
            }
            return results;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public SaveResult Update(RoleDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var role = context.Roles.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    role.Name = entity.Name;
                    role.IsDeleted = entity.IsDeleted;
                    role.Description = entity.Description;
                    role.LastUpdatedBy = entity.LastUpdatedBy;
                    role.LastUpdate = DateTime.Now;

                    context.Entry<Role>(role).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<SaveResult> UpdateAsync(RoleDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var role = context.Roles.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    role.Name = entity.Name;
                    role.IsDeleted = entity.IsDeleted;
                    role.Description = entity.Description;
                    role.LastUpdatedBy = entity.LastUpdatedBy;
                    role.LastUpdate = DateTime.Now;

                    context.Entry<Role>(role).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public SaveResult Add(RoleDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Role add = context.Roles.Create();

                    add.Description = entity.Description;
                    add.IsDeleted = false;
                    add.Name = entity.Name;
                    add.LastUpdatedBy = entity.LastUpdatedBy;
                    add.LastUpdate = DateTime.Now;

                    context.Entry<Role>(add).State = System.Data.Entity.EntityState.Added;
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

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<SaveResult> AddAsync(RoleDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    Role add = context.Roles.Create();

                    add.Description = entity.Description;
                    add.Name = entity.Name;
                    add.IsDeleted = false;
                    add.LastUpdatedBy = entity.LastUpdatedBy;
                    add.LastUpdate = DateTime.Now;

                    context.Entry<Role>(add).State = System.Data.Entity.EntityState.Added;
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

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public SaveResult Delete(RoleDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var role = context.Roles.Single(x => x.Id == entity.ID && x.IsDeleted == false);
                    role.IsDeleted = true;
                    role.LastUpdate = DateTime.Now;
                    role.LastUpdatedBy = entity.LastUpdatedBy;

                    context.Entry<Role>(role).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<SaveResult> DeleteAsync(RoleDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var role = context.Roles.Single(x => x.Id == entity.ID && x.IsDeleted == false);
                    role.IsDeleted = true;
                    role.LastUpdate = DateTime.Now;
                    role.LastUpdatedBy = entity.LastUpdatedBy;

                    context.Entry<Role>(role).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// Deletes the by.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public SaveResult DeleteBy(int Id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var role = context.Roles.Single(x => x.Id == Id && x.IsDeleted == false);
                    role.IsDeleted = true;
                    role.LastUpdate = DateTime.Now;

                    context.Entry<Role>(role).State = System.Data.Entity.EntityState.Modified;
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

        /// <summary>
        /// Deletes the by asynchronous.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public async Task<SaveResult> DeleteByAsync(int Id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var role = context.Roles.Single(x => x.Id == Id && x.IsDeleted == false);
                    role.IsDeleted = true;
                    role.LastUpdate = DateTime.Now;

                    context.Entry<Role>(role).State = System.Data.Entity.EntityState.Modified;
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
