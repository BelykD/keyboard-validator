/**
 * Keyboard Validator: Full-Size (100%) Format
 * 
 * Author: Dylan Belyk
**/

namespace KeyboardTester
{
    internal static class Program
    {
        // The main entry point for the application.
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Opens form window
            Application.Run(new Fullsize());
        }
    }
}