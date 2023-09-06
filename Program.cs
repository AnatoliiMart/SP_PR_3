using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SP_PR_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ex1();
            Console.ReadLine();
            Console.Clear();
            Ex2();
            Console.ReadLine();
            Console.Clear();
            int res = Ex2_mod(1, 100).Result;
            Console.WriteLine($"Amount of prime numbers in selected range: {res}");
            Console.ReadLine();
            Console.Clear();
            Ex4();
            Console.ReadLine();
            Console.Clear();
            Ex5(8);
            Console.ReadLine();
            Console.Clear();

        }

        static void DateTimeShow(object str)
        {
            Console.WriteLine((string)str);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(DateTime.Now);
                Thread.Sleep(1000);
            }
            Console.Clear();
        }
        static bool IsPrimeNumber(int n)
        {
            var result = true;

            if (n > 1)
            {
                for (var i = 2u; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
                result = false;

            return result;
        }
        static void Ex1()
        {
            Task task1 = Task.Run(() =>
            {
                Console.WriteLine("Task.Run method");

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(DateTime.Now);
                    Thread.Sleep(1000);
                }
                Console.Clear();
            });
            task1.Wait();
            Task task2 = Task.Factory.StartNew(DateTimeShow, "Task.Factory.StartNew method");
            task2.Wait();
            Task task3 = new Task(DateTimeShow, "Task.Start method");
            task3.Start();
            task3.Wait();
            //Task.WaitAll(task1, task2, task3);
        }
        static void Ex2()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (IsPrimeNumber(i))
                    {
                        Console.WriteLine(i);
                    }
                }
            });
            Console.Read();
        }

        static async Task<int> Ex2_mod(object min, object max)
        {
         return
                await Task.Run(() =>
            {
                int count = 0;
                for (int i = (int)min; i <= (int)max; i++)
                    if (IsPrimeNumber(i))
                        count++;
                return count;
            });
        }

        static void Ex4()
        {
            double[] arr = { 1.1, 5.3, 0.9, 12.3, 7.7, 32.1 };

            Task<double>[] tasks = new Task<Double>[4];

            tasks[0] = Task<double>.Factory.StartNew(FindMin, arr);
            Console.WriteLine(tasks[0].Result);

            tasks[1] = Task<double>.Factory.StartNew(FindMax, arr);
            Console.WriteLine(tasks[1].Result);

            tasks[2] = Task<double>.Factory.StartNew(FindAverage, arr);
            Console.WriteLine(tasks[2].Result);

            tasks[3] = Task<double>.Factory.StartNew(FindSum, arr);
            Console.WriteLine(tasks[3].Result);

            Task.WaitAll(tasks);
        }
        static double FindMin(object numbers)
        {
            Console.WriteLine("Min elem ");
            double[] doubles = (double[])numbers;
            return doubles.Min();
        }
        static double FindMax(object numbers)
        {
            Console.WriteLine("Max elem ");
            double[] doubles = (double[])numbers;
            return doubles.Max();
        }
        static double FindAverage(object numbers)
        {
            Console.WriteLine("Average of elems ");
            double[] doubles = (double[])numbers;
            return doubles.Average();
        }
        static double FindSum(object numbers)
        {
            Console.WriteLine("Sum of elems ");
            double[] doubles = (double[])numbers;
            return doubles.Sum();
        }

        static void Ex5(int searchVal)
        {
            int[] numbers = { 5, 2, 9, 2, 4, 5, 8, 7, 4, 1 };

            Console.WriteLine("Array before manipulations:");
            PrintArray(numbers);

            Task.Run(() => RemoveDuplicates(numbers))
                .ContinueWith(x => SortArray(x.Result))
                .ContinueWith(x => BinarySearch(x.Result, searchVal))
                .ContinueWith(x =>
                {
                    int index = x.Result;
                    if (index >= 0)
                        Console.WriteLine($"Value {searchVal} was founded on position: {index + 1}");
                    else
                        Console.WriteLine($"Value {searchVal} not found");
                });

            Console.ReadLine();
        }

        static int[] RemoveDuplicates(int[] array)
        {
            int[] uniqueArray = array.Distinct().ToArray();
            Console.WriteLine("The array after removing duplicates:");
            PrintArray(uniqueArray);
            return uniqueArray;
        }

        static int[] SortArray(int[] array)
        {
            Array.Sort(array);
            Console.WriteLine("Sorted array:");
            PrintArray(array);
            return array;
        }

        static int BinarySearch(int[] array, int value)
        {
            int index = Array.BinarySearch(array, value);
            return index;
        }

        static void PrintArray(int[] array)
        {
            foreach (var num in array)
                Console.Write(num + " ");      
            Console.WriteLine();
        }
    }

}


