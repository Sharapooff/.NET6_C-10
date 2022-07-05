var builder = WebApplication.CreateBuilder(args); //builder - ������ WebApplicationBuilder
var app = builder.Build();  //app - ������ WebApplication

app.MapGet("/", () => "Hello World!");

app.Run();

// WebApplication � WebApplicationBuilder____________________________________________________________________________________________

//����� �������� ������� WebApplication ����� WebApplicationBuilder ��������� ��� ��� �����, ����� ������� ����� �������� ���������:
//��������� ������������ ����������
//���������� ��������
//��������� ������������ � ����������
//��������� ��������� ����������
//������������ �������� IHostBuilder � IWebHostBuilder, ������� ����������� ��� �������� ����� ����������

//��� ���������� ���� ����� � ������ WebApplicationBuilder ���������� ��������� ��������:
//  Configuration: ������������ ������ ConfigurationManager, ������� ����������� ��� ���������� ������������ � ����������.
//  Environment: ������������� ���������� �� ���������, � ������� �������� ����������.
//  Host: ������ IHostBuilder, ������� ����������� ��� ��������� �����.
//  Logging: ��������� ���������� ��������� ������������ � ����������.
//  Services: ������������ ��������� �������� � ��������� ��������� ������� � ����������.
//  WebHost: ������ IWebHostBuilder, ������� ��������� ��������� ��������� ��������� �������.

//����� WebApplication ����������� ��� ���������� ���������� �������, ��������� ���������, ��������� �������� � �.�.
//����� WebApplication ��������� ��� ����������:
//    IHost: ����������� ��� ������� � ��������� �����, ������� ������������ �������� �������
//    IApplicationBuilder: ����������� ��� ��������� �����������, ������� ��������� � ��������� �������
//    IEndpointRouteBuilder: ����������� ��� ��������� ���������, ������� �������������� � ���������

//��� ��������� ������� � ���������������� ���������� ����� ������������ �������� ������ WebApplication:
//    Configuration: ������������ ������������ ���������� � ���� ������� IConfiguration
//    Environment: ������������ ��������� ���������� � ���� IWebHostEnvironment
//    Lifetime: ��������� �������� ����������� � �������� ���������� ����� ����������
//    Logger: ������������ ������ ���������� �� ���������
//    Services: ������������ ������� ����������
//    Urls: ������������ ����� �������, ������� ���������� ������

//��� ���������� ������ ����� WebApplication ���������� ��������� ������:
//    Run(): ��������� ����������
//    RunAsync(): ���������� ��������� ����������
//    Start(): ��������� ����������
//    StartAsync(): ��������� ����������
//    StopAsync(): ������������� ����������
