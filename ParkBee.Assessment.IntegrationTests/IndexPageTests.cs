using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using CryptoRate.IntegrationTests.Common;
using CryptoRate.Web;
using CryptoRate.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using RazorPagesProject.Tests.Helpers;
using Xunit;

namespace CryptoRate.IntegrationTests
{
    #region snippet1
    public class IndexPageTests : 
        IClassFixture<TestingWebAppFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly TestingWebAppFactory<Startup>
            _factory;

        public IndexPageTests(
            TestingWebAppFactory<Startup>factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });
        }
        #endregion
        [Fact]
        public async Task Get_ProvidesCryptoCurrencies()
        {
            // Arrange
            var client = _factory.CreateClient();


            //Act
            var defaultPage = await client.GetAsync("/");
            var content = await HtmlHelpers.GetDocumentAsync(defaultPage);
            var optionElements = content.QuerySelectorAll("#SelectedCryptoCurrency>option");

            // Assert
            Assert.True(optionElements.Length==20);
        }
        
        [Fact]
        public async Task Post_ReturnCryptoCurrencyRates()
        {
            var initResponse = await _client.GetAsync("/");
            var antiForgeryValues = await AntiForgeryTokenExtractor.ExtractAntiForgeryValues(initResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/");
            postRequest.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryTokenExtractor.AntiForgeryCookieName, antiForgeryValues.cookieValue).ToString()); 
            
            var formModel = new Dictionary<string, string> 
            { 
                { 
                    AntiForgeryTokenExtractor.AntiForgeryFieldName, 
                    antiForgeryValues.fieldValue 
                },
                { "SelectedCryptoCurrency", "BTC" }
            }; 
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var content = await HtmlHelpers.GetDocumentAsync(response);
            var rateCards = content.QuerySelectorAll("div.card");
            Assert.True(rateCards.Any());
        }
       

       
    }
}
