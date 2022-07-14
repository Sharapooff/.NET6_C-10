var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ��������� ������ HTTP
app.UseStatusCodePages();

app.Map("/hello", () => "Hello ASP.NET Core");

app.Run();


//� ������� �� ���������� ����������� ���������� ������� ASP.NET Core ����� ����� �� ������������ ������ HTTP, ��������, � ������ ���� ������ �� ������.
// ��� ��������� � ��������������� ������� �� ������ � �������� ������ ��������, � ������ ����� ������� ���-�������� �� ������ ������� ��������� ���.
// ���� ������� ����� ���������� �����-�� ����������� ��������.
// �� � ������� ���������� StatusCodePagesMiddleware ����� �������� � ������ �������� ���������� � ��������� ����. ��� ����� ����������� ����� app.UseStatusCodePages().
// ����� UseStatusCodePages() ������� �������� ����� � ������ ��������� ��������� �������, � ���������, �� ���������� middleware ��� ������ �� ������������ ������� � �� ���������� �������� �����.

//��������� ���������
// ���������, ������������ ������� UseStatusCodePages() �� ���������, �� ����� �������������. ������ ���� �� ������ ������ ��������� ��������� ������������ ������������ ���������. 
// � �������� ������� ��������� ����������� MIME-��� ������ - � ������ ������ ������� ����� (""text/plain"").
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//// ��������� ������ HTTP
//app.UseStatusCodePages("text/plain", "Error: Resource Not Found. Status code: {0}");
//app.Map("/hello", () => "Hello ASP.NET Core");
//app.Run();

//��������� ����������� ������
// ��� ���� ������ ������ UseStatusCodePages() ��������� ����� �������� ������ ��������� ������. � ���������, ��� ��������� �������, �������� �������� - ������ StatusCodeContext.
// � ���� �������, ������ StatusCodeContext ����� �������� HttpContext, �� �������� �� ����� �������� ��� ���������� � ������� � ��������� �������� ������.
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//// ��������� ������ HTTP
//app.UseStatusCodePages(async statusCodeContext =>
//{
//    var response = statusCodeContext.HttpContext.Response;
//    var path = statusCodeContext.HttpContext.Request.Path;

//    response.ContentType = "text/plain; charset=UTF-8";
//    if (response.StatusCode == 403)
//    {
//        await response.WriteAsync($"Path: {path}. Access Denied ");
//    }
//    else if (response.StatusCode == 404)
//    {
//        await response.WriteAsync($"Resource {path} Not Found");
//    }
//});
//app.Map("/hello", () => "Hello ASP.NET Core");
//app.Run();

//������ UseStatusCodePagesWithRedirects � UseStatusCodePagesWithReExecute
// ������ ������ UseStatusCodePages() �� ����� ����� ������������ ��� ���� ������, ������� ����� ������������ ������ HTTP.
// � ������� ������ app.UseStatusCodePagesWithRedirects() ����� ��������� ������������� �� ������������ �����, ������� ��������������� ���������� ��������� ���:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseStatusCodePagesWithRedirects("/error/{0}");
//app.Map("/hello", () => "Hello ASP.NET Core");
//app.Map("/error/{statusCode}", (int statusCode) => $"Error. Status Code: {statusCode}");
//app.Run();

//����� ����� ���� ��������������� �� ������ "/error/{0}". � �������� ��������� ����� ����������� "{0}" ����� ������������ ��������� ��� ������.
// �� ������ ��� ��������� � ��������������� ������� ������ ������� ��������� ��� 302 / Found. �� ���� ��������� �������������� ������ ����� ������������,
// ������ ��������� ��� 302 ����� ���������, ��� ������ ��������� �� ������ ����� - �� ���� "/error/404".
// �������� ��������� ����� ���� ��������, �������� � ����� ������ ��������� ����������, � � ���� ������ �� ����� ��������� ������ ����� app.UseStatusCodePagesWithReExecute():
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseStatusCodePagesWithReExecute("/error/{0}");
//app.Map("/hello", () => "Hello ASP.NET Core");
//app.Map("/error/{statusCode}", (int statusCode) => $"Error. Status Code: {statusCode}");
//app.Run();

// � �������� ��������� ����� UseStatusCodePagesWithReExecute() ��������� ���� � �������, ������� ����� ������������ ������. � ����� � ������� ������������ {0}
// ����� �������� ��������� ��� ������. �� ���� � ������ ������ ��� ������������� ������ ����� ���������� �������� �����
// app.Map("/error/{statusCode}", (int statusCode) => $"Error. Status Code: {statusCode}");

