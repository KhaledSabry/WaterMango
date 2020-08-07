import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { DatePipe } from '@angular/common';
import Swal from 'sweetalert2/dist/sweetalert2.js';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  timeOut: 5000;
  displayProgressSpinner = false;
  spinnerWithoutBackdrop = false;
  constructor(
    private toastr: ToastrService,
    private SpinnerService: NgxSpinnerService,
    public datepipe: DatePipe
  ) {}

  groupBy(array, key) {
    if (array) {
      // Return the end result
      return array.reduce((result, currentValue) => {
        // If an array already present for key, push it to the array. Else create an array and push the object
        (result[currentValue[key]] = result[currentValue[key]] || []).push(
          currentValue
        );
        // Return the current iteration `result` value, this will be taken as next iteration `result` value and accumulate
        return result;
      }, {}); // empty object is the initial value for result object
    } else return null;
  }

  setBusy(isBusy) {
    if (isBusy) this.SpinnerService.show();
    else this.SpinnerService.hide();
  }

  //////////////////toastr/////////////////////////
  notificationSuccess(title, message) {
    this.toastr.success(message, title, {
      timeOut: this.timeOut,
      enableHtml: true,
      positionClass: 'toast-bottom-right',
    });
  }
  notificationError(title, message) {
    this.toastr.error(message, title, {
      timeOut: this.timeOut,
      enableHtml: true,
      positionClass: 'toast-bottom-right',
    });
  }
  notificationWarning(title, message) {
    this.toastr.warning(message, title, {
      timeOut: this.timeOut,
      enableHtml: true,
      positionClass: 'toast-bottom-right',
    });
  }
  notificationInfo(title, message) {
    this.toastr.info(message, title, {
      timeOut: 5000,
      enableHtml: true,
      positionClass: 'toast-bottom-right',
      extendedTimeOut: 1000,
    });
  }

  ///////////sweet alert///////////
  alertSuccess(title, message) {
    Swal.fire({
      title: title,
      html: message,
      icon: 'success',
    });
  }
  alertFailed(title, message) {
    Swal.fire({
      title: title,
      text: message,
      icon: 'error',
    });
  }
}
