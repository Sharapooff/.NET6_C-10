var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//app.Run(async (context) => await context.Response.WriteAsync("Hello World!"));
//app.Run(HandleRequst);//можем вынести код middleware в отдельный метод
int x = 2;
app.Run(async (context) => //Компоненты middleware создаются один раз и существуют в течение всего жизненного цикла приложения
{
    x = x * 2;  //  2 * 2 = 4
    await context.Response.WriteAsync($"Result: {x}");
});
app.Run();

//Самый простой способ добавления middleware в конвейер обработки запроса в ASP.NET Core представляет метод Run(),
//который определен как метод расширения для интерфейса IApplicationBuilder (соответствено его поддерживает и класс WebApplication).
//Метод Run добавляет терминальный компонент - такой компонент, который завершает обработку запроса, соответствено он не вызывает никакие другие компоненты
//и обработку запроса дальше - следующим в конвейере компонентам не передает. Данный метод следует вызывать в самом конце построения конвейера обработки запроса.
//До него же могут быть помещены другие методы, которые добавляют компоненты middleware.

//В качестве параметра метод Run принимает делегат RequestDelegate. Этот делегат имеет следующее определение:
// public delegate Task RequestDelegate(HttpContext context);
// Он принимает в качестве параметра контекст запроса HttpContext и возвращает объект Task.

//При необходимости мы можем вынести код middleware в отдельный метод:
async Task HandleRequst(HttpContext context)
{
    await context.Response.WriteAsync("Hello World! :)");
}

//Жизненный цикл middleware_________________________________________________________________

//Компоненты middleware создаются один раз и существуют в течение всего жизненного цикла приложения.
//То есть для последующей обработки запросов используются одни и те же компоненты. 

