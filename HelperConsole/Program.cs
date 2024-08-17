using System.Diagnostics;

namespace HelperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(currentDirectory);
        }
    }
}
