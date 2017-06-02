FROM microsoft/dotnet:latest
COPY . /api
WORKDIR /api

RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000/tcp

ENTRYPOINT ["dotnet", "run"]
