#pragma checksum "E:\App\Test\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "70efef3422f60b4f7816ecad77e9e7f31e28fd42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "E:\App\Test\Views\_ViewImports.cshtml"
using Test;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\App\Test\Views\_ViewImports.cshtml"
using Test.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"70efef3422f60b4f7816ecad77e9e7f31e28fd42", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b938626c1cb27b4184c87d029e4bd0625527155", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "E:\App\Test\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">Welcome</h1>
    <p>Learn about <a href=""https://docs.microsoft.com/aspnet/core"">building Web apps with ASP.NET Core</a>.</p>
</div>
<button id=""testImport"">Test Import</button>
<span id=""testImportResult"" class=""card-title""></span>
<br />
<input id=""testReplaceText"">
<button id=""testReplace"">Test Replace</button>
<span id=""testReplaceResult"" class=""card-title""></span>
<br />

<span>ID</span>
<input id=""testUpdateDoc"">
<span>Field</span>
<input id=""testUpdateField"">
<span>Value</span>
<input id=""testUpdateText"">
<button id=""testUpdate"">Test Update</button>
<span id=""testUpdateResult"" class=""card-title""></span>

<br />

<span>ID</span>
<input id=""testAddFieldDoc"">
<span>Field</span>
<input id=""testAddFieldName"">
<span>Value</span>
<input id=""testAddFieldText"">
<button id=""testAddFieldValue"">Test Add Field</button>
<span id=""testAddFieldResult"" class=""card-title""></span>

<br />
<span>ID</span>
<input id=""testRemoveFieldDoc""");
            WriteLiteral(@">
<span>Field</span>
<input id=""testRemoveFieldName"">

<button id=""testRemoveField"">Test Remove Field</button>
<span id=""testRemoveFieldResult"" class=""card-title""></span>

<br />
<span>ID</span>
<input id=""testDeleteDoc"">


<button id=""testDelete"">Test Delete Doc</button>
<span id=""testRemoveDocResult"" class=""card-title""></span>");
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
