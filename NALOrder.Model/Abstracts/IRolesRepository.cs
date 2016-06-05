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
    public interface IRolesRepository :ISingle<RoleDto>, IGet<RoleDto>, IAdd<RoleDto>, IUpdate<RoleDto>, IDelete<RoleDto>
    {
    }
}
