
#!/bin/bash
set -ex

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!"
>&2 echo "Running entrypoint.sh !!!11!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!"

apk update && apk add wget && \
  apk add bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib && \
  apk add libgdiplus --repository https://dl-3.alpinelinux.org/alpine/edge/testing/ && \
  wget https://dot.net/v1/dotnet-install.sh && \
  chmod +x ./dotnet-install.sh && \
  /bin/bash -c "./dotnet-install.sh -c 5.0" && \
  export PATH="$PATH:/root/.dotnet/" && \
  cd /root/.dotnet/ && \
  chmod +x dotnet && \
  dotnet dev-certs https && \
  cd /app

until dotnet user-secrets init && dotnet user-secrets set ConnectionStrings:DefaultConnection "$1" --project .; do
>&2 echo "Setting up user secret: " $1
sleep 1
done

./stiebel-eltron-apiserver

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: APP RUNNING !!!!!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
