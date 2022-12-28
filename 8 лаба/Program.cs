using System;
using System.Timers;
using System.Collections.Generic;
using System.IO;
using Timer = System.Timers.Timer;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

class Programm
{
    public class SubtitleData
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Text { get; set; }

    }

    private static int elapsedTime = 0;
    private static int timerInterval = 1000;
    private static List<SubtitleData> testSubtitles = new List<SubtitleData>
    {
        new SubtitleData { StartTime = timerInterval, EndTime = 3 * timerInterval, Text = D(0) },
        new SubtitleData { StartTime = timerInterval, EndTime = 3 * timerInterval, Text = D(1) },
        new SubtitleData { StartTime = 2 * timerInterval, EndTime = 6 * timerInterval, Text = D(2) },
        new SubtitleData { StartTime = 4 * timerInterval, EndTime = 7 * timerInterval, Text = D(3) },
        new SubtitleData { StartTime = 7 * timerInterval, EndTime = 15 * timerInterval, Text = D(4) }
    };

    private static void SubtitleReturn(Object source, ElapsedEventArgs e)
    {
        Console.Clear();


        Console.SetCursorPosition(2, 2);
        Console.Write("╔");
        for (int i = 0; i < Console.WindowWidth - 7; i++)
        {
            Console.Write("═");
        }
        Console.Write("╗");

        //Console.SetCursorPosition(2, 3);
        for (int i = 0; i < 20; i++)
        {
            Console.SetCursorPosition(2, i + 3);
            Console.WriteLine("║");
            Console.SetCursorPosition(Console.WindowWidth - 4, i + 3);
            Console.WriteLine("║");
        }

        Console.SetCursorPosition(3, 23);
        for (int i = 0; i < Console.WindowWidth - 7; i++)
        {
            Console.Write("═");
        }
        Console.SetCursorPosition(Console.WindowWidth - 4, 23);
        Console.WriteLine("╝");
        Console.SetCursorPosition(2, 23);
        Console.WriteLine("╚");

        int k = 0;
        foreach (var subtitle in testSubtitles)
        {
            var isSubtitleVisible = elapsedTime >= subtitle.StartTime
                 && elapsedTime <= subtitle.EndTime;


            if (isSubtitleVisible)
            {
                if (k == 0)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - (subtitle.Text.Length / 2), 3);  //hello
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (k == 1)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - (subtitle.Text.Length / 2), Console.WindowHeight - 9); //world
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (k == 2)
                {
                    Console.SetCursorPosition(Console.WindowWidth - 8, 12);  //yes
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (k == 3)
                {
                    Console.SetCursorPosition(3, 12);  //no
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (k == 4)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - (subtitle.Text.Length / 2), Console.WindowHeight - 8);  //bill
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(subtitle.Text); //пишем текст
                Console.ResetColor();

            }
            k++;
        }

        elapsedTime += timerInterval;
    }


    static string D(int s)
    {
        int x = 0;
        string[] array;
        array = new string[6];
        string text = "";
        using (StreamReader fs = new StreamReader(@"laba8.txt"))
        {
            while (true)
            {
                array[x] = fs.ReadLine(); // Читаем строку из файла во временную переменную.
                // Если достигнут конец файла, прерываем считывание
                if (array[x] == null) break;
                x++;
            }
        }
        return array[s];
    }


    public static System.Timers.Timer CreateTimer()
    {
        var timer = new Timer(timerInterval);
        timer.Elapsed += SubtitleReturn;

        return timer;
    }

    public static void Main(string[] args)
    {     
            

        System.Timers.Timer aTimer = CreateTimer();
        while (true)
        {
            aTimer.Start();
        }

        
    }
}