using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Curs
{
    internal class Program
    {
        public static List<MenuItem> items = new List<MenuItem>(); // В колекции храним все наши объекты (пункты меню)

        public void ShowItems(List<MenuItem> items)
        {
            foreach (MenuItem item in items)
            {
                Console.WriteLine(item);
            }
        }

        public void RemoveMenuItem()
        {
            Console.Write("Введите название пункта меню, который нужно удалить: ");
            string name = Console.ReadLine();

            MenuItem item = Program.items.Find(i => i?.Name == name);
            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine("Пункт меню удален.");
            }
            else
            {
                Console.WriteLine("Пункт меню не найден.");
            }
        }

        public void LoadMenuFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    string name = parts[0];
                    double price = double.Parse(parts[1]);
                    int calories = int.Parse(parts[2]);
                    MenuItem item = new MenuItem(name, price, calories);
                    items.Add(item);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка при загрузке данных меню из файла.");
            }
        }

        public void SaveMenuToFile(string filePath)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (MenuItem item in Program.items)
                {
                    string line = $"{item?.Name};{item?.Price};{item?.Calories}";
                    lines.Add(line);
                }

                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Данные меню сохраняются в файл.");
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка сохранения данных меню в файл.");
            }
        }

        public void Order()
        {
            List<MenuItem> orderItems = new List<MenuItem>(); // Заказанные продукты

            while (true)
            {
                Console.Write("Введите название блюда или '0' для завершения заказа: ");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    return;
                }

                MenuItem itemMenu = FindItemByName(items, input);
                if (itemMenu == null)
                {
                    Console.WriteLine("Блюдо не найдено!");
                    return;
                }

                orderItems.Add(itemMenu);

                Console.WriteLine("Общая стоимость: " + orderItems.Select(x => x.Price).Sum() + " руб.");
                Console.WriteLine("Общая калорийность: " + orderItems.Select(x => x.Calories).Sum() + " ккал");
            }
        }


        // метод для поиска блюда по названию
        public MenuItem FindItemByName(List<MenuItem> items, string name)
        {
            return items.FirstOrDefault(x => x?.Name.ToLower() == name?.ToLower());
        }

        static void Main(string[] args)
        {
            Program mainMenu = new Program();

            string menuFilePath = "menu.txt"; // Path to the menu data file
            bool exit = false;
            mainMenu.LoadMenuFromFile(menuFilePath);

            while (!exit)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Просмотр меню");
                Console.WriteLine("2. Добавить пункт меню");
                Console.WriteLine("3. Редактировать пункт меню");
                Console.WriteLine("4. Удалить пункт меню");
                Console.WriteLine("5. Фильтровать по калориям");
                Console.WriteLine("6. Фильтровать по цене");
                Console.WriteLine("7. Сортировать по калориям");
                Console.WriteLine("8. Сортировать по цене");
                Console.WriteLine("9. Сохранить меню в файл");
                Console.WriteLine("10. Сделать заказ");
                Console.WriteLine("0. Выход");

                Console.Write("Введите свой выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        mainMenu.ShowItems(items);
                        break;
                    case "2":
                        MenuItem.Input();
                        break;
                    case "3":
                        MenuItem.EditMenuItem();
                        break;
                    case "4":
                        mainMenu.RemoveMenuItem();
                        break;
                    case "5":
                        Console.Write("Введите максимальное количество калорий: ");
                        int maxCalories = int.Parse(Console.ReadLine());
                        Console.WriteLine("Пункты меню отфильтрованы по калориям:");
                        mainMenu.ShowItems(items.Where(i => i?.Calories < maxCalories).ToList());
                        break;
                    case "6":
                        Console.Write("Введите максимальную цену: ");
                        double maxPrice = double.Parse(Console.ReadLine());
                        Console.WriteLine("Пункты меню отфильтрованы по цене:");
                        mainMenu.ShowItems(items.Where(i => i?.Price < maxPrice).ToList());
                        break;
                    case "7":
                        Console.WriteLine("Пункты меню, отсортированные по калориям:");
                        mainMenu.ShowItems(items.OrderBy(i => i?.Calories).ToList());
                        break;
                    case "8":
                        Console.WriteLine("Пункты меню отсортированы по цене:");
                        mainMenu.ShowItems(items.OrderBy(i => i?.Price).ToList());
                        break;
                    case "9":
                        mainMenu.SaveMenuToFile(menuFilePath);
                        break;
                    case "10":
                        Console.WriteLine("Меню:");
                        mainMenu.ShowItems(items);
                        mainMenu.Order();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
