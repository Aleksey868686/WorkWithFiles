using System.Runtime.Serialization.Formatters.Binary;

class FileTest
{
    const string SettingsFileName = "C:\\Users\\avvakumov\\Desktop\\BinaryFile.bin";
    public static void Main()
    {
        //var fileInfo = new FileInfo(@"C:\SkillFactory\module3\WorkWithFiles\FileTest\Program.cs");
        //using (StreamWriter sw = fileInfo.AppendText())
        //{
        //    sw.WriteLine($"// Время запуска: {DateTime.Now}");
        //}

        //using (StreamReader sr = fileInfo.OpenText())
        //{
        //    string str = "";
        //    while ((str = sr.ReadLine()) != null)
        //        Console.WriteLine(str);

        //}
        var contact = new Contact("Sergey", 89295464567, "sergey65@mail.ru");
        Console.WriteLine("Объект создан");

        BinaryFormatter formatter = new BinaryFormatter();
        // получаем поток, куда будем записывать сериализованный объект
        using (var fs = new FileStream("myContats.dat", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, contact);
            Console.WriteLine("Объект сериализован");
        }
        // десериализация
        using (var fs = new FileStream("myContats.dat", FileMode.Open))
        {
            var newContact = (Contact)formatter.Deserialize(fs);
            Console.WriteLine("Объект десериализован");
            Console.WriteLine($"Имя: {newContact.Name} --- Телефон: {newContact.PhoneNumber} --- Email: {newContact.Email}");
        }
        Console.ReadLine();
    }
    static void ReadValues()
    {
        string StringValue;
        if (File.Exists(SettingsFileName))
        {
            // Создаем объект BinaryReader и инициализируем его возвратом метода File.Open.
            using (BinaryReader reader = new BinaryReader(File.Open(SettingsFileName, FileMode.Open)))
            {
                // Применяем специализированные методы Read для считывания соответствующего типа данных.
                StringValue = reader.ReadString();
            }

            Console.WriteLine("Из файла считано:");
            Console.WriteLine("Строка: " + StringValue);
        }
    }
    static void WriteValues()
    {
        // Создаем объект BinaryWriter и указываем, куда будет направлен поток данных
        using (BinaryWriter writer = new BinaryWriter(File.Open(SettingsFileName, FileMode.Open)))
        {
            // записываем данные в разном формате
            writer.Write($"Файл изменен {DateTime.Now} на компьютере с ОС {Environment.OSVersion}");

        }
    }
}
[Serializable]
class Contact
{
    public string Name { get; set; }
    public long PhoneNumber { get; set; }
    public string Email { get; set; }

    public Contact(string name, long phoneNumber, string email)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}
// Время запуска: 16.12.2022 17:52:59
// Время запуска: 16.12.2022 17:53:15
// Время запуска: 18.12.2022 18:30:13

//Файл изменен 02.11 14:53 на компьютере Windows 11
static void GetCatalogs()
{
    string dirName = @"C:\"; // Прописываем путь к корневой директории MacOS (для Windows скорее всего тут будет "C:\\")
    if (Directory.Exists(dirName)) // Проверим, что директория существует
    {
        Console.WriteLine("Папки:");
        string[] dirs = Directory.GetDirectories(dirName);  // Получим все директории корневого каталога

        foreach (string d in dirs) // Выведем их все
            Console.WriteLine(d);

        Console.WriteLine();
        Console.WriteLine("Файлы:");
        string[] files = Directory.GetFiles(dirName);// Получим все файлы корневого каталога

        foreach (string s in files)   // Выведем их все
            Console.WriteLine(s);
    }
    else
    {
        Console.WriteLine("Нет доступа или файл/папка не существуют!");
    }
}
static void CountFilesAndCatalogs()
{
    try
    {
        DirectoryInfo dirInfo = new DirectoryInfo(@"C:\luft");
        if (!dirInfo.Exists)
            dirInfo.Create();

        dirInfo.CreateSubdirectory("NewFolderrrr");

        DirectoryInfo dirInfo2 = new DirectoryInfo(@"C:\");
        if (dirInfo2.Exists)
        {
            Console.WriteLine(dirInfo2.GetDirectories().Length + dirInfo2.GetFiles().Length);
        }
        Console.WriteLine($"Название каталога: {dirInfo.Name}");
        Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
        Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
        Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
static void DeleteCatalog()
{
    try
    {
        DirectoryInfo dirInfo = new DirectoryInfo(@"C:\luft\NewFolderrrr");
        dirInfo.Delete(true); // Удаление со всем содержимым
        Console.WriteLine("Каталог удален");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
static void MoveFolder()
{
    try
    {
        DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\avvakumov\Desktop\testFolder");
        string trashPath = "C:\\$Recycle.Bin";

        //if (dirInfo.Exists && Directory.Exists(trashPath))
        dirInfo.MoveTo(trashPath);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}
        }