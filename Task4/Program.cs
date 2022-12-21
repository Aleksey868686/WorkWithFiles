using System.Runtime.Serialization.Formatters.Binary;

namespace Task4

{
    internal class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"C:\Users\avvakumov\Desktop\Students";
            string binaryFilePath = GetPath();

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                // Провериям существует ли директория
                if (!directoryInfo.Exists)
                {
                    // Создаем директорию
                    directoryInfo.Create();
                    Console.WriteLine("Directory created successfully.");
                }
                else
                {
                    Console.WriteLine("Directory already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating directory: " + ex.Message);
            }
            ReadValues(binaryFilePath);

        }
        [Serializable]
        class Student
        {
            public string Name { get; set; }
            public string Group { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
        //Реализован метод для получения пути до целевой папки
        static string GetPath()
        {
            string? path = string.Empty;
            try
            {
                do
                {
                    Console.WriteLine("Введите корректный путь(или проверьте права доступа) к бинарному файлу в формате (C:\\xxx\\yyy...)");
                    path = Console.ReadLine().Trim();

                } while (!File.Exists(path));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return path;
        }
        static void ReadValues(string path)
        {
            try
            {
                //var fileInfo = new FileInfo(path);


                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    var students = (Student[])formatter.Deserialize(fs);
                    Console.WriteLine("Объект десериализован");
                    foreach (var student in students)
                    {
                        Console.WriteLine(student.Name);
                        Console.WriteLine(student.Group);
                        Console.WriteLine(student.DateOfBirth);
                    }
                    //fs.Close();
                }

                Console.ReadLine();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}