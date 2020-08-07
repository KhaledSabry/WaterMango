import { PlantModel } from './../../_models/plantModel';
import { WateringHistory } from '../../_models/wateringHistory';
import { PlantService } from 'src/app/services/plant.service';

export class plantManager_viewModel {
  token: string;
  name: string;
  location: string;
  wateringHistory: WateringHistory[];
  isWatering: boolean;
  imageUrl: string;

  wateringtime: number;
  waterAlert: boolean;

  constructor(model: PlantModel) {
    this.token = model.token;
    this.name = model.name;
    this.location = model.location;
    this.wateringHistory = model.wateringHistory;
    this.isWatering = model.isWatering;
    this.imageUrl = model.imageUrl;

    this.wateringtime = 0;
    this.waterAlert = false;
  }

  get wateredLastTime(): Date {
    if (this.wateringHistory && this.wateringHistory.length > 0) {
      var array = this.wateringHistory.sort((a, b) => {
        return <any>new Date(b.time) - <any>new Date(a.time);
      });
      return array[0].time;
    } else return null;
  }

  async setWateringStatus(plantService: PlantService, isWatering: boolean) {
    this.isWatering = isWatering;
    if (this.isWatering) {
      this.waterAlert = false;
      await plantService.startWatring(this.token);
    } else {
      let historyItem = await plantService.stopWatring(
        this.token,
        this.wateringtime + 1
      );
      this.wateringHistory.push(historyItem);
    }

    this.wateringtime = 0;
  }
}
