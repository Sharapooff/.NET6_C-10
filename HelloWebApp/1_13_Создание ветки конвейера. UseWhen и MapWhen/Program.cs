var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// создание ветки происходит один раз при запуске приложения. Т.е. То, что внутри метода, сам объект делегата.
// Поэтому, при определении var time внутри метода, он создается каждый раз, но при определении вне метода, а в теле ветки
// переменная будет создана один раз при запуске приложения
app.UseWhen(
    context => context.Request.Path == "/time", // условие, если путь запроса "/time"
    appBuilder =>
    {
        //var time = DateTime.Now.ToShortTimeString();
        // логгируем данные - выводим на консоль приложения
        appBuilder.Use(async (context, next) =>
        {
            var time = DateTime.Now.ToShortTimeString();
            Console.WriteLine($"Time: {time}");
            await next();   // вызываем следующий middleware
        });

        // отправляем ответ
        appBuilder.Run(async context =>
        {
            var time = DateTime.Now.ToShortTimeString();
            await context.Response.WriteAsync($"Time: {time}");
        });
        //В примере выше ветка конвейера завершалась терминальным компонентом, поэтому остальные действия в основной части конвейера не выполнялись.
        //Однако мы можем также передать запрос на обработку из ветки в основной поток конвейера
        //await next();   // вызываем следующий middleware // что вернет нас в основную ветку
    });
//если не соответствует условию
app.Run(async context =>
{
    await context.Response.WriteAsync("Без ответвления");
});

app.Run();


//UseWhen
// Метод UseWhen() на основании некоторого условия позволяет создать ответвление конвейера при обработке запроса
// Как и Use(), метод UseWhen() реализован как метод расширения для типа IApplicationBuilder.
// В качестве параметра он принимает делегат Func>HttpContext,bool> - некоторое условие, которому должен соответствовать запрос. В этот делегат передается объект HttpContext.
// А возвращаемым типом должен быть тип bool - если запрос соответствует условию, то возвращается true, иначе возвращаеся false.
// Последний параметр метода - делегат Action<IApplicationBuilder> представляет некоторые действия над объектом IApplicationBuilder, 
// который передается в делегат в качестве параметра.

//MapWhen
// Метод MapWhen(), как и метод UseWhen(), на основании некоторого условия позволяет создать ответвление конвейера
// Метод MapWhen() также реализован как метод расширения для типа IApplicationBuilder, принимает те же параметры, что и UseWhen(), и работает во многом аналогичным образом:
//app.MapWhen(
//    context => context.Request.Path == "/time", // условие: если путь запроса "/time"
//    appBuilder => appBuilder.Run(async context =>
//    {
//        var time = DateTime.Now.ToShortTimeString();
//        await context.Response.WriteAsync($"current time: {time}");
//    })
//);

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Без ответвления");
//});

//app.Run();




//Для большей читабельности также можно было бы вынести действия по созданию ветки конвейера в отдельный метод:
//app.UseWhen(
//    context => context.Request.Path == "/time", // условие: если путь запроса "/time"
//    HandleTimeRequest
//);

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello METANIT.COM");
//});

//app.Run();

//void HandleTimeRequest(IApplicationBuilder appBuilder)
//{
//    appBuilder.Use(async (context, next) =>
//    {
//        var time = DateTime.Now.ToShortTimeString();
//        Console.WriteLine($"current time: {time}");
//        await next();   // вызываем следующий middleware
//    });
//}