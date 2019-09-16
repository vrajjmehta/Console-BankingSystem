using System;
using System.IO;

namespace BankingSystem
{
    public class transactAccount : Account
    {
        private int acNumberCursorLeft, acNumberCursorTop;
        private bool foundAccount;
        private int amountCursorLeft, amountCursorTop;
        private const string deposit = "DEPOSIT";
        private const string withdraw = "WITHRAW";
        private string dual, info;
        public string amount { get; set; }
        public DateTime timestamp { get; set; }

        //CONSTRUCTOR TO INTIALIZE ALL BOOL VARIABLES AS FALSE.
        public transactAccount()
        {   
            foundAccount = false;
        }

        private void depositWithdrawMoney()
        {   //CONSOLE UI TO ENTER ACCOUNT NUMBER AND AMOUNT (SAME UI FOR BOTH DEPOSIT AND WITHDRAWAL)
            Console.WriteLine("\t\t╔══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t|                    " + dual + "                       |"); //dual VARIABLE ACCORDINGLY
            Console.WriteLine("\t\t|══════════════════════════════════════════════════|");

            Console.WriteLine("\t\t|              ENTER THE DETAILS                   |");
            Console.WriteLine("\t\t|                                                  |");
            Console.Write("\t\t|    Account Number:");
            acNumberCursorLeft = Console.CursorLeft;
            acNumberCursorTop = Console.CursorTop;
            Console.Write("                               |\n");
            Console.Write("\t\t|    Amount:$");
            amountCursorLeft = Console.CursorLeft;
            amountCursorTop = Console.CursorTop;
            Console.Write("                                      |\n");
            Console.WriteLine("\t\t╚══════════════════════════════════════════════════╝");
            try
            {   //CHECK IF ACCOUNT NUMBER ENTERED IN CORRECT FORMAT
                Console.SetCursorPosition(acNumberCursorLeft, acNumberCursorTop);
                AccountNumber = Convert.ToInt64(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            checkAccountExists();
        }

        private void depositAmount()
        {   //DEPOSIT MONEY TO AN ACCOUNT
            Console.SetCursorPosition(amountCursorLeft, amountCursorTop);
            try
            {
                int money = Convert.ToInt32(Console.ReadLine());
                string[] accountData = File.ReadAllLines(Convert.ToString(AccountNumber) + ".txt");

                accountData[6] = Convert.ToString(Convert.ToInt32(accountData[6]) + 1);
                // OVERWRITES THE STATEMENT IF THERE ARE MORE THAN 5 TRANSACTIONS
                if ((Convert.ToInt32(accountData[6])) % 5 == 0)
                {
                    accountData[(Convert.ToInt32(accountData[6]) % 5) + 11] = "+" + Convert.ToString(money) + "|" + DateTime.Now;
                }
                else
                {
                    accountData[(Convert.ToInt32(accountData[6]) % 5) + 6] = "+" + Convert.ToString(money) + "|" + (DateTime.Now);
                }
                //CALCULATES THE ACCOUNT BALANCE
                money = money + Convert.ToInt32(accountData[5]);
                accountData[5] = Convert.ToString(money);

                //STORE TO THE FILE
                File.WriteAllLines(Convert.ToString(AccountNumber) + ".txt", accountData);
                Console.WriteLine("\n\n\nDeposit Successful! Updated balance is :$" + money);
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void withdrawAmount()
        {   //WITHDRAW MONEY TO AN ACCOUNT
            Console.SetCursorPosition(amountCursorLeft, amountCursorTop);
            try
            {
                int money = Convert.ToInt32(Console.ReadLine());
                string[] accountData = File.ReadAllLines(Convert.ToString(AccountNumber) + ".txt");
                //CHECKS IF WITHDRAWAL AMOUNT IS NOT MORE THAN BALANCE
                if ((Convert.ToInt32(accountData[5]) - money) < 0)
                {
                    Console.WriteLine("\n\nCant withdraw more than account balance!");
                    Console.ReadKey();
                }
                else
                {
                    accountData[6] = Convert.ToString(Convert.ToInt32(accountData[6]) + 1);
                    // OVERWRITES THE STATEMENT IF THERE ARE MORE THAN 5 TRANSACTIONS
                    if ((Convert.ToInt32(accountData[6])) % 5 == 0)
                    {
                        accountData[(Convert.ToInt32(accountData[6]) % 5) + 11] = "-" + Convert.ToString(money) + "|" + (System.DateTime.Now);
                    }
                    else
                    {
                        accountData[(Convert.ToInt32(accountData[6]) % 5) + 6] = "-" + Convert.ToString(money) + "|" + (System.DateTime.Now);
                    }
                    //CALCULATES THE ACCOUNT BALANCE
                    accountData[5] = Convert.ToString(Convert.ToInt32(accountData[5]) - money);

                    //STORE TO THE FILE
                    File.WriteAllLines(Convert.ToString(AccountNumber) + ".txt", accountData);
                    Console.WriteLine("\n\n\nWithdrawal Successful! Updated balance is :$" + accountData[5]);
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }


        public new void checkAccountExists()
        {
            try
            {   //CONVERT ALL THE LINES TO ARRAY OF STRINGS
                string[] accountNumbersData = File.ReadAllLines("accountNumbers.txt");
                foreach (string set in accountNumbersData)
                {
                    if (set == Convert.ToString(AccountNumber))
                    {
                        Console.WriteLine("\n\nAccount found! Enter the amount...");
                        foundAccount = true;
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
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void checkAgain()
        {   //CHECK ANOTHER ACCOUNT DETAILS
            Console.WriteLine("\nRetry(y/n)?");
            info = Console.ReadLine();
            if (info == "y")
            {
                Console.Clear();
                depositWithdrawMoney();
            }
        }

        public void Execute(string dual)
        {   //AS PER USER INPUT CALL THE APPROPIATE METHODS
            if (dual == "deposit")
            {
                this.dual = deposit;
                depositWithdrawMoney();
                if (foundAccount == true)
                {
                    depositAmount();
                }
            }
            else if (dual == "withdraw")
            {
                this.dual = withdraw;
                depositWithdrawMoney();
                if (foundAccount == true)
                {
                    withdrawAmount();
                }
            }
        }

    }
}
