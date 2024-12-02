# ProbabilityApp

# Prerequsites

Net 7.0
node v16.20.0

Open a new terminal for each setup

## Setup Frontend

1. Go to the following directory `cd ./ProbabilityApp/client`
2. Run the following command: `npm install` to install all required packages.
3. Run the following command to start the development server: `npm start`

The development server runs from the port of localhost:5000. Which can be changed from package.json

## Setup Backend

1. Go to the following directory `cd ./ProbabilityApp`
2. Run the following command: `dotnet build` to build solution.
3. Run the following command to start the server: `dotnet run`.

The server runs from the port of localhost:5289. Which can be changed from `Properties/launchSettings.json`

## Setup Tests

1. Go to the following directory `cd ./ProbabilityApp.Tests`
2. Run the following command: `dotnet build` to build solution.
3. Run the following command to start the server: `dotnet test`.

This will run all the tests declared in test Classes

# Bugs

Sometimes the build can fail due to an error during the build process

## Handling build errors on ProbabilityApp

1. Delete the bin folder
2. Delete the obj folder
3. Run `dotnet clean`
4. Run `dotnet build` again

## Handling build errors on ProbabilityApp.Tests

Same method for ProbabilityApp

## Handling build errors on ProbabilityApp/client

1. Check the version of node installed, if it is not the version specified the build may
   fail or the app may not run.
2. Check the version of Net installed.
3. If both versions are correct, delete the node_modules folder
4. Delete the package-lock.json file and run `npm install` again.

## Handling server errors

In order to fetch from the server side both the client and the server must be running.
The server should be listening to requests from port 5289. If a `status 500` error occurs 
then most likely the port is being blocked or the server is not running.

If the port is changed on the client side then Cors policy must be updated in
`Program.cs` at method `policy.WithOrigins`.

Make sure no firewalls or private networks are inteferring with the requests.

