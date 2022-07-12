var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Map("/users/{id}", (int id) => $"User Id: {id}"); //если передать вместо числа и строку - получим ошибку при преобразовании
app.Map("/users/{id:int}", (int id) => $"User Id: {id}"); //ограничение int указывает, что параметр должен представлять тип int
app.Map("/", () => "Index Page");
app.Map("/users/{name:alpha:minlength(2)}/{age:int:range(1, 110)}", (string name, int age) => $"User Age: {age} \nUser Name:{name}");
app.Map("/phonebook/{phone:regex(^7-\\d{{3}}-\\d{{3}}-\\d{{4}}$)}/", (string phone) => $"Phone: {phone}");

app.Run();




// Ограничения маршрутов выполняюся при парсинге пути запроса, сопоставлении его с шаблоном маршрута и выделении из него значений для параметров маршрута. 
// Для установки ограничения после названия параметра через двоеточие указывается название ограничения.
// И если мы не применили ограничение, то получим ошибку преобразования, а е сли указав ограничение пеередадим вместо int строку, то получим ошибку 404, то есть ресурс не найден.
// ASP.NET Core предоставляет следующий ряд ограничений маршрута:
// int, bool, datetime, decimal, double, float, guid, long, (min)(max)length(3), length(min, max), min(value), max(value), range(min, max), regex(expression), required(обязат парам)

// Каждое подобное ограничени представляет определенный класс. Все классы ограничений находятся в пространстве имен Microsoft.AspNetCore.Routing.Constraints
