using System;

namespace opam_lab2
{
    class Program
    {
        public static void Main(string[] args)
        {
            RenderIntro();
            ShowMainMenu();
        }

        public static void RenderIntro()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("===========================================");
            Console.WriteLine("==== Ласкаво просимо до Аптеки Здоров'я ====");
            Console.WriteLine("===========================================");
            Console.ResetColor();
        }

        public static double GetUserInput(string prompt = "Введіть число:")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prompt + " ");

            bool isNumber = Double.TryParse(Console.ReadLine(), out double choice);

            if (!isNumber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ви ввели не число!");
                Console.ResetColor();
                return GetUserInput(prompt);
            }

            Console.ResetColor();
            return choice;
        }

        public static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Головне меню:");
            Console.WriteLine("1. Товари");
            Console.WriteLine("2. Клієнти");
            Console.WriteLine("3. Замовлення");
            Console.WriteLine("4. Пошук");
            Console.WriteLine("5. Статистика");
            Console.WriteLine("6. Вихід");
            Console.ResetColor();

            double choice = GetUserInput("Виберіть пункт меню:");
            switch (choice)
            {
                case 1:
                    ShowProductMenu();
                    break;
                case 2:
                    ShowClientsMenu();
                    break;
                case 3:
                    ShowOrderMenu();
                    break;
                case 4:
                    Console.WriteLine("Пошук ще не реалізований.");
                    ShowMainMenu();
                    break;
                case 5:
                    Console.WriteLine("Статистика ще не реалізована.");
                    ShowMainMenu();
                    break;
                case 6:
                    Console.WriteLine("Бувай!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                    ShowMainMenu();
                    break;
            }
        }

        private static void ShowOrderMenu()
        {
            double priceNurofen = 120;
            double priceIodine = 40;
            double priceEdem = 150;

            Console.WriteLine("Наші товари:");
            Console.WriteLine($"1. Нурофен ({priceNurofen} грн)");
            Console.WriteLine($"2. Йод ({priceIodine} грн)");
            Console.WriteLine($"3. Едем ({priceEdem} грн)");

            double Nurofen = GetUserInput("Введіть кількість Нурофену (пачок):");
            double Iodine = GetUserInput("Введіть кількість Йоду (бутилок): ");
            double Edem = GetUserInput("Введіть кількість Едему (бутилок): ");

            double totalNurofen = Nurofen * priceNurofen;
            double totalIodine = Iodine * priceIodine;
            double totalEdem = Edem * priceEdem;

            double totalPrice = totalNurofen + totalIodine + totalEdem;

            double discount = 5;
            if (totalPrice > 1000 && totalPrice < 5000)
                discount = 15;
            else if (totalPrice > 10_000)
                discount = 50;

            double discountTotal = Math.Round(totalPrice * (discount / 100)); Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine("=== Підсумок покупки ===");
            Console.WriteLine($"Нурофен: {Nurofen} пачок — {totalNurofen} грн.");
            Console.WriteLine($"Йод: {Iodine} бутилок — {totalIodine} грн.");
            Console.WriteLine($"Едем: {Edem} бутилок — {totalEdem} грн.");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Загальна вартість: {totalPrice} грн.");
            Console.WriteLine($"Знижка: {discount}%");
            Console.WriteLine($"Після знижки: {totalPrice - discountTotal} грн.");
            Console.ResetColor();

            Console.WriteLine("Дякуємо за покупку!");
            Console.WriteLine("Натисніть будь-яку клавішу, щоб повернутись у меню...");
            Console.ReadKey();
            ShowMainMenu();
        }

        private static void ShowClientsMenu()
        {
            Console.WriteLine("Функція в розробці");
            ShowMainMenu();
        }

        private static void ShowProductMenu()
        {
            Console.WriteLine("========= МЕНЮ ТОВАРІВ =========");
            Console.WriteLine("1. Додати новий товар");
            Console.WriteLine("2. Переглянути всі товари");
            Console.WriteLine("3. Редагувати товар");
            Console.WriteLine("4. Видалити товар");
            Console.WriteLine("5. Пошук товару за назвою");
            Console.WriteLine("6. Сортувати за ціною / кількістю");
            Console.WriteLine("7. Повернутись у головне меню");
            Console.WriteLine("--------------------------------");

            double choice = GetUserInput("Виберіть дію:");

            if (choice > 0 && choice < 7)
            {
                Console.WriteLine("Функція в розробці");
                ShowProductMenu();
            }
            else
            {
                ShowMainMenu();
                return;
            }
        }
    }
}