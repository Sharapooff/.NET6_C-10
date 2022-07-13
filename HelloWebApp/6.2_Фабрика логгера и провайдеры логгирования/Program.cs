var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());//метод AddConsole() объекта ILoggingBuilder устанавливает вывод сообщений лога на консоль
//ILogger logger = loggerFactory.CreateLogger<Program>();
//app.Run(async (context) =>
//{
//    logger.LogInformation($"Requested Path: {context.Request.Path}");
//    await context.Response.WriteAsync("Hello World!!!");
//});

app.Map("/hello", (ILoggerFactory loggerFactory) => {

    // создаем логгер с категорией "MapLogger"
    ILogger logger = loggerFactory.CreateLogger("MapLogger");
    // логгируем некоторое сообщение
    logger.LogInformation($"Path: /hello   Time: {DateTime.Now.ToLongTimeString()}");
    return "Hello World!";
});



app.Run();


//В примерах в прошлой теме мы получали объект логгера, который добавляется через DI. Но мы можем также использовать фабрику логгера для его создания
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());//метод AddConsole() объекта ILoggingBuilder устанавливает вывод сообщений лога на консоль
//ILogger logger = loggerFactory.CreateLogger<Program>();
//app.Run(async (context) =>
//{
//    logger.LogInformation($"Requested Path: {context.Request.Path}");
//    await context.Response.WriteAsync("Hello World!");
//});
//app.Run();

//Получение фабрики логгера через dependency injection
// Как и логгер, фабрика логгера доступна в приложении в виде сервиса, соответственно ее можно получит через механизм внедрения зависимостей:
// var builder = WebApplication.CreateBuilder();
// var app = builder.Build();
//app.Map("/hello", (ILoggerFactory loggerFactory) => {

//    // создаем логгер с категорией "MapLogger"
//    ILogger logger = loggerFactory.CreateLogger("MapLogger");
//    // логгируем некоторое сообщение
//    logger.LogInformation($"Path: /hello   Time: {DateTime.Now.ToLongTimeString()}");
//    return "Hello World!";
//});
//app.Run();

//Провайдеры логгирования
// В примере выше логгирование шло на консоль. Вообще путь логгирования определяется провайдером логгирования.
// По умолчанию ASP.NET Core предоставляет следующие провайдеры:
// Console: вывод информации на консоль.Устанавливается методом AddConsole()
// Debug: использует для ведения записей лога класс System.Diagnostics.Debug и в частности его метод Debug.WriteLine.Соответственно все записи лога
// мы можем увидеть в окне Output в Visual Studio.Устанавливается методом AddDebug(). Стоит отметить, что данный способ работает только при запуске проекта в режиме отладки
// EventSource: на Windows введет логгирование в лог ETW (Event Tracing for Windows), для просмотра которого может использоваться инструмент PerfView (или аналогичный инструменты).
// Хотя данный провайдер задумывался как кроссплатформенный, для Linux и MacOS пока назначение лога не определено. Устанавливается методом AddEventSourceLogger()
// EventLog: записывает в Windows Event Log, соответственно работает только при запуске на Windows. Устанавливается методом AddEventLog()
// Вместо консоли зададим вывод лога в окне Output в Visual Studio:
// var loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());

