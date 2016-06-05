// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************
using System;

namespace NALOrder.Model
{
    public class UserDto:BaseDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public System.DateTime RegistedDate { get; set; }
        public bool IsLocked { get; set; }
        public string LastUpdatedBy { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public int RoleId { get; set; }

        public RoleDto Role { get; set; }
    }
}
