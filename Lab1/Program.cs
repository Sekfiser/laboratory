namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Для выхода из программы ведите exit");
            var Task = new TaskExecution();

            var result_mt = Task.MultipleThreads();
            FileWrite(result_mt, "multiple_treads");

            var result_st = Task.SingleThread();
            FileWrite(result_st, "single_tread");

            Console.WriteLine("Конец программы!");
            Console.ReadKey();
        }

        static void FileWrite(Object input, string fileName)
        {
            Console.WriteLine($"Результат записывается в файл {fileName}.txt...");
            File.WriteAllText($"{fileName}.txt", input.ToString());
            Console.WriteLine($"Результат записан в файл {fileName}.txt.");
        }
    }
}