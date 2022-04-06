using StruggleMemory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MemoryProject
{
    class Sounds
    {
        private static MediaPlayer player = new MediaPlayer();

        //Background music
        public static void PlayMusic()
        {
            SoundPlayer BackgroundSound = new SoundPlayer(Resources.ChillTrapBeat);
            //Play the SoundPlayer in a loop
            BackgroundSound.PlayLooping();
        }
        //Stop playing the background music
        public static void StopMusic()
        {
            SoundPlayer BackgroundSound = new SoundPlayer(Resources.ChillTrapBeat);
            //Stop playing this SoundPlayer
            BackgroundSound.Stop();
        }
        //Sound playing at start of memorygame
        public static void Startup()
        {
            SoundPlayer XPStartup = new SoundPlayer(Resources.XPStartup);
            //Play this SoundPlayer once
            XPStartup.Play();
        }
        //Sound playing when exiting the memorygame
        public static void Shutdown()
        {
            SoundPlayer XPShutdown = new SoundPlayer(Resources.XPShutdown);
            //Play this SoundPlayer once
            XPShutdown.Play();
        }
        //Sound playing when having a pair in the memorygame 
        public static void Correct()
        {
            var uri = new Uri("pack://siteoforigin:,,,/Sounds/CorrectSound.wav");

            player.Open(uri);
            //Play this SoundPlayer once
            player.Play();
            //Delay the next task reducing the chance of audio cutting of randomly
            Task.Delay(5000);
        }
        //Sound playing when not having a pair in the memorygame
        public static void Wrong()
        {
            var uri = new Uri("pack://siteoforigin:,,,/Sounds/WrongSound.wav");

            player.Open(uri);
            //Play this SoundPlayer once
            player.Play();
            //Delay the next task reducing the chance of audio cutting of randomly
            Task.Delay(5000);
        }
        //Sound playing when pressing button or flipping a card
        public static void Click()
        {
            var uri = new Uri("pack://siteoforigin:,,,/Sounds/ClickSound.wav");

            player.Open(uri);
            //Play this SoundPlayer once
            player.Play();
            //Delay the next task reducing the chance of audio cutting of randomly
            Task.Delay(5000);
        }
    }
}
