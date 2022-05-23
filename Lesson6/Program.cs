using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GBcool;
using System.Collections;

namespace Lesson6
{

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Classes.LogoLesson("6");
                Classes.PrintCenter("█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█", ConsoleColor.Green);
                Classes.PrintCenter("█                       ГЛАВНОЕ МЕНЮ                       █", ConsoleColor.Green);
                Classes.PrintCenter("█▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█", ConsoleColor.Green);
                Console.WriteLine("");
                Classes.PrintLeft("Задача 1: Изменить программу вывода таблицы функции так, чтобы можно", false, ConsoleColor.White);
                Classes.PrintLeft("          было передавать функции типа double(double, double).", false, ConsoleColor.White);
                Console.WriteLine("");
                Classes.PrintLeft("Задача 2: Модифицировать программу нахождения минимума функции так,", false, ConsoleColor.White);
                Classes.PrintLeft("          чтобы можно было передавать функцию в виде делегата", false, ConsoleColor.White); 
                Console.WriteLine("");
                Classes.PrintLeft("Задача 3: Пример использования коллекций", false, ConsoleColor.White);
                Console.WriteLine("");
                Classes.PrintLeft("Задача 5: Выход", false, ConsoleColor.White);
                Console.WriteLine("");
                Classes.PrintLeft("Какую задачу ты хочешь выполнить: ", true, ConsoleColor.Yellow);
                Classes.PrintLeft("", true, ConsoleColor.Green);

                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Task1();
                        Console.WriteLine("");
                        Classes.PrintLeft("Для возврата нажми любую клавишу...", false, ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;

                    case 2:
                        Task2();
                        Console.WriteLine("");
                        Classes.PrintLeft("Для возврата нажми любую клавишу...", false, ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;

                    case 3:
                        Task3();
                        Console.WriteLine("");
                        Classes.PrintLeft("Для возврата нажми любую клавишу...", false, ConsoleColor.Yellow);
                        Console.ReadKey();
                        break;

                    case 5:
                        return;
                }
            }//
        }
        public static void Table(Fun F, double a, double x, double b)
        {
            Classes.PrintLeft("----- A ----- X ----- Y -----", false, ConsoleColor.Yellow);
            while (x <= b)
            {
                Classes.PrintLeft(String.Format("| {0,8:0.000} | {1,8:0.000} | {2,8:0.000} |", a, x, F(a, x)), false, ConsoleColor.Yellow);
                x += 1;
            }
            Classes.PrintLeft("---------------------", false, ConsoleColor.Yellow);
        }
        static void Task1()
        {
            Classes.LogoLesson("6");
            Classes.PrintCenter("█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█", ConsoleColor.White);
            Classes.PrintCenter("█   Изменить программу вывода таблицы                      █", ConsoleColor.White);
            Classes.PrintCenter("█   функции так, чтобы можно было передавать функции       █", ConsoleColor.White);
            Classes.PrintCenter("█   типа double(double, double).                           █", ConsoleColor.White);
            Classes.PrintCenter("█▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█", ConsoleColor.White);
            Console.WriteLine("");

            Classes.PrintLeft("Таблица функции a*x^2:", false, ConsoleColor.White);
            Table(delegate (double a, double x) 
            { 
                return a * Math.Pow(x, 2); 
            }, 3, -2, 2);

            Console.WriteLine("");

            Classes.PrintLeft("Таблица функции a*sin(x):", false, ConsoleColor.White);
            Table(delegate (double a, double x)
            {
                return a * Math.Sin(x);
            }, 3, -2, 2);
        }

        public static double F(double x)
        {
            return x * x - 50 * x + 10;
        }
        public static void SaveFunc(string fileName, Fun2 F, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create,
            FileAccess.Write);
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
        public static double[] Load(string fileName, out double minnum)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double[] array = new double[fs.Length / sizeof(double)];
            double min = double.MaxValue;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                array[i] = bw.ReadDouble();
                if (array[i] < min) min = array[i];
            }
            bw.Close();
            fs.Close();
            minnum = min;
            return array;
        }

        static void Task2()
        {
            Classes.LogoLesson("6");
            Classes.PrintCenter("█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█", ConsoleColor.White);
            Classes.PrintCenter("█   Модифицировать программу нахождения минимума           █", ConsoleColor.White);
            Classes.PrintCenter("█   функции так, чтобы можно было передавать функцию       █", ConsoleColor.White);
            Classes.PrintCenter("█   в виде делегата                                        █", ConsoleColor.White);
            Classes.PrintCenter("█▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█", ConsoleColor.White);
            Console.WriteLine("");
            //придумаем разные функции
            FunArray[] Myfuction = new FunArray[4];
            int number;
            double a, b, h, minnum;
            double[] array;
            Myfuction[0].NameFun = "sin(x)";
            Myfuction[0].MyFunction = delegate (double x) { return Math.Sin(x); };
            Myfuction[1].NameFun = "x * x - 50 * x + 10";
            Myfuction[1].MyFunction = delegate (double x) { return x * x - 50 * x + 10; };
            Myfuction[2].NameFun = "(x*x^2)-x";
            Myfuction[2].MyFunction = delegate (double x) { return (x*Math.Pow(x,2))-x; };
            Myfuction[3].NameFun = "(5*x)/10";
            Myfuction[3].MyFunction = delegate (double x) { return (5 * x)/10; };

            Classes.PrintLeft("Вам доступны следующие функции:", false, ConsoleColor.White);
            for (int i = 0; i < Myfuction.Length; i++)
            {
                Classes.PrintLeft(String.Format($"{i + 1}) Функция {Myfuction[i].NameFun}"), false, ConsoleColor.White);
            }
            Console.WriteLine("");
            Classes.PrintLeft("Какую функцию ты хочешь выбрать: ", true, ConsoleColor.Yellow);
            Classes.PrintLeft("", true, ConsoleColor.Green);
            int.TryParse(Console.ReadLine(), out number);
            Classes.PrintLeft("Укажи начало отрезка, например, -100: ", true, ConsoleColor.Yellow);
            Classes.PrintLeft("", true, ConsoleColor.Green);
            double.TryParse(Console.ReadLine(), out a);
            Classes.PrintLeft("Укажи конец отрезка, например, 100: ", true, ConsoleColor.Yellow);
            Classes.PrintLeft("", true, ConsoleColor.Green);
            double.TryParse(Console.ReadLine(), out b);
            Classes.PrintLeft("Укажи шаг, например, 0,5: ", true, ConsoleColor.Yellow);
            Classes.PrintLeft("", true, ConsoleColor.Green);
            double.TryParse(Console.ReadLine(), out h);
            SaveFunc("data.bin", Myfuction[number-1].MyFunction, a, b, h);
            array = Load("data.bin", out minnum);
            for (int i = 0; i < array.Length; i++)
            {
                if (Console.CursorLeft > 100) Console.WriteLine("");
                Classes.PrintLeft(String.Format($"{array[i]}\t"), true, ConsoleColor.White);
            }
            Console.WriteLine("");
            Classes.PrintLeft("Минимум: " + minnum, false, ConsoleColor.Yellow);
        }

        public class Student : IComparable<Student>
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Kurs { get; set; }

            public int CompareTo(Student other)
            {
                int compare;
                compare = String.Compare(this.Name, other.Name, true);

                return compare;
            }
        }

        static void Task3()
        {
            Classes.LogoLesson("6");
            Classes.PrintCenter("█▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀█", ConsoleColor.White);
            Classes.PrintCenter("█             Пример использования коллекций               █", ConsoleColor.White);
            Classes.PrintCenter("█▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄█", ConsoleColor.White);
            Console.WriteLine("");

            int kurs5_6 = 0; 
            // Создадим необобщенный список
            List<Student> list = new List<Student>();
            Student student = new Student();
            var age18_20 = new SortedDictionary<int,int>();

            StreamReader sr = new StreamReader("students.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    student = new Student();
                    student.Name = s[1] + " " + s[0];
                    student.Kurs = int.Parse(s[6]);
                    student.Age = int.Parse(s[5]);
                    kurs5_6 += (student.Kurs >= 5) && (student.Kurs <= 6) ? 1 : 0;  //сразу считаем количество стундетов на 5 и 6 курсе
                    if (student.Age >= 18 && student.Age <= 20) // сразу считаем количество студентов в возрасте от 18 до 20 на курсах
                    {
                        if (!age18_20.ContainsKey(student.Kurs)) age18_20.Add(student.Kurs, 1);
                        else age18_20[student.Kurs]++;
                    }
                    
                    list.Add(student);                
                }
                catch
                { }
            }
            sr.Close();
            list.Sort(); //сортировка по фамилии и имени                         
            Classes.PrintLeft("Список студентов:", false, ConsoleColor.White);
            foreach (Student s in list)
                Classes.PrintLeft(s.Name + " Возраст: " + s.Age + " Курс: " + s.Kurs, false, ConsoleColor.Yellow);
            Classes.PrintLeft("Всего студентов: "+ list.Count, false, ConsoleColor.Green);
            Classes.PrintLeft("Всего студентов на 5 и 6 курсах: " + kurs5_6, false, ConsoleColor.Green);
            Console.WriteLine("");
            Classes.PrintLeft("Количество студентов в возрасте от 18 до 20 лет на каком курсе учатся:", false, ConsoleColor.White);
            foreach (int i in age18_20.Keys)
                Classes.PrintLeft(String.Format($"На {i} курсе {age18_20[i]} студент{Classes.EndString(age18_20[i])}"), false, ConsoleColor.Green);

            Console.WriteLine("");
            Classes.PrintLeft("Сортировка по возрасту:", false, ConsoleColor.White);
            list.Sort((
                (Student x, Student y) =>
                {
                    if (x.Age < y.Age) return -1;
                    if (x.Age > y.Age) return 1;
                    return 0;
                }));
            foreach (Student s in list)
                Classes.PrintLeft(s.Name + " Возраст: " + s.Age + " Курс: " + s.Kurs, false, ConsoleColor.Yellow);

            Console.WriteLine("");
            Classes.PrintLeft("Сортировка по курсу и возрасту:", false, ConsoleColor.White);
            list.Sort((
                (Student x, Student y) =>
                {
                    if (x.Kurs < y.Kurs) return -1;
                    if (x.Kurs > y.Kurs) return 1;
                    if (x.Kurs == y.Kurs)
                    {
                        if (x.Age < y.Age) return -1;
                        if (x.Age > y.Age) return 1;
                    }
                        return 0;
                }));
            foreach (Student s in list)
                Classes.PrintLeft(s.Name + " Возраст: " + s.Age + " Курс: " + s.Kurs, false, ConsoleColor.Yellow);

        }
    }
}
