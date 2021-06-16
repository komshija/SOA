# Faasd

faasd is a light-weight option for adopting OpenFaaS which uses the same tooling, ecosystem, templates, and containers as OpenFaaS on Kubernetes, but which doesn’t require cluster management. faasd uses containerd as a runtime and CNI for container networking.

[Official faasd GitHub repo](https://github.com/openfaas/faasd)

# Air Quality App

Air Quality app simulates sensor readings and analysis of those readings using a microservice architecture.
It consists of ...

![diagram](https://user-images.githubusercontent.com/63471407/122078224-b65e0600-cdfc-11eb-8d44-1b76bcddbee1.jpg)

## Usage

Start the project by navigating to `docker-compose.yml` location and running `docker compose build` command for building the app. When finished run the `docker compose up` command for starting the app. After starting, open the http://localhost:8080/ URL in the browser. Dashboard will be displayed where you can monitor sensor readings and test services via API gateway.

## Services

- **apigateway** - API gateway services
- **datamicroservice** - Service that recives data from sensors and saves it in database. Also it publishes data to `device/co/messages` and `device/no2/messages` topic depending on which data type it recived.
- **co-sensor-microservice** - Service that simulates senosr work by randomly sending CO data to datamicroservice. Also has routs for setting send (reading) interval and getting sensor information.
- **no2-sensor-microservice** - Service that simulates sensor work by randomly sending NO2 data to datamicroservice only when the value is greater by a certain amount than the previously read value (decided by threshold). Also has routs for setting send (reading) interval and threshold and getting sensor information.
- **cep** - Analytics service which is subscribed on `device/co/messages` and `device/no2/messages` topics. It analyzes recived data and publishes alerts on `device/co/command` and `device/no2/command` topics depending on data type.
- **commandmicroservice** - Service that is subscribed on `device/co/command` and `device/no2/command` topics. When alert arrives, it gives out a command for the actuators on sensors depending on the alert type and generates notification for dashboard.

## Routes
API Gateway route URL is on http://localhot:3500/

- **GET** /api/get/{sensor} - gets all data readings from a given sensor.
- **GET** /api/greater/{sensor}/{value} - gets all data readings greater than given value from a given sensor.
- **GET** /api/less/{sensor}/{value} - gets all data readings less than given value from a given sensor.
- **GET** /api/getlast/{sensor} - gets last 10 data readings from a given sensor.
- **POST** /api/command/{sensor}/{command} - posts a given command to an actuator on a given sensor.
- **GET** /api/coinfo - gets information about CO sensor.
- **POST** /api/cosendinterval/{sendInterval} - sets send (reading) interval on CO sensor for a given interval in seconds.
- **GET** /api/no2info - gets information about NO2 sensor
- **POST** /api/no2sendinterval/{sendInterval} - sets send (reading) interval on NO2 sensor for a given interval in seconds.
- **POST** /api/no2treshold/{threshold} - sets threshold value on NO2 sensor for a given value (between 0 and 1).
