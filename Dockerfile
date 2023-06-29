# Set the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /src

COPY *sln ./
COPY AiPrompter/AiPrompter.csproj AiPrompter/
COPY Gpt4All/Gpt4All.csproj Gpt4All/
RUN dotnet restore

COPY . ./

RUN dotnet build
RUN dotnet publish -c Release -o /app/publish

# Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build-env /src/build_linux.sh .

RUN apt-get update && apt-get install -y cmake build-essential
RUN chmod +x ./build_linux.sh

COPY --from=build-env /src/gpt4all-backend ./gpt4all-backend

RUN mkdir -p runtimes
RUN rm -rf runtimes/linux-x64
RUN mkdir -p runtimes/linux-x64/native
RUN mkdir runtimes/linux-x64/build
RUN cmake -S gpt4all-backend -B runtimes/linux-x64/build
RUN cmake --build runtimes/linux-x64/build --parallel --config Release

RUN cp runtimes/linux-x64/build/libllmodel.so  /app/libllmodel.so
RUN cp runtimes/linux-x64/build/libgptj*.so  /app/
RUN cp runtimes/linux-x64/build/libllama*.so  /app/
RUN cp runtimes/linux-x64/build/libmpt*.so  /app/

COPY --from=build-env /app/publish .
COPY --from=build-env /src/data_model.bin /app/

ENTRYPOINT ["dotnet", "AiPrompter.dll"]
