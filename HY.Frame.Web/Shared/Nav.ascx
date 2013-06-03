<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Nav.ascx.cs" Inherits="HY.Frame.Web.Shared.Nav" %>

<ul class="nav">
    <% foreach (var item in new HY.Auth.AuthedUser("sys,user").GetNodes()) { %>
    <%  if (item.Desc == "divider-vertical") { %>
        <li class="divider-vertical"></li>
    <%} else if(item.Children!=null && item.Children.Count > 0) { %>
        <li class="dropdown">
            <a href="<%=item.Url %>" class="dropdown-toggle" data-toggle="dropdown"><%=item.Title %><b class="caret"></b></a>
            <ul class="dropdown-menu" role="menu">
                <% foreach (var item2 in item.Children) { %>
                    <% if (item2.Desc == "divider") { %>
                        <li class="divider"></li>
                    <% } else if (item2.Children != null && item2.Children.Count > 0) { %>
                        <li class="dropdown-submenu"><a tabindex="-1" href="<%=item2.Url %>"><%=item2.Title %></a>
                            <ul class="dropdown-menu" role="menu">
                        <% foreach (var item3 in item2.Children) { %>
                            <li><a tabindex="-1" href="<%=item3.Url %>"><%=item3.Title %></a></li>
                        <%} %>
                            </ul>
                        </li>
                    <% } else { %>
                        <li><a tabindex="-1" href="<%=item2.Url %>"><%=item2.Title %></a></li>
                <%}}%>
            </ul>
    <%} else { %>
        <li><a href="<%=item.Url %>"><%=item.Title %></a></li>
    <%}}%>
</ul>
