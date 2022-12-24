using System.Diagnostics;
using System.Numerics;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Lab1
{
    public class TaskExecution
    {

        private int[]? randomArray;
        private int elementCount;
        private int maxTheadCount = Environment.ProcessorCount;
        private int threadCount;
        private BigInteger[]? returns;

        public TaskExecution()
        {
            string? input;
            do
            {
                Console.Write("Введите число элементов в массиве в диапозоне от 100000 до 1000000:");

                input = Console.ReadLine();

                if (input.Equals("exit"))
                {
                    Console.WriteLine("Выход из программы!");
                    Environment.Exit(404);
                }
                if (Int32.TryParse(input, out elementCount))
                {
                    if (elementCount >= 100000 && elementCount <= 1000000)
                    {
                        InitializeArray();
                    }
                    else
                    {
                        Console.WriteLine("Число элементов должно быть в диапозоне от 100000 до 1000000");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                }
            } while (randomArray?.Length != elementCount);

            do
            {
                Console.Write($"Введите число потоков в диапозоне от 1 до {maxTheadCount}:");
                input = Console.ReadLine();

                if (input.Equals("exit"))
                {
                    Console.WriteLine("Выход из программы!");
                    Environment.Exit(404);
                }
                if (Int32.TryParse(input, out threadCount))
                {
                    if (threadCount >= 1 && threadCount <= maxTheadCount)
                    {
                    }
                    else
                    {
                        Console.WriteLine($"Число потоков должно быть в диапозоне от 1 до {maxTheadCount}");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                }
            } while (threadCount <= 0 || threadCount > maxTheadCount);
        }

        public BigInteger SingleThread()
        {
            Console.WriteLine("Начало выполнения в однопоточном режиме!");

            Stopwatch stopwatch = new Stopwatch();

            BigInteger result = 1;

            stopwatch.Start();

            foreach (var item in randomArray)
            {
                result *= item;
            }

            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }

        public BigInteger MultipleThreads()
        {
            Console.WriteLine("Начало выполнения в многопоточном режиме!");
            returns = new BigInteger[threadCount];

            int remainder = randomArray.Length % threadCount;

            int chunkSize = randomArray.Length / threadCount + remainder;

            var preparedArrays = randomArray.Chunk(chunkSize);

            Stopwatch stopwatch = new Stopwatch();

            var threads = new Thread[threadCount];

            BigInteger result = 1;
            stopwatch.Start();

            foreach (var (preparedArray, i) in preparedArrays.Select((preparedArray, i) => (preparedArray, i)))
            {
                threads[i] = new Thread(() => { returns[i] = MultiplyArray(preparedArray); });
                threads[i].Start();

            }

            for (int i = 0; i < threadCount; i++)
            {
                threads[i].Join();
                result *= returns[i];
            }

            stopwatch.Stop();
            Console.WriteLine($"Время выполнения в многопоточном режиме: {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }

        private BigInteger MultiplyArray(int[] chunk)
        {
            BigInteger result = 1;

            foreach (var item in chunk)
            {
                result *= item;
            }

            return result;
        }

        private void InitializeArray()
        {
            var rand = new Random();
            randomArray = new Int32[elementCount];

            for (var i = 0; i < elementCount; i++)
            {
                randomArray[i] = rand.Next(100, 10000001);
            };

        }
    }
}
