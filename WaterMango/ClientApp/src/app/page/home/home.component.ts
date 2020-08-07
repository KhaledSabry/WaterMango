import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { PlantService } from './../../services/plant.service';
import { CommonService } from 'src/app/_helpers/common';
import { PlantModel } from 'src/app/_models/plantModel';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  plantsData: PlantModel[];
  constructor(
    private spinner: NgxSpinnerService,
    private plantService: PlantService
  ) {
    this.plantsData = [];
  }

  async ngOnInit() {
    this.spinner.show();
    await this.loadData();
    this.spinner.hide();
  }
  async loadData() {
    this.plantsData = await this.plantService.getAll();
  }

  async resetDatabase(e) {
    this.spinner.show();
    await this.plantService.resetDatabase();
    await this.loadData();
    this.spinner.hide();
  }
}
