﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Ncf.Core.Cache;
using Senparc.Ncf.Core.Config;
using Senparc.Ncf.Core.Models;
using Senparc.Ncf.Core.Models.DataBaseModel;
using Senparc.Ncf.Core.MultiTenant;
using System;
using System.Threading.Tasks;

namespace Senparc.Ncf.Service.MultiTenant
{
    /// <summary>
    /// 多租户中间件
    /// </summary>
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var serviceProvider = context.RequestServices;
            var tenantInfoService = serviceProvider.GetRequiredService<TenantInfoService>();
            var requestTenantInfo = await tenantInfoService.SetScopedRequestTenantInfoAsync(context);//设置当前 Request 的 RequestTenantInfo 参数
            Console.WriteLine("TenantMiddleware requestTenantInfo:"+ requestTenantInfo.GetHashCode());
            await _next(context);
        }
    }
}
