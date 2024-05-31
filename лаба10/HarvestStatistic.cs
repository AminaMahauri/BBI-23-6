using System;
using System.Text.Json.Serialization;

namespace Lab_Lerok
{
    public abstract class HarvestStatistic
    {
        public Crop[] MyCrop { protected set; get; }
        public DateTime[] HarvestDate { protected set; get; }
        public double[] Quantity { protected set; get; }
        protected int size { private set; get; }


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
            Console.WriteLine(this);
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
                res += $"Дата урожая: {HarvestDate[i]}, Количество: {Quantity[i]} кг.\n\n";
            }
            return res;
        }
    }

    public abstract class HarvestStatisticWithCalculator : HarvestStatistic, IHarvestCalculator
    {
        public HarvestStatisticWithCalculator(Crop[] crop, DateTime[] dt, double[] quantity) : base(crop, dt, quantity) { }

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

            double totalQuantity = 0;
            foreach (double quantity in Quantity)
            {
                totalQuantity += quantity;
            }
            Console.WriteLine($"{totalQuantity} единиц собрано");

            return totalQuantity / HarvestDate.Length;
        }

        public double GetYield()
        {
            throw new NotImplementedException(); // Нужно было эту штуку добавить
        }

        public double GetYieldPerDay()
        {
            DateTime firstDay = HarvestDate[0];
            DateTime lastDay = HarvestDate[^1];

            Console.WriteLine($"Самый первый день: {firstDay}");
            Console.WriteLine($"Самый последний день: {lastDay}");

            double totalQuantity = 0;
            foreach (double quantity in Quantity)
            {
                totalQuantity += quantity;
            }
            Console.WriteLine($"{totalQuantity} единиц собрано");

            return totalQuantity / (lastDay - firstDay).Days;
        }
    }

    [Serializable]
    class CropHarvest : HarvestStatisticWithCalculator
    {
        public double PricePerOne { private set; get; }

        [JsonConstructor]
        public CropHarvest(Crop[] crop, DateTime[] dt, double[] quantity, double pricePerOne = 1) : base(crop, dt, quantity)
        {
            this.PricePerOne = pricePerOne;
        }
        public override void DisplayStatistic()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            string res = "Статистика по собранному урожаю:\n";
            for (int i = 0; i < size; i++)
            {
                res += MyCrop[i].ToString() + "\n";
                res += $"Дата урожая: {HarvestDate[i]}, Количество: {Quantity[i]} кг., Цена: {Quantity[i] * PricePerOne}$\n";
            }
            res += $"Общая сумма: {(GetAllYield() * PricePerOne).ToString()}";
            return res;
        }
    }

    [Serializable]
    class CropYield : HarvestStatisticWithCalculator
    {
        public CropYield(Crop[] crop, DateTime[] dt, double[] quantity) : base(crop, dt, quantity) { }

        public override void DisplayStatistic()
        {
            Console.WriteLine("Здесь будет текст");
        }
    }
}
