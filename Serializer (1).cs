using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_Lerok
{
    public class HarvestStatisticXML
    {
        public Crop[] MyCrop { set; get; }
        public DateTime[] HarvestDate { set; get; }
        public double[] Quantity { set; get; }

        
        public HarvestStatisticXML() { }
        public HarvestStatisticXML(Crop[] crop, DateTime[] dt, double[] quantity)
        {
            this.MyCrop = crop;
            this.HarvestDate = dt;
            this.Quantity = quantity;
            this.size = Math.Min(Math.Min(MyCrop.Length, HarvestDate.Length), Quantity.Length);
        }
        
    }
    public abstract class MySerializer
    {
        public abstract void Write(HarvestStatistic[] harvestStatistics, string filename);
        public abstract HarvestStatistic[] Read(string filename);
    }

    public class MyJsonSerializer : MySerializer
    {
        public override void Write(HarvestStatistic[] harvestStatistics, string filename)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(harvestStatistics, options);
            File.WriteAllText(filename, jsonString);
        }

        public override HarvestStatistic[] Read(string filename)
        {
            var jsonString = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<HarvestStatistic[]>(jsonString);
        }
    }
    public class MyXmlSerializer : MySerializer
    {
        public override void Write(HarvestStatistic[] harvestStatistics, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HarvestStatistic[]));
            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(fileStream, harvestStatistics);
            }
        }

        public override HarvestStatistic[] Read(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HarvestStatistic[]));
            using (FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                return (HarvestStatistic[])serializer.Deserialize(fileStream);
            }
        }
    }
}
