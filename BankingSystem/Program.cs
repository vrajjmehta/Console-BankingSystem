using System;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginPage loginObject = new LoginPage();
            loginObject.Login();
            while (!loginObject.loginCheck)
            {
                Console.Clear();
                loginObject.Login();
            }

            Console.Clear();
            Console.WriteLine("Process");
        }
    }
}
