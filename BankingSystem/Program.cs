using System;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
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
