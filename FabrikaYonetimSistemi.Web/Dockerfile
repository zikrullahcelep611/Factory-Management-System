# Build aşaması için .NET SDK kullanıyoruz
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["FormulaOne/FormulaOne.API.csproj", "FormulaOne.Api/"]
COPY ["FormulaOne/FormulaOne.DataService.csproj", "FormulaOne.DataService/"]
COPY ["FormulaOne/FormulaOne.Entities.csproj", "FormulaOne.Entities/"]
RUN dotnet restore "FormulaOne.Api/FormulaOne.Api.csproj"
COPY . .
WORKDIR "/src/FormulaOne.Api"
RUN dotnet build "FormulaOne.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish 
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FormulaOne.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FormulaOne.Api.dll"]

# Proje dosyalarını kopyalıyoruz
COPY FabrikaYonetimSistemi.Core/*.csproj ./FabrikaYonetimSistemi.Core/
COPY FabrikaYonetimSistemi.Data/*.csproj ./FabrikaYonetimSistemi.Data/
COPY FabrikaYonetimSistemi.Entity/*.csproj ./FabrikaYonetimSistemi.Entity/
COPY FabrikaYonetimSistemi.Service/*.csproj ./FabrikaYonetimSistemi.Service/
COPY FabrikaYonetimSistemi.Web/*.csproj ./FabrikaYonetimSistemi.Web/

# Çalışma dizinini web katmanına ayarlıyoruz
WORKDIR /src/FabrikaYonetimSistemi.Web

# Restore işlemi
RUN dotnet restore

# Tüm dosyaları kopyalıyoruz
WORKDIR /src
COPY . .

# Uygulamayı yayınlıyoruz (publish)
WORKDIR /src/FabrikaYonetimSistemi.Web
RUN dotnet publish -c Release -o /app

# Runtime aşaması için yalnızca gerekli dosyalar
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

# Uygulamayı başlatıyoruz
ENTRYPOINT ["dotnet", "FabrikaYonetimSistemi.Web.dll"]
