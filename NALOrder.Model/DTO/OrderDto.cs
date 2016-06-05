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
    public class OrderDto : BaseDto
    {
        public Nullable<int> CustomerId { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string Status { get; set; }

        public  CustomerDto Customer { get; set; }
    }
}
