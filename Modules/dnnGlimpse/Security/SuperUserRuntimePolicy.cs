// <copyright file="SuperUserRuntimePolicy.cs" company="DotNetNuke Corp.">
// dnnGlimpse
// Copyright (c) 2013
// by DotNetNuke Corp.
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;
 
using DotNetNuke.Entities.Users;

namespace DotNetNuke.Modules.dnnGlimpse.Security
{
    public class SuperUserRuntimePolicy : IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
            return UserController.GetCurrentUserInfo().IsSuperUser || policyContext.GetHttpContext().Request.IsLocal ? RuntimePolicy.On : RuntimePolicy.Off;
        }

        public RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest; }
        }
    }
}