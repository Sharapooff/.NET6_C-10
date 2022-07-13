var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());//����� AddConsole() ������� ILoggingBuilder ������������� ����� ��������� ���� �� �������
//ILogger logger = loggerFactory.CreateLogger<Program>();
//app.Run(async (context) =>
//{
//    logger.LogInformation($"Requested Path: {context.Request.Path}");
//    await context.Response.WriteAsync("Hello World!!!");
//});

app.Map("/hello", (ILoggerFactory loggerFactory) => {

    // ������� ������ � ���������� "MapLogger"
    ILogger logger = loggerFactory.CreateLogger("MapLogger");
    // ��������� ��������� ���������
    logger.LogInformation($"Path: /hello   Time: {DateTime.Now.ToLongTimeString()}");
    return "Hello World!";
});



app.Run();


//� �������� � ������� ���� �� �������� ������ �������, ������� ����������� ����� DI. �� �� ����� ����� ������������ ������� ������� ��� ��� ��������
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());//����� AddConsole() ������� ILoggingBuilder ������������� ����� ��������� ���� �� �������
//ILogger logger = loggerFactory.CreateLogger<Program>();
//app.Run(async (context) =>
//{
//    logger.LogInformation($"Requested Path: {context.Request.Path}");
//    await context.Response.WriteAsync("Hello World!");
//});
//app.Run();

//��������� ������� ������� ����� dependency injection
// ��� � ������, ������� ������� �������� � ���������� � ���� �������, �������������� �� ����� ������� ����� �������� ��������� ������������:
// var builder = WebApplication.CreateBuilder();
// var app = builder.Build();
//app.Map("/hello", (ILoggerFactory loggerFactory) => {

//    // ������� ������ � ���������� "MapLogger"
//    ILogger logger = loggerFactory.CreateLogger("MapLogger");
//    // ��������� ��������� ���������
//    logger.LogInformation($"Path: /hello   Time: {DateTime.Now.ToLongTimeString()}");
//    return "Hello World!";
//});
//app.Run();

//���������� ������������
// � ������� ���� ������������ ��� �� �������. ������ ���� ������������ ������������ ����������� ������������.
// �� ��������� ASP.NET Core ������������� ��������� ����������:
// Console: ����� ���������� �� �������.��������������� ������� AddConsole()
// Debug: ���������� ��� ������� ������� ���� ����� System.Diagnostics.Debug � � ��������� ��� ����� Debug.WriteLine.�������������� ��� ������ ����
// �� ����� ������� � ���� Output � Visual Studio.��������������� ������� AddDebug(). ����� ��������, ��� ������ ������ �������� ������ ��� ������� ������� � ������ �������
// EventSource: �� Windows ������ ������������ � ��� ETW (Event Tracing for Windows), ��� ��������� �������� ����� �������������� ���������� PerfView (��� ����������� �����������).
// ���� ������ ��������� ����������� ��� ������������������, ��� Linux � MacOS ���� ���������� ���� �� ����������. ��������������� ������� AddEventSourceLogger()
// EventLog: ���������� � Windows Event Log, �������������� �������� ������ ��� ������� �� Windows. ��������������� ������� AddEventLog()
// ������ ������� ������� ����� ���� � ���� Output � Visual Studio:
// var loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());

