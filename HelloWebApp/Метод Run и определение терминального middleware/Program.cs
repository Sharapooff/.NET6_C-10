var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//app.Run(async (context) => await context.Response.WriteAsync("Hello World!"));
//app.Run(HandleRequst);//����� ������� ��� middleware � ��������� �����
int x = 2;
app.Run(async (context) => //���������� middleware ��������� ���� ��� � ���������� � ������� ����� ���������� ����� ����������
{
    x = x * 2;  //  2 * 2 = 4
    await context.Response.WriteAsync($"Result: {x}");
});
app.Run();

//����� ������� ������ ���������� middleware � �������� ��������� ������� � ASP.NET Core ������������ ����� Run(),
//������� ��������� ��� ����� ���������� ��� ���������� IApplicationBuilder (������������� ��� ������������ � ����� WebApplication).
//����� Run ��������� ������������ ��������� - ����� ���������, ������� ��������� ��������� �������, ������������� �� �� �������� ������� ������ ����������
//� ��������� ������� ������ - ��������� � ��������� ����������� �� ��������. ������ ����� ������� �������� � ����� ����� ���������� ��������� ��������� �������.
//�� ���� �� ����� ���� �������� ������ ������, ������� ��������� ���������� middleware.

//� �������� ��������� ����� Run ��������� ������� RequestDelegate. ���� ������� ����� ��������� �����������:
// public delegate Task RequestDelegate(HttpContext context);
// �� ��������� � �������� ��������� �������� ������� HttpContext � ���������� ������ Task.

//��� ������������� �� ����� ������� ��� middleware � ��������� �����:
async Task HandleRequst(HttpContext context)
{
    await context.Response.WriteAsync("Hello World! :)");
}

//��������� ���� middleware_________________________________________________________________

//���������� middleware ��������� ���� ��� � ���������� � ������� ����� ���������� ����� ����������.
//�� ���� ��� ����������� ��������� �������� ������������ ���� � �� �� ����������. 

