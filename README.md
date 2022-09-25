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

  <h3 align="center">stiebel-eltron-dashboard demo</h3>

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
    <li><a href="#what-is-it-about">What is it about?</a></li>
    <li><a href="#known-limitations">Known limitations</a></li>
    <li><a href="#installation">Installation</a>
      <ul>
        <li><a href="#prerequisites">Installation on ubuntu 21.04</a></li>
        <li><a href="#installation">Installation somewhere else</a></li>
      </ul>
    </li>
    <li><a href="#license">License</a></li>
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
* [marcflohrer/AspNetOnSqlServer](https://github.com/marcflohrer/AspNetOnSqlServer)
* [zzzprojects/html-agility-pack](https://github.com/zzzprojects/html-agility-pack)
* [xunit/xunit](https://github.com/xunit/xunit)
* [HangfireIO/Cronos](https://github.com/HangfireIO/Cronos)
* [aspnet/Identity](https://github.com/aspnet/Identity)
* [dotnet/efcore](https://github.com/dotnet/efcore)
* [AutoFixture/AutoFixture](https://github.com/AutoFixture/AutoFixture)
* [microsoft/vstest](https://github.com/microsoft/vstest)
* [xunit/xunit](https://github.com/xunit/xunit)
* [chartjs](https://github.com/chartjs)
* [jquery/jquery](https://github.com/jquery/jquery)
* [twbs/bootstrap](https://github.com/twbs/bootstrap)
* [jsdelivr/bootstrapcdn](https://github.com/jsdelivr/bootstrapcdn)

## What is it about

This project prvides a dashboard for the things that are missing in the dashboard provided with a Stiebel Eltron(R) heat pump:

* charts showing the performance factor evolving over time plus the performance factor for the whole operating time of the heat pump.
* charts showing all the metrics in a chart that puts them in a historic context: recent days, recent weeks, months and years.

Here is a screen shot of the dashboard:

<img src="src/stiebel-eltron-dashboard.png" alt="Screenshot of the dashboard" style="width:800px;"/>

## Known limitations

* This project has only been tested with the heat pump version WPL 20 A.
* The dashboard labels are partly in German and there is no option to change the language.
* The first day after starting the application no information are available in the database and the site will show an error "Sequence contains no elements".
* The website is not ready for mobile devices. It works at first glance but when switch between views the widgets disappear. This is probably duet to a memory limitation on mobile devices.

### Prerequisites

You need docker and docker-compose on the machine where you want to run the application:

* git
* docker
* docker-compose
* [dotnet 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
* either [armv7](https://en.wikipedia.org/wiki/ARM_architecture)+ && [ubuntu 21.04](https://ubuntu.com/download/desktop?version=21.04&architecture=amd64)
* or x64 && (mac || linux)

## Installation

The installation I tested is the combination ubuntu 21.04 on raspberry pi 4 but generally it should work similarly with different operating systems and chipset architecture combinations.

### Installation on ubuntu 21.04

1. Install [ubuntu 21.04](https://ubuntu.com/download/desktop?version=20.04&architecture=amd64) on a [raspberry pi 4](https://www.raspberrypi.org/products/raspberry-pi-4-model-b/) and [enable ssh](https://linuxhint.com/install_ubuntu_ssh_headless_raspberry_pi_4/).

2. Go to the home directory and call the setup script with your passwords, usernames and your url:

   ```sh
   sudo chmod +x ubuntu-arm64-setup.sh && ./ubuntu-arm64-setup.sh <databasepassword> <serviceweltusername> <serviceweltpassword> <servicewelturl>
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

### Installation somewhere else

1. Install the dependencies listed under **Prerequisites** and then run in the **src** folder of the repository:

   ```sh
   ./start-dbmigrating.sh
   ```

2. When you see 'Migration finished' stop and remove the migration container:

   ```sh
   docker stop migration && docker rm migration
   ```

3. Start the app:

   ```sh
   chmod +x startup-app.sh && ./startup-app.sh &
   ```

## License

This project is licensed under the Reciprocal Public License 1.5 (RPL1.5). This is a GPL-style license with very detailed liability protection and numerous requirements, created to close a perceived loophole in the GPL which let users sell modified software without 'fairly' distributing it. Disputes over this license must be settled through an American arbitration process.
