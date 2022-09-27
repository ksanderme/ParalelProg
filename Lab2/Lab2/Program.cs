using System.Diagnostics;



int[] nubmerThreads = new int[] { 1, 2, 4, 10 };
Random rnd = new Random();
int A = rnd.Next(2000, 3001);
int[,] arr = new int[A, A];

for (int i = 0; i < A; i++)
{
    for (int j = 0; j < A; j++)
    {
        arr[i, j] = rnd.Next(2000, 3001);
    }
}
foreach(int thread_thread_name in nubmerThreads)
{
    CreateThread(thread_thread_name);
}

void CreateThread(int thread_name)
{
        var timer = new Stopwatch();
        int cols_thread = A - ((int)(A / thread_name) * thread_name);//сдвиг по столбцам
        int start_i = 0;
        int start_j = 0;
    timer.Start();
        for (int i = 0; i < thread_name; i++)
        {
            int end_i = start_i + (int)(A / thread_name) + cols_thread;//последний индекс отрезка
            int end_j = start_j + (int)(A / thread_name) + cols_thread;
            int temp_i = start_i;
            int temp_j = start_j;
            int temp_end_i = end_i;
            int temp_end_j = end_j;
            Thread thread = new Thread(() => Transposition(new[] { temp_i, temp_j }, new[] { temp_end_i, temp_end_j }));
            thread.Start();
            thread.Join();
            cols_thread = 0;
            start_i = end_i;
            start_j = end_j;
        }
    timer.Stop();
    TimeSpan ts = timer.Elapsed;
    Console.WriteLine($"Thread: {thread_name}  Time: {ts.ToString(@"ss\.ffff")}\n");
}


void Transposition(int[] start, int[] end)
{
    int tmp;

    for (int i = start[0]; i < end[0]; i++)
    {
        for (int j = start[1]; j < end[1]; j++)
        {
            tmp = arr[i, j];
            arr[i, j] = arr[j, i];
            arr[j, i] = tmp;
        }
    }
}