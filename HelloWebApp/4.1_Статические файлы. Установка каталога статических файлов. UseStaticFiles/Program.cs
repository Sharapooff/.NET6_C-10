//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder( new WebApplicationOptions { WebRootPath = "static" });  // ��������� ����� ��� �������� �������
var app = builder.Build();


//��� �������� ������ ����� ������������ ����� SendFileAsync() ������� HttpResponse:
//app.Run(async (context) => await context.Response.SendFileAsync("index.html"));
//������ ASP.NET Core ����� ������������� ����������� ���������� middleware, ������� ������������ � ������� ������ UseStaticFiles() � ������� �������� ������ �� ������������ �������.
//�� ��������� ��� ����������� ���� �������� ����������� ������ � ������� ������������ ��� ��������� ContentRoot � WebRoot, � ���� ����������� ����� ������ ����������
//� ������� ContentRoot/WebRoot. �� ������ ���������� �������� "ContentRoot" ������������� �������� �������� �������. � �������� "WebRoot" �� ��������� ������������ ����� wwwroot
//� ������ �������� ContentRoot. �� ����, ������ �� �������� �� ���������, �� ����������� ����� ������� ����������� � ����� "wwwroot", ������� ������ ���������� � ������� �������.
//������ ��� ��������� ��� ������������� ����� ��������������.
app.UseStaticFiles();   // ��������� ��������� ����������� ������
//������, ���� �� ��������� � ������������ �����, ��������, �� ���� /index.html, �� �� ��� �����������, �� ������ ����� - Hello World 
app.Run(async (context) => await context.Response.WriteAsync("Hello World"));

app.Run();


//���� �� index.html ��������� �� � �����-�� ��������� �����, ��������, � wwwroot/html/, �� ��� ��������� � ���� �� ����� �� ������������ ���� /html/index.html.
//�� ���� middleware ��� ������ �� ������������ ������� ������������� ������������ ������� � ������ � ����������� ������ � ������ ����� wwwroot.

//��������� ���� � ����������� ������
// ��� ����� ������� � ������ ����� static � ������ � ��������� � ��� �����-������ html-����. ����� �� ����� ���������� content.html
// ����� ���������� ���������� ��� �����, ������� ��� �������� ����� � ����� Program.cs:
//var builder = WebApplication.CreateBuilder( new WebApplicationOptions { WebRootPath = "static" });  // �������� ����� ��� �������� �������
//var app = builder.Build();
//app.UseStaticFiles();   // ��������� ��������� ����������� ������
//app.Run(async (context) => await context.Response.WriteAsync("Hello World"));
//app.Run();
// ��� ���������� ���� � ������ ������������ ������������� ������ ������ CreateBuilder(), ������� � �������� ��������� ��������� ������ WebApplicationOptions.
// ��� �������� WebRootPath ��������� ���������� ����� ��� ����������� ������.