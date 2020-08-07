using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using WaterMango.Helper; 
using System.IO;
using WaterMango.DAL.Entity;

namespace WaterMango.DAL.Repository
{
    public class databaseRepository
    {
        private readonly Settings _configs;
        public databaseRepository(Settings configs)
        {
            this._configs = configs;
        }
        public async Task<List<Plant>> LoadAll()
        {
            string jsonString = "";
            List<Plant> result = new List<Plant>();
            if (File.Exists(this._configs.dbJsonPath))
            {
                using (StreamReader sr = new StreamReader(this._configs.dbJsonPath))
                {
                    jsonString = await sr.ReadToEndAsync();
                }
                if (!string.IsNullOrEmpty(jsonString))
                    result = JsonSerializer.Deserialize<List<Plant>>(jsonString);
            }
                return result; 
        }
        public async Task<Plant> append(Plant plantData)
        {
            List<Plant> contents =await this.LoadAll();
            contents.Add(plantData);
            string jsonString = contents.toJson();
            WriteToFile(jsonString);
            return plantData;
        }
        public async Task<WateringHistory> append(Guid PlantToken, WateringHistory historyData)
        {
            List<Plant> contents = await this.LoadAll();
            Plant plantRecord = contents.Where(a => a.token == PlantToken).First();
            if (plantRecord.wateringHistory == null) plantRecord.wateringHistory = new List<WateringHistory>();
            plantRecord.wateringHistory.Add(historyData); 
            string jsonString = contents.toJson();
            WriteToFile( jsonString);
            return historyData;
        }
        public void restDatabase()
        {
            List<Plant> contents = new List<Plant>()
            {
                new Plant(){
                    token= Guid.NewGuid(),
                    name= "Cactus",
                    location= "near to main door",
                    wateringHistory= new List<WateringHistory>(),
                    isWatering= false,
                    imageUrl=
                      "https://assets.eflorist.com/assets/products/PHR_/TPL11-1A.jpg",
                  },
                  new Plant(){
                    token=Guid.NewGuid(),
                    name= "Jade Plant",
                    location= "near to backdoor",
                    wateringHistory=  new List<WateringHistory>(),
                    isWatering= false,
                    imageUrl=
                      "https://images.crateandbarrel.com/is/image/Crate/PottedJadePlantSHF16/?$web_product_hero$&190411135345&wid=625&hei=625",
                  },
                  new Plant(){
                    token= Guid.NewGuid(),
                    name= "Fiddle Leaf Fig",
                    location= "near to john office",
                    wateringHistory= new List<WateringHistory>(),
                    isWatering= false,
                    imageUrl=
                      "https://www.shelmerdine.com/wp-content/uploads/2019/04/Fiddlen-Leaf-Fig-Tree.jpg",
                  },
                  new Plant(){
                    token= Guid.NewGuid(),
                    name= "Succulent",
                    location= "near to basement window",
                    wateringHistory= new List<WateringHistory>(),
                    isWatering= false,
                    imageUrl=
                      "https://assets.eflorist.com/assets/products/PHR_/TPL11-1A.jpg",
                  },
                  new Plant(){
                    token=Guid.NewGuid(),
                    name= "Zebra Plant",
                    location= "near to sarah's office",
                    wateringHistory= new List<WateringHistory>(),
                    isWatering= false,
                    imageUrl=
                      "https://static1.squarespace.com/static/599ca6a3e4fcb572c84c0670/59a17759e4fcb5244a7b375e/5a32a41eec212d09e4011c89/1528757182830/FullSizeRender.jpg?format=1500w",
                  }

            };
            WriteToFile(contents.toJson());
        }
        private void WriteToFile(string jsonString)
        { 
            System.IO.File.WriteAllText(this._configs.dbJsonPath, jsonString);
        }
    }
}
