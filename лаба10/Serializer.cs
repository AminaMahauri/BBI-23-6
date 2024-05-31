using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Lab_Lerok
{
    public class HarvestStatisticXML
    {
        public Crop[] MyCrop { set; get; }
        public DateTime[] HarvestDate { set; get; }
        public double[] Quantity { set; get; }
        [XmlIgnore]
        public int Size { get; set; }

        public HarvestStatisticXML() { }

        public HarvestStatisticXML(Crop[] crop, DateTime[] dt, double[] quantity)
        {
            this.MyCrop = crop;
            this.HarvestDate = dt;
            this.Quantity = quantity;
            this.Size = Math.Min(Math.Min(MyCrop.Length, HarvestDate.Length), Quantity.Length);
        }
    }

    public abstract class MySerializer
    {
        public abstract void Write(HarvestStatisticXML[] harvestStatistics, string filename);
        public abstract HarvestStatisticXML[] Read(string filename);
    }

    public class MyJsonSerializer : MySerializer
    {
        public override void Write(HarvestStatisticXML[] harvestStatistics, string filename)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(harvestStatistics, options);
            File.WriteAllText(filename, jsonString);
        }

        public override HarvestStatisticXML[] Read(string filename)
        {
            var jsonString = File.ReadAllText(filename);
            return JsonSerializer.Deserialize<HarvestStatisticXML[]>(jsonString);
        }
    }

    public class MyXmlSerializer : MySerializer
    {
        public override void Write(HarvestStatisticXML[] harvestStatistics, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HarvestStatisticXML[]));
            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(fileStream, harvestStatistics);
            }
        }

        public override HarvestStatisticXML[] Read(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HarvestStatisticXML[]));
            using (FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                return (HarvestStatisticXML[])serializer.Deserialize(fileStream);
            }
        }
    }

}
