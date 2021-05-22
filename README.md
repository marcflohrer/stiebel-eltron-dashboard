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

  <h3 align="center">SQL Server on Linux + ASP.NET 5.0 stiebel-eltron-dashboard demo</h3>

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

## What is it

This project prvides a dashboard for the things that are missing in the dashboard provided with a Stiebel Eltron(R) heat pump:

* charts showing the performance factor evolving over time plus the performance factor for the whole operating time of the heat pump.
* charts showing all the metrics in a chart that puts them in a historic context: recent days, recent weeks, months and years.

Here is a screen shot of the dashboard:

<img src="src/stiebel-eltron-dashboard.png" alt="Screenshot of the dashboard" style="width:800px;"/>

## Known limitations

* This project has only been tested with the heat pump version WPL 20 A.
* The dashboard labels are partly in German and there is no option to change the language.
* The first day after starting the application no information are available in the database and the site will show an error "Sequence contains no elements".

### Prerequisites

You need docker and docker-compose on the machine where you want to run the application:

* git
* docker
* docker-compose
* [dotnet 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
* [armv7](https://en.wikipedia.org/wiki/ARM_architecture)+ && [ubuntu 20.04](https://ubuntu.com/download/desktop?version=20.04&architecture=amd64)
* x64 && (mac || linux)

### Start the app

1. Install ubuntu on a raspberry pi and [enable ssh](https://linuxhint.com/install_ubuntu_ssh_headless_raspberry_pi_4/).

2. Clone the repo:

   ```sh
   git clone https://github.com/marcflohrer/stiebel-eltron-dashboard
   ```

3. Put a .env file in the **src** folder with the data that match your environment:

   ```sh
   cd /stiebel-eltron-dashboard/src
   touch .env
   echo 'DatabasePassword="YourStr0ngP@ssword!"' >> .env
   echo 'DatabaseConnectionString="Server=db;Database=master;User=sa;Password=YourStr0ngP@ssword!;"' 
   echo 'ServiceWeltUser="<My-ServiceWelt-User-Name-Goes-here>"' >> .env
   echo 'ServiceWeltPassword="<Y0urStr0ng$ἔrvicἔWἔltP@sswØrd>"' >> .env
   echo 'ServiceWeltUrl="http://192.XXX.XXX.XX"' >> .env
   ```

4. Install docker & docker-compose:

   ```sh
   curl -fsSL https://get.docker.com -o get-docker.sh
   sh get-docker.sh
   sudo usermod -aG docker $USER
   sudo reboot
   ssh user@191.XXX.XXX.YY
   sudo chown root:docker /var/run/docker.sock
   ```

   Restart the pi and check if it was successful:

   ```sh
   docker container run hello-world
   ```

   For detailled instructions on how to install docker an a raspberry pi see [here (German)](https://www.randombrick.de/raspberry-pi-docker-installieren-und-nutzen/).

   Then install docker-compose:

   ```sh
   sudo apt-get install libffi-dev libssl-dev
   sudo apt install python3-dev
   sudo apt-get install -y python3 python3-pip
   sudo pip3 install docker-compose
   ```

   For detailled instructions on how to install docker-compose an a raspberry pi see [here (English)](https://devdojo.com/bobbyiliev/how-to-install-docker-and-docker-compose-on-raspberry-pi).

5. To install dotnet run I installed ubuntu 20.10 on my raspberry pi 4 then I executed these commands on the terminal via ssh from the $HOME directory. Check [https://dotnet.microsoft.com/download/dotnet/5.0](https://dotnet.microsoft.com/download/dotnet/5.0) for more recent versions of the dotnet sdk for the arm64 architecture.

   ```sh
   wget https://download.visualstudio.microsoft.com/download/pr/af5f1e5b-d544-47af-b730-038e4258641b/bccb3982f5690134ab66748a5afc36c7/dotnet-sdk-5.0.203-linux-arm64.tar.gz
   mkdir dotnet-64
   tar zxf dotnet-sdk-5.0.203-linux-arm64.tar.gz -C $HOME/dotnet-64
   export DOTNET_ROOT=$HOME/dotnet-64
   export PATH=$HOME/dotnet-64:$PATH
   echo  'export DOTNET_ROOT=$HOME/dotnet-64' >> ~/.bashrc 
   echo  'export PATH=$HOME/dotnet-64:$PATH' >> ~/.bashrc 
   sudo reboot
   ssh user@191.XXX.XXX
   dotnet --info
   ```  

   , where ```user``` is the login user of your ubuntu system and  ```191.XXX.XXX``` is the IP address of your raspberry pi in your local network.
   If everything was successful the output of the command ```dotnet --info``` looks something like this:

   ```sh
   .NET SDK (reflecting any global.json):
    Version:   5.X.XXX
    Commit:    383637d63f
    Runtime Environment:
    OS Name:     ubuntu
    OS Version:  2X.XX
    OS Platform: Linux
    RID:         ubuntu.2X.XX-arm64
    Base Path:   /home/ubuntu/dotnet-64/sdk/5.0.XXX/
   Host (useful for support):
   Version: 5.X.X
   Commit:  XXXX
   .NET SDKs installed:
   5.0.XXX [/home/ubuntu/dotnet-64/sdk]
   .NET runtimes installed:
   Microsoft.AspNetCore.App 5.X.X [/home/user/dotnet-64/shared/Microsoft.AspNetCore.App]
   Microsoft.NETCore.App 5.0.6 [/home/user/dotnet-64/shared/Microsoft.NETCore.App]
   To install additional .NET runtimes or SDKs:
   https://aka.ms/dotnet-download
   ```

6. Before starting the app for the first time on a specific machine go to the **src** folder and run:

   ```sh
   ./start-dbmigrating.sh
   ```

7. Start the app:

   ```sh
   ./startup-app.sh
   ```
  
8. Open [http://localhost](http://localhost) in any browser. If your raspberry pi is reachable in your local network you can replace localhost with the respective IP address.

9. If you want to contribute to the project and you need to change the database structure you can use the following script to check if your database changes were successful:

   ```sh
   ./start-dbscaffolding.sh
   ```
