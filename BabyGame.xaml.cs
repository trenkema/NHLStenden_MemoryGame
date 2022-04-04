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

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for BabyGame.xaml
    /// </summary>
    public partial class BabyGame : Window
    {
        private MemoryGrid mgrid;
        public static bool babyTheme = false;

        /// <summary>
        /// First we set the babyTheme to true so it will use the correct background and cards.
        /// Then we call the InitializeComponent to define everything.
        /// After that we call the MemoryGrid class so we can use it's variables and methods.
        /// </summary>
        public BabyGame()
        {
            babyTheme = true;
            InitializeComponent();
            mgrid = new MemoryGrid(BabyMemoryGrid, true);
        }

        /// <summary>
        /// This method makes sure that when you press the exit to mainmenu button, a click sound will play and it will ask if you really want to go back to the main menu.
        /// If you do want to go to the mainmenu, and you press yes. The BabyGame window will close and the babyTheme will be set to false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            MessageBoxResult result = MessageBox.Show("Are you sure that you want to return to MainMenu?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                babyTheme = false;
                this.Close();
            }
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

        /// <summary>
        /// This method makes sure that when you press the restart button, a click sound will play and the BabyGame window will be assigned so we can show it when you click on
        /// the replay button. And as final it will close the current BabyGame window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Restart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            BabyGame babyGame = new BabyGame();
            babyGame.Show();
            this.Close();
        }
    }
}
