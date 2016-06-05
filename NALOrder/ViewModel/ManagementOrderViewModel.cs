// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 05-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 05-6-2016
// ***********************************************************************

using System;

namespace NALOrder.ViewModel
{
    public class ManagementOrderViewModel
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public string Country { get; set; }
        public decimal Total { get; set; }
    }
}