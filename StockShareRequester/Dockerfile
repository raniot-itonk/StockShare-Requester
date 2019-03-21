FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["StockShareRequester/StockShareRequester.csproj", "StockShareRequester/"]
RUN dotnet restore "StockShareRequester/StockShareRequester.csproj"
COPY . .
WORKDIR "/src/StockShareRequester"
RUN dotnet build "StockShareRequester.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StockShareRequester.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StockShareRequester.dll"]