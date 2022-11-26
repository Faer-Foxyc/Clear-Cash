using System;
using static System.Console;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Clear_Cash
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Kill_Processes();                                // Убиваем процессы
            WriteLine("_________________________________________________________________________________");
            Get_Dirrectory_User_And_Delete_Sub_Dir();       // Удаляем папки
            Console.WriteLine("Cash is empty, start program 1c ");
            Thread.Sleep(3000); 
            StartProgramm();                               // Запускаем 1с
        }

        /// <summary>
        /// Процедура для отображения и закрытия процессов 1с 
        /// </summary>
        private static void Kill_Processes()
        {
            Process[] process;
            process = Process.GetProcesses();
            foreach (Process proces in process)
            {
                string NameProcess = Convert.ToString(proces); // Конвертируем процесс в строку, что бы выполнить сравнение

                if (NameProcess == "System.Diagnostics.Process (1cv8)")
                {
                    WriteLine("_________________________________________________________________________________");
                    WriteLine();
                    WriteLine("Закрываем процесс {0}..... ", proces);
                    proces.Kill();
                    Thread.Sleep(1000);
                }
            }
        }

        private static void Get_Dirrectory_User_And_Delete_Sub_Dir()
        {
            string User_dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            User_dir = Path.GetDirectoryName(User_dir);
            User_dir += @"\Local\1C\1cv8\";   // Получаем пользовательскуб дирректорию

            WriteLine();
            WriteLine("                             Удаляем папки хранящие кэшь");

            if (Directory.Exists(User_dir))
            {
                WriteLine("_________________________________________________________________________________");
                WriteLine();

                string[] Dirs = Directory.GetDirectories(User_dir); // Выводим папки верхнего уровня
                foreach (string Dir in Dirs)
                {
                    WriteLine($"Parrent: {Dir}");
                    Console.WriteLine("_________________________________________________________________________________");
                    Console.WriteLine();

                    string[] Sub_Dirs = Directory.GetDirectories(Dir); // Выводим подпаки
                    foreach (string sub_Dir in Sub_Dirs)
                    {
                        if (Directory.Exists(sub_Dir))
                        {
                            Console.Write("     Delete ");
                            Console.WriteLine(sub_Dir);
                            Directory.Delete(sub_Dir, true);
                        }
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine("_________________________________________________________________________________");
                    Console.WriteLine();
                    Thread.Sleep(1000);
                }
            }
        }
        
        static void StartProgramm()
        {
            Process.Start(@"C:\Program Files (x86)\1cv8\common\1cestart.exe");
        }
    }
}