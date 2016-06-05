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
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NALOrder.Model
{
    public class UserRepository : IUsersRepository
    {
        /// <summary>
        /// The _log service
        /// </summary>
        private ILogService _logService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SYSUserRepository"/> class.
        /// </summary>
        /// <param name="logService">The log service.</param>
        public UserRepository(ILogService logService)
        {
            this._logService = logService;
        }

        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public UserDto Single(int id)
        {
            UserDto result = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = (from item in context.Users
                              where item.IsDeleted == false && item.Id == id
                              select new UserDto()
                              {
                                  ID = item.Id,
                                  Email = item.Email,
                                  RegistedDate = item.RegistedDate,
                                  Phone = item.Phone,
                                  IsLocked = item.IsLocked,
                                  Firstname = item.Firstname,
                                  Lastname = item.Lastname,
                                  RoleId = item.RoleId,
                                  Role = new RoleDto() { ID = item.Role.Id, Name = item.Role.Name, Description = item.Role.Description },
                                  LastLoginDate = item.LastLoginDate,
                                  IsDeleted = item.IsDeleted,
                                  LastUpdatedBy = item.LastUpdatedBy,
                                  LastUpdate = item.LastUpdate,


                              }).Single();
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
            }
            return result;
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<UserDto> SingleAsync(int id)
        {
            UserDto result = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    result = await (from item in context.Users
                                    where item.IsDeleted == false && item.Id == id
                                    select new UserDto()
                                    {
                                        ID = item.Id,
                                        Email = item.Email,
                                        RegistedDate = item.RegistedDate,
                                        Phone = item.Phone,
                                        IsLocked = item.IsLocked,
                                        Firstname = item.Firstname,
                                        Lastname = item.Lastname,
                                        RoleId = item.RoleId,
                                        Role = new RoleDto() { ID = item.Role.Id, Name = item.Role.Name, Description = item.Role.Description },
                                        LastLoginDate = item.LastLoginDate,
                                        IsDeleted = item.IsDeleted,
                                        LastUpdatedBy = item.LastUpdatedBy,
                                        LastUpdate = item.LastUpdate,


                                    }).SingleAsync();
                }
            }
            catch (Exception ex)
            {
                _logService.Error(ex.Message, ex);
            }
            return result;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDto> GetAll()
        {
            IEnumerable<UserDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = (from item in context.Users
                               where item.IsDeleted == false
                               orderby item.Email
                               select new UserDto()
                               {
                                   ID = item.Id,
                                   Email = item.Email,
                                   RegistedDate = item.RegistedDate,
                                   Phone = item.Phone,
                                   IsLocked = item.IsLocked,
                                   Firstname = item.Firstname,
                                   Lastname = item.Lastname,

                                   RoleId = item.RoleId,
                                   Role = new RoleDto() { ID = item.Role.Id, Name = item.Role.Name, Description = item.Role.Description },
                                   LastLoginDate = item.LastLoginDate,
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
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            IEnumerable<UserDto> results = null;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    results = await (from item in context.Users
                                     where item.IsDeleted == false
                                     orderby item.Email
                                     select new UserDto()
                                     {
                                         ID = item.Id,
                                         Email = item.Email,
                                         RegistedDate = item.RegistedDate,
                                         Phone = item.Phone,
                                         IsLocked = item.IsLocked,
                                         Firstname = item.Firstname,
                                         Lastname = item.Lastname,
                                         RoleId = item.RoleId,
                                         Role = new RoleDto() { ID = item.Role.Id, Name = item.Role.Name, Description = item.Role.Description },
                                         LastLoginDate = item.LastLoginDate,
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
        public SaveResult Update(UserDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var update = context.Users.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    update.RoleId = entity.RoleId;
                    update.Phone = entity.Phone;
                    update.IsLocked = entity.IsLocked;
                    update.Firstname = entity.Firstname;
                    update.Lastname = entity.Lastname;
                    update.IsDeleted = entity.IsDeleted;
                    update.LastUpdatedBy = entity.LastUpdatedBy;
                    update.LastUpdate = DateTime.Now;

                    context.Entry<User>(update).State = System.Data.Entity.EntityState.Modified;
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
        public async Task<SaveResult> UpdateAsync(UserDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var update = context.Users.Single(x => x.Id == entity.ID && x.IsDeleted == false);

                    update.RoleId = entity.RoleId;
                    update.Phone = entity.Phone;
                    update.IsLocked = entity.IsLocked;
                    update.Firstname = entity.Firstname;
                    update.Lastname = entity.Lastname;
                    update.IsDeleted = entity.IsDeleted;
                    update.LastUpdatedBy = entity.LastUpdatedBy;
                    update.LastUpdate = DateTime.Now;

                    context.Entry<User>(update).State = System.Data.Entity.EntityState.Modified;
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
        public SaveResult Add(UserDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    User add = context.Users.Create();

                    add.Email = entity.Email;
                    add.PasswordHash = AppCipher.EncryptCipher(entity.PasswordHash);
                    add.RoleId = entity.RoleId;
                    add.RegistedDate = entity.RegistedDate;
                    add.Phone = entity.Phone;
                    add.IsLocked = entity.IsLocked;
                    add.Firstname = entity.Firstname;
                    add.Lastname = entity.Lastname;
                    add.LastLoginDate = entity.LastLoginDate;
                    add.IsDeleted = entity.IsDeleted;
                    add.LastUpdatedBy = entity.LastUpdatedBy;
                    add.LastUpdate = DateTime.Now;

                    context.Entry<User>(add).State = System.Data.Entity.EntityState.Added;
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
        public async Task<SaveResult> AddAsync(UserDto entity)
        {
            SaveResult result = SaveResult.FAILURE;
            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    User add = context.Users.Create();

                    add.Email = entity.Email;
                    add.PasswordHash = AppCipher.EncryptCipher(entity.PasswordHash);
                    add.RoleId = entity.RoleId;
                    add.RegistedDate = entity.RegistedDate;
                    add.Phone = entity.Phone;
                    add.IsLocked = entity.IsLocked;
                    add.Firstname = entity.Firstname;
                    add.Lastname = entity.Lastname;
                    add.LastLoginDate = entity.LastLoginDate;
                    add.IsDeleted = entity.IsDeleted;
                    add.LastUpdatedBy = entity.LastUpdatedBy;
                    add.LastUpdate = DateTime.Now;

                    context.Entry<User>(add).State = System.Data.Entity.EntityState.Added;
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
        public SaveResult Delete(UserDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Users.Single(x => x.Id == entity.ID && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
        public async Task<SaveResult> DeleteAsync(UserDto entity)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Users.Single(x => x.Id == entity.ID && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
                    var assembly = context.Users.Single(x => x.Id == Id && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
                    var assembly = context.Users.Single(x => x.Id == Id && x.IsDeleted == false);
                    assembly.IsDeleted = true;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
        /// Logins the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public UserDto Login(string email, string password)
        {
            UserDto result = null;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    string passHash = AppCipher.EncryptCipher(password);
                    result = (from item in context.Users
                              where item.Email == email &&
                              item.PasswordHash == passHash
                              select new UserDto()
                              {
                                  ID = item.Id,
                                  Email = item.Email,
                                  Firstname = item.Firstname,
                                  Lastname = item.Lastname,
                                  Phone = item.Phone,
                                  RegistedDate = item.RegistedDate,
                                  RoleId = item.RoleId,
                                  Role = new RoleDto() { ID = item.Role.Id, Name = item.Role.Name, Description = item.Role.Description },
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
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public async Task<UserDto> LoginAsync(string email, string password)
        {
            UserDto result = null;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    string passHash = AppCipher.EncryptCipher(password);
                    result = await (from item in context.Users
                                    where item.Email == email &&
                                    item.PasswordHash == passHash
                                    select new UserDto()
                                    {
                                        ID = item.Id,
                                        Email = item.Email,
                                        Firstname = item.Firstname,
                                        Lastname = item.Lastname,
                                        Phone = item.Phone,
                                        RegistedDate = item.RegistedDate,
                                        RoleId = item.RoleId,
                                        Role = new RoleDto() { ID = item.Role.Id, Name = item.Role.Name, Description = item.Role.Description },
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
        /// Unlockeds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public SaveResult Unlocked(int id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Users.Single(x => x.Id == id && x.IsDeleted == false);

                    assembly.IsLocked = false;
                    assembly.LastUpdate = DateTime.Now;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
        /// Lockeds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public SaveResult Locked(int id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Users.Single(x => x.Id == id && x.IsDeleted == false);

                    assembly.IsLocked = true;
                    assembly.LastUpdate = DateTime.Now;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
        /// Unlockeds the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<SaveResult> UnlockedAsync(int id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Users.Single(x => x.Id == id && x.IsDeleted == false);

                    assembly.IsLocked = false;
                    assembly.LastUpdate = DateTime.Now;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
        /// Lockeds the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<SaveResult> LockedAsync(int id)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var assembly = context.Users.Single(x => x.Id == id && x.IsDeleted == false);

                    assembly.IsLocked = true;
                    assembly.LastUpdate = DateTime.Now;

                    context.Entry<User>(assembly).State = System.Data.Entity.EntityState.Modified;
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
        /// Sets the role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleType">Type of the role.</param>
        /// <returns></returns>
        public SaveResult SetRole(int id, RoleType roleType)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var user = context.Users.Single(x => x.Id == id && x.IsDeleted == false);

                    user.RoleId = (int)roleType;
                    user.LastUpdate = DateTime.Now;

                    context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
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
        /// Sets the role asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleType">Type of the role.</param>
        /// <returns></returns>
        public async Task<SaveResult> SetRoleAsync(int id, RoleType roleType)
        {
            SaveResult result = SaveResult.FAILURE;

            try
            {
                using (OrderAppEntities context = new OrderAppEntities())
                {
                    var user = context.Users.Single(x => x.Id == id && x.IsDeleted == false);

                    user.RoleId = (int)roleType;
                    user.LastUpdate = DateTime.Now;

                    context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
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
