using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Threading;
using System.Media;
using System.IO;
using System.Reflection;

namespace MemoryProject
{
    class MemoryGridStudent
    {
        // Two Player Variables
        public bool PlayerOneTurn = true;
        public bool PlayerTwoTurn = false;
        public int checkPlayer = 0;

        // MainFrame Variables
        private Grid grid;
        private StudentGame StudentGame;
        public int rows = 4;
        public int cols = 4;
        private int NrOfClickedCards = 0;
        private Card firstGuess;
        private Card secondGuess;
        public int scorePlayerOne = 100;
        public int scorePlayerTwo = 100;

        //Highscore variables
        private List<Card> cards = new List<Card>();
        public int correctSets;
        public int highscore = 0;
        public string scorestring = " ";
        string ExactP = "";
        public static List<string> scores = new List<string>();
        public int i = 0;

        /// <summary>
        /// In this method we first set StudentGame to StudentGamemode assigned in the constructor.
        /// Then we set grid to grid2 assigned in the constructor.
        /// After that we initialize everything.
        /// Then we call the method AddImages() to add all the images to the cards.
        /// And final we call the method ShowCards() to show the correct card if you click on it.
        /// </summary>
        /// <param name="grid2"></param>
        /// <param name="StudentGamemode"></param>
        public MemoryGridStudent(Grid grid2, StudentGame StudentGamemode)
        {
            StudentGame = StudentGamemode;
            grid = grid2;
            Initialize();
            AddImages();
            ShowCards();
        }

        /// <summary>
        /// When we click on a card, a click sound will play.
        /// If NrOfClickedCards is already 2, then make sure to not be able to click any more cards.
        /// Then we make an image that we set as image sender.
        /// After that we make an index that is equal the image value.
        /// If NrOfClickedCards is lower than 2 and the card you click on is not already clicked, add 1 to NrOfClickedCards.
        /// Also make the card you clicked, clicked is true so you can't click it again.
        /// Then if firstGuess is not assigned to a value yet, first click is your firstGuess. Then if your secondGuess is not assigned to a value yet, the second click is your secondGuess.
        /// For each card clicked you call the method ShowCards() to show the image.
        /// Now if NrOfClickedCards is 2. Check if firstGuess absolutepath and secondGuess absolutepath is equal to each other and that the firstGuess is not the same card spot as the secondGuess.
        /// Then play the sound Correct and show the front of those two cards.
        /// Otherwise if they are not correct, play the sound wrong and flip the two cards to their back.
        /// After having a correct or wrong match, reset your guesses so you can re-pick two cards.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CardClick(object sender, MouseButtonEventArgs e)
        {
            Sounds.Click();
            if (NrOfClickedCards == 2)
            {
                return;
            }

            Image image = (Image)sender;
            int index = (int)image.Tag;
            image.Source = null;

            if (NrOfClickedCards < 2 && !cards[index].isClicked())
            {
                NrOfClickedCards++;
                cards[index].Clicked();

                if (firstGuess == null)
                {
                    firstGuess = cards[index];
                }
                else if (secondGuess == null)
                {
                    secondGuess = cards[index];
                }

                ShowCards();
            }

            if (NrOfClickedCards == 2)
            {
                if (firstGuess.AbsolutePath == secondGuess.AbsolutePath && firstGuess != secondGuess)
                {
                    Sounds.Correct();
                    FrontShow();
                }
                else
                {
                    Sounds.Wrong();
                    await FlipBack(); 
                }
                Reset();
            }
            ShowCards();
        }

