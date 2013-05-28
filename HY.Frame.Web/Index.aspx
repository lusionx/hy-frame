<%@ Page Title="" Language="C#" MasterPageFile="~/BS.Master" AutoEventWireup="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>首页</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <h1>h1 标题
    </h1>
    <p id="result">
        <a href="/HY.Frame.Bis.class1.test2.c">link</a>
        <a href="DS.svc/com_person(guid'f18b3450-9187-4dc3-a83f-9bad00f16613')">one by id</a>
        curl --header "application/atomsvc+xml;q=0.8, application/json;odata=fullmetadata;q=0.7, application/json;q=0.5, */*;q=0.1"
    </p>
    <div class="row">
        <div class="span8">

        </div>
        <div class="span4">

        </div>
    </div>
    <hr />
    <div class="row">
        <div class="span8">
            <div class="alert">
                <button type="button" class="close" data-dismiss="alert">×</button>
                <strong>Warning!</strong> Best check yo self, you're not looking too good.
            </div>
            <div class="row">
                <div class="span4">
                    <form class="form-search">
                        <a class="btn">高级</a>
                        <input type="text" class="input-small search-query">
                        <button class="btn">Search</button>
                    </form>
                </div>
                <div class="span4">
                    <div class="input-prepend input-append">
                        <span class="add-on"><i class="icon-music"></i></span>
                        <input class="span2" id="appendedPrependedInput" type="text">
                        <span class="add-on">.00</span>
                    </div>
                </div>
            </div> 

            <table class="table table-condensed table-hover table-bordered table-striped">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Username</th>
                        <th>
                            <div class="btn-group">
                                <a class="btn btn-small btn-primary" href="#"><i class="icon-user icon-white"></i>User</a>
                                <a class="btn btn-small btn-primary dropdown-toggle" data-toggle="dropdown"><i class="caret"></i></a>
                                <ul class="dropdown-menu">
                                    <li><a href="#">Action</a></li>
                                    <li><a href="#">Another action</a></li>
                                    <li><a href="#">Something else here</a></li>
                                    <li class="divider"></li>
                                    <li><a href="./View/Sample.aspx">实例</a></li>
                                </ul>
                            </div>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>Mark</td>
                        <td>Otto</td>
                        <td>@mdo</td>
                        <td>
                            <div class="btn-group">
                                <button class="btn">1</button>
                                <button class="btn">2</button>
                                <button class="btn">3</button>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>Jacob</td>
                        <td>Thornton</td>
                        <td>@fat</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td colspan="2">Larry the Bird</td>
                        <td>@twitter</td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
            <div class="pagination pagination-centered">
                <ul>
                    <li class="disabled"><a href="#">&laquo;</a></li>
                    <li class="active"><a href="#">1</a></li>
                    <li><a href="#">2</a></li>
                    <li><a href="#">3</a></li>
                    <li><a href="#">4</a></li>
                    <li><a href="#">5</a></li>
                    <li><a href="#">&raquo;</a></li>
                </ul>
            </div>
        </div>
        <div class="span4">
            <form>
                <fieldset>
                    <legend>Legend</legend>
                    <label>Label name</label>
                    <input type="text" placeholder="Type something…">
                    <span class="help-block">Example block-level help text here.</span>
                    <label class="checkbox">
                        <input type="checkbox">
                        Check me out
                    </label>
                    <button type="submit" class="btn btn-primary">Submit</button>
                </fieldset>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
    <script src="/Scripts/JayDataContext.js"></script>
    <script src="/Core/index.js"></script>
</asp:Content>
