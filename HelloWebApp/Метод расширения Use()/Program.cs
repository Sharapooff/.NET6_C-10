var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string date = "";

app.Use(async (context, next) =>
{
    date = DateTime.Now.ToShortDateString();
    await next.Invoke();                 // �������� middleware �� app.Run //� ������� await next.Invoke(context);  ���� ����� next - RequestDelegate
    Console.WriteLine($"Current date: {date}");  // Current date: 08.12.2021
});

app.Run(async (context) => await context.Response.WriteAsync($"Date: {date}"));

app.Run();


//����� ���������� Use() ��������� ��������� middleware, ������� ��������� �������� ��������� ������� ����� ��������� � ��������� �����������.
// � ����� ������ ���������� ������ Use() �������� ��������� �������:
// app.Use(async (context, next) =>
// {
//     // �������� ����� �������� ������� � ��������� middleware
//     await next.Invoke();
//     // �������� ����� ��������� ������� ��������� middleware
// });
// ����� �������, middleware � ������ Use ��������� �������� �� ���������� � ��������� ���������� � ����� ����.

//�������� ������
// �� ������������� �������� ����� next.Invoke ����� ������ Response.WriteAsync().
// ��������� middleware ������ ���� ������������ ����� � ������� Response.WriteAsync, ���� �������� ��������� ������� ����������� next.Invoke,
// �� �� ��������� ��� ���� �������� ������������.
// ��������� ��������� �� �������������:
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("<p>Hello world!</p>");
//    await next.Invoke();
//});
//app.Run(async (context) =>
//{
//    //await Task.Delay(10000); // ����� ��������� ��������
//    await context.Response.WriteAsync("<p>Good bye, World...</p>");
//});

//������������ ��������� middleware
// Middleware � ������ Use() ������������� ������ �������� � ���������� � ��������� ����������. ������ ����� �� ����� ��������� ��������� �������.
// � ���� ������ �� ����� ��������� � ���� ������ �� ������������� ���������� middleware, � � ���������� �� ������ Run(). ��������:
//app.Use(async (context, next) =>
//{
//    string? path = context.Request.Path.Value?.ToLower();
//    if (path == "/date")
//    {
//        await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
//    }
//    else
//    {
//        await next.Invoke();
//    }
//});
// ������ � �������� �� ����� ������������ ��������� � app.Use ��� ������������ � �������������� ������������:
//app.Use(async (HttpContext context, Func<Task> next) =>
//{
//    await context.Response.WriteAsync("Hello Work!");
//});
//app.Run();

//��������� ����������� � ������
// ����� ����� ������� ��� inline-���������� � ��������� ������:
//app.Use(GetDate);
//app.Run(async (context) => await context.Response.WriteAsync("Hello World.COM"));
//app.Run();

//async Task GetDate(HttpContext context, Func<Task> next)
//{
//    string? path = context.Request.Path.Value?.ToLower();
//    if (path == "/date")
//    {
//        await context.Response.WriteAsync($"Date: {DateTime.Now.ToShortDateString()}");
//    }
//    else
//    {
//        await next.Invoke();
//    }
//}