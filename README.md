# WaterMango

This project build on .net Core 3.1 , Angular 9 and Visual Studio 2019 
Store the data in a local Json file (for the sake of simplicity)

To Lunch the website:
1-	Please make sure you have installed node and npm in your system. You can check your node version and npm version by using the following command:

node --version
npm --version

 if you don’t have it install from  https://nodejs.org/en/download/
 
2-	Install angular cli

npm install - g @angular/cli

3-	Open the solution  file \WaterMango.sln and run the project to Build the backend APIs. 

4-	Make sure that ‘baseurl’  in the file ~\ WaterMango\ClientApp\src\myConfig.ts  contains the base url address of the backend APIs (build in the previous step) . 

5-	from the command line navigate to ~\ WaterMango\ClientApp and compile the Angular project using this command: 

 ng serve
 
6-	Open the Website from http://localhost:4200/ 



