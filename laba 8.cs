using System;
using System.Text.RegularExpressions;

public abstract class Task
{
    public abstract string ToString(string input);
}

public class Task1 : Task
{
    public override string ToString(string input)
    {
        string russianLettersPattern = "[а-я]";
        int totalLettersCount = input.Length;

        var lettersFrequency = new (char, double)[26];
        int[] counts = new int[26];

        foreach (char letter in input)
        {
            if (Regex.IsMatch(letter.ToString(), russianLettersPattern, RegexOptions.IgnoreCase))
            {
                char lowerLetter = char.ToLower(letter);
                int index = lowerLetter - 'а';
                if (index >= 0 && index < 26)
                {
                    if (counts[index] == 0)
                    {
                        lettersFrequency[index] = (lowerLetter, 1);
                    }
                    else
                    {
                        lettersFrequency[index] = (lowerLetter, lettersFrequency[index].Item2 + 1);
                    }

                    counts[index]++;
                }
                
            }
        }

        Array.Sort(lettersFrequency, (x, y) => y.Item2.CompareTo(x.Item2));

        string result = "Задача 1:\n";
        foreach (var letter in lettersFrequency)
        {
            if (letter.Item2 > 0)
            {
                result += $"{letter.Item1}: {letter.Item2 / totalLettersCount:F4}{Environment.NewLine}";
            }
        }

        return result;
    }
}

public class Task3 : Task
{
    public override string ToString(string input)
    {
        string result = "Задача 3:\n";
        int maxLength = 50;
        string[] words = input.Split(' ');
        string currentLine = "";
        foreach (string word in words)
        {
            if ((currentLine + word).Length > maxLength)
            {
                result += currentLine.Trim() + Environment.NewLine;
                currentLine = "";
            }
            currentLine += word + " ";
        }
        result += currentLine.Trim();
        return result;
    }
}

public class Task6 : Task
{
    public override string ToString(string input)
    {
        var syllableCount = new (int, int)[10];
        int[] counts = new int[10];
        string[] words = input.Split(' ');
        foreach (string word in words)
        {
            int syllables = CountSyllables(word);
            int index = syllables - 1;
            if(index >= 0 && index < 26)
            {
                if (counts[index] == 0)
                {
                    syllableCount[index] = (syllables, 1);
                }
                else
                {
                    syllableCount[index] = (syllables, syllableCount[index].Item2 + 1);
                }

                counts[index]++;
            }
            
        }

        Array.Sort(syllableCount, (x, y) => x.Item2.CompareTo(y.Item2));

        string result = "Задача 6:\n";
        foreach (var item in syllableCount)
        {
            if (item.Item2 > 0)
            {
                result += $"{item.Item1} слогов: {item.Item2} слов{Environment.NewLine}";
            }
        }

        return result;
    }

    private int CountSyllables(string word)
    {
        int syllables = 0;
        word = word.ToLower().Trim();

        foreach (char c in word)
        {
            if ("аеёиоуыэюя".Contains(c))
            {
                syllables++;
            }
        }

        return syllables;
    }
}

public class Task12 : Task
{
    public override string ToString(string input)
    {
        string result = "Задача 12:\n";
        string[] lines = input.Split(Environment.NewLine);
        foreach (var line in lines)
        {
            if (line.Contains(":"))
            {
                string[] parts = line.Split(":");
                result = result.Replace(parts[1].Trim(), parts[0].Trim());
            }
            else
            {
                result += line + " ";
            }
        }

        result = result.Replace("раз", "#$@^")
            .Replace("два", "*@!&")
            .Replace("банан", "!@#$%")
            .Replace("три", "%^&*(")
            .Replace("что", "&*()!")
            .Replace("сказать", "^%$#@")
            .Replace("мне", "@#$%^")
            .Replace("окей", "*(^%$")
            .Replace("Bruh", "#@$%^")
            .Replace("слово", "$%^&*");

        return result.Trim();
    }
}

public class Task13 : Task
{
    public override string ToString(string input)
    {
        var lettersCount = new (char, int)[26];
        int[] counts = new int[26];
        string[] words = Regex.Split(input, @"\W+");
        int totalWords = words.Length;

        foreach (var word in words)
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                char firstLetter = char.ToLower(word[0]);
                int index = firstLetter - 'а';
                if (index >= 0 && index < 26)
                {
                    if (counts[index] == 0)
                    {
                        lettersCount[index] = (firstLetter, 1);
                    }
                    else
                    {
                        lettersCount[index] = (firstLetter, lettersCount[index].Item2 + 1);
                    }

                    counts[index]++;
                }
                
            }
        }

        Array.Sort(lettersCount, (x, y) => x.Item2.CompareTo(y.Item2));

        string result = "Задача 13: \n";
        foreach (var item in lettersCount)
        {
            if (item.Item2 > 0)
            {
                double percentage = (double)item.Item2 / totalWords * 100;
                result += $"{item.Item1}: {percentage:F2}%{Environment.NewLine}";
            }
        }

        return result;
    }
}

public class Task15 : Task
{
    public override string ToString(string input)
    {
        int sum = 0;
        string[] words = Regex.Split(input, @"\W+");
        foreach (var word in words)
        {
            if (int.TryParse(word, out int num))
            {
                sum += num;
            }
        }
        return "Задача 15:\n" + sum.ToString();
    }

    class Program
    {
        static void Main(string[] args)
        {
            string text = "После многолетних исследований Bruh ученые обнаружили тревожную тенденцию в вырубке лесов Амазонии. раз Анализ данных показал, что основной участник разрушения лесного покрова – человеческая деятельность. За последние десятилетия рост объема вырубки достиг критических показателей. Главными факторами, способствующими этому, являются промышленные рубки, производство древесины, расширение сельскохозяйственных угодий и незаконная добыча древесины. Это приводит к серьезным экологическим последствиям, таким как потеря биоразнообразия, ухудшение климата и угроза вымирания многих видов животных и растений.";
            Task[] tasks = new Task[] {
            new Task1(),
            new Task3(),
            new Task6(),
            new Task12(),
            new Task13(),
            new Task15()
        };

            for (int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine(tasks[i].ToString(text));
                Console.WriteLine();
            }
        }
    }
}