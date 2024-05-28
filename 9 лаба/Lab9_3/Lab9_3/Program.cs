using lab_9_3.Serialization;
using System.Xml.Serialization;

[XmlInclude(typeof(WomanTeam))]
[XmlInclude(typeof(ManTeam))]
public class Team
{
    public string _name;
    public double[] _results = new double[6];
    public int _count = 0;
    public int _bestteam = 0;
    public Team(string name, double[] results)
    {
        _name = name;
        _results = results;
        for (int i = 0; i < _results.Length; i++)
        {
            for (int j = 1; j <= 5; j++)
            {
                if (results[i] == j)
                {
                    _count += 6 - j;
                }
            }
            if (results[i] == 1)
            {
                _bestteam = 1;
            }
        }
    }
    public Team() { }
    public int bestteam { get { return _bestteam; } }
    public int count { get { return _count; } }
    public void Print(string text = "Некорректная информация")
    {
        if (_name != null)
        {
            text = _name + " " + _count;
        }
        Console.WriteLine(text);
    }
}
public class WomanTeam : Team
{
    public WomanTeam(string name, double[] results) : base(name, results)
    {
        _name = "женская команда: " + name;
    }

    public WomanTeam() { }
}
public class ManTeam : Team
{
    public ManTeam(string name, double[] results) : base(name, results)
    {
        _name = "мужская команда: " + name;
    }

    public ManTeam() { }
}
class Program
{
    static void Main()
    {
        WomanTeam[] womanTeam = new WomanTeam[3];
        double[] result_woman = new double[6];
        string name_woman;
        for (int i = 0; i < womanTeam.Length; i++)
        {
            Console.WriteLine("Введите название женской команды:");
            name_woman = Console.ReadLine();
            Console.WriteLine("Введите места участниц команды:");
            for (int j = 0; j < 6; j++)
            {
                result_woman[j] = double.Parse(Console.ReadLine());
            }
            womanTeam[i] = new WomanTeam(name_woman, result_woman);
        }
        ManTeam[] manteam = new ManTeam[3];
        double[] results_man = new double[6];
        string name_man;
        for (int i = 0; i < manteam.Length; i++)
        {
            Console.WriteLine("Введите название мужской команды:");
            name_man = Console.ReadLine();
            Console.WriteLine("Введите места участников команды:");
            for (int j = 0; j < 6; j++)
            {
                results_man[j] = double.Parse(Console.ReadLine());
            }
            manteam[i] = new ManTeam(name_man, results_man);
        }
        Team[] team = new Team[6];
        for (int i = 0; i < manteam.Length; i++)
        {
            team[i] = manteam[i];
        }
        for (int i = 0; i < womanTeam.Length; i++)
        {
            team[i + 3] = womanTeam[i];
        }
        for (int i = 0; i < team.Length; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (team[j].count < team[j + 1].count || (team[j].count == team[j + 1].count && team[j].bestteam < team[j + 1].bestteam))
                {
                    Team t = team[j];
                    team[j] = team[j + 1];
                    team[j + 1] = t;
                }
            }
        }
        

        #region FilePath
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folder = "FilesForLab9";
        path = Path.Combine(path, folder);
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string[] file_names = new string[2]
        {
            "9.3.json",
            "9.3.xml"
        };
        #endregion

        #region Serialization
        SerializeManager[] serializers = new SerializeManager[2]
        {
            new JsonMySerializer(),
            new XmlMySerializer()
        };

        for (int i = 0; i < serializers.Length; i++)
        {
            serializers[i].Write(team[0], Path.Combine(path, file_names[i]));
        }

        for (int i = 0; i < serializers.Length; i++)
        {
            team[0] = serializers[i].Read<Team>(Path.Combine(path, file_names[i]));
            Console.WriteLine($"Победитель - {team[0]._name} {team[0]._count}");
        }
        #endregion
    }
}