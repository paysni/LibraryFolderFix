using System.Threading.Tasks;
using Atlas_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Atlas_Web.Authorization
{
    public class ReportRunAuthorizationHandler
        : AuthorizationHandler<PermissionRequirement, ReportObject>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement,
            ReportObject report
        )
        {
            // Only catch - Crystal Report (3) & Reporting Workbench (17)
            if (report.ReportObjectTypeId != 3 && report.ReportObjectTypeId != 17)
            {
                context.Succeed(requirement);
            }

            var groups = context.User.Claims
                .Where(x => x.Type == "Group")
                .Select(x => x.Value)
                .ToList();

            // check hrx
            if (report.ReportGroupsMemberships.Any(x => groups.Contains(x.GroupId.ToString())))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // check hrg
            if (
                report.ReportObjectHierarchyParentReportObjects.Any(
                    x =>
                        x.ParentReportObject.ReportGroupsMemberships.Any(
                            y => groups.Contains(y.GroupId.ToString())
                        )
                )
            )
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }

    public class PermissionRequirement : IAuthorizationRequirement { }
}
