var builder = WebApplication.CreateBuilder(args);

// проецируем класс SecretCodeConstraint на inline-ограничение secretcode
// !!! если мы хотим использовать класс ограничения как inline-ограничение внутри шаблона маршрута, то нам необходимо изменить настройки для сервиса RouteOptions
builder.Services.Configure<RouteOptions>(options => options.ConstraintMap.Add("secretcode", typeof(SecretCodeConstraint)));
// альтернативное добавление класса ограничения
// builder.Services.AddRouting(options => options.ConstraintMap.Add("secretcode", typeof(SecretConstraint)));
//2й пример
builder.Services.AddRouting(options => options.ConstraintMap.Add("invalidnames", typeof(InvalidNamesConstraint)));

var app = builder.Build();

app.Map("/users/{name}/{token:secretcode(123466)}/", (string name, int token) => $"Name: {name} \nToken: {token}");
app.Map("/users/{name:invalidnames}", (string name) => $"Name: {name}");
app.Map("/", () => "Index Page");

app.Run();




// ASP.NET Core по умолчанию предоставляет большой набор встроенных ограничений, их может быть недостаточно. И в этом случае мы можем определить свои кастомные ограничения маршрутов.
//Для создания собственного ограничения маршрута нужно реализовать интерфейс IRouteConstraint с одним единственным методом Match, который имеет следующее определение:
//public interface IRouteConstraint
//{
//    bool Match(HttpContext? httpContext, // контекст запроса
//            IRouter? route,              // маршрут запроса
//            string routeKey,             // назание параметра, к которому применяется ограничение
//            RouteValueDictionary values, // набор параметров маршрута в виде словаря
//            RouteDirection routeDirection); // указывает, применяется ограничение при обработке запроса, либо при генерации ссылки
//}

//Ограничение для параметра маршрута
// Клиент в запросе через параметр должен передавать код, который равен некоторой строке. Для этого определим следующий класс ограничения маршрута:
public class SecretCodeConstraint : IRouteConstraint
{
    string secretCode;    // допустимый код
    public SecretCodeConstraint(string secretCode)
    {
        this.secretCode = secretCode;
    }
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return values[routeKey]?.ToString() == secretCode;
    }
}

//Другой пример. Допустим, мы хотим, чтобы параметр не мог иметь одно из набора значений:
public class InvalidNamesConstraint : IRouteConstraint
{
    string[] names = new[] { "Tom", "Sam", "Bob" }; // НЕ должен представлять имена "Tom", "Sam" и "Bob"
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        return !names.Contains(values[routeKey]?.ToString());
    }
}