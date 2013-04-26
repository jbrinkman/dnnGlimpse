using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Exceptions;
using Glimpse.AspNet.Extensibility;
using Glimpse.Core.Extensibility;
using DotNetNuke.Entities.Controllers;

namespace DotNetNuke.Extensions.Glimpse
{
    public class DNNGlimpsePlugin : AspNetTab
    {
        public override string Name
        {
            get { return "DNN"; }
        }

        public override object GetData(ITabContext context)
        {
            try
            {
                var hostSettings = HostController.Instance.GetSettingsDictionary();
                var portalSettings = PortalSettings.Current;
                var portalAliases = from PortalAliasInfo pa in new PortalAliasController().GetPortalAliasArrayByPortalID(portalSettings.PortalId)
                                    select pa.HTTPAlias;

                return new
                {
                    Host = new
                    {
                        AutoAddPortalAlias = hostSettings["AutoAddPortalAlias"],
                        ControlPane = hostSettings["ControlPanel"],
                        Extensions = hostSettings["FileExtensions"],
                        Caching = hostSettings["ModuleCaching"],
                        HostEmail = hostSettings["HostEmail"],
                        Title = hostSettings["HostTitle"],
                        UseFriendlyURLs = hostSettings["UseFriendlyUrls"],
                        SMTP = new { 
                            Server = hostSettings["SMTPServer"],
                            UserName = hostSettings["SMTPUsername"],
                            SSL = hostSettings["SMTPEnableSSL"],
                            Auth = hostSettings["SMTPAuthentication"]
                        }
                    },
                    Portal = new
                    {
                        ID = portalSettings.PortalId,
                        Name = portalSettings.PortalName,
                        Aliases = portalAliases.ToArray(),
                        SSL = new
                        {
                            Enabled = portalSettings.SSLEnabled,
                            Enforced = portalSettings.SSLEnforced,
                        },
                    },
                    User = new
                    {
                        ID = portalSettings.UserId,
                        Username = portalSettings.UserInfo.Username,
                        Roles = portalSettings.UserInfo.Roles,
                    },
                    Tab = new
                    {
                        ID = portalSettings.ActiveTab.TabID,
                        Name = portalSettings.ActiveTab.TabName,
                        Title = portalSettings.ActiveTab.Title,
                        Path = portalSettings.ActiveTab.TabPath,
                        Secure = portalSettings.ActiveTab.IsSecure,
                        SkinPath = portalSettings.ActiveTab.SkinPath,
                        SkinSource = portalSettings.ActiveTab.SkinSrc,
                    }
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
