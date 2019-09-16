using System;
using System.IO;
using System.Linq;

namespace BankingSystem
{
    public class MainMenu
    {
        private bool checkChoice, exitCond;
        private int choice;

        public MainMenu()
        {
            checkChoice = false;
            exitCond = false;
        }

        public void DisplayMenu()
        {
            int choiceCursorLeft, choiceCursorTop;

            //design for console UI of Main Menu
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

            //read input 
            string input = Console.ReadLine();
            bool isNumeric = !string.IsNullOrEmpty(input) && input.All(char.IsDigit);    //linq query to check null & Digits

            if (isNumeric && input.Length==1)
            {
                try
                {   //if input number is very large
                    choice = Convert.ToInt32(input);
                    if (choice >= 1 && choice <= 7)
                    {
                        checkChoice = true;
                    }
                    //create accounNumbers.txt file before any operations
                    StreamWriter sw = new StreamWriter("accountNumbers.txt", append: true);
                    sw.Close();
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
            //do while loop unital exitcond or 1-7 is entered
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
                //if 1 is entered, create a new account
                case 1:
                    Console.Clear();
                    Account account = new Account();
                    account.ExecuteAsync();
                    break;

                //if 2 is entered, search account 
                case 2:
                    Console.Clear();
                    searchAccount search = new searchAccount();
                    search.SearchAccount();
                    break;

                //if 3 is entered, call deposit 
                case 3:
                    Console.Clear();
                    transactAccount deposit = new transactAccount();
                    deposit.Execute("deposit");
                    break;

                //if 4 is entered, call withdraw
                case 4:
                    Console.Clear();
                    transactAccount withdraw = new transactAccount();
                    withdraw.Execute("withdraw");
                    break;

                //if 5 is entered, print statement for account
                case 5:
                    Console.Clear();
                    accountStatement statement = new accountStatement();
                    statement.printStatement();
                    break;

                //if 6 is entered, delete account
                case 6:
                    Console.Clear();
                    deleteAccount delete = new deleteAccount();
                    delete.removeAccount();
                    break;

                //if 7 is entered, exit from console
                case 7:
                    exitCond = true;
                    break;
            }

        }
    }
}
