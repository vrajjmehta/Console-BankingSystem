using System;
using System.IO;
using System.Linq;

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
        long intitalacNumber = 100001;
        string[] accountData;

        public Account()
        { 
            checkPhoneFlag = false;
            checkEmailFlag = false;
        }

        public void displayNewAccountPage()
        {
            Console.WriteLine("\t\t __________________________________________________");
            Console.WriteLine("\t\t|               CREATE A NEW ACCOUNT               |");
            Console.WriteLine("\t\t|__________________________________________________|");
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

            Console.WriteLine("\t\t|__________________________________________________|");

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
            string input = Console.ReadLine();
            bool isNumeric = !string.IsNullOrEmpty(input) && input.All(char.IsDigit);
            if (isNumeric && input.Length == 10)
            {
                PhoneNumber = Convert.ToInt64(input);
                checkPhoneFlag = true;
                
            }
        }

        private void enterEmail()
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
        }

        private void createAccount()
        {
            getAccountNumber();
            accountFileStorage();

            Console.WriteLine("Account number is: " + AccountNumber);
            Console.WriteLine("Account created! details will be provided via email\n");
        }

        private void getAccountNumber()
        {   
            if( new FileInfo("accountNumbers.txt").Length == 0)
            {
                StreamWriter sw = new StreamWriter("accountNumbers.txt",append:true);
                AccountNumber = (intitalacNumber);
                sw.WriteLine(Convert.ToString(AccountNumber));
                sw.Close();       
            }
            else
            {
                string[] accountNumbersData = System.IO.File.ReadAllLines("accountNumbers.txt");
                int acLength = accountNumbersData.Length;

                string num = accountNumbersData[acLength - 1];
                AccountNumber = (Convert.ToInt64(num) + 1);

                StreamWriter sw = new StreamWriter("accountNumbers.txt",append:true);
                sw.WriteLine(Convert.ToString(AccountNumber));
                sw.Close();
                
            }
        }

        private void accountFileStorage()
        {
            accountData = new string[5];
            accountData[0] = FirstName;
            accountData[1] = LastName;
            accountData[2] = Address;
            accountData[3] = Convert.ToString(PhoneNumber);
            accountData[4] = Email;
            
            File.WriteAllLines(Convert.ToString(AccountNumber)+".txt", accountData);
        }

        private void sendEmail()
        {
            
        }

        public void Execute()
        { 
            do
            {
                Console.Clear();
                displayNewAccountPage();
                enterDetail();
                Console.WriteLine("\n\nIs the information correct (y/n)?");
                info = Console.ReadLine();

            } while (!(checkPhoneFlag && checkEmailFlag && info=="y"));

            createAccount();
            sendEmail();

            Console.ReadKey();
        }      
    }
}
