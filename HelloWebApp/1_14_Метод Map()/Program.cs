var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Map() создает ответвление конвейера, которое будет обрабатывать запросы по пути "/time"
app.Map("/time", appBuilder =>
{
    var time = DateTime.Now.ToShortTimeString();

    // логгируем данные - выводим на консоль приложения
    appBuilder.Use(async (context, next) =>
    {
        Console.WriteLine($"Time: {time}");
        await next();   // вызываем следующий middleware
    });

    appBuilder.Run(async context => await context.Response.WriteAsync($"Time: {time}"));
});
//Подобным образом можно создавать ветки для разных путей
app.Map("/index", appBuilder =>
{
    appBuilder.Run(async context => await context.Response.WriteAsync("Index Page"));
});
app.Map("/about", appBuilder =>
{
    appBuilder.Run(async context => await context.Response.WriteAsync("About Page"));
});

app.Run(async (context) => await context.Response.WriteAsync("Main branch"));

app.Run();



//Метод Map()
// Применяется для создания ветки конвейера, которая будет обрабатывать запрос по определенному пути.
// Этот метод реализован как метод расширения для типа IApplicationBuilder и имеет ряд перегруженных версий.
// public static IApplicationBuilder Map (this IApplicationBuilder app, string pathMatch, Action<IApplicationBuilder> configuration);
// В качестве параметра pathMatch метод принимает путь запроса, с которым будет сопоставляться ветка. А параметр configuration представляет делегат,
// в который передается объект IApplicationBuilder и в котором будет создаваться ветка конвейера.

//Вложенные методы Map
// Ветка конвейера, которая создается в методе Map(), может иметь вложенные ветки, которые обрабатывают подзапросы.
//app.Map("/home", appBuilder =>
//{
//    appBuilder.Map("/index", Index); // middleware для "/home/index"
//    appBuilder.Map("/about", About); // middleware для "/home/about"
//    // middleware для "/home"
//    appBuilder.Run(async (context) => await context.Response.WriteAsync("Home Page"));
//});

// Map() не может обработать корневой ("/") путь (выскакивает ошибка) поэтому используем MapWhen()


//При необходимости создание веток конвейера можно вынести в отдельные методы:
//app.Map("/index", Index);
//app.Map("/about", About);

//app.Run(async (context) => await context.Response.WriteAsync("Page Not Found"));

//app.Run();

//void Index(IApplicationBuilder appBuilder)
//{
//    appBuilder.Run(async context => await context.Response.WriteAsync("Index"));
//}
//void About(IApplicationBuilder appBuilder)
//{
//    appBuilder.Run(async context => await context.Response.WriteAsync("About"));
//}