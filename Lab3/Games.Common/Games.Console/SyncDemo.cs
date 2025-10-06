using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Threading;
using System.Threading.Tasks;

// щоб уникнути колізії з namespace Games.Console
using Console = System.Console;

namespace Games.ConsoleApp
{
    internal static class SyncDemo
    {
        private static readonly object _lock = new object();
        private static readonly Semaphore _semaphore = new Semaphore(2, 2); // дозволяє 2 потоки одночасно
        private static readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(true);

        public static void Run()
        {
            Console.WriteLine("=== Демонстрація lock ===");
            Thread t1 = new Thread(PrintWithLock);
            Thread t2 = new Thread(PrintWithLock);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            Console.WriteLine("\n=== Демонстрація Semaphore ===");
            for (int i = 0; i < 5; i++)
            {
                int id = i;
                new Thread(() => AccessResource(id)).Start();
            }

            Thread.Sleep(2000);

            Console.WriteLine("\n=== Демонстрація AutoResetEvent ===");
            Thread signalThread = new Thread(SignalAfterDelay);
            Thread waitThread = new Thread(WaitForSignal);
            waitThread.Start();
            signalThread.Start();

            signalThread.Join();
            waitThread.Join();
        }

        private static void PrintWithLock()
        {
            lock (_lock)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Потік {Thread.CurrentThread.ManagedThreadId}: {i}");
                    Thread.Sleep(100);
                }
            }
        }

        private static void AccessResource(int id)
        {
            Console.WriteLine($"Потік {id} чекає доступ...");
            _semaphore.WaitOne(); // блокування, якщо вже 2 потоки всередині
            Console.WriteLine($"Потік {id} увійшов!");
            Thread.Sleep(500);
            Console.WriteLine($"Потік {id} виходить.");
            _semaphore.Release();
        }

        private static void WaitForSignal()
        {
            Console.WriteLine("Очікування сигналу...");
            _autoResetEvent.WaitOne(); // блокує потік до сигналу
            Console.WriteLine("Отримано сигнал!");
        }

        private static void SignalAfterDelay()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Надсилаю сигнал...");
            _autoResetEvent.Set(); // розблоковує один потік
        }
    }
}