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
            SaveFunc(new Function(Fun),"data.bin", -100, 100, 0.5);
            Console.WriteLine(Load("data.bin"));
            Console.ReadKey();
        }
    }
}
