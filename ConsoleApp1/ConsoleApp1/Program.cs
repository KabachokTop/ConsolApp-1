using System;
using System.Globalization;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Меню ===");
            Console.WriteLine("1 - Fizz Buzz");
            Console.WriteLine("2 - Відсоток від числа");
            Console.WriteLine("3 - Число з чотирьох цифр");
            Console.WriteLine("4 - Обмін цифр у шестизначному числі");
            Console.WriteLine("5 - Пора року і день тижня");
            Console.WriteLine("6 - Температура (Фаренгейт <-> Цельсій)");
            Console.WriteLine("7 - Парні числа в діапазоні");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1": Task1(); break;
                case "2": Task2(); break;
                case "3": Task3(); break;
                case "4": Task4(); break;
                case "5": Task5(); break;
                case "6": Task6(); break;
                case "7": Task7(); break;
                case "0": return;
                default: Console.WriteLine("Невірний пункт меню."); break;
            }
        }
    }


    static bool ReadInt(string prompt, out int value)
    {
        Console.Write(prompt);
        return int.TryParse(Console.ReadLine()?.Trim(), out value);
    }

    static bool ReadDouble(string prompt, out double value)
    {
        Console.Write(prompt);
        string s = (Console.ReadLine() ?? "").Trim().Replace(',', '.');
        return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
    }

    static void Task1()
    {
        if (!ReadInt("Введіть число від 1 до 100: ", out int n) || n < 1 || n > 100)
        {
            Console.WriteLine("Помилка: потрібно ввести ціле число від 1 до 100.");
            return;
        }

        if (n % 3 == 0 && n % 5 == 0) Console.WriteLine("Fizz Buzz");
        else if (n % 3 == 0) Console.WriteLine("Fizz");
        else if (n % 5 == 0) Console.WriteLine("Buzz");
        else Console.WriteLine(n);
    }

    static void Task2()
    {
        if (!ReadDouble("Введіть число: ", out double value) ||
            !ReadDouble("Введіть відсоток: ", out double percent))
        {
            Console.WriteLine("Помилка: введено некоректне число.");
            return;
        }

        double result = value * percent / 100;
        Console.WriteLine($"{percent}% від {value} = {result.ToString(CultureInfo.InvariantCulture)}");
    }

    static void Task3()
    {
        int[] d = new int[4];
        for (int i = 0; i < 4; i++)
        {
            if (!ReadInt($"Введіть цифру №{i + 1}: ", out d[i]) || d[i] < 0 || d[i] > 9)
            {
                Console.WriteLine("Помилка: потрібно ввести цифру від 0 до 9.");
                return;
            }
        }

        int number = d[0] * 1000 + d[1] * 100 + d[2] * 10 + d[3];
        Console.WriteLine($"Результат: {number}");
    }

    static void Task4()
    {
        if (!ReadInt("Введіть шестизначне число: ", out int number) || number < 100000 || number > 999999)
        {
            Console.WriteLine("Помилка: потрібно ввести шестизначне число.");
            return;
        }

        if (!ReadInt("Введіть номер першого розряду (1-6): ", out int p1) || p1 < 1 || p1 > 6 ||
            !ReadInt("Введіть номер другого розряду (1-6): ", out int p2) || p2 < 1 || p2 > 6)
        {
            Console.WriteLine("Помилка: номер розряду має бути від 1 до 6.");
            return;
        }

        char[] digits = number.ToString().ToCharArray();
        (digits[p1 - 1], digits[p2 - 1]) = (digits[p2 - 1], digits[p1 - 1]);
        Console.WriteLine($"Результат: {new string(digits)}");
    }

    static void Task5()
    {
        Console.Write("Введіть дату (дд.мм.рррр): ");
        string input = Console.ReadLine()?.Trim();

        if (!DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime date))
        {
            Console.WriteLine("Помилка: невірний формат дати. Приклад: 22.12.2021");
            return;
        }

        string season;
        switch (date.Month)
        {
            case 12:
            case 1:
            case 2:
                season = "Winter"; break;
            case 3:
            case 4:
            case 5:
                season = "Spring"; break;
            case 6:
            case 7:
            case 8:
                season = "Summer"; break;
            default:
                season = "Autumn"; break;
        }

        Console.WriteLine($"{season} {date.DayOfWeek}");
    }

    static void Task6()
    {
        Console.WriteLine("1 - Фаренгейт -> Цельсій");
        Console.WriteLine("2 - Цельсій -> Фаренгейт");
        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine()?.Trim();

        if (choice != "1" && choice != "2")
        {
            Console.WriteLine("Помилка: оберіть 1 або 2.");
            return;
        }

        if (!ReadDouble("Введіть температуру: ", out double t))
        {
            Console.WriteLine("Помилка: введено некоректне число.");
            return;
        }

        if (choice == "1")
        {
            double c = (t - 32) * 5 / 9;
            Console.WriteLine($"{t.ToString(CultureInfo.InvariantCulture)} °F = {c.ToString("0.##", CultureInfo.InvariantCulture)} °C");
        }
        else
        {
            double f = t * 9 / 5 + 32;
            Console.WriteLine($"{t.ToString(CultureInfo.InvariantCulture)} °C = {f.ToString("0.##", CultureInfo.InvariantCulture)} °F");
        }
    }

    static void Task7()
    {
        if (!ReadInt("Введіть початок діапазону: ", out int a) ||
            !ReadInt("Введіть кінець діапазону: ", out int b))
        {
            Console.WriteLine("Помилка: введено некоректне число.");
            return;
        }

        if (a > b)
        {
            (a, b) = (b, a);
            Console.WriteLine($"Межі нормалізовано: [{a}; {b}]");
        }

        Console.Write("Парні числа: ");
        bool any = false;
        for (int i = a; i <= b; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write(i + " ");
                any = true;
            }
        }
        if (!any) Console.Write("немає");
        Console.WriteLine();
    }
}