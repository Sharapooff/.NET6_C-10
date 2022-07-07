var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//значение среды, необязательно изменять файл launchSettings.json. Это можно сделать также программно:
//app.Environment.EnvironmentName = "Production";

if (app.Environment.IsDevelopment())
{
    app.Run(async (context) => await context.Response.WriteAsync("In Development Stage"));
}
else
{
    app.Run(async (context) => await context.Response.WriteAsync("In Production Stage"));
}
Console.WriteLine($"{app.Environment.EnvironmentName}");

app.Run();




//Для взаимодействия с окружением, в котором запущено приложение, фреймфорк ASP.NET Core предоставляет интерфейс IWebHostEnvironment.
// Этот интерфейс предлагает ряд свойств, с помощью которых мы можем получить информацию об окружении:
// ApplicationName: хранит имя приложения
// EnvironmentName: хранит название среды, в которой хостируется приложение
// ContentRootPath: хранит путь к корневой папке приложения
// WebRootPath: хранит путь к папке, в которой хранится статический контент приложения. По умолчанию это папка wwwroot
// ContentRootFileProvider: возвращает реализацию интерфейса Microsoft.AspNetCore.FileProviders.IFileProvider, которая может использоваться для чтения файлов из папки ContentRootPath
// WebRootFileProvider: возвращает реализацию интерфейса Microsoft.AspNetCore.FileProviders.IFileProvider, которая может использоваться для чтения файлов из папки WebRootPath

// Но наиболее часто при разработке придется сталкиваться со свойством EnvironmentName. По умолчанию имеются три варианта значений для этого свойства:
// Development, Staging и Production. В проекте это свойство задается через установку переменной среды ASPNETCORE_ENVIRONMENT.
// Текущее значение данного параметра задается в файле launchSettings.json, который располагается в проекте в папке Properties.
// Для определения значения этой переменной для интерфейса IWebHostEnvironment определены специальные методы расширения:
// IsEnvironment(string envName): возвращает true, если имя среды равно значению параметра envName
// IsDevelopment(): возвращает true, если имя среды - Development
// IsStaging(): возвращает true, если имя среды - Staging
// IsProduction(): возвращает true, если имя среды - Production

// Данная функциональность позволяет нам выполнять определенный код в зависимости от того, на какой стадии находится приложение.
// Если приложение в процессе разработки, то мы можем выполнять один код, а при разветывании для полноценного использования другой код.

//Определение своих состояний среды
// Хотя по умолчанию среда может принимать три состояния: Development, Staging, Production, но мы можем при желании вводить новые значения. Например, нам надо отслеживать
// какие-то дополнительные состояния. Это можно сделать через изменение файла launchSettings.json либо программно.
// app.Environment.EnvironmentName = "Test";   // изменяем название среды на Test
// или изменить значение "ASPNETCORE_ENVIRONMENT" на "Test" или любое другой в файле launchSettings.json