using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MemoryProject
{
    public partial class SplashScreen : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;
        public SplashScreen()
        {
            InitializeComponent();
            Sounds.Startup();


            _time = TimeSpan.FromSeconds(3);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero) SplashTime();

                if (_time == TimeSpan.Zero) _timer.Stop();

                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
        }

        private void SplashTime()
        {
            MainMenu menuMain = new MainMenu();
            menuMain.Show();
            this.Close();
        }
    }
}
