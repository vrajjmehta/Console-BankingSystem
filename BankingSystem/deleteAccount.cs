using System;
using System.IO;

namespace BankingSystem
{
    public class deleteAccount : customer
    {
        private int acNumberCursorLeft, acNumberCursorTop;
        private bool foundAccount;
        private int acNoCursorLeft, acNoCursorTop;
        private int acBalanceCursorLeft, acBalanceCursorTop;
        private int firstNameCursorLeft, firstNameCursorTop;
        private int lastNameCursorLeft, lastNameCursorTop;
        private int addressCursorLeft, addressCursorTop;
        private int phoneCursorLeft, phoneCursorTop;
        private int emailCursorLeft, emailCursorTop;

        public deleteAccount()
        {
            foundAccount = false;
        }

        private void displayScreen()
        {
            try
            {
                Console.WriteLine("\t\t╔══════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t|              DELETE AN ACCOUNT                   |");
                Console.WriteLine("\t\t|══════════════════════════════════════════════════|");

                Console.WriteLine("\t\t|              ENTER THE DETAILS                   |");
                Console.WriteLine("\t\t|                                                  |");
                Console.Write("\t\t|    Account Number:");
                acNumberCursorLeft = Console.CursorLeft;
                acNumberCursorTop = Console.CursorTop;
                Console.Write("                               |\n");
                Console.WriteLine("\t\t╚══════════════════════════════════════════════════╝");

                Console.SetCursorPosition(acNumberCursorLeft, acNumberCursorTop);
                AccountNumber = Convert.ToInt64(Console.ReadLine());

                checkAccountExists();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void displayAccount()
        {
            Console.WriteLine("\t\t╔══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t|                 ACCOUNT DETAILS                  |");
            Console.WriteLine("\t\t|══════════════════════════════════════════════════|");
            Console.WriteLine("\t\t|                                                  |");

            Console.Write("\t\t|    Account No:");
            acNoCursorLeft = Console.CursorLeft;
            acNoCursorTop = Console.CursorTop;
            Console.Write("                                   |\n");

            Console.Write("\t\t|    Account Balance:$");
            acBalanceCursorLeft = Console.CursorLeft;
            acBalanceCursorTop = Console.CursorTop;
            Console.Write("                             |\n");

            Console.Write("\t\t|    First Name:");
            firstNameCursorLeft = Console.CursorLeft;
            firstNameCursorTop = Console.CursorTop;
            Console.Write("                                   |\n");

            Console.Write("\t\t|    Last Name:");
            lastNameCursorLeft = Console.CursorLeft;
            lastNameCursorTop = Console.CursorTop;
            Console.Write("                                    |\n");

            Console.Write("\t\t|    Address:");
            addressCursorLeft = Console.CursorLeft;
            addressCursorTop = Console.CursorTop;
            Console.Write("                                      |\n");

            Console.Write("\t\t|    Phone:");
            phoneCursorLeft = Console.CursorLeft;
            phoneCursorTop = Console.CursorTop;
            Console.Write("                                        |\n");

            Console.Write("\t\t|    Email:");
            emailCursorLeft = Console.CursorLeft;
            emailCursorTop = Console.CursorTop;
            Console.Write("                                        |\n");
            Console.WriteLine("\t\t╚══════════════════════════════════════════════════╝");


            try
            {
                string[] accountData = File.ReadAllLines(AccountNumber + ".txt");

                Console.SetCursorPosition(acNoCursorLeft, acNoCursorTop);
                Console.WriteLine(AccountNumber);

                Console.SetCursorPosition(acBalanceCursorLeft, acBalanceCursorTop);
                Console.WriteLine(accountData[5]);

                Console.SetCursorPosition(firstNameCursorLeft, firstNameCursorTop);
                Console.WriteLine(accountData[0]);

                Console.SetCursorPosition(lastNameCursorLeft, lastNameCursorTop);
                Console.WriteLine(accountData[1]);

                Console.SetCursorPosition(addressCursorLeft, addressCursorTop);
                Console.WriteLine(accountData[2]);

                Console.SetCursorPosition(phoneCursorLeft, phoneCursorTop);
                Console.WriteLine(accountData[3]);

                Console.SetCursorPosition(emailCursorLeft, emailCursorTop);
                Console.WriteLine(accountData[4]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }

        public void checkAccountExists()
        {
            try
            {
                string[] accountNumbersData = File.ReadAllLines("accountNumbers.txt");
                foreach (string set in accountNumbersData)
                {
                    if (set == Convert.ToString(AccountNumber))
                    {
                        Console.WriteLine("\n\nAccount found!");
                        Console.ReadKey();
                        foundAccount = true;
                        displayAccount();
                    }
                }
                if (foundAccount == false)
                {
                    Console.WriteLine("\n\nAccount not found!");
                    checkAgain();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void checkAgain()
        {
            Console.WriteLine("\n\nCheck another account (y/n)?");
            string info = Console.ReadLine();
            if (info == "y")
            {
                Console.Clear();
                displayScreen();
            }
        }

        public void removeAccount()
        {
            try
            {
                displayScreen();
                if (foundAccount == true)
                {
                    Console.WriteLine("\nDelete(y/n)?");
                    string info = Console.ReadLine();
                    if (info == "y")
                    {
                        string[] accountNumbersData = File.ReadAllLines("accountNumbers.txt");
                        for (int loopVar = 0; loopVar < accountNumbersData.Length; loopVar++)
                        {
                            if (accountNumbersData[loopVar] == Convert.ToString(AccountNumber))
                            {
                                accountNumbersData[loopVar] = null;
                                break;
                            }
                        }
                        File.WriteAllLines("accountNumbers.txt", accountNumbersData);
                        File.Delete(Convert.ToString(AccountNumber) + ".txt");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
