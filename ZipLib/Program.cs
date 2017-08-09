using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip; // Install-Package SharpZipLib -Version 0.86.0 //добавить через диспетчер пакетов - Сервис - > Диспетчер пакетов NuGET
using System.IO;

namespace ZipLib
{


    class Program
    {
        /// <summary>
        /// Функция распаковывает ОДИН архив. Предварительно проверяем есть ли он на диске
        /// </summary>
        /// <param name="ArhchiveName">имя ZIP афайла для распаковки</param>
        static void UnzipMe (String ArhchiveName)
        {
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(ArhchiveName)))
            {

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    Console.WriteLine(theEntry.Name);

                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // создаем папку если нужно
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(theEntry.Name))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            // Проверяем есть ли аргументы для распаковки.
            if (args.Length < 1)
            {
                Console.WriteLine("Использование UnzipFile NameOfFile");
                return;
            }

            //есть ли файл для распаковки
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Не нашли файл !! '{0}'", args[0]);
                return; // нет файла
            }


            /// само использование библиотеки
            UnzipMe(args[0]);

        }
    }


}
