using lab_9_2.Serialization;
public struct Athlet
{
    public string _surname { get;  set; }
    public double _attempt1 { get;  set; }
    public double _attempt2 { get;  set; }
    public double _attempt3 { get;  set; }

    public double Bestresult
    {
        get { return Math.Max(Math.Max(_attempt1, _attempt2), _attempt3); }
    }
    public Athlet(string surname, double attempt1, double attempt2, double attempt3)
    {
        _surname = surname; _attempt1 = attempt1; _attempt2 = attempt2; _attempt3 = attempt3;
    }

}
abstract class Discipline
{
    public string _namediscipline { get; private set; }

    protected Discipline(string namediscipline)
    {
        _namediscipline = namediscipline;
    }
}

class Longjump : Discipline
{
    public Longjump() : base("Прыжки в длину")
    {
    }
}

class Highjump : Discipline
{
    public Highjump() : base("Прыжки в высоту")
    {
    }
}


class Program
{
    static void Main()
    {
        Athlet[] results = new Athlet[5];
        results[0] = new Athlet("Пупкин", 5.5, 4.3, 6.5);
        results[1] = new Athlet("Котиков", 3.4, 4.4, 4.4);
        results[2] = new Athlet("Козлов", 4.0, 5.1, 4.9);
        results[3] = new Athlet("Волков", 5.0, 5.0, 5.0);
        results[4] = new Athlet("Рыбкин", 4.1, 3.9, 4.9);
        Discipline[] disciplines = new Discipline[]
        {
            new Longjump(),
            new Highjump()
        };

        Array.Sort(results, (r1, r2) => r2.Bestresult.CompareTo(r1.Bestresult));

        #region FilePath
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folder = "FilesForLab9";
        path = Path.Combine(path, folder);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string[] file_names = new string[2]
        {
            "9.2.json",
            "9.2.xml"
        };
        #endregion

        SerializeManager[] serializers = new SerializeManager[2]
        {
            new JsonMySerializer(),
            new XmlMySerializer()
        };


        for (int i = 0; i < serializers.Length; i++)
        {
            serializers[i].Write(results, Path.Combine(path, file_names[i]));
        }

        for (int i = 0; i < serializers.Length; i++)
        {
            results = serializers[i].Read<Athlet[]>(Path.Combine(path, file_names[i]));
            foreach (var result in results)
            {
                Console.WriteLine($"{result._surname}, {result.Bestresult} метров");
            }
        }

    }
}