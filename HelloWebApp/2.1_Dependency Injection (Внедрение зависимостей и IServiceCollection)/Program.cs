using System.Text;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection allServices = builder.Services;  // ��������� ��������
//�� ����� ������������ � ��������� ������ ����������
var app = builder.Build();

app.Run(async context =>
{
    var sb = new StringBuilder();
    sb.Append("<h1>��� �������</h1>");
    sb.Append("<table>");
    sb.Append("<tr><th>���</th><th>Lifetime</th><th>����������</th></tr>");
    foreach (var svc in allServices)
    {
        sb.Append("<tr>");
        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
        sb.Append($"<td>{svc.Lifetime}</td>");
        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
        sb.Append("</tr>");
    }
    sb.Append("</table>");
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(sb.ToString());
});

app.Run();




//Dependency injection (DI) ��� ��������� ������������ ������������ ��������, ������� ��������� ������� ����������������� � ���������� ������� ���������������.
//����� ������� ������� ����� ����� ����� ����������, ��������, ����� ����������, ��� ������ ��� ������� ����� ������, ����� ������������ � �����������.

//� ������ ��������� ��������� ��������� ������� ����������� - ��������� ��������, �� ������� ������� ������ ��������. 
//class Logger
//{
//    public void Log(string message) => Console.WriteLine(message);
//}
//class Message
//{
//    Logger logger = new Logger();
//    public string Text { get; set; } = "";
//    public void Print() => logger.Log(Text);
//}
// �������� Message ������� �� �������� - Logger
// ���� �� ������� ������ ������ Logger ������������ ������ ��� ��� �������, ��������, ����������� � ����, � �� �� �������, �� ��� �������� ������ ����� Message.
// � ����� ������ ��������� ������� ��������� � ������� �����������.

//����� �������� ������ Logger �� ������ Message, �� ����� ������� ����������, ������� ����� ������������ ������, � ���������� �� ����� � ������ Message:
interface ILogger
{
    void Log(string message);
}
class Logger : ILogger
{
    public void Log(string message) => Console.WriteLine(message);
}
class Message
{
    ILogger logger;
    public string Text { get; set; } = "";
    public Message(ILogger logger)
    {
        this.logger = logger;
    }
    public void Print() => logger.Log(Text);
}
//������������� ASP.NET Core � ���� �������� �������� ��, ��� ��������� ��� �� ��������� ����� ���������� ��������� ��������� ������������, ������� ����������� �����������
//IServiceProvider. � ���� ����������� ��� ���������� ���������, ���������� ������� ��������� ����� ������� ����������� ��������. ���� ��������� �������� �� �������������
//������������ � ����������� ������ � �� ��������� ������������ � ��������� �������.

//��������� ���������� �������� ����������
// �� ���������� ��������� � ���������� � ������ WebApplicationBuilder ���������� �������� Services, ������� ������������ ������ IServiceCollection - ��������� ��������:
// WebApplicationBuilder builder = WebApplication.CreateBuilder();
// IServiceCollection allServices = builder.Services;  // ��������� ��������
// � ���� ���� �� �� ��������� � ��� ��������� ������� ��������, IServiceCollection ��� �������� ��� �������� �� ���������

//���������� � ��������
// ������ ������ � ��������� IServiceCollection ������������ ������ ServiceDescriptor, ������� ����� ��������� ����������.
// � ���������, �������� ������ �������� ����� �������:
// ServiceType: ��� �������
// ImplementationType: ��� ���������� �������
// Lifetime: ��������� ���� �������

//����������� ���������� �������� ASP.NET Core
// ����� ���� ������������ �� ��������� �������� ASP.NET Core ����� ��� ��� ���������� ��������, ������� �� ����� ���������� � ���������� ��� �������������.
// ��� ������� � ���������� middleware, ������� ��������������� ASP.NET �� ���������, �������������� � ���������� � ������� ������� ����������
// IServiceCollection, ������� ����� ����� Add[��������_�������].
// ��������:
// var builder = WebApplication.CreateBuilder();
// builder.Services.AddMvc();
// ��� ������� IServiceCollection ���������� ��� ������� ����������, ������� ���������� �� Add, ���, ��������, AddMvc().
// ��� ������ ��������� � ������ IServiceCollection ��������������� �������. ��������, AddMvc() ��������� � ���������� ������� MVC,
// ��������� ���� �� ������ �� ������������ � ����������.