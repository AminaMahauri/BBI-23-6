using System;
using System.Text.Json.Serialization;

namespace Lab_Lerok
{
    public struct Crop
    {
        public string CropName { get; set; }
        public int Season { get; set; }
        public string Purpose { get; set; }

        public Crop() { }

        // Конструктор
        [JsonConstructor]
        public Crop(string cropName, int season, string purpose)
        {
            CropName = cropName;
            Season = season;
            Purpose = purpose;
        }

        public override string ToString()
        {
            return $"Название: {CropName}, выращен в {Season}, назначение: {Purpose}";
        }
    }
}
