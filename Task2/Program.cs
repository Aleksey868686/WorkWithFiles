namespace Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Папка со всеми вложениями занимает: {SizeFolder(GetPath())} байт");
        }
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
    }
}