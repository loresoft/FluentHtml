using System;
using FluentHtml.Html.DropDown;
using Xunit;

namespace FluentHtml.Tests.Html
{
    
    public class DropDownListBuilderTest
    {
        [Fact]
        public void Build()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();
            var select = new DropDownList(htmlHelper);
            var builder = new DropDownListBuilder(select);

            builder
                .Name("test")
                .Placeholder("-- Select --")
                .DataValueField("Value")
                .DataTextField("Text");

            var html = builder.ToHtmlString();
        }
    }
}
