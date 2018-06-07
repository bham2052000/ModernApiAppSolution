FROM microsoft/aspnetcore-build:1.0-2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "ModernApiApp.dll"]

