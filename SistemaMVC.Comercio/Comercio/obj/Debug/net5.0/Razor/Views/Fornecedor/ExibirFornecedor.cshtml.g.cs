#pragma checksum "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f7967eb36df88db1e3769594e34bab575e262ba5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Fornecedor_ExibirFornecedor), @"mvc.1.0.view", @"/Views/Fornecedor/ExibirFornecedor.cshtml")]
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
#line 1 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\_ViewImports.cshtml"
using Comercio;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\_ViewImports.cshtml"
using Comercio.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7967eb36df88db1e3769594e34bab575e262ba5", @"/Views/Fornecedor/ExibirFornecedor.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8d203dc8a00f50d8d7278dc5ae1161af78da1fd1", @"/Views/_ViewImports.cshtml")]
    public class Views_Fornecedor_ExibirFornecedor : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Comercio.Models.FornecedorViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Fornecedor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "editar-fornecedor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
  
    ViewData["Title"] = "fornecedor/detalhes";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 6 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
Write(Model.Nome_empresa);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h1>\r\n<hr />\r\n<div>\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 11 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
       Write(Html.DisplayNameFor(model => model.Cnpj));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 14 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
       Write(Html.DisplayFor(model => model.Cnpj));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 17 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
       Write(Html.DisplayNameFor(model => model.Nome_empresa));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 20 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
       Write(Html.DisplayFor(model => model.Nome_empresa));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 23 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 26 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
       Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </dd>
    </dl>
</div>

<hr />
<div>
    <h4>ENDEREÇOS</h4>
    <table class=""table"">
        <thead>
            <tr>
                <th>
                    ENDEREÇO:
                </th>
                <th>

                </th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 46 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
             foreach (var item in Model.Endereco)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 50 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Logradouro));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ,\r\n                        ");
#nullable restore
#line 51 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral(" -\r\n                        ");
#nullable restore
#line 52 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Cep));

#line default
#line hidden
#nullable disable
            WriteLiteral(" -\r\n                        ");
#nullable restore
#line 53 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Cidade));

#line default
#line hidden
#nullable disable
            WriteLiteral(" -\r\n                        ");
#nullable restore
#line 54 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.UF));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 57 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </tbody>
    </table>
</div>
<hr />
<div>
    <h4>TELEFONES</h4>
    <table class=""table"">
        <thead>
            <tr>
                <th>
                    NÚMERO
                </th>
                <th>

                </th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 76 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
             foreach (var item in Model.Telefone)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 80 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Ddd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
#nullable restore
#line 81 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 84 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </tbody>
    </table>
</div>

<hr />

<div>
    <h4>VENDEDORES</h4>
    <table class=""table"">
        <thead>
            <tr>
                <th>
                    NOME
                </th>
                <th>
                    EMAIL
                </th>
                <th>
                    TELEFONE 1
                </th>
                <th>
                    TELEFONE 2
                </th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 111 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
             foreach (var item in Model.Vendedor)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 115 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 118 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 121 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Telefones.First().Ddd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
#nullable restore
#line 122 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Telefones.First().Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 125 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Telefones.Last().Ddd));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
#nullable restore
#line 126 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Telefones.Last().Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 129 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f7967eb36df88db1e3769594e34bab575e262ba515059", async() => {
                WriteLiteral("ATUALIZAR DADOS DO FORNECEDOR");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-fornecedor_id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 134 "C:\Users\rbonotto\Documents\Pessoal\Projetos\Git\appMVC.Comercio\SistemaMVC.Comercio\Comercio\Views\Fornecedor\ExibirFornecedor.cshtml"
                                                                              WriteLiteral(Model.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fornecedor_id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-fornecedor_id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["fornecedor_id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral("</div>\r\n<div>\r\n    <a");
            BeginWriteAttribute("href", " href=\"", 3857, "\"", 3864, 0);
            EndWriteAttribute();
            WriteLiteral(" onClick=\"history.go(-1); return false;\" class=\"btn btn-info btn-sm\"> VOLTAR</a>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Comercio.Models.FornecedorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
