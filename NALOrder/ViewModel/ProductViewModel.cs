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
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
        
        public int Quatity { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}