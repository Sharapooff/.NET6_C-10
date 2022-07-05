var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    if (context.Request.Path == "/old")//Теперь при обращении по адресу "/old" произойдет перенаправление на адрес "/new"
    {
        context.Response.Redirect("/new");
        //context.Response.Redirect("https://www.google.com"); // на внешний ресурс
    }
    else if (context.Request.Path == "/new")
    {
        await context.Response.WriteAsync("New Page");
    }
    else
    {
        await context.Response.WriteAsync("Main Page");
    }
});

app.Run();



//Для выполнения переадресации у объекта HttpResponse определен метод Redirect():
// void Redirect(string location)
// void Redirect(string location, bool permanent)
// Если этот параметр равен true, то переадресация будет постоянной, и в этом случае посылается статусный код 301. Если равен false,
// то переадресация временная, и посылается статусный код 302.