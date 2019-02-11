using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*  2)  Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
        а) Сделать меню с различными функциями и представить пользователю выбор, для какой функции и на каком отрезке находить минимум.Использовать массив(или список) делегатов,
        в котором хранятся различные функции.
        б) * Переделать функцию Load, чтобы она возвращала массив считанных значений.Пусть она возвращает минимум через параметр(с использованием модификатора out). 
    3)  Переделать программу Пример использования коллекций для решения следующих задач:
        а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
        б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(*частотный массив);
        в) отсортировать список по возрасту студента;
        г) * отсортировать список по курсу и возрасту студента;
    4)  ** Считайте файл различными способами.Смотрите “Пример записи файла различными способами”. Создайте методы, которые возвращают массив byte (FileStream, BufferedStream),
        строку для StreamReader и массив int для BinaryReader.

    Александр Кушмилов 
*/

namespace Task_2
{
    delegate double Function(double x);

    class Program
    {
        public static double Fun(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static double FSquare(double x)
        {
            return x * x;
        }

        public static double FSin(double x)
        {
            return x * Math.Sin(x);
        }

        public static double FCos(double x)
        {
            return Math.Cos(x);
        }

        public static void SaveFunc(Function F, string fileName, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x));
                x += h;// x=x+h;
            }
            bw.Close();
            fs.Close();
        }

        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Выберите функцию для расчета минимума");
            Console.WriteLine("1) x * x - 50 * x + 10");
            Console.WriteLine("2) x * x");
            Console.WriteLine("3) x * Sin x");
            Console.WriteLine("4) Cos x");

            Function[] delegates = new Function[4];
            delegates[0] = Fun;
            delegates[1] = FSquare;
            delegates[2] = FSin;
            delegates[3] = FCos;

            int numb = 0;
            bool x;
            do
            {
                Console.WriteLine("Введите номер функции:");
                x = Int32.TryParse(Console.ReadLine(), out numb);
            }
            while (!(x && numb >= 1 && numb <= 4));

            int min = 0;
            int max = 0;

            Console.WriteLine("Введите границы отрезка нахождения минимума");
            Console.Write("От: ");
            Int32.TryParse(Console.ReadLine(), out min);
            Console.Write("До: ");
            Int32.TryParse(Console.ReadLine(), out max);

            SaveFunc(delegates[numb-1],"data.bin", min, max, 0.5);
            Console.WriteLine($"Минимум функции: {Load("data.bin")}");
            Console.ReadKey();
        }
    }
}
