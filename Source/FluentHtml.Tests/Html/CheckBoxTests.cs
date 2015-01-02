using System;
using FluentAssertions;
using FluentHtml.Html.Input;
using Xunit;

namespace FluentHtml.Tests.Html
{
    
    public class CheckBoxTests
    {
        [Fact]
        public void CheckBoxStrongName()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new CheckBox(htmlHelper);
            var builder = new CheckBoxBuilder(component);

            builder
                .Name<Contact, bool>(c => c.IsActive)
                .Checked();


            var html = builder.ToHtmlString();
            html.Should().Be(
@"<input checked=""checked"" id=""IsActive"" name=""IsActive"" type=""checkbox"" value=""true"" />
<input name=""IsActive"" type=""hidden"" value=""false"" />");

        }

        [Fact]
        public void CheckBoxNoHidden()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new CheckBox(htmlHelper);
            var builder = new CheckBoxBuilder(component);

            builder
                .Name("IsActive")
                .IncludeHidden(false);


            var html = builder.ToHtmlString();
            html.Should().Be(@"<input id=""IsActive"" name=""IsActive"" type=""checkbox"" value=""true"" />");
        }

        [Fact]
        public void SliderCheckBoxStrongName()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new SliderCheckBox(htmlHelper);
            var builder = new SliderCheckBoxBuilder(component);

            builder
                .Name<Contact, bool>(c => c.IsActive)
                .Checked();


            var html = builder.ToHtmlString();
            html.Should().Be(
@"<label class=""sliderLabel"">
<input checked=""checked"" id=""IsActive"" name=""IsActive"" type=""checkbox"" value=""true"" />
<span class=""slider"">
<span class=""sliderTrue"">On</span>
<span class=""sliderFalse"">Off</span>
<span class=""sliderBlock""></span>
</span>
</label>");

        }

    }
}