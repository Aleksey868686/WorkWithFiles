using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @"C:\Users\avvakumov\Desktop\Students";
            string binaryFilePath = GetPath();
            CreateDirectory(directoryPath);
            Student[] values = ReadValues(binaryFilePath);
            WriteValues(values);

        }
        //Создаем директорию Students
        static void CreateDirectory(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

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
        //Реализован метод для десериализацми бинарного файла
        static Student[] ReadValues(string path)
        {
            Student[] students;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    students = (Student[])formatter.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Array.Empty<Student>();
            }
            return students;
        }
        //Реализован метод для создания и записи файлов
        static void WriteValues(Student[] array)
        {
            var studentsByGroup = new Dictionary<string, List<Student>>();

            foreach (var student in array)
            {
                if (!studentsByGroup.ContainsKey(student.Group))
                {
                    studentsByGroup[student.Group] = new List<Student>();
                }

                studentsByGroup[student.Group].Add(student);

            }
            foreach (var group in studentsByGroup)
            {
                using (StreamWriter writer = new StreamWriter($"C:\\Users\\avvakumov\\Desktop\\Students\\{group.Key}.txt"))
                {
                    foreach (var student in group.Value)
                    {
                        writer.Write(student.Name);
                        writer.WriteLine($" {student.DateOfBirth.ToString("yyyy-MM-dd")}");
                    }
                }
            }
        }
    }
}