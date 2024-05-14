# Dating-Social
## About

- Install [Node.js (>=17.0.0 )](http://nodejs.org/)
- Install npm 8+
- Install Visual Studio Code 
- Install Visual Studio
## Technology
- Net Core 8
- Angular 16
- Mssql 14+
- Boostrap
- ...
## Installation

Install through git:

```
git init
git remote add origin https://github.com/ManagerCoPilot/Dating-Social.git
git pull origin develop
```

## Development server
I. Handle in Backend .Net (Visual studio)
1. Change default connection string for right with your local computer in appsettings.Development.json file:
```
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=WebDate;User Id=<your_username>;password=<your_password>;Integrated Security=True;Trust Server Certificate=True"
}
```
2. Update database through ef, open `Package Manager Console` and run command:
```
Update-Database
```
3. Run backend

II. Handle in Fontend Angular (Visual studio code)
1. Open client folder have route ...Try-Hard\Client\client\ with visual studio code, and Install npm through terminal:
```
npm i
npm install -g @angular/cli
```
2. Run Fontend by command:
```
ng serve -o
```
