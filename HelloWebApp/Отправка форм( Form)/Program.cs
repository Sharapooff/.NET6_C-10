var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    // если обращение идет по адресу "/postuser", получаем данные формы
    if (context.Request.Path == "/postuser")
    {
        var form = context.Request.Form;
        string name = form["name"];
        string age = form["age"];
        string[] languages = form["languages"];
        // создаем из массива languages одну строку
        string langList = "";
        foreach (var lang in languages)
        {
            langList += $" {lang}";
        }
        await context.Response.WriteAsync($"<div><p>Name: {name}</p>" +
            $"<p>Age: {age}</p>" +
            $"<div>Languages:{langList}</ul></div>");
    }
    else
    {
        await context.Response.SendFileAsync("html/index.html");
    }
});

app.Run();



//Ќередко данные отправл€ютс€ на сервер с помощью форм html, обычно в запросе типа POST. ƒл€ получени€ подобных данных в классе HttpRequest
//определено свойство Form. –ассмотрим, как мы можем получить подобные данные.

//ѕолучение массивов
// ”сложним задачу и добавим в форму на странице index.html несколько полей, которые будут представл€ть массив.
// «десь добавлено три пол€ ввода, которые имеют одно и то же им€. ѕоэтому при их отправке будет формироватьс€ массив из трех значений. 