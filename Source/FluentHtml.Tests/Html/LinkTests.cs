using System;
using FluentAssertions;
using FluentHtml.Html.Tag;
using Xunit;

namespace FluentHtml.Tests.Html
{
    
    public class LinkTests
    {
        [Fact]
        public void LinkStrongNameTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Link(htmlHelper);
            var builder = new LinkBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Action("Schedule", "Invoice")
                .Text("Testing");


            var html = builder.ToHtmlString();
            html.Should().Be("<a href=\"/test/app/Invoice/Schedule\" id=\"Name\">Testing</a>");
        }

        [Fact]
        public void LinkHtml()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Link(htmlHelper);
            var builder = new LinkBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Action("Schedule", "Invoice")
                .Html("<p>Testing</p>");


            var html = builder.ToHtmlString();
            html.Should().Be("<a href=\"/test/app/Invoice/Schedule\" id=\"Name\"><p>Testing</p></a>");
        }

        [Fact]
        public void LinkHtmlEncode()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Link(htmlHelper);
            var builder = new LinkBuilder(component);

            builder
                .Name<Contact, string>(c => c.Name)
                .Action("Schedule", "Invoice")
                .Text("<p>Testing</p>");


            var html = builder.ToHtmlString();
            html.Should().Be("<a href=\"/test/app/Invoice/Schedule\" id=\"Name\">&lt;p&gt;Testing&lt;/p&gt;</a>");
        }

        [Fact]
        public void LinkSecureNoAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper();

            var component = new Link(htmlHelper);
            var builder = new LinkBuilder(component);

            builder
                .Name("CreditLimit")
                .Action("Schedule", "Invoice")
                .Text("1000")
                .Secure("Finance", "Administrator");


            var html = builder.ToHtmlString();
            html.Should().Be("<a class=\"access-denied\" disabled=\"disabled\" id=\"CreditLimit\">1000</a>");

        }

        [Fact]
        public void LinkSecureAccessTest()
        {
            var htmlHelper = MvcHelper.GetHtmlHelper("test.user", new[] { "Finance" });

            var component = new Link(htmlHelper);
            var builder = new LinkBuilder(component);

            builder
                .Name("CreditLimit")
                .Action("Schedule", "Invoice")
                .Text("1000")
                .Secure("Finance", "Administrator");


            var html = builder.ToHtmlString();
            html.Should().Be("<a href=\"/test/app/Invoice/Schedule\" id=\"CreditLimit\">1000</a>");

        }

    }
}