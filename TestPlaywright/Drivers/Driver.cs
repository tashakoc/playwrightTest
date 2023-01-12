using Microsoft.Playwright;

namespace TestPlaywright.Drivers
{
    public class Driver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browserer;

        public Driver() => _page = InitializePlaywright();

        public IPage Page => _page.Result;

        public void Dispose() => _browserer?.CloseAsync();

        public async Task <IPage> InitializePlaywright()
        {
            //Playwright
            var playwright = await Playwright.CreateAsync();

            //Browser
            _browserer = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                //Headless = false
            });

            //Page
            return await _browserer.NewPageAsync();

        }
    }
}
