using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumakovLab_8
{
    public enum AccountType
    {
        Current,
        Saving
    }
    public class Account
    {
        private static int nextAccountNumber;
        private int AccountNumber;
        public decimal Balance;
        public AccountType Accounttype;

        public Account()
        {
            AccountNumber = GenerateAccountNumber();

        }

        private int GenerateAccountNumber()
        {
            Random rand = new Random();
            nextAccountNumber = rand.Next();
            return nextAccountNumber;
        }

        public static int GetNextAccountNumber()
        {
            return ++nextAccountNumber;
        }


        public Account GetAccount()
        {
            Account account = new Account();
            account.Balance = 15000;
            account.Accounttype = AccountType.Current;
            return account;
        }
        public decimal TakeoffAccount(decimal amount)
        {

            if (amount <= Balance && amount > 0)
            {
                Balance -= amount;
                return Balance;
            }
            else
            {
                return -1;
            }
        }

        public void PutonAccount(decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Неправильно введено значение!");
            }
            else
            {
                Balance += amount;
                Console.WriteLine($"Новый баланс: {Balance}");
            }
        }

        public void PrintNewBalance()
        {
            Balance = 15000;
            Console.WriteLine("Снять или пополнить?(после ввода нажмите enter)");
            string str = Console.ReadLine();
            str = str.ToLower();
            if (str == "снять")
            {
                Console.WriteLine("Введите сумму для снятия(после ввода нажмите enter):");
                decimal amount = decimal.Parse(Console.ReadLine());
                decimal newBalance = TakeoffAccount(amount);
                if (newBalance == -1)
                {
                    Console.WriteLine("Недостаточно средств на счете или неправильно введено значение для снятие!");
                }
                else
                {
                    Console.WriteLine($"Новый баланс: {newBalance}");
                }
            }
            else if (str == "пополнить")
            {
                Console.WriteLine("Введите сумму для пополнения(после ввода нажмите enter):");
                decimal amount = decimal.Parse(Console.ReadLine());
                PutonAccount(amount);
            }
        }

        public void Print()
        {
            Console.WriteLine($"Номер: {AccountNumber}");
            Console.WriteLine($"Баланс: {Balance}");
            Console.WriteLine($"Тип счета: {Accounttype}");
        }
        public void TransferMoney(Account account1, decimal amount)
        {
            decimal newBalance = account1.TakeoffAccount(amount);

            if (newBalance != -1)
            {
                account1.AccountNumber++;
                Balance += amount;
                Console.WriteLine($"Новый баланс на счету {AccountNumber}: {Balance}");
                Console.WriteLine($"Новый баланс на счету {account1.AccountNumber}: {newBalance}");
            }
            else
            {
                Console.WriteLine("Недостаточно средств на счете или неправильно введено значение для перевода!");
            }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Упражнение 8.1(добавить метод, который переводит деньги с одного счета на другой)");
            Account BankAccount = new Account().GetAccount();
            BankAccount.Print();
            Console.WriteLine($"Следующий номер счета: {Account.GetNextAccountNumber()}");
            Account BankAccount1 = new Account();
            BankAccount1.PrintNewBalance();
            Account account1 = new Account();
            account1.Balance = 10000;
            account1.Accounttype = AccountType.Current;
            Account account2 = new Account();
            account2.Balance = 5000;
            account2.Accounttype = AccountType.Saving;
            decimal amountToTransfer = 1000;
            account1.TransferMoney(account2, amountToTransfer);

            Console.WriteLine("Упражнение 8.2(реализовать метод, который принимает строку и возвращает строку, буквы в которой идут в обратном порядке.)");
            Console.WriteLine("Введите строку(после ввода нажмите enter): ");
            string a = Console.ReadLine();
            // Присваивание возвращаемого значения метода ReverseString() переменной reversedStr
            string reversedStr = ReverseString(a);
            Console.WriteLine("Результат: " + reversedStr);

            Console.WriteLine("Упражнение 8.3(Написать программу, которая спрашивает у пользователя имя файла и записывает в выходной файл содержимое исходного файла, но заглавными буквами.)");
            File.WriteAllText("file.txt", "Hello, world!");
            Console.WriteLine("Введите название файла(напишите file.txt): ");
            string filename = Console.ReadLine();
            // Проверяем, существует ли файл
            FileInfo fileInfo = new FileInfo(filename);
            if (!fileInfo.Exists)
            {
                Console.WriteLine("Файл не существует");
                return;
                
            }
            // Читаем содержимое исходного файла
            StreamReader streamreader = new StreamReader(filename);
            string input = streamreader.ReadToEnd();
            // Преобразуем содержимое в заглавные буквы
            input = input.ToUpper();
            // Записываем содержимое в выходной файл
            StreamWriter streamwriter = new StreamWriter("output.txt");
            streamwriter.Write(input);
            streamwriter.Close();

            Console.WriteLine("Упражнение 8.4(Реализовать метод, который проверяет реализует ли входной параметр метода интерфейс System.IFormattable.)");
            string str = "Hello";
            int num = 10;
            Console.WriteLine(CheckIFormattable(str));
            Console.WriteLine(CheckIFormattable(num));

            Console.WriteLine("Домашнее задание 8.1(Сформировать новый файл, содержащий список адресов электронной почты)");
            File.WriteAllText("file2.txt", "Иванов Иван Иванович # iviviv@mail.ru\nПетров Петр Петрович # petr@mail.ru");
            Program program = new Program();
            string str1 = "Иванов Иван Иванович # iviviv@mail.ru";
            string str2 = "Петров Петр Петрович # petr@mail.ru";
            program.SearchMail(ref str1);
            program.SearchMail(ref str2);
            // Создаем объект класса StreamWriter
            StreamWriter writer = new StreamWriter("output2.txt");
            // Записываем результат работы метода SearchMail() в файл
            writer.WriteLine(str1);
            writer.WriteLine(str2);
            writer.Close();
        }
        public static string ReverseString(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        public static bool CheckIFormattable(object obj)
        {
            // Проверяем, является ли obj ссылкой
            if (obj is null)
            {
                return false;
            }
            // Проверяем, реализует ли obj интерфейс IFormattable
            if (obj is IFormattable)
            {
                // Преобразуем obj в тип IFormattable с помощью оператора as
                IFormattable formattableObj = obj as IFormattable;
                // Если преобразование прошло успешно, то obj реализует интерфейс IFormattable
                if (formattableObj != null)
                {
                    return true;
                }
            }
            return false;
        }
        public void SearchMail(ref string s)
        {
            if (s.Length <= 0)
            {
                return;
            }
            int index = s.IndexOf('#');
            if (index != -1)
            {
                string email = s.Substring(index + 1);
                s = email;
                Console.WriteLine(s);
            }
        }

    }
}
