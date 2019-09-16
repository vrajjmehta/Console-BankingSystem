using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class accountStatement : customer
    {
        private int acNumberCursorLeft, acNumberCursorTop;
        private bool foundAccount;
        private int acNoCursorLeft, acNoCursorTop, acBalanceCursorLeft, acBalanceCursorTop;
        private int firstNameCursorLeft, firstNameCursorTop, lastNameCursorLeft, lastNameCursorTop;
        private int addressCursorLeft, addressCursorTop, transCursorLeft, transCursorTop;
        private int phoneCursorLeft, phoneCursorTop, emailCursorLeft, emailCursorTop;
        string[] emailStatement = new string[5];

        //CONSTRUCTOR TO INTIALIZE ALL BOOL VARIABLES AS FALSE.
        public accountStatement()
        {
            foundAccount = false;
        }

        public void printStatement()
        {    //CONSOLE UI TO ENTER ACCOUNT NUMBER AND PRINT STATEMENT
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private async Task displayStatementAsync()
        {   //CONSOLE UI TO DISPLAY STATEMENT
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

                Console.SetCursorPosition(transCursorLeft, transCursorTop);
                Console.WriteLine("\t\t\tDeposit/Withdrawal\tTimeStamp");

                //LOGIC TO EMAIL ONLY THE LAST 5 TRANSACTIONS STATEMENT
                int cond;
                if (Convert.ToInt32(accountData[6]) >= 5) { cond = 12; }
                else { cond = 7 + Convert.ToInt32(accountData[6]); }
                for (int loopVar = 7; loopVar < (cond); ++loopVar)
                {
                    string[] split = accountData[loopVar].Split("|");
                    Console.WriteLine("\t\t\t" + split[0] + "\t\t\t" + split[1]);
                    emailStatement[loopVar - 7] = split[0] + "\t" + split[1];
                }
                Console.WriteLine("\t\t\tCurrent Balance:$" + accountData[5]);
                Console.ReadKey();

                Console.WriteLine("Email Statement (y/n)?");
                string info = Console.ReadLine();
                if (info == "y")
                {   //CHECK WHETHER EMAIL SEND SUCCESSFULLY OR NOT
                    Console.WriteLine("\nPlease wait...... Sending the account statement.");
                    bool b = await SendEmail();
                    if (b == true)
                    {
                        Console.WriteLine("Email sent successfully!...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Cannot sent email. Check!");
                        Console.ReadKey();
                    }
                }

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
            {   //CHECK ACCOUNT EXITS 
                string[] accountNumbersData = File.ReadAllLines("accountNumbers.txt");
                foreach (string set in accountNumbersData)
                {
                    if (set == Convert.ToString(AccountNumber))
                    {
                        Console.WriteLine("\n\nAccount found!The statement is displayed below...");
                        Console.ReadKey();
                        foundAccount = true;
                        displayStatementAsync();        
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void checkAgain()
        {   //CHECK ANOTHER ACCOUNT
            Console.WriteLine("\nRetry(y/n)?");
            string info = Console.ReadLine();
            if (info == "y")
            {
                Console.Clear();
                printStatement();
            }
        }

        private async Task<bool> SendEmail()
        {   //EMAIL STATEMENT USING SMTP CLIENT SERVER VIA GMAIL
            try
            {
                string[] accountData = File.ReadAllLines(AccountNumber + ".txt");
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vrajmehta1511@gmail.com");   //FROM EMAIL ADDRESS
                mail.To.Add(accountData[4]);                              // TO EMAIL ADDRESS
                mail.Subject = "Account Statement";                         //SUBJECT OF EMAIL
                //CONTENTS OF EMAIL(BODY)
                mail.Body = "Account Number: " + AccountNumber + "\nFirstName: " + accountData[0] + "\nLastName: " + accountData[1] + "\nAddress: " + accountData[2]
                            + "\nPhone Number: " + accountData[3] + "\nEmail :" + accountData[4] + "\nAccountBalance :$" + accountData[5]
                            + "\nLast 5 transactions are:\n" + emailStatement[0] + "\n" + emailStatement[1] + "\n" + emailStatement[2]
                            + "\n" + emailStatement[3] + "\n" + emailStatement[4];

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vrajmehta1511", "Password0#"); //USERNAME & PASSWORD OF EMAIL ACCOUNT
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return false;
            }
        }

    }
}
