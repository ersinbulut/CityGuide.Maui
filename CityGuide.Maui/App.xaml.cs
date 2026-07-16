using Microsoft.Extensions.DependencyInjection;

namespace CityGuide.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window=new Window(new Views.RegisterPage());
            window.Width = 393;
            window.Height = 752;
            return window;
        }
    }
}