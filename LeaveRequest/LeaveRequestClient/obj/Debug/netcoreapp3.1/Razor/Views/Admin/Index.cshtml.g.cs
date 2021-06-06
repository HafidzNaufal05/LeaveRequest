#pragma checksum "D:\LeaveRequestProject\LeaveRequest\LeaveRequest\LeaveRequestClient\Views\Admin\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b65b747a1ba3de8104d8dfbd3d03c0de12690161"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Index), @"mvc.1.0.view", @"/Views/Admin/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\LeaveRequestProject\LeaveRequest\LeaveRequest\LeaveRequestClient\Views\_ViewImports.cshtml"
using LeaveRequestClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\LeaveRequestProject\LeaveRequest\LeaveRequest\LeaveRequestClient\Views\_ViewImports.cshtml"
using LeaveRequestClient.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b65b747a1ba3de8104d8dfbd3d03c0de12690161", @"/Views/Admin/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"18486de7349ee3163d6e680a7ab6956201a35989", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\LeaveRequestProject\LeaveRequest\LeaveRequest\LeaveRequestClient\Views\Admin\Index.cshtml"
  
    Layout = "DashboardLayout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""content-wrapper"">
    <!-- Content Header (Page header) -->
    <div class=""content-header"">
        <div class=""container-fluid"">
            <div class=""row mb-2"">
                <div class=""col-sm-6"">
                    <h1 class=""m-0"">Admin Dashboard</h1>
                </div><!-- /.col -->
                <div class=""col-sm-6"">
                    <ol class=""breadcrumb float-sm-right"">
                        <li class=""breadcrumb-item""><a href=""#"">Home</a></li>
                        <li class=""breadcrumb-item active"">Admin</li>
                    </ol>
                </div><!-- /.col -->
            </div><!-- /.row -->
        </div><!-- /.container-fluid -->
    </div>
    <!-- /.content-header -->
    <!-- Main content -->
    <section class=""content"">
        <div class=""container-fluid"">
            <!-- Small boxes (Stat box) -->
            <div class=""row"">
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
          ");
            WriteLiteral(@"          <div class=""small-box bg-info"">
                        <div class=""inner"">
                            <h3>150</h3>

                            <p>New Orders</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-bag""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
                    <div class=""small-box bg-success"">
                        <div class=""inner"">
                            <h3>53<sup style=""font-size: 20px"">%</sup></h3>

                            <p>Bounce Rate</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-stats-bars""></i>
                        ");
            WriteLiteral(@"</div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
                    <div class=""small-box bg-warning"">
                        <div class=""inner"">
                            <h3>44</h3>

                            <p>User Registrations</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-person-add""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class=""col-lg-3 col-6"">
                    <!-- small box -->
                    <div class=""small-box bg-danger"">
              ");
            WriteLiteral(@"          <div class=""inner"">
                            <h3>65</h3>

                            <p>Unique Visitors</p>
                        </div>
                        <div class=""icon"">
                            <i class=""ion ion-pie-graph""></i>
                        </div>
                        <a href=""#"" class=""small-box-footer"">More info <i class=""fas fa-arrow-circle-right""></i></a>
                    </div>
                </div>
                <!-- ./col -->
            </div>
            <!-- /.row -->
            <!-- Main row -->
            <div class=""row"">
                <!-- Left col -->
                <section class=""col-lg-7 connectedSortable"">
                    <!-- Calendar -->
                    <div class=""card bg-gradient-success"">
                        <div class=""card-header border-0"">

                            <h3 class=""card-title"">
                                <i class=""far fa-calendar-alt""></i>
                                Cal");
            WriteLiteral(@"endar
                            </h3>
                            <!-- tools card -->
                            <div class=""card-tools"">
                                <!-- button with a dropdown -->
                                <div class=""btn-group"">
                                    <button type=""button"" class=""btn btn-success btn-sm dropdown-toggle"" data-toggle=""dropdown"" data-offset=""-52"">
                                        <i class=""fas fa-bars""></i>
                                    </button>
                                    <div class=""dropdown-menu"" role=""menu"">
                                        <a href=""#"" class=""dropdown-item"">Add new event</a>
                                        <a href=""#"" class=""dropdown-item"">Clear events</a>
                                        <div class=""dropdown-divider""></div>
                                        <a href=""#"" class=""dropdown-item"">View calendar</a>
                                    </div>
                ");
            WriteLiteral(@"                </div>
                                <button type=""button"" class=""btn btn-success btn-sm"" data-card-widget=""collapse"">
                                    <i class=""fas fa-minus""></i>
                                </button>
                                <button type=""button"" class=""btn btn-success btn-sm"" data-card-widget=""remove"">
                                    <i class=""fas fa-times""></i>
                                </button>
                            </div>
                            <!-- /. tools -->
                        </div>
                        <!-- /.card-header -->
                        <div class=""card-body pt-0"">
                            <!--The calendar -->
                            <div id=""calendar"" style=""width: 100%""></div>
                        </div>
                        <!-- /.card-body -->
                    </div>
                    <!-- /.card -->
                </section>
                <!-- right col -->
     ");
            WriteLiteral(@"       </div>
            <!-- /.row (main row) -->
        </div><!-- /.container-fluid -->
    </section>
    <!-- /.content -->
</div>

<!-- Control Sidebar -->
<aside class=""control-sidebar control-sidebar-dark"">
    <!-- Control sidebar content goes here -->
</aside>
<!-- /.control-sidebar -->
<!-- ./wrapper -->
");
            DefineSection("script_admin", async() => {
                WriteLiteral(@"
    <script>
        var retrievedObject = localStorage.getItem('LoginRes');
        console.log('retrievedObject: ', JSON.parse(retrievedObject));
        currentLocation = window.location.pathname;
        console.log(currentLocation);

        if (retrievedObject != '""/Admin""') {
            window.location.replace('https://localhost:44350' + retrievedObject.replace(/^""(.*)""$/, '$1'))
        }
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
