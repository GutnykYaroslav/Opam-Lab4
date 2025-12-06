using System;
using System.Collections.Generic;
using System.Text;

namespace opam_lab3
{
    class Program
    {
        struct Product
        {
            public int Id;
            public string Name;
            public double Price;
            public int Quantity;
        }

        struct Client
        {
            public int Id;
            public string Name;
            public string Phone;
        }

        static List<Product> products = new List<Product>();
        static List<Client> clients = new List<Client>();

        static string login = "admin";
        static string password = "12345";

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            int attempts = 3;
            do
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА ВХОДУ ===");
                Console.Write("Логін: ");
                string l = Console.ReadLine();
                Console.Write("Пароль: ");
                string p = Console.ReadLine();

                if (l == login && p == password) break;

                attempts--;
                Console.WriteLine($"Невірний логін або пароль. Залишилось спроб: {attempts}");
                if (attempts == 0) return;
                Console.ReadKey();
            } while (true);

            string[] productNames = { "Нурофен", "Йод", "Едем", "Аспірин", "Вітамін С" };
            double[] prices = { 120, 40, 150, 60, 200 };
            int[] quantities = { 50, 100, 30, 80, 40 };

            for (int i = 0; i < 5; i++)
            {
                products.Add(new Product
                {
                    Id = i + 1,
                    Name = productNames[i],
                    Price = prices[i],
                    Quantity = quantities[i]
                });

                clients.Add(new Client
                {
                    Id = i + 1,
                    Name = $"Клієнт {i + 1}",
                    Phone = "+38067123456"
                });
            }

