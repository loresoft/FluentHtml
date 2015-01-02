using System;
using FluentAssertions;
using FluentHtml.Html.Input;
using Xunit;

namespace FluentHtml.Tests.Html
{
    
    public class TextBoxTests
    {
        [Fact]
        public void TextBoxStrongNameTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new TextBox(htmlHelper);
            var builder = new TextBoxBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Value("Testing")
                .Placeholder("Name");


            var html = builder.ToHtmlString();
            html.Should().Be("<input id=\"Name\" name=\"Name\" placeholder=\"Name\" type=\"text\" value=\"Testing\" />");
        }

        [Fact]
        public void TextBoxSecureNoAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();
            
            var component = new TextBox(htmlHelper);
            var builder = new TextBoxBuilder(component);

            builder
                .Name("CreditLimit")
                .Value("1000")
                .Secure("Finance", "Administrator")
                .Placeholder("Credit Limit");


            var html = builder.ToHtmlString();
            html.Should().Be("<input class=\"access-denied\" id=\"CreditLimit\" name=\"CreditLimit\" placeholder=\"Credit Limit\" readonly=\"readonly\" type=\"text\" value=\"1000\" />");

        }

        [Fact]
        public void TextBoxSecureAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper("test.user", new[] {"Finance"});

            var component = new TextBox(htmlHelper);
            var builder = new TextBoxBuilder(component);

            builder
                .Name("CreditLimit")
                .Value("1000")
                .Secure("Finance", "Administrator")
                .Placeholder("Credit Limit");


            var html = builder.ToHtmlString();
            html.Should().Be("<input id=\"CreditLimit\" name=\"CreditLimit\" placeholder=\"Credit Limit\" type=\"text\" value=\"1000\" />");

        }

        [Fact]
        public void TextBoxSecureAccessRoleBuilderTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper("test.user", new[] { "Finance" });

            var component = new TextBox(htmlHelper);
            var builder = new TextBoxBuilder(component);

            builder
                .Name("CreditLimit")
                .Value("1000")
                .Secure(r => r.WriteRole("Finance", "Administrator"))
                .Placeholder("Credit Limit");


            var html = builder.ToHtmlString();
            html.Should().Be("<input id=\"CreditLimit\" name=\"CreditLimit\" placeholder=\"Credit Limit\" type=\"text\" value=\"1000\" />");
        }

    }
}
