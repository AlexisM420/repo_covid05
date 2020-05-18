using System.Windows;

namespace Inventaire
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindowView _window;

        public App() 
        { 
        
            _window = new MainWindowView();

            _window.Show();
        }
    }
}
