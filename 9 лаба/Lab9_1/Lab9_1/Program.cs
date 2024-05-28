using lab_9_1.Serialization;

public class Athlet
{
    public string _surname { get; set; }
    public string _society { get; set; }
    public double _attempt1 { get;  set; }
    public double _attempt2 { get;  set; }
    public bool _disqualified { get;  set; }
    public Athlet(string surname, string society, double attempt1, double attempt2)
    {
        _surname = surname;
        _society = society;
        _attempt1 = attempt1;
        _attempt2 = attempt2;
        _disqualified = false;
    }

    public Athlet() { }
    public void Disqualify()
    {
        _disqualified = true;
        _attempt1 = 0;
        _attempt2 = 0;  
    }
    public double SumAttempt
    {
        get { return _attempt1 + _attempt2; }
    }
}
class Program
{
    static void Main()
    {
        Athlet[] results = new Athlet[5];
        results[0] = new Athlet("Пупкин", "А", 5.5, 4.3);
        results[1] = new Athlet("Котиков", "Б", 3.4, 3.0);
        results[2] = new Athlet("Волков", "В", 5.0, 5.0);
        results[3] = new Athlet("Козлов", "Г", 3.9, 4.8);
        results[4] = new Athlet("Рыбкин", "Д", 5.1, 5.3);
        results[2].Disqualify();

        Array.Sort(results, (r1, r2) => r2.SumAttempt.CompareTo(r1.SumAttempt));

        #region FilePath
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folder = "FilesForLab9";
        path = Path.Combine(path, folder);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string[] file_names = new string[2]
        {
            "9.1.json",
            "9.1.xml"
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
                Console.WriteLine($"{result._surname}, {result._society}, {result._attempt1} метров, {result._attempt2} метров");
            }
        }

    }

}
