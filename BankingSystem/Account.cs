using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class Account : customer
    {
        private int firstNameCursorLeft, firstNameCursorTop;
        private int lastNameCursorLeft, lastNameCursorTop;
        private int addressCursorLeft, addressCursorTop;
        private int phoneCursorLeft, phoneCursorTop;
        private int emailCursorLeft, emailCursorTop;

        bool checkPhoneFlag, checkEmailFlag;
        string info;
        string[] accountData;

        public Account()
        {
            checkPhoneFlag = false;
            checkEmailFlag = false;
        }

        public void displayNewAccountPage()
        {
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

        private void enterPhone()
        {
            try
            {
                string input = Console.ReadLine();
                bool isNumeric = !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
                if (isNumeric && input.Length == 10)
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void enterEmail()
        {
            try
            {
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
            {
                AccountNumber = Convert.ToInt64(DateTime.Now.ToString("ddMMHHmmss"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private void accountFileStorage()
        {
            try
            {
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

                File.WriteAllLines(Convert.ToString(AccountNumber) + ".txt", accountData);
                StreamWriter sw = new StreamWriter("accountNumbers.txt", append: true);
                sw.WriteLine(Convert.ToString(AccountNumber));
                sw.Close();
                Console.WriteLine("Account created!\n");
            }
            catch (FileNotFoundException)
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

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vrajmehta1511@gmail.com");
                mail.To.Add(Email);
                mail.Subject = "Account Details";
                mail.Body = "FirstName: " + accountData[0] + "\nLastName: " + accountData[1] + "\nAddress: " + accountData[2] + "\nPhone Number: "
                            + accountData[3] + "\nEmail :" + accountData[4] + "\nAccountBalance :$" + accountData[5];

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("vrajmehta1511", "Password0#");
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
            do
            {
                Console.Clear();
                displayNewAccountPage();
                enterDetail();

                if ((checkPhoneFlag && checkEmailFlag))
                {
                    Console.WriteLine("\n\nIs the information correct (y/n)?");
                    info = Console.ReadLine();
                }

            } while (!(checkPhoneFlag && checkEmailFlag && info == "y"));

            createAccount();

            Console.WriteLine("\nPlease wait...... Sending the account details via email to " + Email);
            bool e = await SendEmail();
            if (e == true)
            {
                Console.WriteLine("\nSucess!\nAccount details has been sent via email\n");
            }
            else
            {
                Console.WriteLine("Could not send email. Check!");
            }
            Console.ReadKey();
        }
    }
}
