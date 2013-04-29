// <copyright file="DNNGlimpsePlugin.cs" company="DotNetNuke Corp.">
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

    using DotNetNuke.Entities.Controllers;
    using DotNetNuke.Entities.Host;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Personalization;

    using global::Glimpse.AspNet.Extensibility;
    using global::Glimpse.Core.Extensibility;

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
                        Host.ControlPanel,
                        Host.AllowedExtensionWhitelist,
                        Host.ModuleCachingMethod,
                        Host.HostEmail,
                        Host.HostTitle,
                        Host.UseFriendlyUrls,
                        SMTP = new { 
                            Server = Host.SMTPServer,
                            UserName = Host.SMTPUsername,
                            SSL = Host.EnableSMTPSSL,
                            Auth = Host.SMTPAuthentication,
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
                        portalSettings.UserInfo.Username,
                        portalSettings.UserInfo.Roles,
                    },
                    Tab = new
                    {
                        ID = portalSettings.ActiveTab.TabID,
                        Name = portalSettings.ActiveTab.TabName,
                        portalSettings.ActiveTab.Title,
                        Path = portalSettings.ActiveTab.TabPath,
                        Secure = portalSettings.ActiveTab.IsSecure,
                        SkinSource = portalSettings.ActiveTab.SkinSrc,
                    },
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
