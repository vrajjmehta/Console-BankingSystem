using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BankingSystem
{   //INHERIT CUSTOMER CLASS
    public class Account : customer
    {
        private int firstNameCursorLeft, firstNameCursorTop;
        private int lastNameCursorLeft, lastNameCursorTop;
        private int addressCursorLeft, addressCursorTop;
        private int phoneCursorLeft, phoneCursorTop;
        private int emailCursorLeft, emailCursorTop;

        bool checkPhoneFlag, checkEmailFlag , foundAccount;
        string info;
        string[] accountData;

        public Account()
        {   //CONSTRUCTOR TO INTIALIZE ALL BOOL VARIABLES AS FALSE.
            checkPhoneFlag = false;
            checkEmailFlag = false;
            foundAccount = false;
        }

        public void displayNewAccountPage()
        {   //DISPLAY UI FOR CREATE A NEW ACCOUNT PAGE
            Console.WriteLine("\t\t╔══════════════════════════════════════════════════╗");
            Console.WriteLine("\t\t|               CREATE A NEW ACCOUNT               |");
            Console.WriteLine("\t\t|══════════════════════════════════════════════════|");
            Console.WriteLine("\t\t|              ENTER THE DETAILS                   |");
            Console.WriteLine("\t\t|                                                  |");
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

        }

        private void enterDetail()
        {   //SET CURSOR FOR ENTERING DETAILS
            try
            {
                Console.SetCursorPosition(firstNameCursorLeft, firstNameCursorTop);
                FirstName = Console.ReadLine();

                Console.SetCursorPosition(lastNameCursorLeft, lastNameCursorTop);
                LastName = Console.ReadLine();

                Console.SetCursorPosition(addressCursorLeft, addressCursorTop);
                Address = Console.ReadLine();

                Console.SetCursorPosition(phoneCursorLeft, phoneCursorTop);
                enterPhone();

                Console.SetCursorPosition(emailCursorLeft, emailCursorTop);
                enterEmail();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void enterPhone()
        {       // CHECK PHONE IS CORRECT ( ALL DIGIT NUMBERS & LENGTH<=10)
                string input = Console.ReadLine();
                bool isNumeric = !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
                if (isNumeric && input.Length <= 10)
                {
                    PhoneNumber = Convert.ToInt64(input);
                    checkPhoneFlag = true;
                }
                else
                {
                    Console.WriteLine("\n\nPlease enter valid Phone Number.");
                    Console.ReadKey();
                }
        }

        private void enterEmail()
        {       // CHECK EMAIL IS CORRECT WITH '@'
                string input = Console.ReadLine();
                for (int loopVar = 0; loopVar < input.Length; loopVar++)
                {
                    if ((input[loopVar]) == '@')
                    {
                        Email = input;
                        checkEmailFlag = true;
                    }
                }
                if (checkEmailFlag == false)
                {
                    Console.WriteLine("\n\nPlease enter valid email address.");
                    Console.ReadKey();
                }
        }

        private void createAccount()
        {
            getAccountNumber();
            accountFileStorage();

            Console.WriteLine("Account number is: " + AccountNumber);
        }

        private void getAccountNumber()
        {
            try
            {    //  ACCOUNTNUMBER IS THE CURRENT DATETIME. 
                do
                {
                    AccountNumber = Convert.ToInt64(DateTime.Now.ToString("ddHHmmss"));
                    checkAccountExists();
                } while (foundAccount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        public void checkAccountExists()
        {
            //IF THE SAME ACCOUNTNUMBER EXISTS, GET ANOTHER ACCOUNT NUMBER
            string[] accountNumbersData = File.ReadAllLines("accountNumbers.txt");
            foreach (string set in accountNumbersData)
            {
                if (set == Convert.ToString(AccountNumber))
                {
                    foundAccount = true;   
                }
            }
            foundAccount = false;
        }

        private void accountFileStorage()
        {
            try
            {   //store account details in array of string. 
                accountData = new string[12];
                accountData[0] = FirstName;
                accountData[1] = LastName;
                accountData[2] = Address;
                accountData[3] = Convert.ToString(PhoneNumber);
                accountData[4] = Email;
                accountData[5] = Convert.ToString(AccountBalance);
                accountData[6] = "0";
                accountData[7] = null;
                accountData[8] = null;
                accountData[9] = null;
                accountData[10] = null;
                accountData[11] = null;

                //Store the data from the string to the AccountNumber file 
                File.WriteAllLines(Convert.ToString(AccountNumber) + ".txt", accountData);
                StreamWriter sw = new StreamWriter("accountNumbers.txt", append: true);
                sw.WriteLine(Convert.ToString(AccountNumber));
                sw.Close();
                Console.WriteLine("Account created!\n");
            }
            catch (FileNotFoundException)           //IF FILE NOT FOUND
            {
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private async Task<bool> SendEmail()
        {
            try
            {
                //email sent using SmtpClient (via Gmail)
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vrajmehta1511@gmail.com");     //FROM EMAIL ADDRESS
                mail.To.Add(Email);                                         // TO EMAIL ADDRESS
                mail.Subject = "Account Details";                           //SUBJECT OF EMAIL
                //CONTENTS OF EMAIL(BODY)
                mail.Body = "Account Number: " + AccountNumber + "FirstName: " + accountData[0] + "\nLastName: " + accountData[1] + "\nAddress: " + accountData[2] + "\nPhone Number: "
                            + accountData[3] + "\nEmail :" + accountData[4] + "\nAccountBalance :$" + accountData[5];

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vrajmehta1511", "Password0#");  //USERNAME & PASSWORD OF EMAIL ACCOUNT
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

        public async Task ExecuteAsync()
        {
            //main method for Account.cs
            do
            {
                Console.Clear();
                displayNewAccountPage();     //display console ui
                enterDetail();               //sets cursor to enter details

                if ((checkPhoneFlag && checkEmailFlag))
                {
                    Console.WriteLine("\n\nIs the information correct (y/n)?");
                    info = Console.ReadLine();
                }
                // proceed only if phone number and email address has been entered correctly
            } while (!(checkPhoneFlag && checkEmailFlag && info == "y"));

            createAccount();            //create new account as a file and store info in it

            Console.WriteLine("\nPlease wait...... Sending the account details via email to " + Email);
            bool e = await SendEmail();
            if (e == true)              //if email is sent successfully
            {
                Console.WriteLine("\nSucess!\nAccount details has been sent via email\n");
            }
            else                        //if email not send(improper email address or no internet connection
            {
                Console.WriteLine("Could not send email. Check! Enter proper email address");
            }
            Console.ReadKey();
        }
    }
}
