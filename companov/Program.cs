// Паттерн «Компоновщик» (Composite)
//
// Идея паттерна:
// отдельные объекты и группы объектов имеют общий интерфейс.
// Поэтому с файлом и папкой можно работать одинаково.

// Общий компонент для всех элементов файловой системы.
// И файл, и папка умеют выводить себя на экран.
interface IFileSystemItem
{
    void Show(int indent = 0);
}

// Лист — отдельный объект, у которого нет вложенных элементов.
class File : IFileSystemItem
{
    private readonly string _name;

    public File(string name)
    {
        _name = name;
    }

    public void Show(int indent = 0)
    {
        // Отступ показывает уровень вложенности файла.
        Console.WriteLine($"{new string(' ', indent)}- Файл: {_name}");
    }
}

// Компоновщик — объект, который может содержать файлы и другие папки.
class Folder : IFileSystemItem
{
    private readonly string _name;
    private readonly List<IFileSystemItem> _items = new();

    public Folder(string name)
    {
        _name = name;
    }

    public void Add(IFileSystemItem item)
    {
        _items.Add(item);
    }

    public void Remove(IFileSystemItem item)
    {
        _items.Remove(item);
    }

    public void Show(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}+ Папка: {_name}");

        // Для каждого вложенного элемента вызывается один и тот же метод Show().
        // Нам не нужно проверять, файл это или папка.
        foreach (IFileSystemItem item in _items)
        {
            item.Show(indent + 4);
        }
    }
}

class Program
{
    static void Main()
    {
        // Создаём отдельные файлы.
        File document = new("document.txt");
        File photo = new("photo.jpg");
        File program = new("program.cs");

        // Создаём папки.
        Folder documents = new("Документы");
        Folder pictures = new("Изображения");
        Folder pictures2 = new("Изображения 2");
        Folder root = new("Мой компьютер");

        // Добавляем файлы в папки.
        documents.Add(document);
        pictures.Add(photo);
        pictures.Add(pictures2);
        pictures2.Add(document);
        // Добавляем файл и папки в корневую папку.
        root.Add(program);
        root.Add(documents);
        root.Add(pictures);

        // Клиентский код работает с корневой папкой через общий интерфейс.
        // Папка сама выведет себя и все вложенные элементы.
        root.Show();
        Console.WriteLine();
        pictures.Show();
    }
}
