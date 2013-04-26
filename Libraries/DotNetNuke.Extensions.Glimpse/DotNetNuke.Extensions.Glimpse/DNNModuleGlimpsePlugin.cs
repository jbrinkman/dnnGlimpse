using System;
using System.Collections.Generic;
using System.Linq;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;

using Glimpse.AspNet.Extensibility;
using Glimpse.Core.Extensibility;

namespace DotNetNuke.Extensions.Glimpse
{
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
                var portalSettings = PortalSettings.Current;

                if (portalSettings.ActiveTab.TabID <= 0)
                    return null;

                var modules = new ModuleController().GetTabModules(portalSettings.ActiveTab.TabID).Values.ToArray();

                var data = new List<object[]> { new object[] { "Module Name", "Module Properties" } };
                foreach (var module in modules)
                {
                    var moduleData = new List<object[]>
                                         {
                                             new object[] { "Property", "Value" },
                                             new object[] { "Module ID", module.ModuleID },
                                             new object[] { "On all Tabs", module.AllTabs },
                                             new object[] { "Cache Time", module.CacheTime },
                                             new object[] { "Container Path", module.ContainerPath },
                                             new object[] { "Container Src", module.ContainerSrc },
                                             new object[] { "Header", module.Header },
                                             new object[] { "Footer", module.Footer },
                                             new object[] { "Inherit View Permissions", module.InheritViewPermissions },
                                             new object[] { "Is Premium", module.DesktopModule.IsPremium },
                                             new object[] { "Control Key", module.ModuleControl.ControlKey },
                                             new object[] { "Control Source", module.ModuleControl.ControlSrc },
                                             new object[] { "Permissions", module.DesktopModule.Permissions },
                                             new object[] { "Pane", module.PaneName },
                                             new object[] { "Start Date", module.StartDate },
                                             new object[] { "End Date", module.EndDate },
                                             new object[] { "Supports Partial Rendering", module.ModuleControl.SupportsPartialRendering },
                                         };

                    var settings = new ModuleController().GetModuleSettings(module.ModuleID);

                    var moduleSettings = new List<object[]> { new object[] { "Setting", "Value" } };
                    foreach (var settingKey in settings.Keys)
                        moduleSettings.Add(new object[] { settingKey.ToString(), settings[settingKey].ToString() });
                    moduleData.Add(new object[] { "Settings", moduleSettings });

                    data.Add(new object[] { (module.ModuleTitle ?? module.DesktopModule.FriendlyName), moduleData });
                }

                return data;
            }
            catch (Exception ex)
            {
                Exceptions.LogException(ex);
                return "There was an error loading the data for this tab";
            }
        }
    }
}