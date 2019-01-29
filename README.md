# Test project for .NET developer

System that monitors currently running processes (like 'top' utility in *nix). Supports multiple clients created with various technologies.

## Description

This project is based on ASP.NET Core 2.2.
* ProcessViewer.Api.Controllers.ProcessController is a simple API controller that returns data stored in ProcessesStore class (which is singleton).
* ProcessViewer.Api.Services.ProcessRefresherService is an IHostedService that constantly refreshes the data in ProcessStore class, with a refresh period configurable from appSettings.json file.
* ProcessViewer.Api.Services.NotificationsService is an IHostedService that listens to incoming tcp connections and upgrades them to WebSocket if possible. Also it's listening to events from instance ProcessViewer.Persistence.Notifications.HighLoadNotificationsProcessor and sends notifications to connected clients whenever these notifications arrive. 

* ProcessViewer.Persistence.Stores.ProcessesStore class stores the list of currently running processes and provides the interface to refresh it.
* ProcessViewer.Persistence.Notifications.NotificationsProcessor provide the interface to run through the list of running processes with a bunch of ProcessViewer.Persistence.Filters.Abstract.ILoadChecker
* ProcessViewer.Persistence.Filters.Abstract.ILoadChecker checks whether each process respect their rules and return notification messages for those that are not.
System uses just 2 of them (ProcessMemoryLoadChecker and ProcessCpuLoadChecker) out of the box, but number of them can be easily increased by creating new objects that implement ProcessViewer.Persistence.Filters.Abstract.ILoadCheckers interface and registering them with ASP.NET Core DI.

* A simple web ui resides in ProcessViewer.WebUI project. It utilizes ajax to get the list of running processes from api. And it also listens to notifications channel using WebSocket. Other interfaces can be easily developed using api description in /Help/api.txt.

## Deployment

ProcessViewer.Api and ProcessViewer.WebUI should be deployed or run inside VS2017.

## Author

* **Maksim Smirnov** - *Initial work*

## Acknowledgments

* **Some dev team** - *for an interesting test project*
