var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//��������� ���������� ������� (����������� �������� Headers)
app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var stringBuilder = new System.Text.StringBuilder("<table>");

    foreach (var header in context.Request.Headers)
    {
        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");
    await context.Response.WriteAsync(stringBuilder.ToString());
    
});
//app.Run(async (context) => await context.Response.WriteAsync($"Path: {context.Request.Path}"));
//app.Run(async (context) => await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" + $"<p>QueryString: {context.Request.QueryString}</p>");
app.Run();



//�������� Request ������� HttpContext ������������ ������ HttpRequest � ������ ���������� � ������� � ���� ��������� �������:
// Body: ������������� ���� ������� � ���� ������� Stream
// BodyReader: ���������� ������ ���� PipeReader ��� ������ ���� �������
// ContentLength: �������� ��� ������������� ��������� Content-Length
// ContentType: �������� ��� ������������� ��������� Content-Type
// Cookies: ���������� ��������� ���� (������ Cookies), ��������������� � ������ ��������
// Form: �������� ��� ������������� ���� ������� � ���� ����
// HasFormContentType: ��������� ������� ��������� Content-Type
// Headers: ���������� ��������� �������
// Host: �������� ��� ������������� ��������� Host
// HttpContext: ���������� ��������� � ������ �������� ������ HttpContext
// IsHttps: ���������� true, ���� ����������� �������� https
// Method: �������� ��� ������������� ����� HTTP
// Path: �������� ��� ������������� ���� ������� � ���� ������� RequestPath
// PathBase: �������� ��� ������������� ������� ���� �������. ����� ���� �� ������ ��������� ����������� ����
// Protocol: �������� ��� ������������� ��������, ��������, HTTP
// Query: ���������� ��������� ���������� �� ������ �������
// QueryString: �������� ��� ������������� ������ �������
// RouteValues: �������� ������ �������� ��� �������� �������
// Scheme: �������� ��� ������������� ����� ������� HTTP

//��������� ���������� ������� (����������� �������� Headers) __________________________________________
// ��� ����������� ����������� ���������� HTTP � ���� ���������� ���������� ����������� ��������, ��������,
// ��� ��������� "content-type" ���������� �������� ContentType, � ��� ��������� "accept" - �������� Accept
// var acceptHeaderValue = context.Request.Headers.Accept;
//����� ������� ���������, � ����� �����-�� ��������� ���������, ��� ������� �� ���������� �������� ��������, ����� �������� ��� � ����� ����� ������� �������:
// var acceptHeaderValue = context.Request.Headers["accept"];

//��������� ���� ������� _________________________________________________________________________________
// �������� path ��������� �������� ����������� ����, �� ���� �����, � �������� ���������� ������.
// �������� path ��������� �������� ����������� ����, �� ���� �����, � �������� ���������� ������:
// app.Run(async(context) => await context.Response.WriteAsync($"Path: {context.Request.Path}"));

//������ ������� ________________________________________________________________________________________
//�������� QueryString ��������� �������� ������ �������. ������ ������� ������������ �� ����� ������������ ������,
//������� ���� ����� ������� ? � ������������ ����� ����������, ����������� �������� ���������� &.
//������ ������� (query string) �� ������ � ���� ������� (path)
//context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" + $"<p>QueryString: {context.Request.QueryString}</p>");
//� ������� �������� Query ����� �������� ��� ��������� ������ ������� � ���� �������.
//foreach (var param in context.Request.Query) {
//    stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");}
//�������������� ����� �������� �� ������� Query �������� ��������� ����������.
//string name = context.Request.Query["name"];
//string age = context.Request.Query["age"];