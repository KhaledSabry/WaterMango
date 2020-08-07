using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterMango.DAL.Entity;
using WaterMango.Model;

namespace WaterMango.BAL.Mapper
{
    public class PlantMapper
    {
        public static PlantModel toModel(Plant entity)
        {
            PlantModel model = new PlantModel();
            if (entity != null)
            {
                model.token = entity.token;
                model.name = entity.name;
                model.location = entity.location;
                model.wateringHistory = entity.wateringHistory == null ? new List<WateringHistoryModel>() : entity.wateringHistory.Select(a => WateringHistoryMapper.toModel(a)).ToList();
                model.isWatering = entity.isWatering;
                model.imageUrl = entity.imageUrl;
            }
            return model;
        }
        public static Plant toEntity(PlantModel model)
        {
            Plant entity = new Plant();
            if (model != null)
            {
                entity.token = model.token;
                entity.name = model.name;
                entity.location = model.location;
                entity.wateringHistory = model.wateringHistory == null ? new List<WateringHistory>() : model.wateringHistory.Select(a => WateringHistoryMapper.toEntity(a)).ToList();
                entity.isWatering = model.isWatering;
                entity.imageUrl = model.imageUrl;
            }
            return entity;
        }
    }
}
