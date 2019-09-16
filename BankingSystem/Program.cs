using System;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //set static console height and width
            Console.WindowHeight = 40;
            Console.WindowWidth = 90;

            //login
            LoginPage loginObject = new LoginPage();
            loginObject.ExecuteLoginPage();

            Console.Clear();

            //if login successful
            //Display the main menu with choices
            MainMenu menuObject = new MainMenu();
            menuObject.ExecuteMainMenu();

        }
    }
}
