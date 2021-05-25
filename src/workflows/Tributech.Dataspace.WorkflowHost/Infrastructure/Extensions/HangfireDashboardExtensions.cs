using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tributech.Dataspace.WorkflowHost.Infrastructure.Extensions {
    public static class HangfireDashboardExtensions {

        public static IEndpointConventionBuilder MapHangfireDashboardWithNoAuthorization(this IEndpointRouteBuilder endpoints) {
            var dashboardOptions = new DashboardOptions() {
                Authorization = new IDashboardAuthorizationFilter[] { }
            };
            return endpoints.MapHangfireDashboard(dashboardOptions);
        }

    }
}
