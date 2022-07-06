var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string date = "";

app.Use(async (context, next) =>
{
    date = DateTime.Now.ToShortDateString();
    await next.Invoke();                 // вызываем middleware из app.Run //и вариант await next.Invoke(context);  если здесь next - RequestDelegate
    Console.WriteLine($"Current date: {date}");  // Current date: 08.12.2021
});

app.Run(async (context) => await context.Response.WriteAsync($"Date: {date}"));

app.Run();


//Метод расширения Use() добавляет компонент middleware, который позволяет передать обработку запроса далее следующим в конвейере компонентам.
// В общем случае применение метода Use() выглядит следующим образом:
// app.Use(async (context, next) =>
// {
//     // действия перед передачи запроса в следующий middleware
//     await next.Invoke();
//     // действия после обработки запроса следующим middleware
// });
// Таким образом, middleware в методе Use выполняет действия до следующего в конвейере компонента и после него.

//Отправка ответа
// Не рекомендуется вызывать метод next.Invoke после метода Response.WriteAsync().
// Компонент middleware должен либо генерировать ответ с помощью Response.WriteAsync, либо вызывать следующий делегат посредством next.Invoke,
// но не выполнять оба этих действия одновременно.
// Следующая обработка не рекомендуется:
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("<p>Hello world!</p>");
//    await next.Invoke();
//});
//app.Run(async (context) =>
//{
//    //await Task.Delay(10000); // можно поставить задержку
//    await context.Response.WriteAsync("<p>Good bye, World...</p>");
//});

//Терминальный компонент middleware
// Middleware в методе Use() необязательно должен вызывать к следующему в конвейере компоненту. Вместо этого он может завершить обработку запроса.
// В этом случае он может выступать в роли такого же терминального компонента middleware, а и компоненты из метода Run(). Например:
//app.Use(async (context, next) =>
//{
//    string? path = context.Request.Path.Value?.ToLower();
//    if (path == "/date")
//    {
//        await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
//    }
//    else
//    {
//        await next.Invoke();
//    }
//});
// Причем в принципе мы можем использовать компонент в app.Use как единственный и соответственно терминальный:
//app.Use(async (HttpContext context, Func<Task> next) =>
//{
//    await context.Response.WriteAsync("Hello Work!");
//});
//app.Run();

//Вынесение компонентов в методы
// Также можно вынести все inline-компоненты в отдельные методы:
//app.Use(GetDate);
//app.Run(async (context) => await context.Response.WriteAsync("Hello World.COM"));
//app.Run();

//async Task GetDate(HttpContext context, Func<Task> next)
//{
//    string? path = context.Request.Path.Value?.ToLower();
//    if (path == "/date")
//    {
//        await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
//    }
//    else
//    {
//        await next.Invoke();
//    }
//}