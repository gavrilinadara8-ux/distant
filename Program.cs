using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static SemaphoreSlim comps = new SemaphoreSlim(3);

    static async Task Main()
    {
        Console.WriteLine("Очередь к 3 компьютерам\n");

        Console.Write("Сколько студентов? ");
        int n = int.Parse(Console.ReadLine() ?? "10");

        for (int i = 1; i <= n; i++)
        {
            int id = i;
            _ = Task.Run(() => Student(id));
            await Task.Delay(200);
        }

        await Task.Delay(5000);
        Console.WriteLine("\nВсе студенты закончили!");
    }

    static async Task Student(int id)
    {
        Console.WriteLine($"Студент {id} ждёт...");
        await comps.WaitAsync();

        Console.WriteLine($"Студент {id} сел за комп");
        await Task.Delay(1500);

        Console.WriteLine($"Студент {id} закончил");
        comps.Release(); 
    }
}
