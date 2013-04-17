<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="DotNetNuke.Modules.dnnGlimpse.View" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnJsInclude ID="glimpseInclude" runat="server" FilePath="~/Desktopmodules/dnnGlimpse/Scripts/jquery.glimpse.js" Priority="20" />

<a id="glimpseControl" class="dnnPrimaryAction" href=".">Enable Glimpse</a>