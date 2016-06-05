// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using System.Collections.Generic;
namespace NALOrder.Model
{
    public interface IOrderDetailRepository : IGet<OrderDetailDto>, IAdd<OrderDetailDto>, IUpdate<OrderDetailDto>, IDelete<OrderDetailDto>
    {
        decimal TotalOrder(int orderId);

        IEnumerable<OrderDetailDto> GetByOrder(int id);
    }
}
