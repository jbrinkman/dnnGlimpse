using Glimpse.AspNet.Extensibility;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Tab.Assist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.dnnGlimpse.Tab
{
    public class DotNetNukeTab : AspNetTab
    {
        public override object GetData(ITabContext context)
        {
            var plugin = Plugin.Create("Feedback");

            var instructions = @"<h2>Using Glimpse with DotNetNuke</h2><div>This module is still under construction.  If you have DotNetNuke specific data that would be helpful to see here, then create an item in the <a href='http://machinemonitor.codeplex.com/workitem/list/basic'>issue tracker on CodePlex</a>.</div>";

            plugin.AddRow()
                .Column(instructions).Raw();

            return plugin;
        }

        public override string Name
        {
            get { return "DotNetNuke"; }
        }
    }
}