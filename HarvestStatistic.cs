using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lab_Lerok
{
    abstract partial class HarvestStatistic
    {
        public Crop[] MyCrop { protected set; get; }
        public DateTime[] HarvestDate { protected set; get; }
        public double[] Quantity { protected set; get; }
        public int size { protected set; get; }

        // Конструктор
        [JsonConstructor]
        public HarvestStatistic(Crop[] crop, DateTime[] dt, double[] quantity)
        {
            this.MyCrop = crop;
            this.HarvestDate = dt;
            this.Quantity = quantity;
            this.size = Math.Min(Math.Min(MyCrop.Length, HarvestDate.Length), Quantity.Length);
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
                res = MyCrop[i].ToString() + "\n";
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

            // Подсчитываем общее количество культур
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

            // Подсчитываем общее количество культур
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
}
