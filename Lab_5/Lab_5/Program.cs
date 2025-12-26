
using System.Diagnostics;
using System.IO;

namespace Lab_5
{
    internal class Program
    {
        private static readonly object fileLock = new object();
        private static string? logFilePath;
        private static Stopwatch? globalStopwatch;
        private class ThreadData
        {
            public string? id;
            public int iterations;
            public Stopwatch? stopwatch;
        }

        static ThreadPriority ConvertToPriority(int number)
        {
            switch (number)
            {
                case 0:
                    return ThreadPriority.Normal;
                case 1:
                    return ThreadPriority.AboveNormal;
                case 2:
                    return ThreadPriority.Highest;
                case -1:
                    return ThreadPriority.BelowNormal;
                case -2:
                    return ThreadPriority.Lowest;
                default:
                    return ThreadPriority.Normal;
            }
        }
        static void Main(string[] args)
        {

            // Параметры: число итераций для каждого потока 
            int[] workloads = new int[5];
            workloads[0] = 100000000;
            workloads[1] = 100000000;
            workloads[2] = 100000000;
            workloads[3] = 100000000;
            workloads[4] = 100000000;

            ThreadPriority[] priorities = new ThreadPriority[5];
            for (int i = 0; i < 5; i++)
            {
                priorities[i] = ThreadPriority.Normal;
            }

            // Получение потоков из флагов запуска, объединение в одну строку.
            string priorityString = "";
            bool IsFoundP = false;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-p")
                {
                    if (i + 1 < args.Length)
                    {
                        priorityString = args[i + 1];
                        IsFoundP = true;
                    }
                    break;
                }
            }


            if (IsFoundP)
            {
                string[] nums = priorityString.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < 5; i++)
                {
                    if (i < nums.Length && int.TryParse(nums[i], out int n))
                    {
                        priorities[i] = ConvertToPriority(n);
                    }
                    else
                    {
                        priorities[i] = ThreadPriority.Normal;
                        Console.WriteLine($"Приоритет {i + 1} потока не задан");
                    }
                }
            }
            else
            {
                Console.WriteLine("Приоритеты не заданы");
            }

            Console.WriteLine("Приоритеты потоков:");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"   Поток {i + 1}: {priorities[i]}");
            }
            Console.WriteLine();

            Console.WriteLine("Запуск 5 потоков с вычислительной нагрузкой (x = Math.Cos(x))\n");

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            logFilePath = "log_" + timestamp + ".csv";

            File.WriteAllText(logFilePath, "time_sec,thread_id,iterations_done\n");
            string prioritiesLine = "# thread_priorities: ";
            for (int i = 0; i < 5; i++)
            {
                prioritiesLine += $"thread_{i + 1}={priorities[i]}";
                if (i < 4) prioritiesLine += ", ";
            }
            prioritiesLine += "\n";
            File.AppendAllText(logFilePath, prioritiesLine);
            Console.WriteLine($"Лог-файл: {Path.GetFullPath(logFilePath)}\n");

            globalStopwatch = Stopwatch.StartNew();
            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Начало: {startTime:HH:mm:ss.fff}\n");
            Thread[] threads = new Thread[5];

            for (int i = 0; i < 5; i++)
            {
                ThreadData data_i = new ThreadData();
                data_i.id = (i + 1).ToString();
                data_i.iterations = workloads[i];
                data_i.stopwatch = globalStopwatch;

                Thread t = new Thread(Compute);
                t.Priority = priorities[i];
                threads[i] = t;
                t.Start(data_i);
            }

            // основной поток
            ThreadData data = new ThreadData();
            data.id = "0";
            data.iterations = 100000000;
            data.stopwatch = globalStopwatch;

            Compute(data);

            for (int i = 0; i < 5; i++)
            {
                threads[i].Join();
            }

            DateTime endTime = DateTime.Now;
            TimeSpan delta = endTime - startTime;

            Console.WriteLine($"\nВсе потоки завершили работу. Время выполнения: {delta.TotalSeconds:F3} сек.");
            Console.WriteLine($"Общее время выполнения: {delta.TotalSeconds:F3} сек.");
            Console.WriteLine($"Лог сохранён в: {logFilePath}");
            Console.Read();
        }

        static void Write(string id, int iterations, double time)
        {
            lock (fileLock)
            {
                try
                {
                    File.AppendAllText(logFilePath, $"{time:F3}, {id}, {iterations}\n");
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Не удалось записать в лог: {ex.Message}");
                }
            }
        }

        static void Compute(object obj)
        {
            ThreadData? data = (ThreadData)obj;
            string? threadId = data.id;
            int Iterations = data.iterations;

            Stopwatch sw = data.stopwatch;

            Console.WriteLine("[START] Поток \"" + threadId + "\" НАЧАЛ вычисления.");

            double id = double.Parse(threadId);
            double x = 1 + id * Math.PI;

            double startTimeSec = sw.ElapsedMilliseconds / 1000.0;

            Write(threadId, 0, startTimeSec);

            // Время последней записи в файл (в миллисекундах)

            long lastLogMs = sw.ElapsedMilliseconds;
            int lastLoggedCount = 0;

            for (int cnt = 0; cnt < Iterations; cnt++)
            {
                x = Math.Cos(x);
                long currentMs = sw.ElapsedMilliseconds;
                if (currentMs - lastLogMs >= 3000)
                {
                    double time = currentMs / 1000.0;
                    Write(threadId, cnt, time);
                    lastLogMs = currentMs;
                    lastLoggedCount = cnt;
                }
            }

            long finalMs = sw.ElapsedMilliseconds;
            double endTimeSec = finalMs / 1000.0;

            if (lastLoggedCount < Iterations)
            {
                Write(threadId, Iterations, endTimeSec);
            }

            Console.WriteLine("[END] Поток \"" + data.id + "\" ЗАВЕРШИЛ " +
                data.iterations.ToString("N0") + " итераций. x = " + x.ToString("F6"));
        }
    }
}