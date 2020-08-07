import { Injectable } from '@angular/core';
import { PlantModel } from '../_models/plantModel';
import { WateringHistory } from '../_models/wateringHistory';
import { HttpRqstService } from './http-rqst.service';

@Injectable({
  providedIn: 'root',
})
export class PlantService {
  constructor(private http: HttpRqstService) {}

  public async getAll(): Promise<PlantModel[]> {
    const data = await this.http.get(`/api/plant/getall`).toPromise();
    return data.value as PlantModel[];
  }

  public async startWatring(plantToken: string): Promise<any> {
    const response = await this.http
      .get(`/api/plant/watering/${plantToken}/start/`)
      .toPromise();
    let data = response;
  }

  public async stopWatring(
    plantToken: string,
    wateringPeriod: number
  ): Promise<WateringHistory> {
    const response = await this.http
      .get(`/api/plant/watering/${plantToken}/stop/${wateringPeriod}/`)
      .toPromise();
    let data = response;

    return data as WateringHistory;
  }
  public async resetDatabase(): Promise<any> {
    const response = await this.http
      .get('/api/plant/reset/database')
      .toPromise();
    let data = response;
    return data;
  }
}
