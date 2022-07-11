var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/users/{id}/{name}", (string id, string name) => $"User Id: {id}   User Name: {name}");//другие символы-разделители "/users/{id}-{name}"
app.Map("/users/{id}-{name}", HandleRequest);
app.Map("/users/{id}", (string id) => $"User Id: {id}");
app.Map("/users", () => "Users Page");
app.Map("/", () => "Index Page");

app.Run();




//Шаблон маршрута, который сопоставляется с конечной точкой, может иметь параметры.
// Параметры имеют имя и определяются в шаблоне маршрута внутри фигурных скобок: {название_параметра}

//Определение нескольких параметров
// Подобным образом можно определять и большее количество параметров маршрута

//Вынесение обработчика маршрута в отдельный метод
string HandleRequest(string id, string name)
{
    return $"User Id: {id}   User Name: {name}";
}

//Необязательные параметры следует помещать в конце шаблона маршрута
//app.Map("/users/{id?}", (string? id) => $"User Id: {id??"Undefined"}");

//Значения параметров по умолчанию
// Параметрам маршрута также можно назначить значения по умолчанию на случай, если им не переданы значения:
//app.Map(
//    "{controller=Home}/{action=Index}/{id?}",
//    (string controller, string action, string? id) =>
//        $"Controller: {controller} \nAction: {action} \nId: {id}"
//);

//Передача произвольного количества параметров в запросе
// Мы можем обозначить любое количество сегментов в запросе, чтобы не быть жестко привязанным к числу сегментов с помощью
// параметра со знаком * ("звездочка") или * *(две звездочки) (это так называемый catchall-параметр):
// app.Map("users/{**info}", (string info) => $"User Info: {info}");