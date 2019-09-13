using System;
using System.Linq;

namespace BankingSystem
{
    public class MainMenu
    {
        private int choiceCursorLeft;
        private int choiceCursorTop;
        private bool checkChoice, exitCond;
        private int choice;

        public MainMenu()
        {
            checkChoice = false;
            exitCond = false;
        }

        public void DisplayMenu()
        {
            Console.WriteLine("\t\t╔══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t|           WELCOME TO SIMPLE BANKING SYSTEM       |");
            Console.WriteLine("\t\t|══════════════════════════════════════════════════|");

            Console.WriteLine("\t\t|       1.Create a new account                     |");
            Console.WriteLine("\t\t|       2.Search for an account                    |");
            Console.WriteLine("\t\t|       3.Deposit                                  |");
            Console.WriteLine("\t\t|       4.Withdraw                                 |");
            Console.WriteLine("\t\t|       5.A/C statement                            |");
            Console.WriteLine("\t\t|       6.Delete account                           |");
            Console.WriteLine("\t\t|       7.Exit                                     |");
            Console.WriteLine("\t\t╚══════════════════════════════════════════════════╝");

            Console.Write("\t\t|     Enter your choice(1-7):");
            choiceCursorLeft = Console.CursorLeft;
            choiceCursorTop = Console.CursorTop;
            Console.WriteLine("                      |");
            Console.WriteLine("\t\t╚══════════════════════════════════════════════════╝");

            Console.SetCursorPosition(choiceCursorLeft, choiceCursorTop);

            string input = Console.ReadLine();
            bool isNumeric = !string.IsNullOrEmpty(input) && input.All(char.IsDigit);

            if (isNumeric)
            {
                try
                {   //if input number is very large
                    choice = Convert.ToInt32(input);
                    if (choice >= 1 && choice <= 7)
                    {
                        checkChoice = true;
                    }
                }
                catch (Exception e)
                {
                    //handle exception
                    Console.WriteLine("\n\n");
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void ExecuteMainMenu()
        {
            do
            {
                Console.Clear();
                DisplayMenu();
                Choice();
            } while (!checkChoice || !exitCond);
        }


        public void Choice()
        {
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Account account = new Account();
                    account.ExecuteAsync();
                    break;

                case 2:
                    Console.Clear();
                    searchAccount search = new searchAccount();
                    search.SearchAccount();
                    break;

                case 3:
                    Console.Clear();
                    transactAccount deposit = new transactAccount();
                    deposit.Execute("deposit");
                    break;

                case 4:
                    Console.Clear();
                    transactAccount withdraw = new transactAccount();
                    withdraw.Execute("withdraw");
                    break;

                case 5:
                    Console.Clear();
                    accountStatement statement = new accountStatement();
                    statement.printStatement();
                    break;

                case 6:
                    Console.Clear();
                    deleteAccount delete = new deleteAccount();
                    delete.removeAccount();
                    break;

                case 7:
                    exitCond = true;
                    break;
            }

        }
    }
}
