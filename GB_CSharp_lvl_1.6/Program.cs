using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*  1)  Изменить программу вывода таблицы функции так, чтобы можно было передавать функции типа double (double, double).
        * Продемонстрировать работу на функции с функцией a* x^2 и функцией a* sin(x).
    2)  Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата.
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

namespace GB_CSharp_lvl_1._6
{
    public delegate double Fun(double x, double a);

    class Program
    {
        /// <summary>
        /// Метод строит таблицу функции, используя метод расчета, полученный через делегат.
        /// </summary>
        /// <param name="F">Делегат метода расчета функции</param>
        /// <param name="x"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public static void Table(Fun F, double x, double b, double a)
        {
            Console.WriteLine("\t----- X ------- Y -----");
            while (x <= b)
            {
                Console.WriteLine($"\t| {x,8:0.000} | {F(x, a),8:0.000} |");
                x += 1;
            }
            Console.WriteLine("\t-----------------------");
        }

        /// <summary>
        /// Метод расчета функции a * x^2
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double MyFunc1(double x, double a)
        {
            return a * x * x;
        }

        /// <summary>
        /// Метод расчета функции a * sin(x)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double MyFunc2(double x, double a)
        {
            return a * Math.Sin(x);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Таблица функции MyFunc1 (a * x^2):");
            Table(new Fun(MyFunc1), -2, 2, 3);
            Console.WriteLine();

            Console.WriteLine("Таблица функции MyFunc2 (a * sin(x)):");
            Table(new Fun(MyFunc2), -2, 2, 3);
            Console.ReadLine();
        }
    }
}
