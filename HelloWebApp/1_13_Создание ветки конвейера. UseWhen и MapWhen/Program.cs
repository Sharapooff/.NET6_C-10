var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// �������� ����� ���������� ���� ��� ��� ������� ����������. �.�. ��, ��� ������ ������, ��� ������ ��������.
// �������, ��� ����������� var time ������ ������, �� ��������� ������ ���, �� ��� ����������� ��� ������, � � ���� �����
// ���������� ����� ������� ���� ��� ��� ������� ����������
app.UseWhen(
    context => context.Request.Path == "/time", // �������, ���� ���� ������� "/time"
    appBuilder =>
    {
        //var time = DateTime.Now.ToShortTimeString();
        // ��������� ������ - ������� �� ������� ����������
        appBuilder.Use(async (context, next) =>
        {
            var time = DateTime.Now.ToShortTimeString();
            Console.WriteLine($"Time: {time}");
            await next();   // �������� ��������� middleware
        });

        // ���������� �����
        appBuilder.Run(async context =>
        {
            var time = DateTime.Now.ToShortTimeString();
            await context.Response.WriteAsync($"Time: {time}");
        });
        //� ������� ���� ����� ��������� ����������� ������������ �����������, ������� ��������� �������� � �������� ����� ��������� �� �����������.
        //������ �� ����� ����� �������� ������ �� ��������� �� ����� � �������� ����� ���������
        //await next();   // �������� ��������� middleware // ��� ������ ��� � �������� �����
    });
//���� �� ������������� �������
app.Run(async context =>
{
    await context.Response.WriteAsync("��� �����������");
});

app.Run();


//UseWhen
// ����� UseWhen() �� ��������� ���������� ������� ��������� ������� ����������� ��������� ��� ��������� �������
// ��� � Use(), ����� UseWhen() ���������� ��� ����� ���������� ��� ���� IApplicationBuilder.
// � �������� ��������� �� ��������� ������� Func>HttpContext,bool> - ��������� �������, �������� ������ ��������������� ������. � ���� ������� ���������� ������ HttpContext.
// � ������������ ����� ������ ���� ��� bool - ���� ������ ������������� �������, �� ������������ true, ����� ����������� false.
// ��������� �������� ������ - ������� Action<IApplicationBuilder> ������������ ��������� �������� ��� �������� IApplicationBuilder, 
// ������� ���������� � ������� � �������� ���������.

//MapWhen
// ����� MapWhen(), ��� � ����� UseWhen(), �� ��������� ���������� ������� ��������� ������� ����������� ���������
// ����� MapWhen() ����� ���������� ��� ����� ���������� ��� ���� IApplicationBuilder, ��������� �� �� ���������, ��� � UseWhen(), � �������� �� ������ ����������� �������:
//app.MapWhen(
//    context => context.Request.Path == "/time", // �������: ���� ���� ������� "/time"
//    appBuilder => appBuilder.Run(async context =>
//    {
//        var time = DateTime.Now.ToShortTimeString();
//        await context.Response.WriteAsync($"current time: {time}");
//    })
//);

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("��� �����������");
//});

//app.Run();




//��� ������� ������������� ����� ����� ���� �� ������� �������� �� �������� ����� ��������� � ��������� �����:
//app.UseWhen(
//    context => context.Request.Path == "/time", // �������: ���� ���� ������� "/time"
//    HandleTimeRequest
//);

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello METANIT.COM");
//});

//app.Run();

//void HandleTimeRequest(IApplicationBuilder appBuilder)
//{
//    appBuilder.Use(async (context, next) =>
//    {
//        var time = DateTime.Now.ToShortTimeString();
//        Console.WriteLine($"current time: {time}");
//        await next();   // �������� ��������� middleware
//    });
//}