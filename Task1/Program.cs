namespace Task1
{
    using System;
    using System.IO;
    internal class Program
    {
        static void Main(string[] args)
        {
            DeleteContentFromFolder();
        }
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
        static void DeleteContentFromFolder()
        {
            try
            {
                string? path = string.Empty;
                do
                {
                    Console.WriteLine("Введите корректный путь(или проверьте права доступа) к папке в формате (C:\\xxx\\yyy...)");
                    path = Console.ReadLine().Trim();

                } while (!Directory.Exists(path));
                //Удаляем директории с вложениями
                DirectoryInfo pathDir = new DirectoryInfo(path);
                foreach (var dir in pathDir.GetDirectories())
                {
                    if ((DateTime.Now - dir.LastAccessTime) > TimeSpan.FromMinutes(30))
                    {
                        dir.Delete(true);
                    }
                    else
                    {
                        Console.WriteLine($"Последнее время доступа к директории {dir.Name} менее 30 мин! Оно было: {dir.LastAccessTime}");
                    }
                }
                //Удаляем все файлы в целевой директории
                foreach (FileInfo file in pathDir.GetFiles())
                {
                    if ((DateTime.Now - file.LastAccessTime) > TimeSpan.FromMinutes(30))
                    {
                        file.Delete();
                    }
                    else
                    {
                        Console.WriteLine($"Последнее время доступа к файлу {file.Name} менее 30 мин! Оно было: {file.LastAccessTime}");
                    }
                }
                Console.WriteLine("Файлы и папки, которые не использовались более 30 мин. были удалены");

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

}