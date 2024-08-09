<!-- PROJECT TITLE -->
<h3 align="center">Stiebel Eltron Dashboard Demo</h3>

<p align="center">
    <br />
    <a href="https://github.com/marcflohrer/AspNetOnSqlServer"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/marcflohrer/AspNetOnSqlServer/issues/new/choose">Report Bug</a>
    ·
    <a href="https://github.com/marcflohrer/AspNetOnSqlServer/issues">Request Feature</a>
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#about-the-project">About The Project</a></li>
    <li><a href="#built-with">Built With</a></li>
    <li><a href="#what-is-it-about">What Is It About?</a></li>
    <li><a href="#known-limitations">Known Limitations</a></li>
    <li><a href="#installation">Installation</a>
      <ul>
        <li><a href="#installation-on-ubuntu-2204">Installation on Ubuntu 22.04</a></li>
        <li><a href="#installation-somewhere-else">Installation Somewhere Else</a></li>
      </ul>
    </li>
    <li><a href="#local-development">Local Development</a></li>
    <li><a href="#migrating-from-raspberry-pi-4-or-older-to-raspberry-pi-5">Migrating from Raspberry Pi 4 (or older) to Raspberry Pi 5</a></li>
    <li><a href="#license">License</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

This project is intended to provide a more insightful presentation of the data provided by the original Stiebel Eltron dashboard. Additionally, it adds the missing performance factor chart (Arbeitszahl).

### Built With

This section lists major frameworks and projects that were used:

