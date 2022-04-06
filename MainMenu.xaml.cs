using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MemoryProject
{

    public partial class MainMenu : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;

        /// <summary>
        /// In this method we initialize all the components and begin to play the background music.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
            Sounds.PlayMusic();
        }

        /// <summary>
        /// This method makes sure that when you press the quit button, a click sound will play and it will ask if you really want to quit the game.
        /// If you do want to quit the game, and you press yes. The shutdown music will play and after two seconds the game will be closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitButton(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            MessageBoxResult result = MessageBox.Show("Are you sure that you want to Quit?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Sounds.StopMusic();
                Sounds.Shutdown();
                _time = TimeSpan.FromSeconds(2);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    if (_time == TimeSpan.Zero) Close();

                    if (_time == TimeSpan.Zero) _timer.Stop();

                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);
            }
        }

        /// <summary>
        /// This method makes sure that when you click on the baby button, a click sound will play and the BabyGame window will be assigned so we can show it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BabyButton(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            BabyGame BabyGame = new BabyGame();
            BabyGame.Show();
        }

        /// <summary>
        /// This method makes sure that when you click on the student button, a click sound will play and the StudentGame window will be assigned so we can show it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StudentButton(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            StudentOptions StudentMenu = new StudentOptions();
            StudentMenu.Show();
        }

        /// <summary>
        /// This method makes sure that when you press the playmusic button, a click sound will play and the background music will play.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayMusicButton(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            Sounds.PlayMusic();
        }

        /// <summary>
        /// This method makes sure that when you press the stopmusic button, a click sound will play and the background music will stop playing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopMusicButton(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            Sounds.StopMusic();
        }
    }
}
