# Project Short Description :
-----------------------------------------
Our project develop in .Net Core-3 console application  will provide backend reply solution which talks with a user and provides him/her relevant information like 
the top 3 news of enter location and , user can get weather details of enter location.

# Initial Requirement
-----------------------------------------
+ Our Application will consist below features:
  - Our bot should first ask the name of the user, and start addressing him/her by that name
  - Our the bot should ask about the location of the person
  - Our bot will tell the weather of the day in that location
  - Our bot will also tell the top 3 news in that country

# Dependency
-----------------------------------------
- DotNet core 3.0 or 3.0+
- Telegram Bot from Nuget
- I have created bot with username and bot name using telegram bot father 
- I have used token in my code which got from telegram bot father
- Our Created bot address:- t.me/BeepBotNew_bot
- User can start communication using telegram bot directly .

## Deviations : For Hosting on heroku.com

We have tried to deploy the do net core application from both windwos and linux :-

### A. Windows

    Install docker on your machine with windows environment by following the below steps :-
        step 1: You can create a new account on heroku.com or can use the old account also.
        step 2: Install Heroku CLI on your machine if not installed.
        step 3: Go to heroku.com and log in.
        step 4: Create a new Heroku app.
        step 5: Create a docker file in your .net core application at the root folder. 
                Right-click on the project and then add docker support. And then choose windows .
                Remove all default code from the docker file and add the below code as the given code is 
                not working.

                CMD ASPNETCORE_URLS=http://*:$PORT dotnet {projectName}.dll

        step 6: Now publish the project and copy docker file into publish folder.
                (bin-->Release--><yourdotnetcoreversion>-->publish)
        step 7 : Now go to the docker terminal and execute the below commands for building docker image.
                    $ docker build -t {imagename} {publish folder path }
        step 8: Now login to heroku container.
                    heroku container:login
        step 9: Heroku runs a container registry on registry.heroku.com for that execute the below command.
                    $ docker tag {imagename} registry.heroku.com/{heroku app name}/{process -type}
        step 10: Now push the image to container registry. 
                    $ docker push registry .heroku.com /{heroku app name}/{process type}
                    
        But at this step when image is pushed on Heroku it gives error: 
        Error: Received unexpected HTTP status:500 internal server error.

        After that, we have tried with the Linux environment. As we heroku support linux environment as
        the container created from the windows enviroment was creating error for this. So we have done the container 
        creating from linux.
        Belows are the steps followed to create and push container from the docker linux terminal.

### B. Linux
     Install docker on your machine with linux environment and follow the below steps :-
        step 1: You can create a new account on heroku.com or can use the old account also.
        step 2: Install Heroku CLI on your machine if not installed.
        step 3: Go to heroku.com and log in.
        step 4: Create a new Heroku app.
        step 5: Create a docker file in your .net core application at the root folder. 
                Right-click on the project and then add docker support. And then choose linux .
                Remove all default code from the docker file and add the below code as the given code is not working.

                FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
                WORKDIR /app
                COPY . .

                CMD ASPNETCORE_URLS=http://*:$PORT dotnet {projectName}.dll

        step 6: Now publish the project and copy docker file into publish folder.
                (bin-->Release--><yourdotnetcoreversion>-->publish)
        step 7 : Now go to the docker terminal and execute the below commands for building docker image.
                    $ docker build -t {imagename} {publish folder path }
        step 8: Now login to heroku container.
                    heroku container:login
        step 9: Heroku runs a container registry on registry.heroku.com for that execute the below command.
                    $ docker tag {imagename} registry.heroku.com/{heroku app name}/{process -type}
        step 10: Now push the image to container registry. 
                    $ docker push registry .heroku.com /{heroku app name}/{process type}
        step 11: Now release the container on heroku. After this the application will run.
                    $ heroku container : release {process type} -- app {heroku app name}

        All the above mentioned steps have been successfully executed and the container is released.
        But when we open the application on heroku we get the below mentioned error.

        Error: 503 Service Unavailable Error is occurred:
            503 Service Unavailable Error is an HTTP response status code indicating that a server is temporarily
            unable to handle the request. This may be due to the server being overloaded or down for maintenance. 
            This particular response code differs from a code like the 500 Internal Server Error.

        Link : https://airbrake.io/blog/http-errors/503-service-unavailable

# BeepBotUnitTest Project
-------------------------------------------
- If any error occur while executing the unit test cases, then check the properties of all the ChatBot.json files.
- If "Copy to Ouput Directory" is not set to "Copy if newer" then make it "Copy if newer".
- For unit test I created one mock method which communicate with our diclaired method and check our expected result with get reply.
- I have created one ChatBot.json file which consist our input comment.

# Reference
-------------------------------------------
Telegram Web
https://web.telegram.org

Bots: An introduction for developers
https://core.telegram.org
