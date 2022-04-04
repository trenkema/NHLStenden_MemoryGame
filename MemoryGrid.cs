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

namespace MemoryProject
{
    public class MemoryGrid
    {
        private Grid grid;
        public int rows = 3;
        public int cols = 3;
        private List<Card> cards = new List<Card>();
        private int NrOfClickedCards = 0;
        private Card firstGuess;
        private Card secondGuess;
        public int score = 100;
        public bool isBabyGame;
        /// <summary>
        /// In this method we first set isBabyGame to babyGame assigned in the constructor.
        /// Then we set grid to grid2 assigned in the constructor.
        /// After that we initialize everything. In this case initilize doesn't do much, because we already created our own grid to fill with cards.
        /// Then we call the method AddImages() to add all the images to the cards.
        /// And final we call the method ShowCards() to show the correct card if you click on it.
        /// </summary>
        /// <param name="grid2"></param>
        /// <param name="babyGame"></param>
        public MemoryGrid(Grid grid2, bool babyGame = false)
        {
            //StudentGame = StudentGamemode;
            isBabyGame = babyGame;
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
        /// After that we skip the 4th position, because we only have 8 cards. So we add an empty card in there and tell it to skip this position.
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
                    if (isBabyGame && i * j == 4)
                    {
                        cards.Insert(4, null);
                        continue;
                    }

                    ImageSource cardImage = images.First();
                    cards.Add(new Card(cardImage, ((BitmapImage)cardImage).UriSource.OriginalString));
                    images.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// In this method we set our firstGuess and secondGuess to null, so we can re-choose them. We also set NrOfClickedCards to 0, otherwise we can't click anymore cards, after we clicked 2.
        /// </summary>
        private void Reset()
        {
            firstGuess = null;
            secondGuess = null;
            NrOfClickedCards = 0;
        }

        /// <summary>
        /// In this method we flip our firstGuess and secondGuess to the back after 1 second.
        /// </summary>
        /// <returns></returns>
        private async Task FlipBack()
        {
            //score -= 5;
            await Task.Delay(1000);
            firstGuess.FlipToBack();
            secondGuess.FlipToBack();
            //StudentGame.UpdateLabel();
        }

        /// <summary>
        /// In this method we flip our firstGuess and secondGuess to the front. After we've done that, we reset the firstGuess and secondGuess.
        /// </summary>
        public void FrontShow()
        {
            firstGuess.Show();
            secondGuess.Show();
            firstGuess = null;
            secondGuess = null;
            //StudentGame.UpdateLabel();
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
        /// In this method we check if isBabyGame is false, if it is, then create a new grid.
        /// </summary>
        public void Initialize()
        {
            if (isBabyGame == false)
            {
                for (int i = 0; i < rows; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
                for (int i = 0; i < cols; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
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

            int cardTotal = (int)Math.Floor((rows * cols) / 2d) * 2;
            for (int i = 0; i < cardTotal; i++)
            {
                int imageNr = i % 4 + 1;

                ImageSource image = new BitmapImage(new Uri("Cards/BabyCards/" + imageNr + ".png", UriKind.Relative));
                images.Add(image);
            }
            //shuffle
            Random random = new Random();
            for (int i = 0; i < cardTotal; i++)
            {
                int r = random.Next(0, cardTotal);
                ImageSource schaap = images[r];
                images[r] = images[i];
                images[i] = schaap;
            }

            return images;
        }
    }
}
