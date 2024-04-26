using System;
using System.Text.Json;
using System.Text.Json.Serialization;

abstract class MainStr
{
    protected string text;

    public string Text
    {
        get => text;
        protected set => text = value;
    }
    public MainStr(string text)
    {
        this.text = text;
    }
}



class Task1 : MainStr
{

    protected string NewStr;
    [JsonConstructor]
    public Task1(string text) : base(text) { }


    public override string ToString()
    {
        string RemovePunctuation(string text)
        {
            string NewStr = "";

            for (int i = 0; i < text.Length; i++)
            {
                char cur = text[i];

                if (char.IsLetterOrDigit(cur))
                {
                    NewStr += cur;
                }
            }

            return NewStr;
        }

    }
        
}



class Task2 : MainStr
{

    protected string result;
    protected string reversed;

    [JsonConstructor]
    public Task2(string text) : base(text) { }

    public override string ToString()
    {
        result = "";
        reversed = "";

        foreach (char c in text)
        {
            if (Char.IsLetter(c))
            {
                reversed = c + reversed;
            }
            else
            {
                result += reversed + c;
                reversed = "";
            }
        }

        result += reversed;

        return result;
    }
}


class JsonMode
{
    public static void Write<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            JsonSerializer.Serialize(fs, obj);
        }
    }
    public static T Read<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            return JsonSerializer.Deserialize<T>(fs);
        }
        return default(T);
    }
}

class Program
{
    static void Main()
    {

        MainStr[] tasks = {
            new Task1("На празднике, который проходил в большом парке, дети играли в футбол, веселились, пели песни, а их родители готовили вкусные угощения: шашлык, салаты, пироги и напитки. "),
            new Task2("На празднике, который проходил в большом парке, дети играли в футбол, веселились, пели песни, а их родители готовили вкусные угощения: шашлык, салаты, пироги и напитки. ")
        };
        Console.WriteLine(tasks[0].ToString());
        Console.WriteLine(tasks[1].ToString());

        // 3 задание
        string path = @"C:\Users\m2304511";
        string folder = "Control work";
        path = Path.Combine(path, folder);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string file1 = "cw2_1.json";
        string file2 = "cw2_2.json";

        file1 = Path.Combine(path, file1);
        file2 = Path.Combine(path, file2);

        // 4 задание
        if (!File.Exists(file2))
        {
            JsonMode.Write<Task1>(tasks[0] as Task1, file1);
            JsonMode.Write<Task2>(tasks[1] as Task2, file2);
        }
        else
        {
            var peremenaya1 = JsonMode.Read<Task1>(file1);
            var peremenaya2 = JsonMode.Read<Task2>(file2);
            Console.WriteLine(peremenaya1);
            Console.WriteLine(peremenaya2);
        }
    }
}











