using System;
using System.IO;

namespace BankingSystem
{
    public class accountStatement : customer
    {
        private int acNumberCursorLeft, acNumberCursorTop;
        private bool foundAccount;
        private int acNoCursorLeft, acNoCursorTop, acBalanceCursorLeft, acBalanceCursorTop;
        private int firstNameCursorLeft,firstNameCursorTop, lastNameCursorLeft,lastNameCursorTop;
        private int addressCursorLeft,addressCursorTop, transCursorLeft, transCursorTop;
        private int phoneCursorLeft, phoneCursorTop, emailCursorLeft,emailCursorTop;

        public accountStatement()
        {
            foundAccount = false;
        }

        public void printStatement()
        {
            try
            {
                Console.WriteLine("\t\t╔══════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t|                   STATEMENT                      |");
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void displayStatement()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("\n\t\t╔══════════════════════════════════════════════════╗");
                Console.WriteLine("\t\t|               SIMPLE BANKING SYSTEM              |");
                Console.WriteLine("\t\t|══════════════════════════════════════════════════|");
                Console.WriteLine("\t\t|        ACCOUNT STATEMENT                         |");
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

                Console.WriteLine("\t\t\tLast 5 transactions:");
                transCursorLeft = Console.CursorLeft;
                transCursorTop = Console.CursorTop;

                string[] accountData = System.IO.File.ReadAllLines(AccountNumber + ".txt");

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

                Console.SetCursorPosition(transCursorLeft, transCursorTop);
                Console.WriteLine("\t\t\tDeposit/Withdrawal\tTimeStamp");

                int cond;
                if (Convert.ToInt32(accountData[6]) >= 5) { cond = 12; }
                else { cond = 7 + Convert.ToInt32(accountData[6]); }
                for (int loopVar = 7; loopVar < (cond); ++loopVar)
                {
                    string[] split = accountData[loopVar].Split("|");
                    Console.WriteLine("\t\t\t" + split[0] + "\t\t\t" + split[1]);
                }
                Console.WriteLine("\t\t\tCurrent Balance:$" + accountData[5]);

                Console.ReadKey();

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
                string[] accountNumbersData = System.IO.File.ReadAllLines("accountNumbers.txt");
                foreach (string set in accountNumbersData)
                {
                    if (set == Convert.ToString(AccountNumber))
                    {
                        Console.WriteLine("\n\nAccount found!The statement is displayed below...");
                        Console.ReadKey();
                        foundAccount = true;
                        displayStatement();
                        break;
                    }
                }
                if (foundAccount == false)
                {
                    Console.WriteLine("\n\nAccount not found!");
                    checkAgain();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void checkAgain()
        {
            Console.WriteLine("\nRetry(y/n)?");
            string info = Console.ReadLine();
            if (info == "y")
            {
                Console.Clear();
                printStatement();
            }
        }
    }
}
