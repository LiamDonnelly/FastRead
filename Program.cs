using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;


namespace FastRead
{
    class Program
    {
        static void Main(string[] args)
        {
            const string fileName = @"D:\Projects\Contest\testfile.sue";
            double[,] ArrayData = new double[999999, 9];
            Random rnd = new Random();
            var stopwatch = new Stopwatch();
            
            //Creating random data for the test
            for (int i = 0; i < ArrayData.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < ArrayData.GetUpperBound(1) + 1; j++)
                {
                    ArrayData[i, j] = rnd.Next(0, 10000000) / 10000000d;
                }
            }

            //writing fast
            stopwatch.Start();
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                for (int i = 0; i < ArrayData.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < ArrayData.GetUpperBound(1) + 1; j++)
                    {
                        writer.Write(ArrayData[i, j]);
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine("Written in: " + stopwatch.ElapsedMilliseconds + " Milliseconds");

            //Reset Stopwatch
            stopwatch.Reset();
            stopwatch.Start();

            //reading fast
            double[,] DataFromFile = new double[ArrayData.GetUpperBound(0) + 1, ArrayData.GetUpperBound(1) + 1];

            byte[] fileContents;
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                fileContents = reader.ReadBytes(((ArrayData.GetUpperBound(0) + 1) * (ArrayData.GetUpperBound(1) + 1)) * sizeof(double));
            }
            
            Buffer.BlockCopy(fileContents, 0, DataFromFile, 0, fileContents.Length);
            
            stopwatch.Stop();
            Console.WriteLine("Read in: " + stopwatch.ElapsedMilliseconds + " Milliseconds");
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
            //Pizza
        }
        


    }
}
