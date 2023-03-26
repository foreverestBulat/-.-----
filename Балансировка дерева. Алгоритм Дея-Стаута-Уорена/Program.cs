using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Globalization;
//using Балансировка_дерева._Алгоритм_Дея_Стаута_Уорена;
using System.Xml.Linq;
using Balancingthetree;

namespace Balancingthetree
{
    public class Program
    {
        public static int[][] Data;


        // Создание данных: 50 наборов
        // от меньшего к большему
        // первый набор 100000 элементов
        // последний набор 1000000 элементов
        // записывается в Data.txt
        private void CreateData()
        {
            var r = new Random();

            var m = new int[50][];

            var k = 100000;

            for (int i = 0; i < 50; i++)
            {
                var mas = new int[k];


                for (int j = 0; j < k; j++)
                {
                    mas[j] = r.Next(1, 1000000);
                }
                k += 18000;

                m[i] = mas;
            }

            var path = "Data.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
            {

                for (int i = 0; i < 50; i++)
                {
                    var text = new StringBuilder();
                    for (int j = 0; j < m[i].Length; j++)
                    {
                        text.Append($"{m[i][j]} ");
                    }

                    writer.WriteLine(text.ToString());
                }
            }
        }


        // Чтение наборов из Data.txt
        // Запись в переменную Data
        private void ReadData(string path = "Data.txt")
        {
            Data = new int[50][];

            using (StreamReader reader = new StreamReader(path))
            {
                for (int i = 0; i < 50; i++)
                {
                    string[] line = reader.ReadLine().Split();
                    int[] ints = new int[line.Length];
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j] == "") break;
                        ints[j] = int.Parse(line[j]);
                    }

                    Data[i] = ints;
                }
            }
        }

        //Запись результатов для графика на время в Result.txt
        private void CountOfElementsAtTime()
        {
            ReadData();

            using (StreamWriter st = new StreamWriter("Result.txt"))
            {
                for (int i = 0; i < 50; i++)
                {
                    var tree = new Tree<int>(Data[i]);

                    Stopwatch sw = new Stopwatch();

                    sw.Start();

                    tree.Rebalance();

                    sw.Stop();

                    var str = $"{sw.ElapsedMilliseconds}\t{Data[i].Length}";

                    st.WriteLine(str);
                }
            }
        }


        // Запись результатов для графика итераций в Result2.txt
        private void CountOfElementsAtIterations()
        {
            ReadData();

            using (StreamWriter st = new StreamWriter("Result2.txt"))
            {
                for (int i = 0; i < 50; i++)
                {
                    var tree = new Tree<int>(Data[i]);

                    tree.Rebalance();

                    var str = $"{tree.iterations}\t{Data[i].Length}";

                    st.WriteLine(str);
                }
            }
        }

        public static void Main()
        {
            // Здесь можете проверять методы
        }
    }
}


//var a = new Tree<int>();

//a.Add(9);
//a.Add(7);
//a.Add(11);
//a.Add(2);
//a.Add(2);
//a.Add(8);
//a.Add(10);
//a.Add(25);
//a.Add(23);
//a.Add(31);
//a.Add(15);
//a.Add(24);
//a.Add(13);
//a.Add(17);

//a.Rebalance();


//using (StreamWriter st = new StreamWriter("iters.txt", false))
//{
//    for (int i = 0; i < 50; i++)
//    {
//        var tree = new Tree<int>(Data[i]);

//        Stopwatch stopwatch = new Stopwatch();

//        stopwatch.Start();

//        tree.Rebalance();

//        stopwatch.Stop();

//        var time = stopwatch.ElapsedMilliseconds;

//        var str = $"{time}\t{Data[i].Length}";


//        st.WriteLine(str);
//    }
//}