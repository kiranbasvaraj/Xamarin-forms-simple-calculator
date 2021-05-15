using Xamarin.Forms;
using Prism;
using Prism.Ioc;
using XFCalculatorApp.ViewModels;
using XFCalculatorApp.Views;

namespace XFCalci
{
    public partial class App 
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CalculatorPage, CalculatorPageViewModel>();

        }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/CalculatorPage");
        }
    }
}
