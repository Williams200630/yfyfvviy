using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Lab3
{
    class Program
    {
        public static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Выберите задание для выполнения:");
                Console.WriteLine("1. Задание 1 ");
                Console.WriteLine("2. Задание 2 ");
                Console.WriteLine("3. Задание 3");
                Console.WriteLine("4. Задание 4");
                Console.WriteLine("5. Задание 5");
                Console.WriteLine("6. Задание 6");
                Console.WriteLine("7. Задание 7");
                Console.WriteLine("8. Задание 8");
                Console.WriteLine("9. Задание 9");
                Console.WriteLine("10. Задание 10");
                Console.WriteLine("0. Выход");
                Console.Write("Введите номер задания: ");

                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            RunTask1();
                            break;
                        case 2:
                            RunTask2();
                            break;
                        case 3:
                            RunTask3();
                            break;
                        case 4:
                            RunTask4();
                            break; // Добавлен break
                        case 5:
                            RunTask5();
                            break;
                        case 6:
                            RunTask6();
                            break;
                        case 7:
                            RunTask7();
                            break;
                        case 8:
                            RunTask8();
                            break;
                        case 9:
                            RunTask9();
                            break;
                        case 10:
                            RunTask10();
                            break;
                        case 0:
                            Console.WriteLine("Программа завершена.");
                            return; // Завершаем программу
                        default:
                            Console.WriteLine("Неверный номер задания. Пожалуйста, выберите один из предложенных.");
                            break;

                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод, пожалуста введите число");
                }
                Console.WriteLine();
            }
        }
        static void RunTask1()
        {
            // Запрашиваем количество элементов массива 
            Console.Write("Введите количество элементов массива: ");
            int arraySize = int.Parse(Console.ReadLine());
            // Создаем массив заданного размера 
            int[] array = new int[arraySize];
            // Инициализируем генератор случайных чисел
            Random random = new Random();
            // Заполняем массив случайными числами от -30 до 45
            for (int i = 0; i < arraySize; i++)
            {
                array[i] = random.Next(-30, 46); // 46, так как верхняя граница исключается
            }
            //Выводим массив по 10 элементов в строке
            Console.WriteLine("\nCrенерированный массив:");
            for (int i = 0; i < arraySize; i++)
            {
                Console.Write($"{array[i],4}"); // Форматируем вывод с отступом для удобства чтения
                if ((i + 1) % 10 == 0) Console.WriteLine();
            }
            Console.WriteLine();
            // Выводим положительные элементы массива в обратном порядке
            Console.WriteLine("\nПоложительные элементы массива в обратном порядке:");
            for (int i = arraySize - 1; i >= 0; i--)
            {
                if (array[i] > 0)
                {
                    Console.Write($"{array[i],4}");
                }
            }
            Console.WriteLine();
        }
        static void RunTask2()
        {
            // Инициализация двумерного массива 7х7
            int size = 7;
            int[,] matrix = new int[size, size]; // Исправлено: добавлен оператор присваивания

            //Заполнение массива значениями для наглядности
            int counter = 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = counter++;
                }
            }

            // Вывод оригинального массива
            Console.WriteLine("Оригинальный массив:");
            PrintMatrix(matrix);

            // Поворот массива на 90 градусов вправо
            RotateMatrix90Degrees(matrix);

            // Вывод повёрнутого нассива
            Console.WriteLine("\nМассив после поворота на 90 градусов вправо:");
            PrintMatrix(matrix);
        }

        protected static void PrintMatrix(int[,] matrix) // Исправлено: исправлена опечатка в названии метода
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j],4}");
                }
                Console.WriteLine();
            }
        }

        protected static void RotateMatrix90Degrees(int[,] matrix)
        {
            int n = matrix.GetLength(0); // Размерность матрицы

            // Обходим "слои" матрицы
            for (int layer = 0; layer < n / 2; layer++)
            {
                int first = layer;
                int last = n - 1 - layer;

                for (int i = first; i < last; i++)
                {
                    int offset = i - first;

                    // Сохраняем верхний элемент
                    int top = matrix[first, i];

                    // Левый -> верхний
                    matrix[first, i] = matrix[last - offset, first];

                    // Нижний -> левый
                    matrix[last - offset, first] = matrix[last, last - offset];

                    // Правый -> нижний
                    matrix[last, last - offset] = matrix[i, last];

                    // Верхний (сохраненный) -> правый
                    matrix[i, last] = top;
                }
            }
        }
        static void RunTask3()
        {
            // Пример массива
            int[] array = { 1, 2, 3, 4, 5, 6, 7 };
            // Ввод сдвига k
            Console.Write("Введите количество позиций для сдвига влево: ");
            int k = int.Parse(Console.ReadLine());
            // Выполнение сдвига массива на k позиций влево
            LeftRotate(array, k);
            // Вывод результата
            Console.WriteLine("Mассив после циклического сдвига влево:");
            Console.WriteLine(string.Join(" ", array));

        }
        protected static void LeftRotate(int[] array, int k)
        {
            int n = array.Length;
            k = k % n; // Избавляемся от лишних циклов, если k больше длины массива
            //Переворачиваем первую часть (от начала до k-1)
            Reverse(array, 0, k - 1);
            // Переворачиваеи вторую часть (от к до конца)
            Reverse(array, k, n - 1);
            // Переворачиваем весь массив
            Reverse(array, 0, n - 1);
        }
        // Метод для переворота части массива от индекса start до индекса end 
        protected static void Reverse(int[] array, int start, int end)
        {
            while (start < end)
            {
                int temp = array[start];
                array[start] = array[end];
                array[end] = temp;
                start++; end--;

            }
        }
        static void RunTask4()
        {
            // Инициализация двух массивов 3×3
            int[,] matrix1 = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
            int[,] matrix2 = {
            { 9, 8, 7 },
            { 6, 5, 4 },
            { 3, 2, 1 }
        };
            Console.WriteLine("Матрица 1:"); PrintMatrix(matrix1);
            Console.WriteLine("Матрица 2:");
            PrintMatrix(matrix2);
            // Переменная для хранения среднего значения
            double averageSum;
            // Сложение матриц
            int[,] sumMatrix = AddMatrices(matrix1, matrix2, out averageSum);
            Console.WriteLine("Результат сложения матриц:");
            PrintMatrix(sumMatrix);
            Console.WriteLine($"Среднее значение всех элементов входных матриц: {averageSum:F2}");

            // Вычитание матриц
            double averageDiff;
            int[,] diffMatrix = SubtractMatrices(matrix1, matrix2, out averageDiff);
            Console.WriteLine("\nРезультат вычитания матриц:");
            PrintMatrix(diffMatrix);
            Console.WriteLine($"Среднее значение всех элементов входных матриц: {averageDiff:F2}");
        }

        protected static int[,] AddMatrices(int[,] matrix1, int[,] matrix2, out double average)
        {
            int size = 3;
            int[,] result = new int[size, size];
            int sum = 0;
            int totalElements = size * size * 2; // Общее количество элементов двух входных матриц
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = matrix1[i, j] + matrix2[i, j];
                    sum += matrix1[i, j] + matrix2[i, j];
                }
            }
            average = (double)sum / totalElements;
            return result;
        }

        protected static int[,] SubtractMatrices(int[,] matrix1, int[,] matrix2, out double average)
        {
            int size = 3;
            int[,] result = new int[size, size];
            int sum = 0;
            int totalElements = size * size * 2; // Общее количество элементов двух входных матриц
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = matrix1[i, j] - matrix2[i, j]; // Исправлено на вычитание
                    sum += matrix1[i, j] + matrix2[i, j]; // Исправлено на сумму двух исходных матриц.
                }
            }
            average = (double)sum / totalElements;
            return result;
        }
        static void RunTask5()
        {
            // Инициализация двух матриц 5х5
            int[,] matrix1 = {
            { 1, 2, 3, 4, 5 },
            { 6, 7, 8, 9, 10 },
            { 11, 12, 13, 14, 15 },
            { 16, 17, 18, 19, 20 },
            { 21, 22, 23, 24, 25 }
        };
            int[,] matrix2 = {
            { 25, 24, 23, 22, 21 },
            { 20, 19, 18, 17, 16 },
            { 15, 14, 13, 12, 11 },
            { 10, 9, 8, 7, 6 },
            { 5, 4, 3, 2, 1 }
        };
            // Умножение матриц
            int[,] resultMatrix = MultiplyMatrices(matrix1, matrix2);
            // Вывод результата
            Console.WriteLine("Результат перемножения матриц:");
            PrintMatrix(resultMatrix);
        }
        protected static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
        {
            int size = 5;
            int[,] result = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < size; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return result;
        }
        static void RunTask6()
        {
            int[] array = { 5, 12, -4, 7, 3, -9, 15, 8 };
            // Демонстрация работы функций
            Console.WriteLine("Mассив: " + string.Join(", ", array));
            // Вычисление суммы
            Console.WriteLine("\nСумма элементов массива (итеративно): " + SumIterative(array));
            Console.WriteLine("Сумма элементов массива (рекурсивно): " + SumRecursive(array, array.Length - 1));
            // Вычисление минимального элемента
            Console.WriteLine("\nМинимальный элемент массива (итеративно): " + MinIterative(array));
            Console.WriteLine("Минимальный элемент массива (pекурсивно): " + MinRecursive(array, array.Length - 1));
        }
        protected static int SumIterative(int[] array)
        {
            int sum = 0;
            foreach (int num in array)
            {
                sum += num;
            }
            return sum;
        }
        protected static int SumRecursive(int[] array, int index)
        {
            if (index < 0)
                return 0;
            return array[index] + SumRecursive(array, index - 1);
        }
        protected static int MinRecursive(int[] array, int index)
        {
            if (index == 0)
                return array[0];
            int minOfRest = MinRecursive(array, index - 1);
            return array[index] < minOfRest ? array[index] : minOfRest;
        }
        protected static int MinIterative(int[] array)
        {
            int min = array[0];
            foreach (int num in array)
            {
                if (num < min)
                    min = num;

            }
            return min;
        }
        static void RunTask7()
        {
            Console.Write("Введите номер элемента ряда Фибоначчи (n): ");
            int n = int.Parse(Console.ReadLine());
            // Вызываем рекурсивную функцию и выводим результат
            int result = Fibonacci(n);
            Console.WriteLine($" {n}-й член ряда Фибоначчи: {result}");
        }
        protected static int Fibonacci(int n)
        {
            // Базовые случаи
            if (n == 0)
                return 0;
            if (n == 1) return 1;
            // Рекурсивное вычисление
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
        static void RunTask8()
        {
            Console.Write("Введите размерность матрицы N (NxN): ");
            int n = int.Parse(Console.ReadLine());
            // Создаем и заполняем матрицу
            int[,] matrix = new int[n, n];
            Console.WriteLine("Введите элементы матрицы:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"Элемент [{i + 1},{j + 1}]: ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }
            // Вычисляем определитель
            int determinant = CalculateDeterminant(matrix, n);
            Console.WriteLine($"Определитель матрицы: {determinant}");

        }
        protected static int CalculateDeterminant(int[,] matrix, int size)
        {
            // Базовый случай: определитель 1х1 матрицы - это единственный элемент
            if (size == 1)
            {
                return matrix[0, 0];
            }
            //Базовый случай: определитель 2x2 матрицы
            if (size == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            }
            int determinant = 0;
            int sign = 1;
            // Разложение по первой строке
            for (int col = 0; col < size; col++)
            {
                // Получаен минор матрицы, удаляя 0-ю строку и col-й creлбец
                int[,] minor = GetMinor(matrix, size, col);
                //Рекурсивное вычисление определителя минора и добавление к общему определителю
                determinant += sign * matrix[0, col] * CalculateDeterminant(minor, size - 1);
                // Меняем знак для следующего члена (по правилу чередования знаков в определителе)
                sign = -sign;
            }
            return determinant;
        }
        protected static int[,] GetMinor(int[,] matrix, int size, int colToRemove)
        {
            int[,] minor = new int[size - 1, size - 1];
            int minorRow = 0, minorCol;
            for (int i = 1; i < size; i++) // Пропускаем первую строку
            {
                minorCol = 0;
                for (int j = 0; j < size; j++)
                {
                    if (j == colToRemove) continue; // Пропускаем столбец colToRemove
                    minor[minorRow, minorCol] = matrix[i, j];
                    minorCol++;
                }
                minorRow++;
            }
            return minor;
        }
        static void RunTask9()
        {
            // Размерность матрицы 9x9
            int size = 9;
            int[,] matrix = new int[size, size];
            // Инициализация генератора случайных чисел
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = random.Next(1, 100); // Числа от 1 до 99
                }

            }
            // Вывод исходной матрицы
            Console.WriteLine("Исходная матрица:");
            PrintMatrix(matrix);
            // Симметричное отображение диагоналей относительно вертикальной оси
            SymmetricDiagonalDisplay(matrix);

        }
        protected static void SymmetricDiagonalDisplay(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            Console.WriteLine("\nГлавная и побочная диагонали, симметрично отображённые относительно вертикальной оси:");

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (j == i)
                    {
                        Console.Write($"{matrix[i, size - 1 - i],4}"); // Побочная диагональ
                    }
                    else if (j == size - 1 - i)
                    {
                        Console.Write($"{matrix[i, i],4}"); // Главная диагональ
                    }
                    else
                    {
                        Console.Write("  "); // Пробелы для остальных элементов
                    }
                }
                Console.WriteLine();
            }
        }
        static void RunTask10()
        {
            int N;
            // Запрашиваем у пользователя чётное число элементов массива
            while (true)
            {
                Console.Write("Введите четное количество элементов массива (N): ");
                N = int.Parse(Console.ReadLine());
                if (N > 0 && N % 2 == 0) // Проверка на четность и положительность
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка: введите положительное четное число.");
                }
            }
            // Инициализируем массив и запрашиваем у пользователя его элементы
            int[] array = new int[N];
            Console.WriteLine("Введите элементы массива:");
            for (int i = 0; i < N; i++)
            {
                Console.Write($"Элемент {i + 1}: ");
                array[i] = int.Parse(Console.ReadLine());
            }
            // Проверка на строго возрастающую последовательность
            bool isIncreasing = true;
            for (int i = 0; i < N - 1; i++)
            {
                if (array[i] >= array[i + 1])
                {
                    isIncreasing = false;
                    break;
                }
            }
            //Вывод результата
            Console.WriteLine(isIncreasing ? "TRUE" : "FALSE");
        }
    }
}