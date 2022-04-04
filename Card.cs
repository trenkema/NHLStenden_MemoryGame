using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MemoryProject
{
    class Card
    {
        // Matches the front and pair of a card
        private ImageSource front, back;

        // Boolean to see if a card is clicked
        private bool clicked;

        // Creates the AbsolutePath of a card to check equality between a pair
        public string AbsolutePath { get; }

        /// <summary>
        /// This method will set the image for the back of the card based on the theme selected in StudentOptions.
        /// It sets the uri, which is the url for an image.
        /// It will also set clicked to false, to return the back of the card.
        /// We assign the public string AbsolutePath to the absoluteUri which we assigned in the constructor.
        /// Then finally we assign the ImageSource front to the frontOfCard which we assigned in the constructor.
        /// </summary>
        /// <param name="frontOfCard"></param>
        /// <param name="absoluteUri"></param>
        public Card(ImageSource frontOfCard, string absoluteUri)
        {
            if(StudentOptions.theme == 1)
            {
                back = new BitmapImage(new Uri("Cards/BeerCards/Bierback.png", UriKind.Relative));
            }
            if (StudentOptions.theme == 2)
            {
                back = new BitmapImage(new Uri("Cards/DrinkCards/DrankBack.png", UriKind.Relative));
            }
            if (BabyGame.babyTheme == true)
            {
                back = new BitmapImage(new Uri("Cards/BabyCards/BabyBack.png", UriKind.Relative));
            }

            clicked = false;
            AbsolutePath = absoluteUri;
            front = frontOfCard;
        }

        /// <summary>
        /// This method will set clicked to true.
        /// </summary>
        public void Clicked()
        {
            clicked = true;
        }

        /// <summary>
        /// This method will return the front of the card if clicked is true. Otherwise it will return the back.
        /// </summary>
        /// <returns></returns>
        public ImageSource Show()
        {
            if (clicked == true)
            {
                return front;
            }
            else
                    return back;
        }

        /// <summary>
        /// This method will set clicked to false;
        /// </summary>
        public void FlipToBack()
        {
            clicked = false;
        }

        /// <summary>
        /// This method will make sure that when a card is already clicked, you can't click it again.
        /// </summary>
        /// <returns></returns>
        public bool isClicked()
        {
            return clicked;
        }

    }
}
