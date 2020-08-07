using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterMango.DAL.Entity;
using WaterMango.Model;

namespace WaterMango.BAL.Mapper
{
    public class WateringHistoryMapper
    {
        public static WateringHistoryModel toModel(WateringHistory entity)
        {
            WateringHistoryModel model = new WateringHistoryModel();
            if (entity != null)
            {
                model.token = entity.token;
                model.time = entity.time;
                model.duration = entity.duration;
            }
            return model;
        }
        public static WateringHistory toEntity(WateringHistoryModel model)
        {
            WateringHistory entity = new WateringHistory();
            if (model != null)
            {
                entity.token = model.token;
                entity.time = model.time;
                entity.duration = model.duration;
            }
            return entity;
        }
    }
}
