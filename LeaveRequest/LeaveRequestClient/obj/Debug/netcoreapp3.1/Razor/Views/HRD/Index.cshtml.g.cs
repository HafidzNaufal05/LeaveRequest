#pragma checksum "D:\BOOTCAMP\PROJEK\LeaveRequest\LeaveRequest\LeaveRequestClient\Views\HRD\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "081d827b5bbbeef5111023bd1f85358d91a1bdd8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_HRD_Index), @"mvc.1.0.view", @"/Views/HRD/Index.cshtml")]
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
#line 1 "D:\BOOTCAMP\PROJEK\LeaveRequest\LeaveRequest\LeaveRequestClient\Views\_ViewImports.cshtml"
using LeaveRequestClient;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\BOOTCAMP\PROJEK\LeaveRequest\LeaveRequest\LeaveRequestClient\Views\_ViewImports.cshtml"
using LeaveRequestClient.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"081d827b5bbbeef5111023bd1f85358d91a1bdd8", @"/Views/HRD/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"18486de7349ee3163d6e680a7ab6956201a35989", @"/Views/_ViewImports.cshtml")]
    public class Views_HRD_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<p>INI HRD</p>

<script>
    var retrievedObject = localStorage.getItem('LoginRes');
    console.log('retrievedObject: ', JSON.parse(retrievedObject));
    currentLocation = window.location.pathname;
    console.log(currentLocation);

    if (retrievedObject != '""/HRD""') {
        window.location.replace('https://localhost:44383' + retrievedObject.replace(/^""(.*)""$/, '$1'))
    }
</script>");
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
