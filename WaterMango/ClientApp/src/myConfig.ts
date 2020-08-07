export class MyConfig {
  static baseurl = 'http://localhost:64577'; //window.location.href.split('/')[0]; //backend APIs base url
  static wateringPeriod = 10; //in seconds
  static restLastWatering = 30; //in seconds
  static wateringAlert = 6 * 60 * 60 * 1000; //in mileseconds
}
