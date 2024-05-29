using System;
using System.Net.Http.Json;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
// using Newtonsoft.Json;
// using ProtoBuf;

namespace Lab_Lerok
{
    public class Program
    {
        static void Main(string[] args)
        {
            Crop[] crops = {
                new Crop("Кукуруза", 9, "Еда"),
                new Crop("Картофель", 9, "Еда"),
                new Crop("Морковь", 10, "Еда"),
                new Crop("Свёкла", 11, "Еда"),
                new Crop("Капуста", 12, "Еда"),
                new Crop("Огурцы", 1, "Еда"),
                new Crop("Помидоры", 2, "Еда"),
                new Crop("Клубника", 3, "Еда и напитки"),
                new Crop("Малина", 4, "Еда и напитки"),
                new Crop("Яблоко", 5, "Еда")
            };

            DateTime[] dt =
            {
            new DateTime(2024, 6, 10),
            new DateTime(2024, 7, 10),
            new DateTime(2024, 8, 10),
            new DateTime(2024, 9, 10),
            new DateTime(2024, 10, 10),
            new DateTime(2024, 10, 11),
            new DateTime(2024, 10, 12),
            new DateTime(2024, 10, 13),
            new DateTime(2024, 10, 14),
            new DateTime(2024, 10, 15),
        };
            double[] q = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            CropHarvest ch = new CropHarvest(crops, dt, q);

            ch.DisplayStatistic();
            Console.WriteLine($"Цена всего урожая: {ch.GetAllYield()}");

            MyXMLSerializer xml = new MyXMLSerializer();
            xml.Write(ch, "raw_data.xml");
            HarvestStatistic raw_hs = xml.Read("raw_data.xml");
            raw_hs.DisplayStatistic();
        }
    }
}