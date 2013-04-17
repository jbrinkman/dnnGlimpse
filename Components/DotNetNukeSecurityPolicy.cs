using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;
using DotNetNuke.Entities.Users;

namespace DotNetNuke.Modules.dnnGlimpse.Components
{
    public class DotNetNukeSecurityPolicy : IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            UserInfo user = UserController.GetCurrentUserInfo();
            RuntimePolicy runtimePolicy = user.IsSuperUser ? RuntimePolicy.On : RuntimePolicy.Off; 

            return runtimePolicy;
        }

        public RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest; }
        }
    }
}