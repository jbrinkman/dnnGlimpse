// <copyright file="FeatureController.cs" company="DotNetNuke Corp.">
// dnnGlimpse
// Copyright (c) 2013
// by DotNetNuke Corp.
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Upgrade;
using System;

namespace DotNetNuke.Modules.dnnGlimpse.Components
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for dnnGlimpse
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController : IUpgradeable //: IPortable, ISearchable
    {

        public string UpgradeModule(string Version)
        {
            try
            {
                switch (Version)
                {
                    case "01.00.00":
                        int hostTabId = TabController.GetTabByTabPath(Null.NullInteger, "//Host", Null.NullString);

                        AddHostPage(hostTabId,
                                    "//Host//ManageGlimpse",
                                    "DotNetNuke Glimpse",
                                    "Manage Glimpse",
                                    "Manage Glimpse features",
                                    "~/desktopmodules/dnnglimpse/icons/glimpse_16px.gif",
                                    "~/desktopmodules/dnnglimpse/icons/glimpse_16px.gif");
                        break;
                }
                return "Success";

            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        public void AddHostPage(int parentId, string tabPath, string moduleFriendlyName, string tabName, string tabDescription, string smallIcon, string largeIcon)
        {
            var tabController = new TabController();
            var moduleController = new ModuleController();
            TabInfo hostTab;

            //Get web servers module
            ModuleDefinitionInfo moduleDef = ModuleDefinitionController.GetModuleDefinitionByFriendlyName(moduleFriendlyName);

            //Add Pages under Advanced Features Tab
            int tabId = TabController.GetTabByTabPath(Null.NullInteger, tabPath, Null.NullString);

            if (tabId == Null.NullInteger)
            {
                //Add host page
                hostTab = Upgrade.AddHostPage(tabName, tabDescription, smallIcon, largeIcon, true);
                hostTab.ParentId = parentId;
                tabController.UpdateTab(hostTab);

                //Add module to page
                Upgrade.AddModuleToPage(hostTab, moduleDef.ModuleDefID, tabName, largeIcon, true);
            }
            else
            {
                hostTab = tabController.GetTab(tabId, Null.NullInteger, false);
                foreach (var kvp in moduleController.GetTabModules(tabId))
                {
                    if (kvp.Value.DesktopModule.ModuleName == "ProfessionalPreview")
                    {
                        //Preview module so hard delete
                        moduleController.DeleteTabModule(tabId, kvp.Value.ModuleID, false);
                        break;
                    }
                }
                //Add module to page
                Upgrade.AddModuleToPage(hostTab, moduleDef.ModuleDefID, tabName, largeIcon, true);
            }
        }
    }
}