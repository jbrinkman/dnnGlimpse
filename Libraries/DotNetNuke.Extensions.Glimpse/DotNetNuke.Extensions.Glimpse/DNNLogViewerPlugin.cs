// <copyright file="DNNLogViewerPlugin.cs" company="DotNetNuke Corp.">
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

    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Log.EventLog;

    using global::Glimpse.AspNet.Extensibility;
    using global::Glimpse.Core.Extensibility;

    public class DNNLogViewerPlugin : AspNetTab
    {
        public override string Name
        {
            get { return "DNN Log"; }
        }

        public override object GetData(ITabContext context)
        {
            try
            {
                var totalRecords = 0;
                return from LogInfo log in new LogController().GetLog(PortalSettings.Current.PortalId, 15, 0, ref totalRecords)
                       select new
                                  {
                                      CreatedDate = log.LogCreateDate,
                                      LogType = log.LogTypeKey,
                                      Username = log.LogUserName,
                                      Content = log.LogProperties.Cast<LogDetailInfo>().ToDictionary(p => p.PropertyName, p => p.PropertyValue),
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