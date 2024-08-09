#!/bin/bash

if [ -z "$1" ]; then
    echo "Missing Database password \n Expected usage: \n Expected usage: ubuntu21-arm64-setup.sh databasepassword serviceweltusername serviceweltpassword servicewelturl"
    exit 1;
fi


if [ -z "$2" ]; then
    echo "Missing ServiceWelt Username. \n Expected usage: \n Expected usage: ubuntu21-arm64-setup.sh databasepassword serviceweltusername serviceweltpassword servicewelturl"
    exit 1;
fi

if [ -z "$3" ]; then
    echo "Missing ServiceWelt password. \n Expected usage: ubuntu21-arm64-setup.sh databasepassword serviceweltusername serviceweltpassword servicewelturl"
    exit 1;
fi

if [ -z "$4" ]; then
    echo "Missing ServiceWelt url.\n Expected usage: ubuntu21-arm64-setup.sh databasepassword serviceweltusername serviceweltpassword servicewelturl"
    exit 1;
fi


git clone https://github.com/marcflohrer/stiebel-eltron-dashboard

cd stiebel-eltron-dashboard/src
touch .env
echo 'DatabasePassword="$1"' >> .env
echo 'DatabaseConnectionString="Server=db;Database=master;User=sa;Password=$1"'  >> .env
echo 'ServiceWeltUser="$2"' >> .env
echo 'ServiceWeltPassword="$3"' >> .env
echo 'ServiceWeltUrl="$4"' >> .env

sudo apt-get remove docker docker-engine docker.io containerd runc
sudo apt-get remove docker docker.io containerd runc
 sudo apt-get update
 sudo apt-get install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
echo \
  "deb [arch=arm64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io
sudo docker run hello-world
sudo groupadd docker
sudo usermod -aG docker $USER
docker run hello-world

sudo apt-get install libffi-dev libssl-dev -y
sudo apt install python3-dev -y
sudo apt-get install -y python3 python3-pip -y

HASH=caa0e6fb-770c-4b21-ba55-30154a7a9e11/3231af451861147352aaf43cf23b16ea
FILE=dotnet-sdk-7.0.101-linux-arm64.tar.gz
if [ -f "$FILE" ]; then
    echo "$FILE exists."
else
    wget https://download.visualstudio.microsoft.com/download/pr/$HASH/$FILE
fi
DIRECTORY=dotnet-64
if [ -d "$DIRECTORY" ]; then
   echo "deleting $DIRECTORY directory."
   rm -r $DIRECTORY
fi
mkdir dotnet-64
echo "unpacking $FILE."
tar zxf $FILE -C $DIRECTORY
echo "$FILE unpacked."

export DOTNET_ROOT=$HOME/dotnet-64
export PATH=$HOME/dotnet-64:$PATH
echo  'export DOTNET_ROOT=$HOME/dotnet-64' >> ~/.bashrc 
echo  'export PATH=$HOME/dotnet-64:$PATH' >> ~/.bashrc 
source ~/.bashrc 
dotnet --info

cd stiebel-eltron-dashboard/src && chmod +x start-dbmigrating.sh && ./start-dbmigrating.sh &
