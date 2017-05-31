FROM microsoft/dotnet:latest

MAINTAINER Romaan Khadeer romaan@gmail.com

COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

EXPOSE 5000

ENTRYPOINT ["dotnet", "run", "--server.urls", "http://0.0.0.0:5000"]
