using SFML.Audio;
using System;
using System.Runtime.InteropServices;

namespace ConsolePlayer
{
    class Program
    {
        [DllImport("ncursesAPI.dll", CharSet = CharSet.Unicode)]
        public static extern int WGetch();
        [DllImport("ncursesAPI.dll", CharSet = CharSet.Unicode)]
        public static extern void InitScr();
        [DllImport("ncursesAPI.dll", CharSet = CharSet.Unicode)]
        public static extern void EndWin();
        [DllImport("ncursesAPI.dll", CharSet = CharSet.Unicode)]
        public static extern void HalfDelay(int delay);
        [DllImport("ncursesAPI.dll", CharSet = CharSet.Unicode)]
        public static extern void NoEcho();

        private static Music disco;

        static void Main(string[] args)
        {
            Init(args[0]);
            int prevSeconds = 0;
            while (disco.Status != SoundStatus.Stopped)
            {
                var input = WGetch();
                if (input == ' ')
                {
                    PauseOrPlay(disco);
                }
                else if(input == 'q')
                    break;
                if (prevSeconds != Math.Truncate(disco.PlayingOffset.AsSeconds()))
                {
                    ShowMusicInfo(args[0], disco);
                    prevSeconds = Convert.ToInt32(disco.PlayingOffset.AsSeconds());
                }
            }
            EndWin();
            disco.Dispose();
        }

        private static void ShowMusicInfo(string name, Music music)
        {
            string time = " Position: " + Math.Truncate(music.PlayingOffset.AsSeconds()) + " seconds";
            Console.Clear();
            Console.WriteLine(name + time);
        }

        private static void PauseOrPlay(Music music)
        {
            if (music.Status == SoundStatus.Paused)
                music.Play();
            else
                music.Pause();
        }

        private static void Init(string arg)
        {
            InitScr();
            NoEcho();
            HalfDelay(3);
            Console.CursorVisible = false;
            disco = new Music(arg);
            disco.Loop = false;
            disco.Play();
            Console.Clear();
            ShowMusicInfo(arg, disco);
        }
    }
}