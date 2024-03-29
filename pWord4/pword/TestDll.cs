﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myPword.Tests
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to the folder containing DLLs.");
                return;
            }

            string folderPath = args[0];

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"The folder '{folderPath}' does not exist. Please provide a valid folder path.");
                return;
            }

            Console.WriteLine($"Checking DLLs in folder: {folderPath}");

            foreach (string filePath in Directory.GetFiles(folderPath, "*.dll"))
            {
                Console.WriteLine($"Checking: {filePath}");
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(fs))
                        {
                            // Read the PE header location
                            fs.Seek(0x3C, SeekOrigin.Begin);
                            int peHeader = reader.ReadInt32();

                            // Read the machine field from the PE header
                            fs.Seek(peHeader + 4, SeekOrigin.Begin);
                            ushort machine = reader.ReadUInt16();

                            switch (machine)
                            {
                                case 0x8664:
                                    Console.WriteLine("The DLL is 64-bit.");
                                    break;
                                case 0x014c:
                                    Console.WriteLine("The DLL is 32-bit.");
                                    break;
                                default:
                                    Console.WriteLine("The DLL is of an unknown architecture.");
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
