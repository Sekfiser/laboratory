using System;
using System.Diagnostics;
using MPI;

namespace Lab3
{
    internal class Program
    {
        public static int calculate(int[] array, int rank, int size)
        {
            int nt, beg, h, end;
            int max = 0;
            nt = rank;
            h = array.Length / size;
            beg = h * nt;
            end = beg + h - 1;
            if (nt == size - 1) { end = array.Length - 1; }
            Console.WriteLine("Process rank {0} - beg: {1} end: {2}", rank, beg, end);
            for (int i = beg; i <= end; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }
        static void Main(string[] args)
        {
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            Random rnd = new Random();
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                int rank = comm.Rank;
                int size = comm.Size;

                Console.WriteLine("{0} connected", rank);
                comm.Barrier();
                if (rank == 0)
                {
                    int N = 100000;
                    int[] array = new int[N];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = rnd.Next(100, 1000001);
                    }
                    for (int i = 1; i < size; i++) 
                    {
                        comm.Send(N, i, 7);
                    }
                    for (int i = 1; i < size; i++) 
                    {
                        comm.Send(array, i, 3); 
                    }
                    int max = calculate(array, rank, size);
                    int pmax = 0;

                    for (int i = 1; i < size; i++)
                    {
                        comm.Receive(i, 4, out pmax);
                        if (pmax > max)
                        {
                            max = pmax;
                        }
                    }

                    Console.WriteLine("Max: {0}", max);
                    sWatch.Stop();
                    Console.WriteLine("Time: {0}", sWatch.ElapsedMilliseconds);
                }
               
            }
            Console.ReadLine();

        }
    }
}