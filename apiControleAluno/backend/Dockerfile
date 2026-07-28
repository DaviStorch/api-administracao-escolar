FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["apiControleAluno.csproj", "."]
RUN dotnet restore "apiControleAluno.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "apiControleAluno.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apiControleAluno.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apiControleAluno.dll"]