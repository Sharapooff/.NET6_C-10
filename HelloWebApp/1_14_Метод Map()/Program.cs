var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Map() ������� ����������� ���������, ������� ����� ������������ ������� �� ���� "/time"
app.Map("/time", appBuilder =>
{
    var time = DateTime.Now.ToShortTimeString();

    // ��������� ������ - ������� �� ������� ����������
    appBuilder.Use(async (context, next) =>
    {
        Console.WriteLine($"Time: {time}");
        await next();   // �������� ��������� middleware
    });

    appBuilder.Run(async context => await context.Response.WriteAsync($"Time: {time}"));
});
//�������� ������� ����� ��������� ����� ��� ������ �����
app.Map("/index", appBuilder =>
{
    appBuilder.Run(async context => await context.Response.WriteAsync("Index Page"));
});
app.Map("/about", appBuilder =>
{
    appBuilder.Run(async context => await context.Response.WriteAsync("About Page"));
});

app.Run(async (context) => await context.Response.WriteAsync("Main branch"));

app.Run();



//����� Map()
// ����������� ��� �������� ����� ���������, ������� ����� ������������ ������ �� ������������� ����.
// ���� ����� ���������� ��� ����� ���������� ��� ���� IApplicationBuilder � ����� ��� ������������� ������.
// public static IApplicationBuilder Map (this IApplicationBuilder app, string pathMatch, Action<IApplicationBuilder> configuration);
// � �������� ��������� pathMatch ����� ��������� ���� �������, � ������� ����� �������������� �����. � �������� configuration ������������ �������,
// � ������� ���������� ������ IApplicationBuilder � � ������� ����� ����������� ����� ���������.

//��������� ������ Map
// ����� ���������, ������� ��������� � ������ Map(), ����� ����� ��������� �����, ������� ������������ ����������.
//app.Map("/home", appBuilder =>
//{
//    appBuilder.Map("/index", Index); // middleware ��� "/home/index"
//    appBuilder.Map("/about", About); // middleware ��� "/home/about"
//    // middleware ��� "/home"
//    appBuilder.Run(async (context) => await context.Response.WriteAsync("Home Page"));
//});

// Map() �� ����� ���������� �������� ("/") ���� (����������� ������) ������� ���������� MapWhen()


//��� ������������� �������� ����� ��������� ����� ������� � ��������� ������:
//app.Map("/index", Index);
//app.Map("/about", About);

//app.Run(async (context) => await context.Response.WriteAsync("Page Not Found"));

//app.Run();

//void Index(IApplicationBuilder appBuilder)
//{
//    appBuilder.Run(async context => await context.Response.WriteAsync("Index"));
//}
//void About(IApplicationBuilder appBuilder)
//{
//    appBuilder.Run(async context => await context.Response.WriteAsync("About"));
//}