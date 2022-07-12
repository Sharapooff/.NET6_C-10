var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();  // поддержка страниц html по умолчанию
app.UseStaticFiles();

app.Run(async (context) => await context.Response.WriteAsync("Hello World"));

app.Run();




//— помощью специального метода расширени€ UseDefaultFiles() можно настроить отправку статических веб-страниц по умолчанию без обращени€ к ним по полному пути.
// ¬ этом случае при отправке запроса к корню веб-приложени€ типа http://localhost:xxxx/ приложение будет искать в папке wwwroot следующие файлы:
// default.htm
// default.html
// index.htm
// index.html

//≈сли же мы хотим использовать файл, название которого отличаетс€ от вышеперечисленных, то нам надо в этом случае применить объект DefaultFilesOptions:
//DefaultFilesOptions options = new DefaultFilesOptions();
//options.DefaultFileNames.Clear(); // удал€ем имена файлов по умолчанию
//options.DefaultFileNames.Add("hello.html"); // добавл€ем новое им€ файла
//app.UseDefaultFiles(options); // установка параметров
//app.UseStaticFiles();

//ћетод UseDirectoryBrowser
// ћетод UseDirectoryBrowser позвол€ет пользовател€м просматривать содержимое каталогов на сайте:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseDirectoryBrowser();
//app.UseStaticFiles();
//app.Run();

//ƒанный метод имеет перегрузку, котора€ позвол€ет сопоставить определенный каталог на жестком диске или в проекте с некоторой строкой запроса и тем самым потом 
//отобразить содержимое этого каталога:
//using Microsoft.Extensions.FileProviders; 
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseDirectoryBrowser(new DirectoryBrowserOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
//    RequestPath = new PathString("/pages")
//});
//app.UseStaticFiles();
//app.Run();

//—опоставление каталогов с пут€ми
// ѕерегрузка метода UseStaticFiles() позвол€ет сопоставить пути с определенными каталогами:
//using Microsoft.Extensions.FileProviders; 
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions() // обрабатывает запросы к каталогу wwwroot/html
//{
//    FileProvider = new PhysicalFileProvider( Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
//    RequestPath = new PathString("/pages")
//});
//app.Run();

//ћетод UseFileServer
// ћетод UseFileServer() объедин€ет функциональность сразу всех трех вышеописанных методов UseStaticFiles, UseDefaultFiles и UseDirectoryBrowser:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseFileServer();
//app.Run();

//ѕо умолчанию этот метод позвол€ет обрабатывать статические файлы и отправл€ть файлы по умолчанию типа index.html. ≈сли нам надо еще включить просмотр каталогов,
//то мы можем использовать перегрузку данного метода:
//app.UseFileServer(enableDirectoryBrowsing: true);

//≈ще одна перегрузка метода позвол€ет более точно задать параметры:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseFileServer(new FileServerOptions
//{
//    EnableDirectoryBrowsing = true,
//    EnableDefaultFiles = false
//});
//app.Run();

//“акже можно настроить сопоставление путей запроса с каталогами:
//using Microsoft.Extensions.FileProviders; 
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseFileServer(new FileServerOptions
//{
//    EnableDirectoryBrowsing = true,
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
//    RequestPath = new PathString("/pages"),
//    EnableDefaultFiles = false
//});
//app.Run();
//¬ этом случае будет разрешен обзор каталога по пути http://localhost:xxxx/pages/, но при этом путь http://localhost:xxxx/html/ работать не будет.