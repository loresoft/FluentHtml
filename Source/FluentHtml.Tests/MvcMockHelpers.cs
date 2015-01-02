using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace FluentHtml.Tests
{
    public static class MvcMockHelpers
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();
            
            // context
            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            // context.User
            context.Setup(ctx => ctx.User).Returns(user.Object);
            user.Setup(ctx => ctx.Identity).Returns(identity.Object);
            // stream
            request.Setup(r => r.InputStream).Returns(new MemoryStream());
            response.Setup(r => r.OutputStream).Returns(new MemoryStream());

            return context.Object;
        }

        public static HttpContextBase FakeHttpContext(string url)
        {
            HttpContextBase context = FakeHttpContext();
            context.Request.SetupRequestUrl(url);
            return context;
        }


        public static Controller FakeController()
        {
            var controller = new Mock<Controller>();
            return controller.Object;
        }

        public static void SetFakeControllerContext(this Controller controller)
        {
            var httpContext = FakeHttpContext();
            var requestContext = new RequestContext(httpContext, new RouteData());
            var context = new ControllerContext(requestContext, controller);
            controller.ControllerContext = context;
        }


        public static void SetHttpMethodResult(this HttpRequestBase request, string httpMethod)
        {
            Mock.Get(request)
                .Setup(req => req.HttpMethod)
                .Returns(httpMethod);
        }

        public static void SetupRequestUrl(this HttpRequestBase request, string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            if (!url.StartsWith("~/"))
                throw new ArgumentException("Sorry, we expect a virtual url starting with \"~/\".");

            var mock = Mock.Get(request);

            mock.Setup(req => req.QueryString)
                .Returns(GetQueryStringParameters(url));

            mock.Setup(req => req.AppRelativeCurrentExecutionFilePath)
                .Returns(GetUrlFileName(url));
            
            mock.Setup(req => req.PathInfo)
                .Returns(string.Empty);
        }


        static string GetUrlFileName(string url)
        {
            return url.Contains("?") 
                ? url.Substring(0, url.IndexOf("?", StringComparison.Ordinal)) 
                : url;
        }

        static NameValueCollection GetQueryStringParameters(string url)
        {
            if (!url.Contains("?"))
                return null;

            var parameters = new NameValueCollection();

            string[] parts = url.Split("?".ToCharArray());
            string[] keys = parts[1].Split("&".ToCharArray());

            foreach (string key in keys)
            {
                string[] part = key.Split("=".ToCharArray());
                parameters.Add(part[0], part[1]);
            }

            return parameters;
        }
    }


}