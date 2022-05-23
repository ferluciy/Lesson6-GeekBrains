using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBcool
{
    public delegate double Fun(double a, double x); // делегат для задачи 1 урок 6
    public delegate double Fun2(double x); //делегат для задачи 2 урок 6
    public struct FunArray //название функция + сама функция 
    {
        public string NameFun;
        public Fun2 MyFunction;
    }
    public struct Account
    {
        public string Login;
        public string Password;
    }
    public struct user
    {
        public string FIO;
        public double ball;
    }
    public static class Message //класс для работы со строками
    {
        private static string[] separators = { ",", ".", "!", "?", ";", ":", " ", " - " };
        public static StringBuilder WordsLength(string message, int N) //вывести слова которые содержат не более N символов
        {
            StringBuilder s = new StringBuilder("");
            string[] words = message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length <= N)
                {
                    s.Append(words[i] + "; ");
                }
            }
            return s;
        }
        public static StringBuilder RemoveWordsLength(string message, char K) //удалить слова которые заканчиваются на символ K
        {
            StringBuilder s = new StringBuilder(message);
            string[] words = message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i][words[i].Length - 1] == K) s.Replace(words[i], "");
            }
            for (int i = 0; i < separators.Length; i++) s.Replace(" " + separators[i], separators[i]); //уберем лишние пробелы
            return s;
        }
        public static string MaxWordLength(string message) //самое длинное слово
        {
            string[] words = message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int max = 0;
            for (int i = 1; i < words.Length; i++)
            {
                max = words[max].Length > words[i].Length ? max : i;
            }
            return words[max];
        }

        public static StringBuilder MaxWords(string message) //предложение из самых длинных слов
        {
            StringBuilder s = new StringBuilder("");
            string[] words = message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int max = 0;
            for (int i = 0; i < words.Length - 1; i++)
            {
                max = words[i].Length > words[i + 1].Length ? words[i].Length : words[i + 1].Length;
            }
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == max) s.Append(words[i] + " ");
            }
            return s;
        }
        public static void ChastotaSlov(string[] listword, string message) //частотный анализ текста
        {
            string[] words = message.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            var strings = new Dictionary<string, int>(); //создаем коллекцию Dictionary
            int counter;
            for (int i = 0; i < listword.Length; i++)
            {
                counter = 0;
                for (int j = 0; j < words.Length; j++) counter += listword[i].ToLower() == words[j].ToLower() ? 1 : 0;
                if (!strings.ContainsKey(listword[i])) strings.Add(listword[i], counter);
            }
            foreach (string s in strings.Keys)
            {
                Classes.PrintLeft(String.Format($"Слово <{s}> повторяется {strings[s]} раз"), false, ConsoleColor.Yellow);
            }
        }
        public static bool isPerestanovka(string source, string str) //является ли str перестановкой source
        {
            int counter_source;
            int counter_str;
            bool flag = false;
            if (source.Length != str.Length) return false;

            for (int i = 0; i < source.Length; i++)
            {
                counter_source = 0;
                counter_str = 0;
                for (var j = 0; j < source.Length; j++)
                {
                    counter_source += source[i] == source[j] ? 1 : 0;
                    counter_str += str[j] == source[i] ? 1 : 0;
                }
                flag = counter_str == counter_source ? true : false;
                if (!flag) return false;
            }
            return flag;
        }
    }
    public class MyDoubleArray
    {
        #region Fields

        private int[,] arr;

        #endregion

        #region Properties

        public int this[int index, int index2] //возращает или изменяет значение элемента массива
        {
            get
            {
                return arr[index, index2];
            }

            set
            {
                arr[index, index2] = value;
            }
        }

        public int Min
        {
            get 
            {
                int a= arr[0, 0];
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                       a = arr[i, j] > a ? a : arr[i, j];
                    }

                }
                return a;
            }
        }

        public int Max
        {
            get
            {
                int a = arr[0, 0];
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        a = arr[i, j] < a ? a : arr[i, j];
                    }

                }
                return a;
            }
        }
        #endregion

        #region Constructor
        public MyDoubleArray(int count1, int count2, int min, int max) //заполняет массив размером в count случайными значениями от min до max включительно
        {
            Random rnd = new Random();
            this.arr = new int[count1, count2];

            for (int i = 0; i < count1; i++)
            {
                for (int j = 0; j < count2; j++)
                {
                    this.arr[i, j] = rnd.Next(min, max + 1);

                }
            }
        }
        public MyDoubleArray(string fileName, char splitter) //заполняет массив из файла
        {
            this.arr = LoadArrayFromFile(fileName, splitter);
        }
        #endregion

        #region Methods

        public int GetLength(int level)//количество элементов массива
        {
            return arr.GetLength(level);
        }

        public void IndexMaxVal(out int a, out int b) //номер максимального значения массива
        {
            a = -1;
            b = -1;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                    if (this.Max == arr[i,j])
                    {
                        a = i;
                        b = j;
                        break;
                    }
                    }
                if (a > -1 && b > -1) break;
                }
        }
        public (int, int) GetMaxIndex()
        {
            int x = 0;
            int y = 0;
            int max = arr[x, y];
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] > max)
                    {
                        x = i;
                        y = j;
                    }
                }
            return (x, y);
        }

        public int AllSum() //сумма всех чисел массива
        {
            int sum = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    sum += arr[i, j];
                }
            }
            return sum;

        }
        public int AllSum(int a) //сумма всех чисел массива, которые больше a 
        {
            int sum = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    sum +=  arr[i, j] > a ? arr[i, j] : 0;
                }
            }
            return sum;

        }

        public void PrintArray()
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (Console.CursorLeft > 100) Console.WriteLine("");
                    Classes.PrintLeft(String.Format($"{arr[i,j]}\t"), true, ConsoleColor.White);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private int[,] LoadArrayFromFile(string fileName, char splitter)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }
            StreamReader streamReader = new StreamReader(fileName);
         
            int counter = 0;
            string s;            
            s = streamReader.ReadLine();
            int[,] arr = new int[System.IO.File.ReadAllLines(fileName).Length, s.Split(splitter).Length]; //создаем массив на основе первой строки файла
            streamReader.Close();

            streamReader = new StreamReader(fileName); //открываем повторно
            while (!streamReader.EndOfStream)
            {
                try {
                    s = streamReader.ReadLine();
                    for (int i = 0; i < arr.GetLength(1); i++)
                    {
                        if (s.Split(splitter).Length > i) int.TryParse(s.Split(splitter)[i], out arr[counter, i]);
                    }
                    counter++;
                }
                catch (Exception exc)
                {
                    Classes.PrintLeft(exc.Message, false, ConsoleColor.Red);
                    break;
                }
            }
            streamReader.Close();
            return arr;
        }
        public void SaveArrayInFile(string fileName, char splitter) //сохранение массива в файл с разделителем для возможности его загрузки 
        {
            try
            {
                    StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + fileName);

                    string s = "";
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            if (j != arr.GetLength(1) - 1) s += arr[i, j].ToString() + splitter;
                            else s += arr[i, j];
                        }
                        streamWriter.WriteLine(s);
                        s = "";
                    }
                    streamWriter.Close();
                    Classes.PrintLeft("Успешно сохранен массив в файл <" + fileName + ">!", false, ConsoleColor.Green);
               

            }
            catch (Exception e)
            {
                Classes.PrintLeft("Exception: " + e.Message, false, ConsoleColor.Red);
            }            
        }

        #endregion
    }//двумерный массив
    public class MyArray
        {

            #region Fields

            private int[] arr;

            #endregion

            #region Properties

            public int this[int index] //возращает или изменяет значение элемента массива
            {
                get
                {
                    return arr[index];
                }

                set
                {
                    arr[index] = value;
                }
            }
            public int Length //количество элементов массива
            {
                get { return arr.Length; }
            }
        public int MaxCount //количество максимальных элементов массива
        {
            get 
            {
                int a = arr[0], sum = 0;
                for (int i = 0; i < arr.Length; i++) a = arr[i] > a ? arr[i] : a;
                for (int i = 0; i < arr.Length; i++) sum += a == arr[i] ? 1 : 0; 
                    return sum; 
            }
        }
        public int Sum //сумма всех чисел массива
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < arr.Length; i++) sum = sum + arr[i];
                    return sum;

                }
            }

            #endregion

            #region Constructors


            public MyArray(int count, int min, int max, bool IN) //заполняет массив размером в count случайными значениями от min до max включительно или не включительно
            {
                Random rnd = new Random();
                arr = new int[count];

                for (int i = 0; i < count; i++)
                {
                    if (IN) arr[i] = rnd.Next(min, max + 1);
                    else arr[i] = rnd.Next(min, max);
                }

            }

            public MyArray(int[] arr) //заполняет массив элементами из другого массива
            {
                this.arr = new int[arr.Length];
                Array.Copy(arr, this.arr, arr.Length);

            }

            public MyArray(int count, int min, int step) //заполняет массив размеров в count числами от начального значения min с заданным шагом step
            {
                arr = new int[count];
                arr[0] = min;
                for (int i = 1; i < count; i++)
                {
                   arr[i] = arr[i - 1] + step;
                }

            }

            public MyArray(string fileName) //заполняет массив из файла
            {
                arr = LoadArrayFromFile(fileName);
            }

            #endregion

            #region Methods

            private int[] LoadArrayFromFile(string fileName)
            {
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException();
                }
                StreamReader streamReader = new StreamReader(fileName);
                int[] buf = new int[System.IO.File.ReadAllLines(fileName).Length]; //считаем количество строк в файле
                int counter = 0;
                while (!streamReader.EndOfStream)
                {
                    buf[counter] = int.Parse(streamReader.ReadLine());
                    counter++;
                }
                streamReader.Close();
                return buf;
            }

            public void PrintArray()
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (Console.CursorLeft > 100) Console.WriteLine("");
                    Classes.PrintLeft(String.Format($"{arr[i]}\t"), true, ConsoleColor.White);
                }
                Console.WriteLine();
            }

            public static void SearchPara(MyArray arr, int search) //поиск пары, в которой одно числи делится на search без остатка
            {
                int number = 0;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (Classes.DelNaChislo(arr[i], arr[i + 1], 3))
                {
                        number++;
                        Classes.PrintLeft(String.Format($"Найденная пара: {arr[i]} : {arr[i + 1]}"), false, ConsoleColor.Yellow);
                    }

                }
                Console.WriteLine("");
                Classes.PrintLeft(String.Format($"Количество пар в массиве: {number}"), false, ConsoleColor.Green);
            }

            public MyArray Inverse()  //Возращает такой же массив но с измененным знаком
            {
                MyArray arr1 = new MyArray(arr.Length, 0, 0, true);
                for (int i = 0; i < arr.Length; i++)
                {
                    arr1[i] = arr[i] * (-1);
                }
                return arr1;
            }

            public void Multi(int n) //умножает каждый элемент массива на n
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = arr[i] * n;
                }
            }

            #endregion

        }//одномерный массив
    public class MyDrob //класс дробей
        {
            private int x, y; //x числитель, y знаменатель

            public int X //доступ на чтение и запись числителя
            {
                get { return x; }
                set { x = value; }
            }
            public int Y //доступ на чтение и запись знаменателя
            {
                get { return y; }
                set
                {
                    if (y == 0) { throw new ArgumentException("На ноль делить нельзя"); }
                    else y = value;
                }
            }
            public double Decimal //доступ на чтение десятичной дроби
            {
                get { return (double)x / y; }
            }

            public MyDrob(int x, int y) //конструктор
            {
                this.x = x;
                if (y == 0)
                {
                    throw new ArgumentException("На ноль делить нельзя");
                }
                else this.y = y;
            }
            public override string ToString()
            {
                return $"{x} / {y}";
            }

            public static MyDrob Sokratit(MyDrob a) //сокращатель дробей с помощью алгоритма Эвклида
            {

                //int max = 0;
                ////выбираем что больше числитель или знаменатель
                //if (a.x > a.y) max = Math.Abs(a.y);
                //else max = Math.Abs(a.x);

                //for (int i = max; i >= 2; i--)
                //{

                //    if ((a.x % i == 0) & (a.y % i == 0))
                //    {
                //        a.x = a.x / i;
                //        a.y = a.y / i;
                //    }

                //}
                ////передвигаем минус в числитель
                //if (a.y < 0)
                //{
                //    a.x = -1 * a.x;
                //    a.y = Math.Abs(a.y);
                //}
                //return a;
                int nod;
                int m = Math.Abs(a.x), n = Math.Abs(a.y);
                while (m != n)
                {
                    if (m > n)
                    {
                        m = m - n;
                    }
                    else
                    {
                        n = n - m;
                    }
                }

                nod = n;
                a.x = a.x / nod;
                a.y = a.y / nod;
                if (a.y < 0) //передвигаем минус в числитель для удобства
                {
                    a.x = -1 * a.x;
                    a.y = Math.Abs(a.y);
                }
                return a;
            }

            public static MyDrob operator +(MyDrob a, MyDrob b) //сложение дробей
            {
                MyDrob DrobDef = new MyDrob((a.x * b.y) + (a.y * b.x), a.y * b.y);
                return DrobDef;
            }
            public static MyDrob operator -(MyDrob a, MyDrob b) //вычитание дробей
            {
                MyDrob DrobDef = new MyDrob((a.x * b.y) - (a.y * b.x), a.y * b.y);
                return DrobDef;
            }
            public static MyDrob operator *(MyDrob a, MyDrob b) //умножение дробей
            {
                MyDrob DrobDef = new MyDrob(a.x * b.x, a.y * b.y);
                return DrobDef;
            }
            public static MyDrob operator /(MyDrob a, MyDrob b) //деление дробей
            {
                MyDrob DrobDef = new MyDrob(a.x * b.y, a.y * b.x);
                return DrobDef;
            }
        }
    public class ComplexClass
        {
            double re;
            double im;


            public ComplexClass()
            {
                re = 0;
                im = 0;
            }
            public ComplexClass(double re, double im)
            {
                this.re = re;
                this.im = im;
            }
            public ComplexClass Plus(ComplexClass x) //сложение комплексных чисел
            {
                ComplexClass y = new ComplexClass(re + x.re, im + x.im);
                return y;
            }
            public ComplexClass Minus(ComplexClass x) //вычитание комплексных чисел
            {
                ComplexClass y = new ComplexClass(re - x.re, im - x.im);
                return y;
            }
            public ComplexClass Multi(ComplexClass x) // произведение двух комплексных чисел
            {
                ComplexClass y = new ComplexClass(re * x.re - im * x.im, re * x.im + im * x.re);
                return y;
            }
            public override string ToString()
            {
                return re + (im > 0 ? "+" : "") + im + "i";
            }
        } //класс для комплексных чисел
    public class Classes
        {
            public struct Complex
            {
                public double im;
                public double re;
                public Complex Plus(Complex x) //сложение комплексных чисел
                {
                    Complex y;
                    y.im = im + x.im;
                    y.re = re + x.re;
                    return y;
                }
                public Complex Minus(Complex x) //вычитание комплексных чисел
                {
                    Complex y;
                    y.im = im - x.im;
                    y.re = re - x.re;
                    return y;
                }
                public Complex Multi(Complex x) // произведение двух комплексных чисел
                {
                    Complex y;
                    y.im = re * x.im + im * x.re;
                    y.re = re * x.re - im * x.im;
                    return y;
                }
                public override string ToString()
                {
                    return re + (im > 0 ? "+" : "") + im + "i";
                }
            }
            public static bool DelNaChislo(int a, int b, int del) //делиться только a или только b на del без остатка
        {
            // if (((a % del == 0) && (b % del != 0)) || ((b % del == 0) && (a % del != 0))) { return true; }
            if ((a % del == 0) ^ (b % del == 0)) { return true; }
            else { return false; }
            }
            public static void Print(string s, int x, int y, ConsoleColor foregroundcolor) //вывод текста по координатам
            {
                if (x != 0 || y != 0) Console.SetCursorPosition(x, y);
                Console.ForegroundColor = foregroundcolor;
                Console.WriteLine(s);
            }
            public static void PrintCenter(string s, ConsoleColor foregroundcolor) //вывод текста по центру строки
            {
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
                Console.ForegroundColor = foregroundcolor;
                Console.WriteLine(s);
            }
            public static void PrintLeft(string s, bool InLine, ConsoleColor foregroundcolor) //вывод текста по левому краю с отступом
            {
                if (Console.CursorLeft < 20) Console.SetCursorPosition(20, Console.CursorTop);
                else Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
                Console.ForegroundColor = foregroundcolor;
                if (InLine) Console.Write(s); else Console.WriteLine(s);
            }
            public static void Pause(string s) //пауза
            {
                if (s != "") Console.WriteLine(s);
                Console.ReadKey();
                Console.WriteLine(""); //делаем отступ
            }
            public static int NumberSumm(int n) //сумма цифр числа
            {
                int s = 0;
                while (n != 0)
                {
                    s = s + n % 10;
                    n = n / 10;
                }
                return s;
            }
            public static int NumberCount(int n) //количество цифр в числе
            {
                int s = 0;
                int h = 0;
                while (n != 0)
                {
                    s = s + n % 10;
                    n = n / 10;
                    h++;
                }
                return h;
            }
            public static double IndexMassa(double m, double h) //подсчет индекса массы тела
            {
                return (m / Math.Pow(h / 100, 2));
            }
            public static void LogoLesson(string s) //шапка программы
            {
                Console.WindowHeight = 45;
                Console.WindowWidth = 130;
                Console.Clear();
                Console.WriteLine("");
                Classes.PrintCenter("░░░░░░▐▀▄░░░░░░░░░▄▀▌░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░", ConsoleColor.Red);
                Classes.PrintCenter("░░░░░░▐▓░▀▄▀▀▀▀▀▄▀░▓▌░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░", ConsoleColor.Red);
                Classes.PrintCenter("░░░░░░▐░▓░▄▀░░░▀▄░▓░▌░░░░▄▀▀ █▀▀ █▀▀ █░█ █▀▄ █▀▄ ▄▀▄ ▀█▀ █▄░█ ▄▀▀░░░", ConsoleColor.Red);
                Classes.PrintCenter("░░░░░░░█░░▌█▐░▌█▐░░█░░░░░█░█ █▀▀ █▀▀ █▀▄ █▀▄ █▀▄ █▄█ ░█░ █▀██ ░▀▄░░░", ConsoleColor.Red);
                Classes.PrintCenter("░░░░▄▄▄▐▀░░░▀█▀░░░▀▌▄▄▄░░░▀▀ ▀▀▀ ▀▀▀ ▀░▀ ▀▀░ ▀░▀ ▀░▀ ▀▀▀ ▀░░▀ ▀▀░░░░", ConsoleColor.Red);
                Classes.PrintCenter("░░░█▐▐▐▌▀▄░▀▄▀▄▀░▄▀▐▌▌▌█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░", ConsoleColor.Red);
                Console.WriteLine("");
                Classes.PrintCenter("____LESSON_" + s + "________________________________________by_Моисеев_Игорь", ConsoleColor.Yellow);
                Console.WriteLine("");


            }
            public static void RecursiaOne(int a, int b)
            {
                if (a < b)
                {
                    PrintLeft(a.ToString() + "\t", true, ConsoleColor.White);
                    a++;
                    RecursiaOne(a, b);
                }
            }//вывод чисел пока a<b
            public static int RecursiaTwo(int a, int b)
            {
                if (a > b) return 0;
                else
                {
                    a++;
                    return a - 1 + RecursiaTwo(a, b);
                }
            }//рекурсивный метод, который считает сумму чисел от a до b включительно.
            public static void quickSort(user[] array, int low, int high) //реализация быстрой сортировки для массива user[]
        {
            if ((array.Length == 0) || (low >= high)) return;
            // выбрать опорный элемент
            int middle = low + (high - low) / 2;
            user opora = array[middle];
            // разделить на подмассивы, который больше и меньше опорного элемента
            int i = low, j = high;
            while (i <= j)
            {
                while (array[i].ball < opora.ball) i++;                
                while (array[j].ball > opora.ball) j--;
                if (i <= j)
                {
                    if (i != j) // меняем местами если не равны
                    {
                        user temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                    i++;
                    j--;
                }
            }
            // вызов рекурсии для сортировки левой и правой части
            if (low < j) quickSort(array, low, j);
            if (high > i) quickSort(array, i, high);
        }
             
            public static string EndString(int a)
            {
            string s = a.ToString();
            int num = int.Parse(s[s.Length-1].ToString());
            if ((num == 0) || (num > 4)) return "ов";
            if ((num > 1) && (num < 5)) return "а";
            return "";
        }
    }
    }


