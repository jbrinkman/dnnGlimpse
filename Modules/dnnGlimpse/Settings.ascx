<%@ Control Inherits="DotNetNuke.Modules.dnnGlimpse.Settings" CodeBehind="Settings.ascx.cs" language="C#" AutoEventWireup="false" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm dnnHTMLSettings dnnClear">
	<div class="dnnFormItem">
		<dnn:label id="enableAllUsersLabel" controlname="enableAllUsers" runat="server" />
		<asp:CheckBox ID="enableAllUsers" runat="server" />
	</div>
</div>