using _1_15_������_middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//��� ���������� ���������� middleware, ������� ������������ �����, � �������� ��������� ������� ����������� ����� UseMiddleware(). 
app.UseMiddleware<TokenMiddleware>();

app.Run(async (context) => await context.Response.WriteAsync("Main branch"));
//� ������� ������ UseMiddleware<T> � ����������� ������� TokenMiddleware ����� ���������� ������ ��� ��������� RequestDelegate next.
//������� ����� ������� ���������� �������� ��� ����� ��������� ��� �� �����.

//����� ���������� ��� ����������� middleware/ �.�. ����� ������� ��� ������� ����
//app.UseToken();

app.Run();




//� �������� ����, ������������ ���������� middleware ���������� ������������ ������, �� ���� ��� ���������� inline middleware.
//������ ASP.NET Core ��������� ���������� middleware � ���� ��������� �������.

//����� ���������� ��� ����������� middleware 
// ����� ������� ��� ����������� �������� ����������� middleware ������������ ����������� ������ ����������.
// ���, ������� � ������ ����� �����, ������� ������� TokenExtensions.

//�������� ����������
// ������� ����� TokenMiddleware, ����� �� ����� ������� ������� ������ ��� ���������:
//public class TokenMiddleware
//{
//    private readonly RequestDelegate next;
//    string pattern;
//    public TokenMiddleware(RequestDelegate next, string pattern)
//    {
//        this.next = next;
//        this.pattern = pattern;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        var token = context.Request.Query["token"];
//        if (token != pattern)
//        {
//            context.Response.StatusCode = 403;
//            await context.Response.WriteAsync("Token is invalid");
//        }
//        else
//        {
//            await next.Invoke(context);
//        }
//    }
//}

// ������� ������, � ������� ���� ���������, ��������������� ����� �����������. ����� �������� ��� � �����������, ������� ����� TokenExtensions:
//public static class TokenExtensions
//{
//    public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string pattern)
//    {
//        return builder.UseMiddleware<TokenMiddleware>(pattern);
//    }
//}

//� ����� builder.UseMiddleware ����� �������� ����� ��������, ������� ���������� � ����������� ���������� middleware.
//� ��� ������ ������ ���������� UseToken � ���� ����� �������� ���������� ��������:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();

//app.UseToken("555555");

//app.Run(async (context) => await context.Response.WriteAsync("Hello METANIT.COM"));

//app.Run();