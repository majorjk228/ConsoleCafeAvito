using System;


namespace Curs
{
    public partial class MenuItem
    {
        public static void Input(string name)
        {
            Console.Write("Введите цену пункта меню: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Введите калорийность пункта меню: ");
            int calories = int.Parse(Console.ReadLine());

            Program.items.Add(new MenuItem(name, price, calories));

            Console.WriteLine("Добавлен пункт меню.");
        }

        public static void EditMenuItem(string name)
        {
            Console.Write("Введите новую цену пункта меню: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Введите новые калории пункта меню: ");
            int calories = int.Parse(Console.ReadLine());

            MenuItem item = Program.items.Find(i => i?.Name == name);
            if (item != null)
            {
                item.Price = price;
                item.Calories = calories;
                Console.WriteLine("Пункт меню отредактирован.");
            }
            else
            {
                Console.WriteLine("Пункт меню не найден.");
            }
        }

        public string Name { get; set; }
        public double Price { get; set; }
        public int Calories { get; set; }

        public MenuItem(string name, double price, int calories)
        {
            Name = name;
            Price = price;
            Calories = calories;
        }

        public override string ToString()
        {
            return $"{Name} - Цена: {Price}, Калорийность: {Calories}";
        }
    }
}