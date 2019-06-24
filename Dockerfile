FROM microsoft/aspnetcore
LABEL name="foxsoundi-api"
ENTRYPOINT ["dotnet", "Api.dll"]
ARG source=.
WORKDIR /app
EXPOSE 80
COPY $source .