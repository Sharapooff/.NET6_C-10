using Microsoft.Extensions.FileProviders;//+

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Run(async (context) => await context.Response.SendFileAsync("D:\\forest.jpg"));//������ ����
//app.Run(async (context) => await context.Response.SendFileAsync("forest.jpg")); //������������� ����
//app.Run(async context =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    await context.Response.SendFileAsync("html/index.html");
//});
//app.Run(async (context) =>      //�������� ����������� ������ (html)
//{
//    var path = context.Request.Path;
//    var fullPath = $"html/{path}";
//    var response = context.Response;

//    response.ContentType = "text/html; charset=utf-8";
//    if (File.Exists(fullPath))
//    {
//        await response.SendFileAsync(fullPath);
//    }
//    else
//    {
//        response.StatusCode = 404;
//        await response.WriteAsync("<h2>Not Found</h2>");
//    }
//});
//app.Run(async (context) =>     // �������� �����
//{
//    context.Response.Headers.ContentDisposition = "attachment; filename=my_forest.jpg";
//    await context.Response.SendFileAsync("forest.jpg");
//});
app.Run(async (context) =>
{
    var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
    var fileinfo = fileProvider.GetFileInfo("forest.jpg");

    context.Response.Headers.ContentDisposition = "attachment; filename=my_forest2.jpg";
    await context.Response.SendFileAsync(fileinfo);
});

app.Run();

//��� �������� ������
// ����������� ����� SendFileAsync(), ������� �������� ���� ���� � ����� � ���� ������, ���� ���������� � ����� � ���� ������� IFileInfo.
// ����� �� ����� ������������ ������������� ����. ��������, ������� � ������ �����-������ ���� (� ���� ������ ��� ���� forest.jpg)
// ��� ����� ����� � ���� ������� ��������� ��� ����� Copy to Output Directory �������� Copy if newer ��� Copy always, ����� ���� ������������� �����������
// � �������� ������� ��� ���������� ����������. � ��������� ������������� ���� ������������ ����� ����������.

//�������� html-��������
// �������� ������� �� ����� ���������� � ������ ���� ������, ��������, html-��������. ���, ��������� � ������� ����� �����, ������� ������� html.
// � ��� ����� ������� ����� ���� index.html
// ����� ��������, ��� � ASP.NET Core ��� ������� ���������� middleware, ������� ��������� ��������� ������ �� ������������ �������.

//�������� �����
// �� ��������� ������� �������� ������� ������������ ����, ��� ����� ���� ������� � ������ ������ html - �� ����� ���������� ���� html � ����� �������
// ��������� ������� ���-��������. �� ����� ����� ���� ����������, ����� ������� �������� ���� ��� ��� ��������.
// � ���� ������ �� ����� ���������� ��� ��������� "Content-Disposition" �������� "attachment".

//IFileInfo
// � �������� ���� ����������� ������ ������ SendFileAsync(), ������� �������� ���� � ����� � ���� ������. ����� ����� ������������ ������ ������,
// ������� �������� ���������� � ����� � ���� ������� IFileInfo.