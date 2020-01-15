#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["OFA.Accounts.WM.BgWorker/OFA.Accounts.WM.BgWorker.csproj", "OFA.Accounts.WM.BgWorker/"]
RUN dotnet restore "OFA.Accounts.WM.BgWorker/OFA.Accounts.WM.BgWorker.csproj"
COPY . .
WORKDIR "/src/OFA.Accounts.WM.BgWorker"
RUN dotnet build "OFA.Accounts.WM.BgWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OFA.Accounts.WM.BgWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OFA.Accounts.WM.BgWorker.dll"]