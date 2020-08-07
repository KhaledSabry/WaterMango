using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterMango.BAL.Mapper;
using WaterMango.DAL.Entity;
using WaterMango.DAL.Repository;
using WaterMango.Helper;
using WaterMango.Model;

namespace WaterMango.BAL
{
    public class PlantSystem
    {
        private readonly Settings _configs;
        public PlantSystem(Settings configs )
        { 
            _configs = configs;
        }

        public async Task<List<PlantModel>> getAll( )
        { 
            var databaseManagment = new databaseRepository(_configs);

            List<Plant> plants = await databaseManagment.LoadAll();

            var result = plants.Select(a=>PlantMapper.toModel(a));
            return result.ToList();

        }
        public async Task<WateringHistoryModel> addWateringRecored(Guid plantToken ,int duration)
        {
            var databaseManagment = new databaseRepository(_configs);

            WateringHistoryModel model = new WateringHistoryModel() { token = Guid.NewGuid(), time = DateTime.Now, duration = duration };
            WateringHistory entity = WateringHistoryMapper.toEntity(model);

            WateringHistory result = await databaseManagment.append(plantToken, entity);
             
            return WateringHistoryMapper.toModel(result);

        }
        public void restDatabase()
        {
            var databaseManagment = new databaseRepository(_configs);
            databaseManagment.restDatabase(); 
        }
    }
}
