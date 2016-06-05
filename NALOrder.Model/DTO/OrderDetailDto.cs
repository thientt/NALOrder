// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

namespace NALOrder.Model
{
    public class OrderDetailDto : BaseDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }

        public OrderDto Order { get; set; }
        public ProductDto Product { get; set; }

        private decimal total = 0;
        public decimal Total
        {
            get
            {
                total = Quantity * UnitPrice;
                return total;
            }
            set
            {
                total = value;
            }
        }
    }
}
