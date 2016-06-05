// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using NALOrder.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NALOrder.ViewModel
{
    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quality { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        public int Max { get; set; }
        public int No { get; set; }

        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
    }
}