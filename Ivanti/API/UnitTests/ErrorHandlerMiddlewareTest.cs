using Ivanti.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ErrorHandlerMiddlewareTest
    {
        [Fact]
        public async Task WhenAnyUnhandledExceptionOccurs_ThenResponseStatusCodeShouldBe500()
        {
            using var host = await new HostBuilder()
               .ConfigureWebHost(webBuilder =>
               {
                   webBuilder.UseTestServer()
                       .Configure(app =>
                       {
                           app.UseMiddleware<ErrorHandlerMiddleware>();
                           app.Use( async (context,next) => {
                               throw new Exception();
                           });
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync("/");

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task WhenNoUnhandledExceptionOccurs_ThenResponseStatusCodeShouldNotBe500()
        {
            using var host = await new HostBuilder()
               .ConfigureWebHost(webBuilder =>
               {
                   webBuilder.UseTestServer()
                       .Configure(app =>
                       {
                           app.UseMiddleware<ErrorHandlerMiddleware>();
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync("/");

            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
