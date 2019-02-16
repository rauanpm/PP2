using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarManager
{
    class Screen
    { // Класс, выводящий все папки и файлы
        public static string Path { get; set; }

        public FileSystemInfo[] Content { get; set; }

        int _curr;
        public int CurrentItem
        { // Индекс выбранной папки или файла 
            get
            {
                return _curr;
            }
            set
            {
                if (value >= 0 && value < Content.Length)
                { // Ограничения для выбранного файла: индекс >= 0 и < количества элементов
                    _curr = value;
                }
                else if (value >= Content.Length)
                {
                    _curr = Content.Length - 1;
                }
                else
                {
                    _curr = 0;
                }
            }
        }

        public enum FMode
        { // Мод для открытия, удаления, переименовывания
            Directory, // Мод для папки
            File //Файл
        }

        public FMode Mode { get; set; }

        public Screen(string path)
        { // Конструктор принимает строку - путь до папки, элементы которой нужно отобразить
            Path = path;
            DirectoryInfo dir = new DirectoryInfo(Path);
            Content = dir.GetFileSystemInfos();
            CurrentItem = 0; // Устанавливает выбранный элемент на 0й
            Mode = FMode.Directory; // По умолчанию FMode = Directory
        }

        public void Display()
        { // Вывод в консоль всех папок и файлов
            Console.Clear(); // Очистка консоли от предыдущих папок и файлов
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Arrow Up - next | Arrow down - previous | Enter - Open | Backspace - Back | R - Rename | Esc - Exit"); // Вывести команды в консоль
            Mode = FMode.Directory;
            for (int i = 0; i < Content.Length; i++)
            {
                if (i == CurrentItem)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Если текущий индекс соответствует выбранному, то выделить цветом
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(i + 1 + ". " + Content[i].Name);
            }
        }
    }

    class Program
    {
        static void OpenItem(FileSystemInfo item, Stack<Screen> screens)
        { // Функция, отвечающая за открытие папок и файлов 
            if (item.GetType() == typeof(DirectoryInfo))
            { // Если открыли папку, то применить методы для DirectoryInfo
                screens.Push(new Screen(item.FullName)); // Добавляем в историю открытую папку
                screens.Peek().Display();
            }
            else
            {
                FileInfo file = new FileInfo(item.FullName);
                screens.Peek().Mode = Screen.FMode.File; // Если открыт был файл - изменить Mode на File
                Console.Clear();
                Console.WriteLine("Arrow Up - next | Arrow down - previous | Enter - Open | Backspace - Back | R - Rename | Esc - Exit");
                using (FileStream fs = file.Open(FileMode.Open, FileAccess.ReadWrite))
                { // Чтение содержимого открытого файла
                    byte[] rawText = new byte[fs.Length]; // Использован FileStream и его методы для вывода.
                    fs.Read(rawText, 0, rawText.Length);
                    string text = Encoding.Default.GetString(rawText);
                    Console.Write(text);
                }
            }
        }

        static void BackTo(Stack<Screen> history)
        { // Функция реализующая назад в историю
            if (history.Peek().Mode == Screen.FMode.Directory)
            { // Если открыта папка, то удалить текущую папку из истории и открыть предыдущую
                history.Pop();
                history.Peek().Display();
            }
            else
            {
                history.Peek().Display(); // Если файл, то открыть родительскую папку
            }
        }

        static void DeleteItem(FileSystemInfo item, Stack<Screen> screens)
        { // Реализция удаления папки
            if (item.GetType() == typeof(DirectoryInfo))
            { // Если папка, то использовать методы для DirectoryInfo
                DirectoryInfo dir = new DirectoryInfo(item.FullName);
                dir.Delete(true); // Удалить папку
                screens.Pop(); // Обновить папку в истории(удалить предыдущую запись из истории и вставить новую)
                screens.Push(new Screen(dir.Parent.FullName) { CurrentItem = screens.Count > 0 ? screens.Peek().CurrentItem-- : 0 });
            }
            else
            {
                FileInfo file = new FileInfo(item.FullName);
                file.Delete(); // Удалить файл
                screens.Pop(); // Обновить файл в истории(удалить предыдущу запись из истории и вставить новую)
                screens.Push(new Screen(file.DirectoryName));
            }
            screens.Peek().Display();
        }

        static void RenameItem(FileSystemInfo item, Stack<Screen> screens)
        { // Реализация переименовывания
            Console.Clear();
            Console.WriteLine("Write new name (Just name, not a path)");
            string newName = Console.ReadLine(); // ввод нового имени
            if (item.GetType() == typeof(DirectoryInfo))
            {
                DirectoryInfo dir = new DirectoryInfo(item.FullName);
                newName = newName.Insert(0, dir.Parent.FullName + "\\"); // Добавить абсолютный путь к введенному имени
                dir.MoveTo(newName); // Переименовывание реализовано как перемещение папки, изменяя имя
                screens.Pop(); // Обновление файла
                screens.Push(new Screen(dir.Parent.FullName)); // Обновление файла
            }
            else
            {
                FileInfo file = new FileInfo(item.FullName);
                newName = newName.Insert(0, file.DirectoryName + "\\"); // Добавить абсолютный путь к введенному имени
                file.MoveTo(newName);
                screens.Pop(); // Обновление файла
                screens.Push(new Screen(file.DirectoryName)); // Обновление файла
            }
            screens.Peek().Display();
        }

        static void Main(string[] args)
        {
            Stack<Screen> screens = new Stack<Screen>();
            screens.Push(new Screen(@"C:\Users\Faizullayev Rauan\Desktop")); // Создание первого экрана(вывод всех папок и файлов)
            screens.Peek().Display();
            ConsoleKeyInfo KeyPress; // Переменная, хранящая имя нажатой клавиши
            do
            { // Ждать нажатия клавиши, пока esc не нажата
                KeyPress = Console.ReadKey();
                switch (KeyPress.Key)
                {
                    case ConsoleKey.DownArrow: // Отслеживание нажатия "вниз"
                        if (screens.Peek().Mode == Screen.FMode.Directory)
                        {
                            screens.Peek().CurrentItem++;
                            screens.Peek().Display();
                        }
                        break;
                    case ConsoleKey.UpArrow: // "вверх"
                        if (screens.Peek().Mode == Screen.FMode.Directory)
                        {
                            screens.Peek().CurrentItem--;
                            screens.Peek().Display();
                        }
                        break;
                    case ConsoleKey.Enter: // "Enter"
                        OpenItem(screens.Peek().Content[screens.Peek().CurrentItem], screens);
                        break;
                    case ConsoleKey.Backspace: // "BackSpace"
                        BackTo(screens);
                        break;
                    case ConsoleKey.Delete: // "Del"
                        DeleteItem(screens.Peek().Content[screens.Peek().CurrentItem], screens);
                        break;
                    case ConsoleKey.R: // "R"
                        RenameItem(screens.Peek().Content[screens.Peek().CurrentItem], screens);
                        break;
                }
            }
            while (KeyPress.Key != ConsoleKey.Escape); // Если нажата esc - выход из консоли
        }
    }
}