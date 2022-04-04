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

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for WinnerMenu.xaml
    /// </summary>
    public partial class WinnerMenu : Window
    {
        /// <summary>
        /// In this method we initialize all the components.
        /// </summary>
        public WinnerMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method makes sure that when you press the restart button, a click sound will play and the StudentGame window will be assigned so we can show it when you click on
        /// the replay button. And as final it will close the current WinnerMenu window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Restart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            this.Close();
            StudentGame studentGame = new StudentGame();
            studentGame.Show();
        }

        /// <summary>
        /// This method makes sure that when you press the replay button, a click sound will play and the MainMenu window will be assigned so we can show it when you click on
        /// the replay button. And as final it will close the current WinnerMenu window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            this.Close();
        }
    }
}
