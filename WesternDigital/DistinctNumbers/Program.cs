using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DistinctNumbers
{
    /* Write a program to process a large data set containing 1 billion numbers and find 
     * distinct numbers in the file.
     * Constraint 1 : You can only intialize an array of 1 million boolean values.
     */
    public class Program
	{
        public static void CreateDataSet(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (StreamWriter sw = File.CreateText(fileName))
            {
                for (int i = 0; i < 1000000000; i++)
                {
                    sw.WriteLine(i);
                }
                sw.Close();
            }
        }

        public static void Main(String[] args)
		{
            string fileName = @"Numbers.txt";

            CreateDataSet(fileName);

            int uniqueNumbers = 0;

            Parallel.For<int>(1, 1001, () => 0, (i, loopState, count) =>
            {
                int SIZE = 1000000; //1 MILLION
                
                bool[] array = new bool[SIZE];
                
                int start = (i - 1) * SIZE;
                
                int end = i * SIZE - 1;

                using (var stream = new StreamReader(fileName))
                {
                    string ln;
                    while ((ln = stream.ReadLine()) != null)
                    {
                        int number = Convert.ToInt32(ln);
                        if (number >= start && number <= end)
                        {
                            int index = number % SIZE;
                            if (!array[index])
                            {
                                array[index] = true;
                                count++;
                            }

                        }
                    }
                    stream.Close();
                }
                return count;
            },
                (x) => Interlocked.Add(ref uniqueNumbers, x)
            );

            Console.WriteLine(uniqueNumbers);
		}
	}
}

