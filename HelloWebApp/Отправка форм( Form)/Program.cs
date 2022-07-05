var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";

    // ���� ��������� ���� �� ������ "/postuser", �������� ������ �����
    if (context.Request.Path == "/postuser")
    {
        var form = context.Request.Form;
        string name = form["name"];
        string age = form["age"];
        string[] languages = form["languages"];
        // ������� �� ������� languages ���� ������
        string langList = "";
        foreach (var lang in languages)
        {
            langList += $" {lang}";
        }
        await context.Response.WriteAsync($"<div><p>Name: {name}</p>" +
            $"<p>Age: {age}</p>" +
            $"<div>Languages:{langList}</ul></div>");
    }
    else
    {
        await context.Response.SendFileAsync("html/index.html");
    }
});

app.Run();



//������� ������ ������������ �� ������ � ������� ���� html, ������ � ������� ���� POST. ��� ��������� �������� ������ � ������ HttpRequest
//���������� �������� Form. ����������, ��� �� ����� �������� �������� ������.

//��������� ��������
// �������� ������ � ������� � ����� �� �������� index.html ��������� �����, ������� ����� ������������ ������.
// ����� ��������� ��� ���� �����, ������� ����� ���� � �� �� ���. ������� ��� �� �������� ����� ������������� ������ �� ���� ��������. 