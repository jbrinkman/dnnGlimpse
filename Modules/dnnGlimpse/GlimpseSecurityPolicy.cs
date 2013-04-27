using Glimpse.Core.Extensibility;
 
using DotNetNuke.Entities.Users;

namespace DotNetNuke.Modules.dnnGlimpse
{
    public class GlimpseSecurityPolicy:IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            return UserController.GetCurrentUserInfo().IsSuperUser ? RuntimePolicy.On : RuntimePolicy.Off;
        }

        public RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest; }
        }
    }
}