//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder( new WebApplicationOptions { WebRootPath = "static" });  // добавляем папку для хранения статики
var app = builder.Build();


//для отправки файлов можно использовать метод SendFileAsync() объекта HttpResponse:
//app.Run(async (context) => await context.Response.SendFileAsync("index.html"));
//однако ASP.NET Core также предоставляет специальный встроенный middleware, который подключается с помощью метода UseStaticFiles() и который упрощает работу со статическими файлами.
//По умолчанию для определения пути хранения статических файлов в проекте используются два параметра ContentRoot и WebRoot, а сами статические файлы должны помещаться
//в каталог ContentRoot/WebRoot. На стадии разработки параметр "ContentRoot" соответствует каталогу текущего проекта. А параметр "WebRoot" по умолчанию представляет папку wwwroot
//в рамках каталога ContentRoot. То есть, исходя из значений по умолчанию, то статические файлы следует располагать в папке "wwwroot", которая должна находиться в текущем проекте.
//Однако эти параметры при необходимости можно переопределить.
app.UseStaticFiles();   // добавляем поддержку статических файлов
//Теперь, если мы обратимся к добавленному файлу, например, по пути /index.html, то он нам отобразится, по другим путям - Hello World 
app.Run(async (context) => await context.Response.WriteAsync("Hello World"));

app.Run();


//Если бы index.html находился бы в какой-то вложенной папке, например, в wwwroot/html/, то для обращения к нему мы могли бы использовать путь /html/index.html.
//То есть middleware для работы со статическими сайтами автоматически сопоставляет запросы с путями к статическим файлам в рамках папки wwwroot.

//Изменение пути к статическим файлам
// Для этого добавим в проект папку static в проект и определим в ней какой-нибудь html-файл. Пусть он будет называться content.html
// Чтобы приложение восприняло эту папку, изменим код создания хоста в файле Program.cs:
//var builder = WebApplication.CreateBuilder( new WebApplicationOptions { WebRootPath = "static" });  // изменяем папку для хранения статики
//var app = builder.Build();
//app.UseStaticFiles();   // добавляем поддержку статических файлов
//app.Run(async (context) => await context.Response.WriteAsync("Hello World"));
//app.Run();
// Для добавления пути к файлам используется перегруженная версия метода CreateBuilder(), которая в качестве параметра принимает объект WebApplicationOptions.
// Его свойство WebRootPath позволяет установить папку для статических файлов.