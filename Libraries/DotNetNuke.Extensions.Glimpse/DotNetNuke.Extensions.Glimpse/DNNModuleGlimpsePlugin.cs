// <copyright file="DNNModuleGlimpsePlugin.cs" company="DotNetNuke Corp.">
// DotNetNuke.Extensions.Glimpse
// Copyright (c) 2013
// by DotNetNuke Corp.
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace DotNetNuke.Extensions.Glimpse
{
    using System;
    using System.Linq;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Security.Permissions;
    using DotNetNuke.Services.Exceptions;

    using global::Glimpse.AspNet.Extensibility;
    using global::Glimpse.Core.Extensibility;

    public class DNNModuleGlimpsePlugin : AspNetTab
    {
        public override string Name
        {
            get { return "DNN Modules"; }
        }

        public override object GetData(ITabContext context)
        {
            try
            {
                if (PortalSettings.Current.ActiveTab.TabID <= 0)
                {
                    return null;
                }

                return from module in new ModuleController().GetTabModules(PortalSettings.Current.ActiveTab.TabID).Values
                       select new
                                  {
                                      Title = module.ModuleTitle ?? module.DesktopModule.FriendlyName,
                                      module.ModuleID,
                                      module.AllTabs,
                                      module.CacheTime,
                                      module.ContainerSrc,
                                      module.Header,
                                      module.Footer,
                                      module.InheritViewPermissions,
                                      Permissions = from ModulePermissionInfo p in module.ModulePermissions
                                                    select new
                                                               {
                                                                   p.AllowAccess,
                                                                   p.PermissionName,
                                                                   p.RoleName,
                                                                   p.Username,
                                                               },
                                      module.DesktopModule.IsPremium,
                                      module.ModuleControl.ControlKey,
                                      module.ModuleControl.ControlSrc,
                                      module.PaneName,
                                      StartDate = Null.IsNull(module.StartDate) ? (DateTime?)null : module.StartDate,
                                      EndDate = Null.IsNull(module.EndDate) ? (DateTime?)null : module.EndDate,
                                      module.ModuleControl.SupportsPartialRendering,
                                      module.ModuleSettings,
                                  };
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return "There was an error loading the data for this tab";
            }
        }
    }
}