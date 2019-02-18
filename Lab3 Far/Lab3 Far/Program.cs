using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    class FarManager
    {
        DirectoryInfo dir = null;
        public int cursor;
        public string path;
        public int size;
        FileSystemInfo f1 = null;

        public FarManager(string path) // Конструктор который получает путь и записывает его в исходный путь
        {
            this.path = path;
            cursor = 0;
        }
        public void Color(FileSystemInfo f, int index) // Метод для обозначения разных типов данных разными цветами
        {
            if (cursor == index) // Меняет цвет места где находится курсор
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                f1 = f; // Присваеваемзначение чтобы мы могли использовать переменную f1
            }
            else if (f.GetType() == typeof(DirectoryInfo)) // Цвет для Папки
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else // Цвета для остальных
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
        }
        public void Show() // Метод вписывающий нашу папку
        {

            dir = new DirectoryInfo(path); // Назначает путь
            FileSystemInfo[] FSI = dir.GetFileSystemInfos(); // Записывает всю информацию про папки и файлы в массив
            Console.BackgroundColor = ConsoleColor.Black;//Постоянно меняет цвет нашего фона
            Console.Clear();
            for (int i = 0, j = 0; i < FSI.Length; i++) // Цикл показывающий всё что есть в папке
            {
                if (FSI[i].Name[0] == '.') // Не показывать скрытые файлы
                    continue;
                Color(FSI[i], j); // Вызывает функцию которая перекрашывает наши папки и файлы
                Console.WriteLine(j + 1 + ". " + FSI[i].Name); // Нумерует и показывает файлы и папки
                j++;
            }

        }
        public void HiddenFiles() // Метод вычисляющий скрытые файлы
        {
            DirectoryInfo d = new DirectoryInfo(path); // Посылает путь
            FileSystemInfo[] fi = d.GetFileSystemInfos(); // Создаёт массив из папок и файлов
            size = fi.Length; // размер равен всем элементам 
            for (int i = 0; i < fi.Length; i++) // Цикл для нахождения скрытых файлов
            {
                if (fi[i].Name[0] == '.') // Уменьшаем размер если мы находим скрытые файлы
                    size--;
            }
        }
        public void Down() // Метод для перехода вниз
        {
            cursor++;
            if (cursor == size) // 
                cursor = 0;
        }
        public void Up()//Метод для перехода вверх
        {
            cursor--;
            if (cursor < 0) // 
                cursor = size - 1;
        }
        public void Start() //Главная функция
        {

            ConsoleKeyInfo Cons;
            bool Ok = true; // Булеаная из-за которой наша консоль остаётся открытой
            while (Ok == true) //Цикл зависящий от булеаной
            {
                HiddenFiles(); // Размер без скрытых файлов
                Show(); // Показывает все элементы в массиве 
                Cons = Console.ReadKey(); //Кнопка с определёнными биндами
                if (Cons.Key == ConsoleKey.DownArrow) // Вызов метода Down нажатием клавишей вниз
                {
                    Down();
                }
                else if (Cons.Key == ConsoleKey.Q) // Выход из консоли нажатием Q
                {
                    Ok = false;
                }
                else if (Cons.Key == ConsoleKey.UpArrow) // Вызов метода Up нажатием клавишы вверх
                {
                    Up();
                }
                else if (Cons.Key == ConsoleKey.Enter) // Открываем папку или файл нажатием ENTER
                {
                    if (f1.GetType() == typeof(DirectoryInfo)) // Проверка типа файла
                    {
                        cursor = 0;
                        path = f1.FullName; // Путь приравнивается пути этой папки
                    }
                    else
                    {
                        StreamReader SR = new StreamReader(f1.FullName);
                        Console.WriteLine(SR.ReadToEnd());// Показвает все что есть в файле
                        SR.Close();
                        Console.ReadKey();
                        Console.Clear();

                    }
                }
                else if (Cons.Key == ConsoleKey.Escape) // Идём назад если нажимется клавиша Escape
                {
                    if (dir.Parent.FullName != @"C:\") //Ограничение до диска C
                    {
                        path = dir.Parent.FullName;//Обновляет наш путь
                        cursor = 0;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You Can't go out from the disk"); // Если дошёл до диска С и нажмаешь Escape
                    }
                }
                else if (Cons.Key == ConsoleKey.Backspace) //Удаление
                {
                    if (f1.GetType() == typeof(DirectoryInfo))
                    {
                        cursor = 0;
                        Directory.Delete(f1.FullName, true);
                    }
                    else
                    {
                        cursor = 0;
                        File.Delete(f1.FullName);
                    }
                }
                else if (Cons.Key == ConsoleKey.Tab) // Переминеование
                {
                    Console.Clear();
                    string name = Console.ReadLine(); // Новое имя
                    Console.Clear();
                    string copPath = Path.Combine(dir.FullName, name); //Создаём путь 
                    if (f1.GetType() == typeof(DirectoryInfo))
                    {
                        Directory.Move(f1.FullName, copPath);
                    }
                    else
                    {
                        File.Move(f1.FullName, copPath);
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Faizullayev Rauan\Desktop\acmp";
            FarManager FM = new FarManager(path);
            FM.Start(); // Вызываем функцию
        }
    }
}