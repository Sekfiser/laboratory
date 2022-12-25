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
            int N;
            if (args.Length > 0)
            {
                N = Convert.ToInt32(args[0]);
            }
            else
            {
                N = 100000;
            }
            Random rnd = new Random();
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                int rank = comm.Rank;
                int size = comm.Size;
                Console.WriteLine("{0} connected", rank);
                comm.Barrier();
                if (rank == 0)
                {
                    int[] array = new int[N];
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = rnd.Next(100, 1000001);
                    }
                    for (int i = 1; i < size; i++) { comm.Send(N, i, 7); }
                    for (int i = 1; i < size; i++) { comm.Send(array, i, 3); }
                    int max = calculate(array, rank, size);
                    for (int i = 1; i < size; i++)
                    {
                        int pmax = 0;
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
                else
                {
                    int N1 = 0;
                    comm.Receive(0, 7, out N1);
                    int[] array = new int[N1];
                    comm.Receive(0, 3, ref array);
                    int psum = calculate(array, comm.Rank, comm.Size);
                    Console.WriteLine("Pmax: {0} rank {1}", psum, comm.Rank);
                    comm.Send(psum, 0, 4);
                }
            }
            Console.ReadLine();
        }
    }
}