FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY *.sln ./
COPY CTT.Products.API/CTT.Products.API.csproj CTT.Products.API/
COPY CTT.Products.Business/CTT.Products.Business.csproj CTT.Products.Business/
COPY CTT.Products.Domain/CTT.Products.Domain.csproj CTT.Products.Domain/
COPY CTT.Products.Infrastructure/CTT.Products.Infrastructure.csproj CTT.Products.Infrastructure/
COPY CTT.Products.Tests/CTT.Products.Tests.csproj CTT.Products.Tests/

RUN dotnet restore

COPY . ./
RUN dotnet publish CTT.Products.API/CTT.Products.API.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app

COPY --from=build /out .

EXPOSE 5073
EXPOSE 7299

ENTRYPOINT ["dotnet", "CTT.Products.API.dll"]