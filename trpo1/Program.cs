using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;

//Листы и словари для вычисления римских цифр
Dictionary<char, int> dict = new Dictionary<char, int> { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
List<char> roman = new List<char>() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

//Входные данные
string symbols = "2 0,16 0 -1 1,6 0,1E+2 28E-1 MMMMMMMMMDCCCCLXXXXVIIII";
//List<string> list = new List<string>() { "2", "1,6", "0,1E+2", "MMMMMMMMMDCCCCLXXXXVIIII" };

Console.WriteLine("------------------------------------------");
try
{
    //Разделение строки по пробелу
    List<string> split(string text)
    {
        List<string> words = new List<string>();
        int lastIndex = 0;
        int index;
        while ((index = text.IndexOf(' ', lastIndex)) != -1)
        {
            words.Add(text.Substring(lastIndex, index - lastIndex));
            lastIndex = index + 1;
        }
        words.Add(text.Substring(lastIndex));
        return words;
    }
    //Перевод строки в разделенную строку по пробелу
    var list = split(symbols);
    //Цикл с выводом все цифр в экспоненциальной форме
    foreach (var item in list)
    {
        type(item.ToString());
    }

    //Определение типа данных и операций с ними
    void type(string symbol)
    {
        if (!symbol.Contains('E'))
        {
            foreach (var item in roman)
            {
                if (symbol.Contains(item))
                {
                    Console.WriteLine(RomanToInt(symbol));
                    return;
                }
            }
            Console.WriteLine(ToExp(symbol));
        }
        else
        {
            if (!isExp(symbol))
            {
                Console.WriteLine(FromWrongExp(symbol));
                return;
            }
            Console.WriteLine(symbol);
        }
    }

    //Для проверки правильности экспоненциального числа
    bool isExp(string symbol)
    {
        if (symbol.Contains(","))
        {
            return true;
        }
        return false;
    }

    //В случае ввода неправильной формы экспоненциального числа
    string FromWrongExp(string symbol)
    {
        string number = symbol.Substring(0, symbol.IndexOf('E'));
        return ToExp(number);
    }

    //Перевод числа в экспоненциальную форму
    string ToExp(string symbol)
    {
        double number = Convert.ToDouble(symbol);
        int nulls = 0;
        if (number > 1)
        {
            while (number > 1)
            {
                number /= 10;
                nulls++;
            }
            return Math.Round(number, nulls+1).ToString() + "E" + "+" + nulls.ToString();
        }
        if (number <= 0)
        {
            return Math.Round(number, nulls + 1).ToString() + "E" + "+" + nulls.ToString();
        }
        while (number < 1)
        {
            number *= 10;
            nulls++;
        }
        return Math.Round(number, nulls+1).ToString() + "E" + "-" + nulls.ToString();
    }

    //Перевод римского числа в арабское, сразу с переводом в экспоненциальную форму
    string RomanToInt(string s)
    {
        char[] ch = s.ToCharArray();

        double result = 0;
        double intVal, nextIntVal;

        for (int i = 0; i < ch.Length; i++)
        {
            intVal = dict[ch[i]];

            if (i != ch.Length - 1)
            {
                nextIntVal = dict[ch[i + 1]];

                if (nextIntVal > intVal)
                {
                    intVal = nextIntVal - intVal;
                    i = i + 1;
                }
            }
            result = result + intVal;
        }
        return ToExp(result.ToString());
    }
}
catch
{
    Console.WriteLine("Неверный формат данных");
}



