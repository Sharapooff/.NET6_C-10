var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    if (context.Request.Cookies.ContainsKey("name")) // ����� ������� ��������� ����� TryGetValue() ��� ��������� ���: if (context.Request.Cookies.TryGetValue("name", out var login))
    {
        string? name = context.Request.Cookies["name"];
        await context.Response.WriteAsync($"Hello {name}!");
    }
    else
    {
        context.Response.Cookies.Append("name", "Tom");
        await context.Response.WriteAsync("Hello World!");
    }
});

app.Run();

//���� ������������ ����� ������� ������ ��������� ������ ������������. ���� �������� �� ���������� ������������ � ����� ��������������� ��� �� �������, ��� � �� �������.
// ��� ��� ���� ���������� � ������ �������� �� ������, �� �� ������������ ������ ��������� 4096 �������.

//���������
//��� ������ � ������ ����� ����� ������������ �������� ������� HttpContext, ������� ���������� � �������� ��������� � ���������� middleware, � ����� �������� � ������������ � RazorPages.
// ����� �������� ����, ������� �������� ������ � �������� � ����������, ��� ���� ������������ ��������� Request.Cookies ������� HttpContext. 
// ��������� context.Request.Cookies ������ ������ ��� ��������� �������� ���.

// ��� ���� ��������� ���������� ��������� �������:
// bool ContainsKey(string key): ���������� true, ���� � ��������� ��� ���� ���� � ������ key
// bool TryGetValue(string key, out string value): ���������� true, ���� ������� �������� �������� ���� � ������ key � ���������� value
// ����� ��������, ��� ���� - ��� ��������� ��������. �������, ��� �� ��������� ��������� � ���� - ��� ��� ���������� ��������� � ������ �
// �������������� ��������� �� ��� �� ���� ������.
// if (context.Request.Cookies.ContainsKey("name"))
// string name = context.Request.Cookies["name"];

//���������
// ��� ��������� ���, ������� ������������ � ����� �������, ����������� ������ context.Response.Cookies, ������� ������������ ��������� IResponseCookies.
// ���� ��������� ���������� ��� ������:
// Append(string key, string value): ��������� ��� ���� � ������ key �������� value
// Delete(string key): ������� ���� �� �����

