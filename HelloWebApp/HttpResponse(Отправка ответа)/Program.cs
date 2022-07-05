var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Run(async (context) => await context.Response.WriteAsync("Hello World!", System.Text.Encoding.Default));
//����� �������� context, ������� ���������� � middleware � ������ app.Run() ��� ��� ������������ ������ HttpContext. � ����� ���� ������,
//������ ����� ��� �������� Response �� ����� ��������� ������� ��������� �����: context.Response.WriteAsync().

app.Run(async (context) =>//��������� ����������
{
    var response = context.Response;
    response.Headers.ContentLanguage = "ru-RU";
    response.Headers.ContentType = "text/plain; charset=utf-8";
    response.Headers.Append("key-id", "010");    // ���������� ���������� ���������
    
    //response.StatusCode = 404; // ��������� ����� �������
    
    //response.ContentType = "text/html; charset=utf-8";
    //await response.WriteAsync("<h2>Hello World!</h2><h3>Welcome to ASP.NET Core</h3>");
    
    await response.WriteAsync("Hello World!!!");
});

app.Run();


//��� ������ ������� ���������� � middleware ����� ������ Microsoft.AspNetCore.Http.HttpContext. ���� ������ ������������� ���������� � �������,
//��������� ��������� ������� �, ����� ����, ����� ��� ����� ������ ����������������.

//�������� Response ������� HttpContext ������������ ������ HttpResponse � �������������, ��� ����� ����������� � ���� ������. 
//��� ��������� ��������� �������� ������ ����� HttpResponse ���������� ��������� ��������:
//    Body: �������� ��� ������������� ���� ������ � ���� ������� Stream
//    BodyWriter: ���������� ������ ���� PipeWriter ��� ������ ������
//    ContentLength: �������� ��� ������������� ��������� Content-Length
//    ContentType: �������� ��� ������������� ��������� Content-Type
//    Cookies: ���������� ����, ������������ � ������
//    HasStarted: ���������� true, ���� �������� ������ ��� ��������
//    Headers: ���������� ��������� ������
//    Host: �������� ��� ������������� ��������� Host
//    HttpContext: ���������� ������ HttpContext, ��������� � ������ �������� Response
//    StatusCode: ���������� ��� ������������� ��������� ��� ������
//    ����� ��������� �����, �� ����� ������������ ��� ������� ������ HttpResponse:
//    Redirect(): ��������� �������������(��������� ��� ����������) �� ������ ������
//    WriteAsJson()/ WriteAsJsonAsync(): ���������� ����� � ���� �������� � ������� JSON
//    WriteAsync(): ���������� ��������� ����������. ���� �� ������ ������ ��������� ������� ���������. ���� ��������� �� �������, �� �� ��������� ����������� ��������� UTF-8
//    SendFileAsync(): ���������� ����


//��������� ����������
//    ��� ��������� ���������� ����������� �������� Headers, ������� ������������ ��� IHeaderDictionary. ��� ����������� ����������� ���������� HTTP
//    � ���� ���������� ���������� ����������� ��������, ��������, ��� ��������� "content-type" ���������� �������� ContentType.
//    ������, � ��� ����� ���� ��������� ��������� ����� �������� ����� ����� Append().

//��������� ����� �������
//��� ��������� ��������� ����� ����������� �������� StatusCode, �������� ���������� �������� ��� �������/

//�������� html-����
//  ���� ���������� ��������� html-���, �� ��� ����� ���������� ���������� ��� ��������� Content-Type �������� text/html.