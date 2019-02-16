using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        public static void Spaces(int level)//Создаём функцию для пробелов
        {
            for (int k = 0; k < level; k++)
            {
                Console.Write("  ");
            }
        }
        public static void Show(DirectoryInfo Dir, int level)//Функция для показа наших файлов и папок
        {
            foreach (FileInfo f in Dir.GetFiles())//Создаём пробелы для файлов через цикл
            {
                Spaces(level + 1);//Увеличиваем количество пробелов для файла 
                Console.WriteLine(f.Name);//Выводим название файла
            }
            foreach (DirectoryInfo d in Dir.GetDirectories())//Создаём пробелы для папок через цикл
            {
                Spaces(level + 1);//Увеличиваем количество пробелов для папки
                Console.WriteLine(d.Name);//Вывод названия папки
                Show(d, level + 2);//Рекурсия для проверки папок в папке
            }
        }
        static void Main(string[] args)
        {
            DirectoryInfo Dir = new DirectoryInfo(@"C:\Users\Faizullayev Rauan\Desktop\PP2\Lab 2");//Подключаем наш путь к консоли
            Console.WriteLine(Dir.Name);//Вывод названия папки
            Show(Dir, 0);//Вызываем функицию 
            Console.ReadKey();//Чтобы консоль сразу не закрывалась
        }
    }
}