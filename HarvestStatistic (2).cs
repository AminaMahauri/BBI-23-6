using Lab_Lerok;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab_Lerok
{
    public class Crop
    {
        public string CropName { get; set; }
        public Crop(string name)
        {
            CropName = name;
        }

        public override string ToString()
        {
            return $"Crop: {CropName}";
        }
    }

    abstract partial class HarvestStatistic
    {
        public Crop[] MyCrop { protected set; get; }
        public DateTime[] HarvestDate { protected set; get; }
        public double[] Quantity { protected set; get; }
        public int size { protected set; get; }

        [JsonConstructor]
        public HarvestStatistic(Crop[] crop, DateTime[] dt, double[] quantity)
        {
            MyCrop = crop;
            HarvestDate = dt;
            Quantity = quantity;
            size = Math.Min(Math.Min(MyCrop.Length, HarvestDate.Length), Quantity.Length);
        }

        public virtual void DisplayStatistic()
        {
            Console.WriteLine(this.ToString());
        }

        public double GetYieldByCrop(Crop crop)
        {
            double res = 0;
            for (int i = 0; i < size; i++)
            {
                if (MyCrop[i].CropName == crop.CropName)
                {
                    res += Quantity[i];
                }
            }
            return res;
        }

        public double GetYieldByCrop(string cropName)
        {
            double res = 0;
            for (int i = 0; i < size; i++)
            {
                if (MyCrop[i].CropName == cropName)
                {
                    res += Quantity[i];
                }
            }
            return res;
        }

        public override string ToString()
        {
            string res = "Статистика по урожаю:\n";
            for (int i = 0; i < size; i++)
            {
                res += MyCrop[i].ToString() + "\n";
                res += $"Дата урожая: {HarvestDate[i]}, Количество: {Quantity[i]}кг.\n\n";
            }
            return res;
        }
    }

    abstract partial class HarvestStatistic : IHarvestCalculator
    {
        public double GetAllYield()
        {
            double res = 0;
            for (int i = 0; i < size; i++)
            {
                res += Quantity[i];
            }
            return res;
        }

        public double GetAvgYield()
        {
            DateTime firstDay = HarvestDate[0];
            DateTime lastDay = HarvestDate[^1];

            Console.WriteLine($"Самый первый день: {firstDay}");
            Console.WriteLine($"Самый последний день: {lastDay}");

            int totalQuantity = 0;
            foreach (double quantity in Quantity)
            {
                totalQuantity += Convert.ToInt32(quantity);
            }
            Console.WriteLine($"{totalQuantity} единиц собрано");

            return totalQuantity / HarvestDate.Length;
        }

        public double GetYield()
        {
            double res = 0;
            for (int i = 0; i < size; i++)
            {
                res += Quantity[i];
            }
            return res;
        }

        public double GetYieldPerDay()
        {
            DateTime firstDay = HarvestDate[0];
            DateTime lastDay = HarvestDate[^1];

            Console.WriteLine($"Самый первый день: {firstDay}");
            Console.WriteLine($"Самый последний день: {lastDay}");

            int totalQuantity = 0;
            foreach (double quantity in Quantity)
            {
                totalQuantity += Convert.ToInt32(quantity);
            }
            Console.WriteLine($"{totalQuantity} единиц собрано");

            return totalQuantity / (lastDay - firstDay).Days;
        }
    }

    [Serializable]
    class CropHarvest : HarvestStatistic
    {
        public double pricePerOne { private set; get; }
        [JsonConstructor]
        public CropHarvest(Crop[] crop, DateTime[] dt, double[] quantity, double pricePerOne = 1) : base(crop, dt, quantity)
        {
            this.pricePerOne = pricePerOne;
        }
        public override void DisplayStatistic()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            string res = "Статистика по собранному урожаю:\n";
            for (int i = 0; i < size; i++)
            {
                res += MyCrop[i].ToString() + "\n";
                res += $"Дата урожая: {HarvestDate[i]}, Количество: {Quantity[i]}кг., Цена: {Quantity[i] * pricePerOne}";
            }
            res += $"Общая сумма: {(GetYield() * pricePerOne).ToString()}";
            return res;
        }
    }

    [Serializable]
    class CropYield : HarvestStatistic
    {
        public CropYield(Crop[] crop, DateTime[] dt, double[] quantity) : base(crop, dt, quantity)
        {
        }
        public override void DisplayStatistic()
        {
            Console.WriteLine("Здесь будет текст");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var crops = new Crop[] { new Crop("Wheat"), new Crop("Corn"), new Crop("Barley") };

            var harvestSeasons = new List<CropHarvest>();

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
                harvestSeasons.Add(cropHarvest);
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(harvestSeasons, options);

            File.WriteAllText("raw_data.json", jsonString);

            Console.WriteLine("Data saved to raw_data.json");
        }
    }

    interface IHarvestCalculator
    {
        double GetAllYield();
        double GetAvgYield();
        double GetYield();
        double GetYieldPerDay();
    }
}