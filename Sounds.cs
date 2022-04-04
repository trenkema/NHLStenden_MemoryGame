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

        public static string exactPath = System.Environment.CurrentDirectory;
        public static DirectoryInfo parent1;
        private static DirectoryInfo parent2;
        private static DirectoryInfo parent3;
        private static DirectoryInfo parent4;

        //Background music
        public static void PlayMusic()
        {
            parent1 = Directory.GetParent(exactPath);
            parent2 = Directory.GetParent(Convert.ToString(parent1));
            parent3 = Directory.GetParent(Convert.ToString(parent2));
            parent4 = Directory.GetParent(Convert.ToString(parent3));
            string ExactP = Convert.ToString(parent4) + @"\Sounds\ChillTrapBeat.wav";
            //New SoundPlayer with ExactP as Uri
            SoundPlayer BackgroundSound = new SoundPlayer(ExactP);
            //Play the SoundPlayer in a loop
            BackgroundSound.PlayLooping();
        }
        //Stop playing the background music
        public static void StopMusic()
        {
            parent1 = Directory.GetParent(exactPath);
            parent2 = Directory.GetParent(Convert.ToString(parent1));
            string ExactP = Convert.ToString(parent2) + @"\Sounds\ChillTrapBeat.wav";
            //New SoundPlayer with ExactP above as the Uri
            SoundPlayer BackgroundSound = new SoundPlayer((ExactP));
            //Stop playing this SoundPlayer
            BackgroundSound.Stop();
        }
        //Sound playing at start of memorygame
        public static void Startup()
        {
            parent1 = Directory.GetParent(exactPath);
            parent2 = Directory.GetParent(Convert.ToString(parent1));
            parent3 = Directory.GetParent(Convert.ToString(parent2));
            parent4 = Directory.GetParent(Convert.ToString(parent3));
            string ExactP = Convert.ToString(parent4) + @"\Sounds\XPStartup.wav";
            //New SoundPlayer with ExactP as the Uri
            SoundPlayer XPStartup = new SoundPlayer((ExactP));
            //Play this SoundPlayer once
            XPStartup.Play();
        }
        //Sound playing when exiting the memorygame
        public static void Shutdown()
        {
            parent1 = Directory.GetParent(exactPath);
            parent2 = Directory.GetParent(Convert.ToString(parent1));
            parent3 = Directory.GetParent(Convert.ToString(parent2));
            parent4 = Directory.GetParent(Convert.ToString(parent3));
            string ExactP = Convert.ToString(parent4) + @"\Sounds\XPShutdown.wav";
            //New SoundPlayer with ExactP as the Uri
            SoundPlayer XPShutdown = new SoundPlayer((ExactP));
            //Play this SoundPlayer once
            XPShutdown.Play();
        }
        //Sound playing when having a pair in the memorygame 
        public static void Correct()
        {
            parent1 = Directory.GetParent(exactPath);
            parent2 = Directory.GetParent(Convert.ToString(parent1));
            parent3 = Directory.GetParent(Convert.ToString(parent2));
            parent4 = Directory.GetParent(Convert.ToString(parent3));
            string ExactP = Convert.ToString(parent4) + @"\Sounds\CorrectSound.wav";
            //MediaPlayer can play mutiple sounds at once so any other sound playing wont stop
            //Open the MediaPlayer but not playing it yet
            player.Open(new Uri(ExactP));
            //Play this MediaPlayer once
            player.Play();
            //Delay the next task reducing the chance of audio cutting of randomly
            Task.Delay(5000);
        }
        //Sound playing when not having a pair in the memorygame
        public static void Wrong()
        {
            parent1 = Directory.GetParent(exactPath);
            parent2 = Directory.GetParent(Convert.ToString(parent1));
            parent3 = Directory.GetParent(Convert.ToString(parent2));
            parent4 = Directory.GetParent(Convert.ToString(parent3));
            string ExactP = Convert.ToString(parent4) + @"\Sounds\WrongSound.wav";
            //MediaPlayer can play mutiple sounds at once so any other sound playing wont stop
            //Open the MediaPlayer but not playing it yet
            player.Open(new Uri(ExactP));
            //Play this MediaPlayer once
            player.Play();
            //Delay the next task reducing the chance of audio cutting of randomly
            Task.Delay(5000);
        }
        //Sound playing when pressing button or flipping a card
        public static void Click()
        {
            parent1 = Directory.GetParent(exactPath);
            parent2 = Directory.GetParent(Convert.ToString(parent1));
            parent3 = Directory.GetParent(Convert.ToString(parent2));
            parent4 = Directory.GetParent(Convert.ToString(parent3));
            string ExactP = Convert.ToString(parent4) + @"\Sounds\ClickSound.wav";
            //MediaPlayer can play mutiple sounds at once so any other sound playing wont stop
            //Open the MediaPlayer but not playing it yet
            player.Open(new Uri(ExactP));
            //Play this MediaPlayer once
            player.Play();
            //Delay the next task reducing the chance of audio cutting of randomly
            Task.Delay(5000);
        }
    }
}
