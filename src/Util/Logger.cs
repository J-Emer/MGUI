using System;
using System.Text;

namespace MGUI.Util
{
    public static class Logger
    {
        public static void Log(string message) => Console.WriteLine(message);

        public static void Log(object sender, object obj)
        {
            Console.WriteLine($"{sender.GetType().Name} | {obj.ToString()}");
        } 
        public static void Log(object sender, string message)
        {
            Console.WriteLine($"{sender.GetType().Name} | {message}");
        }  

        public static void Space() => Console.WriteLine('\n');
        public static void Seperator() => Console.WriteLine("-----------------------------------------------");

    }
}