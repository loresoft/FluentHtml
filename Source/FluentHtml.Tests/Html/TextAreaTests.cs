using System;
using FluentAssertions;
using FluentHtml.Html.Input;
using Xunit;

namespace FluentHtml.Tests.Html
{
    
    public class TextAreaTests
    {
        [Fact]
        public void TextAreaStrongNameTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new TextArea(htmlHelper);
            var builder = new TextAreaBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Columns(30)
                .Rows(5)
                .Value("Testing")
                .Placeholder("Name");


            var html = builder.ToHtmlString();
            html.Should().Be("<textarea cols=\"30\" id=\"Name\" name=\"Name\" placeholder=\"Name\" rows=\"5\">Testing</textarea>");
        }

        [Fact]
        public void TextAreaHtml()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new TextArea(htmlHelper);
            var builder = new TextAreaBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Columns(30)
                .Rows(5)
                .Value("<p>Testing</p>")
                .Placeholder("Name");


            var html = builder.ToHtmlString();
            html.Should().Be("<textarea cols=\"30\" id=\"Name\" name=\"Name\" placeholder=\"Name\" rows=\"5\">&lt;p&gt;Testing&lt;/p&gt;</textarea>");
        }

        [Fact]
        public void TextAreaSecureNoAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new TextArea(htmlHelper);
            var builder = new TextAreaBuilder(component);

            builder
                .Name("CreditLimit")
                .Value("1000")
                .Secure("Finance", "Administrator")
                .Placeholder("Credit Limit");


            var html = builder.ToHtmlString();
            html.Should().Be("<textarea class=\"access-denied\" id=\"CreditLimit\" name=\"CreditLimit\" placeholder=\"Credit Limit\" readonly=\"readonly\">1000</textarea>");

        }

        [Fact]
        public void TextAreaSecureAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper("test.user", new[] { "Finance" });

            var component = new TextArea(htmlHelper);
            var builder = new TextAreaBuilder(component);

            builder
                .Name("CreditLimit")
                .Value("1000")
                .Secure("Finance", "Administrator")
                .Placeholder("Credit Limit");


            var html = builder.ToHtmlString();
            html.Should().Be("<textarea id=\"CreditLimit\" name=\"CreditLimit\" placeholder=\"Credit Limit\">1000</textarea>");

        }

        [Fact]
        public void TextAreaSecureAccessRoleBuilderTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper("test.user", new[] { "Finance" });

            var component = new TextArea(htmlHelper);
            var builder = new TextAreaBuilder(component);

            builder
                .Name("CreditLimit")
                .Value("1000")
                .Secure(r => r.WriteRole("Finance", "Administrator"))
                .Placeholder("Credit Limit");


            var html = builder.ToHtmlString();
            html.Should().Be("<textarea id=\"CreditLimit\" name=\"CreditLimit\" placeholder=\"Credit Limit\">1000</textarea>");
        }

    }
}