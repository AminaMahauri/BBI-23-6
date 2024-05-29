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

        /*
        public HarvestStatisticXML() { }
        public HarvestStatisticXML(Crop[] crop, DateTime[] dt, double[] quantity)
        {
            this.MyCrop = crop;
            this.HarvestDate = dt;
            this.Quantity = quantity;
            this.size = Math.Min(Math.Min(MyCrop.Length, HarvestDate.Length), Quantity.Length);
        }
        */
    }

    abstract class MySerializer
    {
        public abstract HarvestStatistic Read(string filename);
        public abstract void Write(HarvestStatistic harvestStatistics, string filename);
    }

    class MyXMLSerializer : MySerializer
    {
        public override HarvestStatistic Read(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HarvestStatisticXML));

            using (FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                HarvestStatisticXML xml = (HarvestStatisticXML)serializer.Deserialize(fileStream);
                CropHarvest hs = new CropHarvest(xml.MyCrop, xml.HarvestDate, xml.Quantity);
                return hs;
            }
        }

        public override void Write(HarvestStatistic harvestStatistics, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HarvestStatisticXML));

            HarvestStatisticXML xml_hs = new HarvestStatisticXML
            {
                MyCrop = harvestStatistics.MyCrop,
                HarvestDate = harvestStatistics.HarvestDate,
                Quantity = harvestStatistics.Quantity
            };

            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(fileStream, xml_hs);
            }
        }
    }
}
