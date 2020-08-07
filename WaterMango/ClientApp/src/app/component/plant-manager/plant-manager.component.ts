import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { PlantModel } from 'src/app/_models/plantModel';
import { NgxSpinnerService } from 'ngx-spinner';
import { MyConfig } from './../../../myConfig';
import { PlantService } from 'src/app/services/plant.service';
import { plantManager_viewModel } from './plant-manager.viewModel';
import { BehaviorSubject, timer, interval } from 'rxjs';
import {
  switchMap,
  takeUntil,
  catchError,
  finalize,
  takeWhile,
  delay,
} from 'rxjs/operators';
import { CommonService } from 'src/app/_helpers/common';

@Component({
  selector: 'plant-manager',
  templateUrl: './plant-manager.component.html',
  styleUrls: ['./plant-manager.component.css'],
})
export class PlantManagerComponent implements OnInit, OnDestroy {
  @Input('model')
  model: PlantModel;
  plant: plantManager_viewModel;

  maxWateringPeriod = MyConfig.wateringPeriod;
  restLastWatering = MyConfig.restLastWatering;
  wateringAlert = MyConfig.wateringAlert;

  disableTime = 0;
  autoStopinterval;
  btnDisapleInterval;
  alertTimer;

  constructor(
    //private spinner: NgxSpinnerService,
    private plantService: PlantService,
    private common: CommonService
  ) {}
  ngOnInit(): void {
    this.plant = new plantManager_viewModel(this.model);
    if (this.plant.wateredLastTime) {
      var dif =
        new Date().getTime() -
        Date.parse(this.plant.wateredLastTime.toString());
      if (this.wateringAlert - dif) {
        this.alertTimer = timer(this.wateringAlert - dif).subscribe(() => {
          this.raiseAlert();
        });
      } else {
        this.raiseAlert();
      }
    }
  }
  ngOnDestroy() {
    if (this.btnDisapleInterval) this.btnDisapleInterval.unsubscribe();
    if (this.autoStopinterval) this.autoStopinterval.unsubscribe();
    if (this.alertTimer) this.alertTimer.unsubscribe();
  }
  get btnWateringLabel() {
    if (this.toggleBtnDisabled) {
      return 'Enabled after ' + this.disableTime + ' seconds';
    } else {
      if (this.plant.isWatering) {
        return `Stop Watering (${
          this.maxWateringPeriod - this.plant.wateringtime
        })`;
      } else return 'Start Watering';
    }
  }
  get toggleBtnDisabled() {
    return this.disableTime > 0;
  }

  raiseAlert() {
    this.common.notificationError(
      'Watering Alert',
      `Plant '${this.plant.name}' should be watered immediately.`
    );
    this.plant.waterAlert = true;
  }

  async startTimer() {
    await this.plant.setWateringStatus(this.plantService, true);
    this.autoStopinterval = interval(1000)
      .pipe(
        delay(0),
        finalize(async () => {
          await this.stopTimer();
        }),
        takeWhile(() => this.plant.wateringtime <= this.maxWateringPeriod - 1)
      )
      .subscribe((n) => {
        this.plant.wateringtime = n;
      });
  }

  async stopTimer() {
    await this.plant.setWateringStatus(this.plantService, false);

    this.alertTimer = timer(this.wateringAlert).subscribe(() => {
      this.raiseAlert();
    });

    this.disableTime = this.restLastWatering;
    this.btnDisapleInterval = interval(1000)
      .pipe(
        delay(0),
        finalize(() => {
          this.disableTime = 0;
        }),
        takeWhile(() => this.disableTime > 0)
      )
      .subscribe((n) => {
        this.disableTime = this.restLastWatering - n;
      });
  }

  async toggleWateringClick(event: MouseEvent) {
    event.stopPropagation();
    event.preventDefault();

    await this.toggleWatering();
  }
  async toggleWatering() {
    //this.spinner.show();
    if (this.plant.isWatering) {
      this.autoStopinterval.unsubscribe();
    } else {
      await this.startTimer();
    }
    //this.spinner.hide();
  }
}
