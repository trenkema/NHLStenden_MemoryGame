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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MemoryProject
{
    public partial class StudentOptions : Window
    {
        public static bool MultiplayerEnabled;

        public static int theme = 1;
        public static int playfieldsize = 1;
        public static string PlayerNameOne;
        public static string PlayerNameTwo;
        public static string exactPath = System.Environment.CurrentDirectory;
        public static DirectoryInfo parent1;

        string placeHolderPlayerText = "Enter Name";

        /// <summary>
        /// In this method we initialize all the components and set the label of PlayerTwoName to hidden.
        /// </summary>
        public StudentOptions()
        {
            InitializeComponent();
            MultiplayerEnabled = false;
            PlayerTwoName.Visibility = Visibility.Hidden;

            NoNameFilled.Visibility = Visibility.Hidden;

            PlayerOneName.MaxLength = 7;
            PlayerTwoName.MaxLength = 7;

            PlayerOneName.Text = placeHolderPlayerText;
            PlayerTwoName.Text = placeHolderPlayerText;
        }

        /// <summary>
        /// In this combobox you choose the theme of the cards and background. The first case stands for beer and sets the theme equal to 1. The second case stands for beverages
        /// and sets the theme equal to 2.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_Theme(object sender, SelectionChangedEventArgs e)
        {
            var CardTheme = ((sender as ComboBox).SelectedItem as ComboBoxItem).Name as string;
            switch (CardTheme)
            {
                case "Bier":
                    theme = 1;
                    break;
                case "Drank":
                    theme = 2;
                    break;
            }
        }

        /// <summary>
        /// In this combobox you choose the playfield size. The first case sets the field equal to 4x4 and the playfieldsize = 1. The second case sets the field equal tot 2x2 and the playfieldsize = 2.
        /// The third case sets the playfield equal to 1x2 and the playfieldsize = 3.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_PlayField(object sender, SelectionChangedEventArgs e)
        {
            var PlayField = ((sender as ComboBox).SelectedItem as ComboBoxItem).Name as string;
            switch (PlayField)
            {
                case "Vier":
                    playfieldsize = 1;
                    break;
                case "Twee":
                    playfieldsize = 2;
                    break;
                case "Een":
                    playfieldsize = 3;
                    break;
            }
        }

        /// <summary>
        /// This method makes sure that when you press the start button, StudenGame window will be showen as defined. And a click sound will be played.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButtonStudent(object sender, MouseButtonEventArgs e)
        {
            if (MultiplayerEnabled)
            {
                if (PlayerOneName.Text != string.Empty && PlayerOneName.Text != placeHolderPlayerText)
                {
                    if (PlayerTwoName.Text != string.Empty && PlayerTwoName.Text != placeHolderPlayerText)
                    {
                        Sounds.Click();

                        StudentGame StudentTest = new StudentGame();
                        StudentTest.Show();
                        //this.Close();
                    }
                    else
                    {
                        NoNameFilled.Visibility = Visibility.Visible;

                        return;
                    }
                }
                else
                {
                    NoNameFilled.Visibility = Visibility.Visible;
                    
                    return;
                }
            }
            else
            {
                if (PlayerOneName.Text != string.Empty && PlayerOneName.Text != placeHolderPlayerText)
                {
                    Sounds.Click();

                    StudentGame StudentTest = new StudentGame();
                    StudentTest.Show();
                    //this.Close();
                }
                else
                {
                    NoNameFilled.Visibility = Visibility.Visible;

                    return;
                }
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
        /// This method makes sure that when you press the exit to mainmenu button, a click sound will play and it will close the current StudentOptions window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToMain(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            this.Close();
        }

        /// <summary>
        /// This textbox will take the input whenver the input changes. With a max amount of 10 characters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerOneName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NoNameFilled.Visibility = Visibility.Hidden;

            if (PlayerOneName.Text != string.Empty)
            {
                PlayerNameOne = PlayerOneName.Text;
            }
            else
            {
                PlayerNameOne = string.Empty;
            }
        }

        /// <summary>
        /// This textbox will take the input whenver the input changes. With a max amount of 10 characters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayerTwoName_TextChanged(object sender, TextChangedEventArgs e)
        {
            NoNameFilled.Visibility = Visibility.Hidden;

            if (PlayerTwoName.Text != string.Empty)
            {
                PlayerNameTwo = PlayerTwoName.Text;
            }
            else
            {
                PlayerNameTwo = string.Empty;
            }
        }

        /// <summary>
        /// This checkbox changes the PlayerTwoName to visible and enable multiplayer when you check the checkbox. Otherwise it will change the PlayerTwoName to hidden and disable multiplayer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Toggle_Multiplayer(object sender, RoutedEventArgs e)
        {
            Sounds.Click();

            MultiplayerEnabled = !MultiplayerEnabled;
            PlayerTwoName.Visibility = MultiplayerEnabled ? PlayerTwoName.Visibility = Visibility.Visible : PlayerTwoName.Visibility = Visibility.Hidden;
        }

        private void PlayerOneName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PlayerOneName.Text == placeHolderPlayerText)
            {
                PlayerOneName.Text = string.Empty;
            }
        }

        private void PlayerOneName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PlayerOneName.Text == string.Empty)
            {
                PlayerOneName.Text = placeHolderPlayerText;
            }
        }

        private void PlayerTwoName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PlayerTwoName.Text == placeHolderPlayerText)
            {
                PlayerTwoName.Text = string.Empty;
            }
        }

        private void PlayerTwoName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PlayerTwoName.Text == string.Empty)
            {
                PlayerTwoName.Text = placeHolderPlayerText;
            }
        }
    }
}
