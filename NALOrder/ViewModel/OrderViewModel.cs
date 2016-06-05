// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using NALOrder.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NALOrder.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public int CountryId { get; set; }
        public IEnumerable<CountryDto> Countries { get; set; }

        public int ProductId { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }

        public List<OrderDetailViewModel> OrderDetail { get; set; }

        public string CounntryName { get; set; }
        public DateTime? DateOrder { get; set; }
        public string Status { get; set; }
    }
}