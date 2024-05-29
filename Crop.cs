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
        public string CropName { set; get; }
        public int Season { set; get; }
        public string Purpose { set; get; }

        // Конструктор
        [JsonConstructor]
        public Crop() { }
        public Crop(string name, int season, string purpose)
        {
            this.CropName = name;
            this.Season = season;
            this.Purpose = purpose;
        }

        public override string ToString()
        {
            return $"Название: {CropName}, выращен в {Season}, назначение: {Purpose}";
        }
    }
}
