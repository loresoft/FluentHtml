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

            var sb = new System.Text.StringBuilder();
            sb.AppendLine(@"<input checked=""checked"" id=""IsActive"" name=""IsActive"" type=""checkbox"" value=""true"" />");
            sb.Append(@"<input name=""IsActive"" type=""hidden"" value=""false"" />");

            var html = builder.ToHtmlString();
            html.Should().Be(sb.ToString());
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

            var sb = new System.Text.StringBuilder();
            sb.AppendLine(@"<label class=""sliderLabel"">");
            sb.AppendLine(@"<input checked=""checked"" id=""IsActive"" name=""IsActive"" type=""checkbox"" value=""true"" />");
            sb.AppendLine(@"<span class=""slider"">");
            sb.AppendLine(@"<span class=""sliderTrue"">On</span>");
            sb.AppendLine(@"<span class=""sliderFalse"">Off</span>");
            sb.AppendLine(@"<span class=""sliderBlock""></span>");
            sb.AppendLine(@"</span>");
            sb.Append(@"</label>");


            var html = builder.ToHtmlString();
            html.Should().Be(sb.ToString());

        }

    }
}