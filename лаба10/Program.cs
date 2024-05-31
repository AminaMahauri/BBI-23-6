using System;
using System.IO;
using System.Text.Json;

namespace Lab_Lerok
{
    public class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var crops = new Crop[]
            {
                new Crop("Wheat", 1, "Food"),
                new Crop("Corn", 2, "Food"),
                new Crop("Barley", 3, "Food")
            };
            var harvestSeasons = new CropHarvest[3];
            for (int season = 0; season < 3; season++)
            {
                var cropArray = new Crop[15];
                var dateArray = new DateTime[15];
                var quantityArray = new double[15];
                for (int day = 0; day < 15; day++)
                {
                    cropArray[day] = crops[random.Next(crops.Length)];
                    dateArray[day] = DateTime.Now.AddDays(-day - (season * 15));
                    quantityArray[day] = random.Next(100, 1000);
                }
                var cropHarvest = new CropHarvest(cropArray, dateArray, quantityArray, random.Next(1, 10));
                harvestSeasons[season] = cropHarvest;
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(harvestSeasons, options);
            File.WriteAllText("raw_data.json", jsonString);
            Console.WriteLine("Data saved to raw_data.json");

            Crop[] crops2 =
            {
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
            CropHarvest ch = new CropHarvest(crops2, dt, q, 5); // Example price per one
            ch.DisplayStatistic();
            Console.WriteLine($"Цена всего урожая: {ch.GetAllYield() * ch.PricePerOne}");

            MyXMLSerializer xml = new MyXMLSerializer();
            xml.Write(ch, "raw_data.xml");
            HarvestStatistic raw_hs = xml.Read("raw_data.xml");
            // raw_hs.DisplayStatistic();  // ВНИМАНИЕ! Пока нет XML, то он будет так срабатывать
        }
    }

    public class MyXMLSerializer
    {
        public void Write(HarvestStatistic hs, string fileName)
        {
            // Реализация метода записи в XML
        }

        public HarvestStatistic Read(string fileName)
        {
            // Реализация метода чтения из XML
            return null;
        }
    }
}