* [Docker](https://docs.docker.com/)
* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet)
* [SQL Server on Linux](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-overview?view=sql-server-ver15)
* [ASP.NET Core Identity Sample MVC](https://github.com/dotnet/aspnetcore/tree/main/src/Identity/samples/IdentitySample.Mvc) - [Licensed under Apache 2.0](legal/aspnetcore/LICENSE) (Notice: Some files are modified.)
* [HTML Agility Pack](https://github.com/zzzprojects/html-agility-pack)
* [xUnit](https://github.com/xunit/xunit)
* [Cronos](https://github.com/HangfireIO/Cronos)
* [Entity Framework Core](https://github.com/dotnet/efcore)
* [AutoFixture](https://github.com/AutoFixture/AutoFixture)
* [VSTest](https://github.com/microsoft/vstest)
* [Chart.js](https://github.com/chartjs)
* [jQuery](https://github.com/jquery/jquery)
* [Bootstrap](https://github.com/twbs/bootstrap)
* [jsDelivr](https://github.com/jsdelivr/bootstrapcdn)

## What Is It About?

This project provides a dashboard for the features missing in the dashboard provided with a Stiebel Eltron® heat pump:

* Charts showing the performance factor evolving over time, plus the performance factor for the entire operating time of the heat pump.
* Charts showing various metrics in a historical context: recent days, weeks, months, and years.

Here is a screenshot of the dashboard:

<img src="src/stiebel-eltron-dashboard.png" alt="Screenshot of the dashboard" style="width:800px;"/>

## Known Limitations

* This project has only been tested with the heat pump version WPL 20 A.
* The dashboard labels are partly in German, and there is no option to change the language.
* On the first day after starting the application, no information is available in the database, and the site will show an error: "Sequence contains no elements."
* After migrating to the .NET 6 version or later, ensure your database connection string contains `;SslMode=Disable` in your .env file. Example:

```keyvaluepair
DatabaseConnectionString="Host=db;Database=master;Username=sa;Password=MySuperSecretPassword;SslMode=Disable"
```

## Installation

The installation I only tested on raspberry pi 4 and ubuntu 22.04.

### Installation On Ubuntu 22.04

1. Install [ubuntu 22.04](https://ubuntu.com/download/desktop?version=22.04&architecture=amd64) on a [raspberry pi 4](https://www.raspberrypi.org/products/raspberry-pi-4-model-b/) and [enable ssh](https://linuxhint.com/install_ubuntu_ssh_headless_raspberry_pi_4/).

2. Go to the home directory and call the setup script with your passwords, usernames and your url:

   ```sh
   sudo chmod +x ubuntu21-arm64-setup.sh && ./ubuntu21-arm64-setup.sh <databasepassword> <serviceweltusername> <serviceweltpassword> <servicewelturl> <httpport> <httpsport>
   ```

3. When you see 'Migration finished' stop and remove the migration container:

   ```sh
   docker stop migration && docker rm migration
   ```

4. Start the app:

   ```sh
   chmod +x startup-app.sh && ./startup-app.sh &
   ```

5. Wait a minute then open [http://localhost](http://localhost) in any browser. If your raspberry pi is reachable in your local network you can replace localhost with the respective IP address.

6. If you want to contribute to the project and you need to change the database structure you can use the following script to check if your database changes were successful:

   ```sh
   ./start-dbscaffolding.sh
   ```

### Installation Somewhere Else

#### Prerequisites

You need docker on the machine where you want to run the application:

* git
* docker
* [dotnet 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
* either [armv7](https://en.wikipedia.org/wiki/ARM_architecture)+ && [ubuntu 22.04](https://ubuntu.com/download/desktop?version=22.04&architecture=amd64)
* or x64 && (linux, mac)

1. Navigate into in the **src** folder of the repository:

   ```sh
   ./start-dbmigrating.sh
   ```

2. When you see 'Migration finished' stop and remove the migration container:

   ```sh
   docker stop migration && docker rm migration
   ```

3. Create a environment file with the .env in the src folder of the repository with the following structure:

   ```env
    DatabasePassword=<MySuperSecretPassword>
    DatabaseConnectionString="Host=db;Database=master;Username=sa;Password=<MySuperSecretPassword>;SslMode=Disable"
    ServiceWeltUser="<serviceweltusername>"
    ServiceWeltPassword="<serviceweltpassword>"
    ServiceWeltUrl="<servicewelturl>"
    HttpPort="<httpPort>"
    HttpsPort="<HttpsPort>"
   ```

4. Start the app:

   ```sh
   chmod +x startup-app.sh && ./startup-app.sh &
   ```

## Migrating from Raspberry Pi 4 (or older) to Raspberry Pi 5

If you are moving from a Raspberry Pi 4 or older to a Raspberry Pi 5, you can easily back up and restore your existing data using the integrated Swagger API. The following steps will guide you through backing up your data on your Raspberry Pi 4 and restoring it on the Raspberry Pi 5:

### 1. Backing Up Data on the Raspberry Pi 4

* Start your application on the Raspberry Pi 4.
* Open a web browser and navigate to `http://<your-raspberry-pi-ip>:<port>/swagger/index.html`. Replace `<your-raspberry-pi-ip>` with your Raspberry Pi's IP address and `<port>` with the port on which your application is running.
* You will see the Swagger API interface.
* Use the API endpoints to back up your data:
  * **GET `/HeatPumpData`**: Download the file containing all current data.
  * **GET `/HeatPumpDataPerPeriod`**: Download the file containing the periodic data.
* Save these files in a secure location.

#### Crontab for Regular Backups

To automate the backup process, you can use the following crontab settings on your Raspberry Pi 4:

```plaintext
5 * * * * cd /home/pi/backup/ && curl --silent -X 'GET' 'http://<your-raspberry-pi-ip>:<port>/HeatPumpData' -H 'accept: */*' --output heatpumpdata.zip
6 * * * * cd /home/pi/backup/ && curl --silent -X 'GET' 'http://<your-raspberry-pi-ip>:<port>/HeatPumpDataPerPeriod' -H 'accept: */*' --output heatpumpdataperperiod.zip
```

## Local Development

If you want to develop the project locally on your machine you have to put  an appsettings.json file in the folder src/secrets with the following structure:

```json
{
    "DatabasePassword": "<MySuperSecretPassword>",
    "DefaultConnection": "Host=localhost;Database=master;Username=sa;Password=<MySuperSecretPassword>;SslMode=Disable",
    "ServiceWeltUser": "<serviceweltusername>",
    "ServiceWeltPassword": "<serviceweltpassword>",
    "ServiceWeltUrl": "<servicewelturl>",
    "HttpPort": "<httpPort>",
    "HttpsPort": "<httpsPort>"
}
```

Replace the placeholders \<MySuperSecretPassword\>, \<serviceweltusername\>, \<serviceweltpassword\>, \<servicewelturl\> with the respective values in your environment.
After running the project in the IDE of your choice the website [http://localhost:55876/] should be up and running after some time.

## License

This project is licensed under the Reciprocal Public License 1.5 (RPL1.5). This is a GPL-style license with very detailed liability protection and numerous requirements, created to close a perceived loophole in the GPL which let users sell modified software without 'fairly' distributing it. Disputes over this license must be settled through an American arbitration process.
