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
    /// <summary>
    /// Interaction logic for StudentGame.xaml
    /// </summary>
    public partial class StudentGame : Window
    {
        WinnerMenu winnerMenu = new WinnerMenu();

        private MemoryGridStudent mgridStudent;
        public static string exactPath = System.Environment.CurrentDirectory;
        public static DirectoryInfo parent1;
       
        /// <summary>
        /// In this method we initialize all the components and check if we need to use only player one's label or also player two's label.
        /// First we call the InitializeComponent to define everything.
        /// Then we set Player Two's name to nothing, because it will load in if you enable Multiplayer and set a name.
        /// Then we load in our theme background. Based on the theme you choose.
        /// After that we call the MemoryGridStudent class so we can use it's variables and methods.
        /// Then we check if multiplayer is enabled, if not, only update the first player's name and score. 
        /// If yes, make sure that the second player name loads in and gets a score.
        /// </summary>
        public StudentGame()
        {
            InitializeComponent();
            ScorePlayerTwo.Content = "";

            ThemeSelector();
            //mgridStudent = new MemoryGrid(StudentMemoryGrid, this, false);
            mgridStudent = new MemoryGridStudent(StudentMemoryGrid, this);
            if (!StudentOptions.MultiplayerEnabled)
            {
                ScorePlayerOne.Content = StudentOptions.PlayerNameOne + ":" + Environment.NewLine + mgridStudent.scorePlayerOne;
            }
            else
            {
                ScorePlayerOne.Content = StudentOptions.PlayerNameOne + ":" + Environment.NewLine + mgridStudent.scorePlayerOne;
                ScorePlayerTwo.Content = StudentOptions.PlayerNameTwo + ":" + Environment.NewLine + mgridStudent.scorePlayerTwo;
            }
        }

        /// <summary>
        /// In this method we will update our player names and scores, based on if multiplayer is enabled or not.
        /// Here we check if multiplayer is enabled, if not, only update the first player's name and score.
        /// If yes, make sure that the second player also updates the name and score aswell as the first player.
        /// </summary>
        public void UpdateLabel()
        {
            if (!StudentOptions.MultiplayerEnabled)
            {
                ScorePlayerOne.Content = StudentOptions.PlayerNameOne + ":" + Environment.NewLine + mgridStudent.scorePlayerOne.ToString();
            }
            else
            {
                ScorePlayerOne.Content = StudentOptions.PlayerNameOne + ":" + Environment.NewLine + mgridStudent.scorePlayerOne.ToString();
                ScorePlayerTwo.Content = StudentOptions.PlayerNameTwo + ":" + Environment.NewLine + mgridStudent.scorePlayerTwo.ToString();
            }
        }

        /// <summary>
        /// In this method we will make sure that our backgrounds match our selected themes.
        /// First we set our initialize our window background with myBrush, then we assign StudentGame to myBrush so we can easily change it.
        /// Then we will use theme beer if the theme choice is 1, and theme beverages if the theme choice is 2.
        /// </summary>
        public void ThemeSelector()
        {
            ImageBrush myBrush = new ImageBrush();

            this.Background = myBrush;
            // Weghalen wnnr themes zijn gefixt
            if (StudentOptions.theme == 1)
            {
                myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Cards/BeerCards/Background.jpg"));
            }
            if (StudentOptions.theme == 2)
            {
                myBrush.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Cards/DrinkCards/DrankBackground.jpg"));
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
        /// This method makes sure that when you press the exit to mainmenu button, a click sound will play and it will ask if you really want to go back to the main menu.
        /// If you do want to go to the mainmenu, and you press yes. The StudentGame window will close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMain(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            MessageBoxResult result = MessageBox.Show("Are you sure that you want to return to MainMenu?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// This method makes sure that when you press the restart button, a click sound will play and the StudentGame window will be assigned so we can show it when you click on
        /// the replay button. And as final it will close the current StudentGame window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplayButtonStudent(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            StudentGame StudentGame = new StudentGame();
            StudentGame.Show();
            this.Close();
 
        }

        /// <summary>
        /// This method will close the current StudentGame window and show the winnerMenu created. On there it will first clear the label and then load it with saved scores.
        /// If the total amount of scores is under 10. It will display the amount of scores that are available, and if the total amout of scores if over 10, it will display 10 scores.
        /// Then it will load the score of player one scored in the game before the winner window showed. It will also load the score of player two scored in the game before the winner window showed
        /// if multiplayer was enabled.
        /// </summary>
        public void WinnerMenuOpen()
        {
            this.Close();
            winnerMenu.Show();
            winnerMenu.HighScoreList.Content = "";
            if (MemoryGridStudent.scores.Count < 10)
            {
                for (int i = 0; i < MemoryGridStudent.scores.Count - 1; i++)
                {
                    winnerMenu.HighScoreList.Content += MemoryGridStudent.scores[i].ToString() + Environment.NewLine;
                    //MessageBox.Show(scores[i]);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    winnerMenu.HighScoreList.Content += MemoryGridStudent.scores[i].ToString() + Environment.NewLine;
                    //MessageBox.Show(scores[i]);
                }
            }

            if (!StudentOptions.MultiplayerEnabled)
            {
                winnerMenu.ScorePlayerOne.Content = StudentOptions.PlayerNameOne + ":" + Environment.NewLine + mgridStudent.scorePlayerOne;
                winnerMenu.ScorePlayerTwo.Content = "";
            }
            else
            {
                winnerMenu.ScorePlayerOne.Content = StudentOptions.PlayerNameOne + ":" + Environment.NewLine + mgridStudent.scorePlayerOne;
                winnerMenu.ScorePlayerTwo.Content = StudentOptions.PlayerNameTwo + ":" + Environment.NewLine + mgridStudent.scorePlayerTwo;
            }
        }
    }
}
