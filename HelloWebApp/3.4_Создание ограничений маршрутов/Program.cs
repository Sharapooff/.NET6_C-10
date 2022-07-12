var builder = WebApplication.CreateBuilder(args);

// ���������� ����� SecretCodeConstraint �� inline-����������� secretcode
// !!! ���� �� ����� ������������ ����� ����������� ��� inline-����������� ������ ������� ��������, �� ��� ���������� �������� ��������� ��� ������� RouteOptions
builder.Services.Configure<RouteOptions>(options => options.ConstraintMap.Add("secretcode", typeof(SecretCodeConstraint)));
// �������������� ���������� ������ �����������
// builder.Services.AddRouting(options => options.ConstraintMap.Add("secretcode", typeof(SecretConstraint)));
//2� ������
builder.Services.AddRouting(options => options.ConstraintMap.Add("invalidnames", typeof(InvalidNamesConstraint)));

var app = builder.Build();

app.Map("/users/{name}/{token:secretcode(123466)}/", (string name, int token) => $"Name: {name} \nToken: {token}");
app.Map("/users/{name:invalidnames}", (string name) => $"Name: {name}");
app.Map("/", () => "Index Page");

app.Run();




// ASP.NET Core �� ��������� ������������� ������� ����� ���������� �����������, �� ����� ���� ������������. � � ���� ������ �� ����� ���������� ���� ��������� ����������� ���������.
//��� �������� ������������ ����������� �������� ����� ����������� ��������� IRouteConstraint � ����� ������������ ������� Match, ������� ����� ��������� �����������:
//public interface IRouteConstraint
//{
//    bool Match(HttpContext? httpContext, // �������� �������
//            IRouter? route,              // ������� �������
//            string routeKey,             // ������� ���������, � �������� ����������� �����������
//            RouteValueDictionary values, // ����� ���������� �������� � ���� �������
//            RouteDirection routeDirection); // ���������, ����������� ����������� ��� ��������� �������, ���� ��� ��������� ������
//}

//����������� ��� ��������� ��������
// ������ � ������� ����� �������� ������ ���������� ���, ������� ����� ��������� ������. ��� ����� ��������� ��������� ����� ����������� ��������:
public class SecretCodeConstraint : IRouteConstraint
{
    string secretCode;    // ���������� ���
    public SecretCodeConstraint(string secretCode)
    {
        this.secretCode = secretCode;
    }
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return values[routeKey]?.ToString() == secretCode;
    }
}

//������ ������. ��������, �� �����, ����� �������� �� ��� ����� ���� �� ������ ��������:
public class InvalidNamesConstraint : IRouteConstraint
{
    string[] names = new[] { "Tom", "Sam", "Bob" }; // �� ������ ������������ ����� "Tom", "Sam" � "Bob"
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        return !names.Contains(values[routeKey]?.ToString());
    }
}