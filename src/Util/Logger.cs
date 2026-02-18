using System;

namespace MGUI.Util
{
    public static class Logger
    {
        public static void Log(string message) => Console.WriteLine(message);

        public static void Log(object sender, string message)
        {
            Console.WriteLine($"{sender.GetType().Name} | {sender.GetType().Name + message}");
        }    
        public static void Space() => Console.WriteLine('\n');
        public static void Seperator() => Console.WriteLine("-----------------------------------------------");

    }
}