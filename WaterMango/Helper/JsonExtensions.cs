using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WaterMango.DAL.Entity;
using WaterMango.Model;

namespace WaterMango.Helper
{
    public static class JsonExtensions
    {
        public static string toJson(this PlantModel model)
        {
            return JsonSerializer.Serialize(model);
        }
        public static string toJson(this List<PlantModel> models)
        {
            return JsonSerializer.Serialize(models);
        }

        public static string toJson(this WateringHistoryModel model)
        {
            return JsonSerializer.Serialize(model);
        }
        public static string toJson(this List<WateringHistoryModel> models)
        {
            return JsonSerializer.Serialize(models);
        }


        public static string toJson(this Plant model)
        {
            return JsonSerializer.Serialize(model);
        }
        public static string toJson(this List<Plant> models)
        {
            return JsonSerializer.Serialize(models);
        }

        public static string toJson(this WateringHistory model)
        {
            return JsonSerializer.Serialize(model);
        }
        public static string toJson(this List<WateringHistory> models)
        {
            return JsonSerializer.Serialize(models);
        }
    }
}