        /// <summary>
        /// In this method we add the images to the cards.
        /// First we get our ImageList.
        /// The when make a for loop for our rows and cols.
        /// Then we take our cardImage and say that images returns the first element of a sequence.
        /// Then we add our cardImage to the card.
        /// At final we remove the image at index 0.
        /// </summary>
        private void AddImages()
        {
            List<ImageSource> images = GetImageList();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    ImageSource cardImage = images.First();
                    cards.Add(new Card(cardImage, ((BitmapImage)cardImage).UriSource.OriginalString));
                    images.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// If multiplayer is enabled. If it was player one's turn, player two gets the turn now. If it was player two's turn, player one gets the turn now.
        /// After that we set our firstGuess and secondGuess to null, so we can re-choose them. We also set NrOfClickedCards to 0, otherwise we can't click anymore cards, after we clicked 2.
        /// If multiplayer was not eabled, we set our firstGuess and secondGuess to null, so we can re-choose them. We also set NrOfClickedCards to 0, otherwise we can't click anymore cards, after we clicked 2.
        /// </summary>
        private void Reset()
        {
            if (StudentOptions.MultiplayerEnabled)
            {
                // Check if its Player One's turn.
                if (checkPlayer == 0)
                {
                    PlayerOneTurn = true;
                    PlayerTwoTurn = false;
                }
                // Check if its Player Two's turn.
                if (checkPlayer == 1)
                {
                    PlayerOneTurn = false;
                    PlayerTwoTurn = true;
                }
                firstGuess = null;
                secondGuess = null;
                NrOfClickedCards = 0;
            }
            else
            {
                firstGuess = null;
                secondGuess = null;
                NrOfClickedCards = 0;
            }
        }

        // Player One: CheckPlayer = 0;
        // Player Two: CheckPlayer = 1;
        /// <summary>
        /// If multiplayer is not enabled, remove 5 points from the player's score. Then we flip our firstGuess and secondGuess to the back after 1 second. As final we update our score label.
        /// If multiplayer is enabled, check if player one's turn, if yes. Then say that it's now player two's turn. After that remove 5 points from player one's score. After that we flip our firstGuess and secondGuess to the back after 1 second. As final we update our score label.
        /// Check if player two's turn, if yes. Then say that it's now player one's turn. After that remove 5 points from player two's score. After that we flip our firstGuess and secondGuess to the back after 1 second. As final we update our score label.
        /// </summary>
        /// <returns></returns>
        private async Task FlipBack()
        {
            // If MultiplayerEnables is 1 (true) in StudentOptions Then..
            if (!StudentOptions.MultiplayerEnabled)
            {
                //checkPlayer = 1;
                scorePlayerOne -= 5;
                await Task.Delay(1000);
                firstGuess.FlipToBack();
                secondGuess.FlipToBack();
                StudentGame.UpdateLabel();
            }
            // If MultiplayerEnables is 0 (false) in StudentOptions Then..
            else
            if (PlayerOneTurn == true)
            {
                checkPlayer = 1;
                scorePlayerOne -= 5;
                await Task.Delay(1000);
                firstGuess.FlipToBack();
                secondGuess.FlipToBack();
                StudentGame.UpdateLabel();
            }
            if (PlayerTwoTurn == true)
            {
                checkPlayer = 0;
                scorePlayerTwo -= 5;
                await Task.Delay(1000);
                firstGuess.FlipToBack();
                secondGuess.FlipToBack();
                StudentGame.UpdateLabel();
            }
        }

        /// <summary>
        /// First check if multiplayer is enabled. If not, add 15 points to the score. Add one to correctSets. Show firstGuess and secondGuess, then set firstGuess and secondGuess to null. At final update score label.
        /// </summary>
        public void FrontShow()
        {
            // If MultiplayerEnables is 0 (false) in StudentOptions Then..
            if (!StudentOptions.MultiplayerEnabled)
            {
                //checkPlayer = 0;
                scorePlayerOne += 15;
                correctSets++;
                firstGuess.Show();
                secondGuess.Show();
                firstGuess = null;
                secondGuess = null;
                StudentGame.UpdateLabel();
                progress();
            }
            // If MultiplayerEnabled is (1) true in StudentOptions Then..
            else
            {
                if (PlayerOneTurn == true)
                {
                    checkPlayer = 0;
                    scorePlayerOne += 15;
                    correctSets++;
                    firstGuess.Show();
                    secondGuess.Show();
                    firstGuess = null;
                    secondGuess = null;
                    StudentGame.UpdateLabel();
                    progress();
                }
                if (PlayerTwoTurn == true)
                {
                    checkPlayer = 1;
                    scorePlayerTwo += 15;
                    //// Player Two gets another turn.
                    //PlayerOneTurn = false;
                    //PlayerTwoTurn = true;
                    correctSets++;
                    firstGuess.Show();
                    secondGuess.Show();
                    firstGuess = null;
                    secondGuess = null;
                    StudentGame.UpdateLabel();
                    progress();
                }
            }
        }

        /// <summary>
        /// In this method we first make a for list for our rows and cols.
        /// For each row and column we make sure it's an image.
        /// Then we check if the card position is not equal to an empty card. If it's not equal to an empty card, on a mouse click. We show the front image of the card, and add an image tag to it.
        /// Then we set an image for each column and row in our grid.
        /// </summary>
        private void ShowCards()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Image image = new Image();
                    if (cards[j * cols + i] != null)
                    {
                        image.MouseDown += new MouseButtonEventHandler(CardClick);
                        image.Source = cards[j * cols + i].Show();
                        image.Tag = j * cols + i;
                    }
                    Grid.SetColumn(image, j);
                    Grid.SetRow(image, i);
                    grid.Children.Add(image);
                }
            }
        }

        /// <summary>
        /// First we check what we selected in the OptionsMenu for playfieldsize.
        /// Then we will create a grid with the given playfieldsize.
        /// </summary>
        public void Initialize()
        {
            if (StudentOptions.playfieldsize == 1)
            {
                cols = 4;
                rows = 4;
            }
            if (StudentOptions.playfieldsize == 2)
            {
                cols = 2;
                rows = 2;
            }
            if (StudentOptions.playfieldsize == 3)
            {
                cols = 1;
                rows = 2;
            }

            for (int i = 0; i < rows; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
                for (int i = 0; i < cols; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
        }

        /// <summary>
        /// This is a list with all the Images for the front.
        /// fist we create a list with type ImageSource to add images to it.
        /// Then we define cardTotal which will do the (rows*cols / 2) * 2. Since the rows*cols is 9. You can't devide it by 2, so we get a decimal.
        /// We floor that decimal, so that if we multiply it by 2, we get an even number.
        /// Then we make a for loop where we add all the cards to the list, based on their image number. In this case number 1 to 8.
        /// After that we will start shuffling the cards by making a random.
        /// We will make a for loop and add a random card till we filled all the card spots.
        /// </summary>
        /// <returns></returns>
        private List<ImageSource> GetImageList()
        {
            List<ImageSource> images = new List<ImageSource>();

            //int cardTotal = (int)Math.Floor((rows * cols) / 2d) * 2;
            for (int i = 0; i < rows * cols; i++)
            {
                int imageNr = i % ((rows * cols) / 2) + 1;

                if(StudentOptions.theme == 1)
                {
                    ImageSource image = new BitmapImage(new Uri("Cards/BeerCards/" + imageNr + ".png", UriKind.Relative));
                    images.Add(image);
                }
                if (StudentOptions.theme == 2)
                {
                    ImageSource image = new BitmapImage(new Uri("Cards/DrinkCards/" + imageNr + ".png", UriKind.Relative));
                    images.Add(image);
                }
            }
            //shuffle
            Random random = new Random();
            for (int i = 0; i < rows * cols; i++)
            {
                int r = random.Next(0, rows * cols);
                ImageSource schaap = images[r];
                images[r] = images[i];
                images[i] = schaap;
            }

            return images;
        }

        // High Score System
        /// <summary>
        /// This will check if the correct sets of card is equal to half of the totalcards.
        /// If that's true, it says congrats and shows your score.
        /// And at last it will call the highscoresave method to save the score to the scorelog file.
        /// </summary>
        public async void progress()
        {
            if (correctSets == (rows * cols) / 2)
            {
                //WinnerMenu winnerMenuOpen = new WinnerMenu();
                //winnerMenuOpen.Show();
                highscoresave();
                await Task.Delay(1000);
                StudentGame.WinnerMenuOpen();
            }
        }

        /// <summary>
        /// First it checks if the score is under 100. If that's true then it will add a 0 before the score so the sorting will be correct and convert it to a string.
        /// If the score is above 100, just convert the score to a string to save it into a text file and make it easy to read.
        /// Then it will go up two file paths to go into the correct path where the scorelog.txt is.
        /// Then if scorelog.txt exists, it will add the new score to the file on a new line. If that's finished, it will give you a message "score saved!".
        /// Then we fist clear our scores, so it will not duplicate or give any unwanted errors.
        /// And at last it will call the method readscores to show you the highscore list.
        /// </summary>
        public void highscoresave()
        {
            if (!StudentOptions.MultiplayerEnabled)
            {
                if (scorePlayerOne < 100)
                {
                    scorestring = "0" + Convert.ToString(scorePlayerOne);
                }
                else
                {
                    scorestring = Convert.ToString(scorePlayerOne);
                }

                string playerscore = scorestring + " " + StudentOptions.PlayerNameOne;
                ExactP = Path.Combine(Environment.CurrentDirectory, "scorelog.txt");

                if (!File.Exists(ExactP))
                {
                    File.Create(ExactP).Dispose();
                }

                if (File.Exists(ExactP))
                {
                    File.AppendAllText(ExactP, playerscore + Environment.NewLine);
                    scores.Clear();
                    readscores();
                }
            }
        }

        /// <summary>
        /// First this will read all the string lines in the scorelog file.
        /// Then it will sort them from high to low.
        /// </summary>
        public void readscores()
        {
            foreach (string line in File.ReadLines(ExactP))
            {
                scores.Add(line);
            }
            scores.Sort();
            scores.Reverse();
        }
    }
}