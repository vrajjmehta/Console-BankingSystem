using System;
using System.IO;

namespace BankingSystem
{
    public class LoginPage
    {
        private bool loginCheck;

        public LoginPage()
        {
            loginCheck = false;
        }

        public void Login()
        { 
            string userName, passWord;
            int loginCursorLeft, loginCursorTop, passCursorTop, passCursorLeft;

            //design for console UI

            Console.WriteLine("\t\t╔══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t|           WELCOME TO SIMPLE BANKING SYSTEM       |");
            Console.WriteLine("\t\t|══════════════════════════════════════════════════|");

            Console.WriteLine("\t\t|             LOGIN TO START                       |");
            Console.WriteLine("\t\t|                                                  |");

            Console.Write("\t\t|    User Name:");
            loginCursorLeft = Console.CursorLeft;
            loginCursorTop = Console.CursorTop;
            Console.Write("                                    |\n");

            Console.Write("\t\t|    Password:");
            passCursorLeft = Console.CursorLeft;
            passCursorTop = Console.CursorTop;
            Console.Write("                                     |\n");

            Console.WriteLine("\t\t╚══════════════════════════════════════════════════╝");


            //set cursor for login and pasword
            Console.SetCursorPosition(loginCursorLeft, loginCursorTop);
            userName = Console.ReadLine();

            Console.SetCursorPosition(passCursorLeft, passCursorTop);
            passWord = "";
            // password display '*' code
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    passWord += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && passWord.Length > 0)
                    {
                        passWord = passWord.Substring(0, (passWord.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            Console.WriteLine("\n\n");
            //check the credentials from a file
            try
            { 
                string[] loginData = System.IO.File.ReadAllLines("login.txt");

                // Split each line using "|" as delimiter and check the values as
                // User Name: ------, Passowrd: .... "
                foreach (string set in loginData)
                {
                    // Split each line
                    string[] splits = set.Split('|');
                    //Check the values of username and password
                    if(splits[0]==userName && splits[1] == passWord)
                    {
                        Console.WriteLine("\nValid Credentails!... Login Successsful.Please enter");
                        Console.ReadKey();
                        loginCheck = true;
                        break;
                    }     
                }
                if(loginCheck==false)
                {
                    Console.WriteLine("\nWrong UserName/Pasword... Login Unsucessful!\nPlease enter again");
                    Console.ReadKey();
                }
            }

            // Catch the FileNotFoundException exception
            catch (FileNotFoundException)
            {
                // Display the error message
                Console.WriteLine("File not found");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        
         }

        public void ExecuteLoginPage()
        {
            do
            {
                Console.Clear();
                Login();
            } while (!loginCheck);

        }
       
    }
}
