using System;
using System.Collections.Generic;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Log.EventLog;
using Glimpse.AspNet.Extensibility;
using Glimpse.Core.Extensibility;

namespace DotNetNuke.Extensions.Glimpse
{
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
                var portalSettings = PortalSettings.Current;

                int totalRecords = 0;
                LogInfoArray logs = new LogController().GetLog(portalSettings.PortalId, 15, 0, ref totalRecords);

                if (logs.Count == 0)
                    return null;

                var data = new List<object[]> {new object[] {"Created Date", "Log Type", "UserName", "Content"}};
                for (int i = 0; i < logs.Count; i++)
                {
                    var log = logs.GetItem(i);

                    var logProperties = new List<object[]> {new object[] {"Property", "Value"}};
                    for (int j = 0; j < log.LogProperties.Count; j++)
                    {
                        var logDetail = (LogDetailInfo)log.LogProperties[j];
                        logProperties.Add(new object[] { logDetail.PropertyName, logDetail.PropertyValue });
                    }

                    data.Add(new object[] {log.LogCreateDate, log.LogTypeKey, log.LogUserName, logProperties});
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