<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<br />

  <h3 align="center">SQL Server on Linux + ASP.NET 5.0 stiebel-eltron-apiserver demo</h3>

  <p align="center">
    <br />
    <a href="https://github.com/marcflohrer/AspNetOnSqlServer"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/marcflohrer/AspNetOnSqlServer/issues/new/choose">Report Bug</a>
    ·
    <a href="https://github.com/marcflohrer/AspNetOnSqlServer/issues">Request Feature</a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

This project is intended to provide a more insightful presentation of the data provided by the original Stiebel Eltron dashboard.
On top it adds the missing performance factor chart (Arbeitszahl).

### Built With

This section lists major frameworks and projects that were used:

* [docker-compose](https://docs.docker.com/compose/)
* [docker](https://docs.docker.com/)
* [Asp.net](https://dotnet.microsoft.com/apps/aspnet)
* [SQL Server on Linux](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-overview?view=sql-server-ver15)
* [aspnetcore/src/Identity/samples/IdentitySample.Mvc/](https://github.com/dotnet/aspnetcore/tree/main/src/Identity/samples/IdentitySample.Mvc) that is [licensed under Apache 2.0](legal/aspnetcore/LICENSE) (Notice: Some files are changed.)
* [EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/)
* [HTML Agility Pack](https://github.com/zzzprojects/html-agility-pack)
* [Xunit](https://xunit.net/)
* [Cronos](https://github.com/HangfireIO/Cronos)
* [Chart.Js](https://github.com/chartjs)
* [JQuery](https://github.com/jquery/jquery)
* [Bootstrap](https://github.com/twbs/bootstrap)
* [BootstapCDN](https://github.com/jsdelivr/bootstrapcdn)

## What is it

This project prvides a dashboard for the things that are missing in the dashboard provided with a Stiebel Eltron(R) heat pump:

* charts showing the performance factor evolving over time plus the performance factor for the whole operating time of the heat pump.
* charts showing all the metrics in a chart that puts them in a historic context: recent days, recent weeks, months and years.

Here is a screen shot of the dashboard:

<img src="src/stiebel-eltron-dashboard.png" alt="Screenshot of the dashboard" style="width:800px;"/>

## Known limitations

* This project has only been tested with the heat pump version WPL 20 A.
* The dashboard labels are German and there is no option to change lanuages.

<!-- GETTING STARTED -->
## Getting Started

First thing should be to change the default database password in the .env file:

  ```.env
  DatabasePassword="YourStr0ngP@ssword!"
  DatabaseConnectionString="Server=db;Database=master;User=sa;Password=YourStr0ngP@ssword!;"
  ServiceWeltUser="<My-ServiceWelt-User-Name-Goes-here>"
  ServiceWeltPassword="<Y0urStr0ng$ἔrvicἔWἔltP@sswØrd>"
  ServiceWeltUrl="http://192.XXX.XXX"
  ```

To start up the app run

  ```sh
  startup-app.sh
  ```

To adjust the database structure to the database description in the project:

  ```sh
  start-dbmigrating.sh
  ```

To reverse engineer the database structure run

  ```sh
  start-dbscaffolding.sh
  ```

### Prerequisites

You need docker and docker-compose on the machine where you want to run the application:

* docker

### Installation

1. Clone the repo:

   ```sh
   git clone https://github.com/your_username_/Project-Name.git
   ```

2. Install docker:

   ```sh
   brew install docker docker-compose docker-machine xhyve docker-machine-driver-xhyve
   ```

3. Start the app:

   ```sh
   startup-app.sh
   ```
  
4. Open [http://localhost](http://localhost) in any browser.
