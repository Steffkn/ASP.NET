﻿namespace Blog.Web.Routes.Tests
{
    using System.Web.Routing;
    using Blog.Web.Controllers;
    using MvcRouteTester;
    using NUnit.Framework;

    [TestFixture]
    public class JokesRouteTests
    {
        [Test]
        public void TestRouteById()
        {
            const string Url = "/Joke/Mjc2NS4xMjMxMjMxMzEyMw==";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<JokesController>(c => c.ById("Mjc2NS4xMjMxMjMxMzEyMw=="));
        }
    }
}