            RenderIntro();
            ShowMainMenu();
        }

        public static void RenderIntro()
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine("==== Ласкаво просимо до Аптеки Здоров'я ====");
            Console.WriteLine("===========================================");
            Console.WriteLine("Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }

        public static double GetUserInput(string prompt = "Введіть число:")
        {
            Console.Write(prompt + " ");
            bool isNumber = Double.TryParse(Console.ReadLine(), out double choice);
            if (!isNumber)
            {
                Console.WriteLine("Ви ввели не число! Спробуйте ще раз.");
                return GetUserInput(prompt);
            }
            return choice;
        }

        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nГоловне меню:");
                Console.WriteLine("1. Товари");
                Console.WriteLine("2. Клієнти");
                Console.WriteLine("3. Замовлення");
                Console.WriteLine("4. Пошук");
                Console.WriteLine("5. Статистика");
                Console.WriteLine("6. Вихід");

                int choice = (int)GetUserInput("Виберіть пункт меню:");

                switch (choice)
                {
                    case 1: ShowProductMenu(); break;
                    case 2: ShowClientsMenu(); break;
                    case 3: ShowOrderMenu(); break;
                    case 4: SearchProductByNameStart(); break;
                    case 5: ShowStatistics(); break;
                    case 6: Environment.Exit(0); break;
                    default: Console.WriteLine("Неправильний вибір."); Console.ReadKey(); break;
                }
            }
        }

        private static void ShowProductMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ТОВАРИ ===");
                Console.WriteLine("1. Перегляд товарів (Таблиця)");
                Console.WriteLine("2. Додати товар");
                Console.WriteLine("3. Видалити товар");
                Console.WriteLine("4. Сортування товарів");
                Console.WriteLine("5. Пошук товару");
                Console.WriteLine("6. Назад");

                int choice = (int)GetUserInput("Виберіть дію:");

                switch (choice)
                {
                    case 1: DisplayProducts(); break;
                    case 2: AddProduct(); break;
                    case 3: DeleteProduct(); break;
                    case 4: ShowSortMenu(); break;
                    case 5: SearchProductByNameStart(); break;
                    case 6: return;
                    default: Console.WriteLine("Невірний вибір!"); Console.ReadKey(); break;
                }
            }
        }

        private static void DisplayProducts()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ТОВАРІВ ===");
            Console.WriteLine("| {0,-5} | {1,-20} | {2,10} | {3,10} |", "ID", "Назва", "Ціна", "К-сть");
            Console.WriteLine(new string('-', 56));

            foreach (var p in products)
            {
                Console.WriteLine("| {0,-5} | {1,-20} | {2,10:F2} | {3,10} |",
                    p.Id, p.Name, p.Price, p.Quantity);
            }
            Console.WriteLine(new string('-', 56));
            Console.ReadKey();
        }

        private static void AddProduct()
        {
            Console.Write("Назва: ");
            string name = Console.ReadLine();
            double price = GetUserInput("Ціна:");
            int quantity = (int)GetUserInput("Кількість:");

            int newId = (products.Count > 0) ? products[products.Count - 1].Id + 1 : 1;

            products.Add(new Product
            {
                Id = newId,
                Name = name,
                Price = price,
                Quantity = quantity
            });
            Console.WriteLine("Товар додано!");
            Console.ReadKey();
        }

        private static void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("--- Видалення ---");
            foreach (var p in products) Console.WriteLine($"{p.Id}. {p.Name}");

            int idToDelete = (int)GetUserInput("Введіть ID товару для видалення:");

            int index = products.FindIndex(p => p.Id == idToDelete);

            if (index != -1)
            {
                Console.WriteLine($"Видалено: {products[index].Name}");
                products.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Товар з таким ID не знайдено.");
            }
            Console.ReadKey();
        }

        private static void SearchProductByNameStart()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК ===");
            Console.Write("Введіть перші літери назви: ");
            string search = Console.ReadLine().ToLower();

            bool found = false;
            Console.WriteLine("\nРезультати пошуку:");
            foreach (var p in products)
            {
                if (p.Name.ToLower().StartsWith(search))
                {
                    Console.WriteLine($"ID: {p.Id} | {p.Name} - {p.Price} грн");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Не знайдено");
            Console.ReadKey();
        }

        private static void ShowSortMenu()
        {
            Console.Clear();
            Console.WriteLine("\n--- Сортування ---");
            Console.WriteLine("1. Стандартне (List.Sort)");
            Console.WriteLine("2. Власне (Бульбашкове)");

            int choice = (int)GetUserInput("Виберіть метод:");

            if (choice == 1)
            {
                products.Sort((a, b) => a.Price.CompareTo(b.Price));
                Console.WriteLine("Відсортовано за зростанням ціни (Standard).");
            }
            else if (choice == 2)
            {
                for (int i = 0; i < products.Count - 1; i++)
                {
                    for (int j = 0; j < products.Count - i - 1; j++)
                    {
                        if (products[j].Price > products[j + 1].Price)
                        {
                            var temp = products[j];
                            products[j] = products[j + 1];
                            products[j + 1] = temp;
                        }
                    }
                }
                Console.WriteLine("Відсортовано за зростанням ціни (Bubble).");
            }

            DisplayProducts();
        }

        private static void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine("=== СТАТИСТИКА ===");

            if (products.Count == 0)
            {
                Console.WriteLine("Немає товарів");
                Console.ReadKey();
                return;
            }

            double totalValue = 0;
            double maxPrice = 0;
            double minPrice = double.MaxValue;
            int totalQuantity = 0;
            int expensiveCount = 0;

            foreach (var p in products)
            {
                totalValue += p.Price * p.Quantity;
                totalQuantity += p.Quantity;
                if (p.Price > maxPrice) maxPrice = p.Price;
                if (p.Price < minPrice) minPrice = p.Price;
                if (p.Price > 100) expensiveCount++;
            }

            Console.WriteLine($"Загальна вартість складу: {totalValue:F2} грн");
            Console.WriteLine($"Середня ціна товару:      {(totalQuantity > 0 ? totalValue / totalQuantity : 0):F2} грн");
            Console.WriteLine($"Максимальна ціна:         {maxPrice} грн");
            Console.WriteLine($"Мінімальна ціна:          {minPrice} грн");
            Console.WriteLine($"Товарів дорожче 100 грн:  {expensiveCount} шт");
            Console.WriteLine($"Всього найменувань:       {products.Count}");
            Console.ReadKey();
        }

        private static void ShowClientsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== КЛІЄНТИ ===");
                Console.WriteLine("1. Перегляд клієнтів");
                Console.WriteLine("2. Додати клієнта");
                Console.WriteLine("3. Назад");

                int choice = (int)GetUserInput("Виберіть дію:");
                switch (choice)
                {
                    case 1: DisplayClients(); break;
                    case 2: AddClient(); break;
                    case 3: return;
                    default: Console.WriteLine("Невірний вибір!"); Console.ReadKey(); break;
                }
            }
        }

        private static void DisplayClients()
        {
            Console.WriteLine("\nСписок клієнтів:");
            for (int i = 0; i < clients.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {clients[i].Name} - {clients[i].Phone}");
            }
            Console.ReadKey();
        }

        private static void AddClient()
        {
            Console.Write("Ім'я: ");
            string name = Console.ReadLine();
            Console.Write("Телефон: ");
            string phone = Console.ReadLine();

            clients.Add(new Client
            {
                Id = clients.Count + 1,
                Name = name,
                Phone = phone
            });
            Console.WriteLine("Клієнта додано!");
            Console.ReadKey();
        }

        private static void ShowOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАМОВЛЕННЯ ===");
            Console.WriteLine("Доступні товари:");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Name} - {p.Price} грн");
            }

            Console.WriteLine("\n*Розрахунок для 3-х фіксованих товарів (демо)*");
            double priceNurofen = 120;
            double priceIodine = 40;
            double priceEdem = 150;

            double Nurofen = GetUserInput("Кількість Нурофену:");
            double Iodine = GetUserInput("Кількість Йоду:");
            double Edem = GetUserInput("Кількість Едему:");

            double totalPrice = Nurofen * priceNurofen + Iodine * priceIodine + Edem * priceEdem;
            double discount = totalPrice > 1000 ? 15 : 5;
            double discountTotal = totalPrice * discount / 100;

            Console.WriteLine($"\nЗагальна вартість: {totalPrice} грн");
            Console.WriteLine($"Знижка: {discount}%");
            Console.WriteLine($"До сплати: {totalPrice - discountTotal} грн");
            Console.ReadKey();
        }
    }
}