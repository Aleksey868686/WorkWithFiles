namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo path = GetPath();
            long originalFolderSize = SizeFolder(path);
            Console.WriteLine($"Исходный размер папки: {originalFolderSize} байт");
            DeleteContentFromFolder(path);
            long afterDelFolderSize = SizeFolder(path);
            long deltaFolderSize = originalFolderSize - afterDelFolderSize;
            Console.WriteLine($"Освобождено: {deltaFolderSize} байт");
            Console.WriteLine($"Текущий размер папки: {afterDelFolderSize} байт");
        }
        //Реализован метод для получения пути до целевой папки
        static DirectoryInfo GetPath()
        {
            string? path = string.Empty;
            try
            {
                do
                {
                    Console.WriteLine("Введите корректный путь(или проверьте права доступа) к папке в формате (C:\\xxx\\yyy...)");
                    path = Console.ReadLine().Trim();

                } while (!Directory.Exists(path));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new DirectoryInfo(path);
        }
        //Реализован метод для получения размера папки с вложениями
        static long SizeFolder(DirectoryInfo di)
        {
            long size = 0;
            try
            {
                foreach (var file in di.GetFiles())
                {
                    size += file.Length;
                }

                foreach (var dir in di.GetDirectories())
                {
                    size += SizeFolder(dir);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return size;
        }
        //Реализован метод для получения для удаления содержимого папки
        static void DeleteContentFromFolder(DirectoryInfo di)
        {
            try
            {
                //Удаляем директории с вложениями
                foreach (var dir in di.GetDirectories())
                {
                    if ((DateTime.Now - dir.LastAccessTime) > TimeSpan.FromMinutes(30))
                    {
                        dir.Delete(true);
                    }
                }
                //Удаляем все файлы в целевой директории
                foreach (FileInfo file in di.GetFiles())
                {
                    if ((DateTime.Now - file.LastAccessTime) > TimeSpan.FromMinutes(30))
                    {
                        file.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}