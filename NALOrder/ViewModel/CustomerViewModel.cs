// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using NALOrder.Model;
using System.ComponentModel.DataAnnotations;

namespace NALOrder.ViewModel
{
    public class CustomerViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public int CountryId { get; set; }
        public CountryDto Country { get; set; }
    }
}