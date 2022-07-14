var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseDeveloperExceptionPage(); // не обязательно

// меняем имя окружения
app.Environment.EnvironmentName = "Production"; 

app.Run(async (context) =>
{
    int a = 5;
    int b = 0;
    int c = a / b;
    await context.Response.WriteAsync($"c = {c}");
});


app.Run();




//Ошибки в приложении можно условно разделить на два типа:
// исключения, которые возникают в процессе выполнения кода (например, деление на 0), и стандартные ошибки протокола HTTP (например, ошибка 404).

// Обычные исключения могут быть полезны для разработчика в процессе создания приложения, но простые пользователи не должны будут их видеть.
// Для обработки исключений в приложении, которое находится в процессе разработки, предназначен специальный middleware - DeveloperExceptionPageMiddleware.
// Однако вручную нам его не надо добавлять, поскольку оно добавляется автоматически:
// if (context.HostingEnvironment.IsDevelopment())
// {
//    app.UseDeveloperExceptionPage();
// }
// Если приложение находится в состоянии разработки, то с помощью middleware app.UseDeveloperExceptionPage()
// приложение перехватывает исключения и выводит информацию о них разработчику.

// Выражение app.Environment.EnvironmentName = "Production" меняет имя окружения с "Development" (которое установлено по умолчанию) на "Production".
// В этом случае среда ASP.NET Core больше не будет расценивать приложение как находящееся в стадии разработки.
// Соответственно middleware из метода app.UseDeveloperExceptionPage() не будет перехватывать исключение. А приложение будет возвращать пользователю ошибку 500,
// которая указывает, что на сервере возникла ошибка при обработке запроса. Однако реально природа подобной ошибки может заключать в чем угодно.

//UseExceptionHandler
// Это не самая лучшая ситуация, и нередко все-таки возникает необходимость дать пользователям некоторую информацию о том, что же все-таки произошло.
// Либо потребуется как-то обработать данную ситуацию. Для этих целей можно использовать еще один встроенный middleware - ExceptionHandlerMiddleware,
// который подключается с помощью метода UseExceptionHandler(). Он перенаправляет при возникновении исключения на некоторый адрес и позволяет обработать исключение.
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.Environment.EnvironmentName = "Production"; // меняем имя окружения
//// если приложение не находится в процессе разработки
//// перенаправляем по адресу "/error"
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error"); // перенаправляет при возникновении исключения на некоторый адрес и позволяет обработать исключение
//}
//// middleware, которое обрабатывает исключение
//app.Map("/error", app => app.Run(async context =>
//{
//    context.Response.StatusCode = 500;
//    await context.Response.WriteAsync("Error 500. DivideByZeroException occurred!");
//}));

//  данный способ имеет небольшой недостаток - мы можем напрямую обратиться по адресу "/error" и получить тот же ответ от сервера.
//  Но в этом случае мы можем использовать другую версию метода UseExceptionHandler(), которая принимает делегат, параметр которого представляет объект IApplicationBuilder:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.Environment.EnvironmentName = "Production";
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler(app => app.Run(async context =>
//    {
//        context.Response.StatusCode = 500;
//        await context.Response.WriteAsync("Error 500. DivideByZeroException occurred!");
//    }));
//}
//app.Run(async (context) =>
//{
//    int a = 5;
//    int b = 0;
//    int c = a / b;
//    await context.Response.WriteAsync($"c = {c}");
//});
//app.Run();
// Следует учитывать, что app.UseExceptionHandler() следует помещать ближе к началу конвейера middleware.








