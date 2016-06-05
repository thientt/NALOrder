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
    public interface IOrderRepository : ISingle<OrderDto>, IGet<OrderDto>, IAdd<OrderDto>, IUpdate<OrderDto>, IDelete<OrderDto>
    {
        OrderDto AddOrder(OrderDto order);

        SaveResult UpdateStatus(int id,string status);
    }
}
