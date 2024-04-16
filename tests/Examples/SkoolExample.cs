using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;


[TestFixture]
public class Skool : PageTest
{
    [Test]
    public async Task SkoolDotComGearIconRedirectsToLoginPage()
    {
        await Page.GotoAsync("https://www.skool.com");

        await Page.GetByRole(AriaRole.Button).First.ClickAsync();
        await Page.GetByRole(AriaRole.Button).Nth(37).ClickAsync();

        await Expect(Page).ToHaveURLAsync(new Regex("https://www.skool.com/login\\?redirect=%2Fsettings%3Ft%3Dcommunities"));
    }
}