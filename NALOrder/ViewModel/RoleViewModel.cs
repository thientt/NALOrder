// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 06-05-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 06-05-2016
// ***********************************************************************

using System;
using System.ComponentModel.DataAnnotations;

namespace NALOrder.ViewModel
{
    public class RoleViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the inserted by.
        /// </summary>
        /// <value>
        /// The inserted by.
        /// </value>
        public string LastUpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the inserted on.
        /// </summary>
        /// <value>
        /// The inserted on.
        /// </value>
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name length max is 50")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Max length max is 255")]
        public string Description { get; set; }
    }
}