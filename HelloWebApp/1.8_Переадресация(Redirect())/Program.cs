var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    if (context.Request.Path == "/old")//������ ��� ��������� �� ������ "/old" ���������� ��������������� �� ����� "/new"
    {
        context.Response.Redirect("/new");
        //context.Response.Redirect("https://www.google.com"); // �� ������� ������
    }
    else if (context.Request.Path == "/new")
    {
        await context.Response.WriteAsync("New Page");
    }
    else
    {
        await context.Response.WriteAsync("Main Page");
    }
});

app.Run();



//��� ���������� ������������� � ������� HttpResponse ��������� ����� Redirect():
// void Redirect(string location)
// void Redirect(string location, bool permanent)
// ���� ���� �������� ����� true, �� ������������� ����� ����������, � � ���� ������ ���������� ��������� ��� 301. ���� ����� false,
// �� ������������� ���������, � ���������� ��������� ��� 302.