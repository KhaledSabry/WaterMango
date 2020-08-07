import { WateringHistory } from './wateringHistory';
export class PlantModel {
  constructor() {
    this.wateringHistory = [];
    this.isWatering = false;
  }
  token: string;
  name: string;
  location: string;
  wateringHistory: WateringHistory[];
  isWatering: boolean;
  imageUrl: string;
}
