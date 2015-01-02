using System;
using FluentAssertions;
using FluentHtml.Html.Tag;
using Xunit;

namespace FluentHtml.Tests.Html
{
    
    public class ButtonTests
    {
        [Fact]
        public void ButtonStrongNameTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Button(htmlHelper);
            var builder = new ButtonBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Text("Testing");


            var html = builder.ToHtmlString();
            html.Should().Be("<button id=\"Name\" name=\"Name\" type=\"button\">Testing</button>");
        }

        [Fact]
        public void ButtonHtml()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Button(htmlHelper);
            var builder = new ButtonBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Html("<p>Testing</p>");


            var html = builder.ToHtmlString();
            html.Should().Be("<button id=\"Name\" name=\"Name\" type=\"button\"><p>Testing</p></button>");
        }

        [Fact]
        public void ButtonHtmlEncode()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Button(htmlHelper);
            var builder = new ButtonBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Type(ButtonType.Submit)
                .Text("<p>Testing</p>");


            var html = builder.ToHtmlString();
            html.Should().Be("<button id=\"Name\" name=\"Name\" type=\"submit\">&lt;p&gt;Testing&lt;/p&gt;</button>");
        }

        [Fact]
        public void ButtonSecureNoAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Button(htmlHelper);
            var builder = new ButtonBuilder(component);

            builder
                .Name("CreditLimit")
                .Text("1000")
                .Secure("Finance", "Administrator");


            var html = builder.ToHtmlString();
            html.Should().Be("<button class=\"access-denied\" disabled=\"disabled\" id=\"CreditLimit\" name=\"CreditLimit\" type=\"button\">1000</button>");

        }

        [Fact]
        public void ButtonSecureAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper("test.user", new[] { "Finance" });

            var component = new Button(htmlHelper);
            var builder = new ButtonBuilder(component);

            builder
                .Name("CreditLimit")
                .Text("1000")
                .Secure("Finance", "Administrator");


            var html = builder.ToHtmlString();
            html.Should().Be("<button id=\"CreditLimit\" name=\"CreditLimit\" type=\"button\">1000</button>");

        }

    }
}