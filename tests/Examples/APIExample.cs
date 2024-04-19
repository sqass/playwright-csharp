using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[TestFixture]
public class APIExample : PageTest
{
    private IAPIRequestContext Request = null;

    [Test]
    public async Task APITestGetReturns200()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer ");

        Request = await this.Playwright.APIRequest.NewContextAsync(new()
        {
            // All requests we send go to this API endpoint.
            BaseURL = "http://127.0.0.1:7174",
            ExtraHTTPHeaders = headers,
        });

        var Response = await Request.GetAsync("/api/employee/1");
        Assert.AreEqual(200, Response.Status);
    }

    [Test]
    public async Task AddEmployeeReturns201()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", "Bearer ");

        Request = await this.Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "http://127.0.0.1:7174",
            ExtraHTTPHeaders = headers,
        });

        var data = new Dictionary<string, object>() {
            { "EmployeeId", 6 },
            { "FirstName", Faker.Name.First() },
            { "LastName", Faker.Name.Last()},
            { "Age", 41 },
            { "State", Faker.Address.UsStateAbbr() }
        };

        var Response = await Request.PostAsync("/api/employee", new() { DataObject = data });
        Assert.AreEqual(201, Response.Status);

    }
}