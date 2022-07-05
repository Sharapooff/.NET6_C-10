using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//��������
//app.Run(async (context) =>
//{
//    Person tom = new("Tom", 22);
//    await context.Response.WriteAsJsonAsync(tom);//��������
//});

//���������
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    if (request.Path == "/api/user")
    {
        var message = "������������ ������";   // ���������� ��������� �� ���������
        try
        {
            // �������� �������� ������ json
            var person = await request.ReadFromJsonAsync<Person>();
            if (person != null) // ���� ������ ��������������� � Person
                message = $"Name: {person.Name}  Age: {person.Age}";
        }
        catch { }
        // ���������� ������������ ������
        await response.WriteAsJsonAsync(new { text = message });
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();


public record Person(string Name, int Age);


//��� �������� json ����� ��������������� ������� WriteAsJson()/WriteAsJsonAsync() ������� HttpResponse.
// ���� ����� ��������� ������������� ���������� � ���� ������� � ������ JSON � ������������� ��� ��������� "content-type"
// ������������� �������� "application/json; charset=utf-8"

//��������� JSON. ����� ReadFromJsonAsync
// ��� ��������� �� ������� ������ � ������� JSON � ������ HttpRequest ��������� ����� ReadFromJsonAsync().
// �� ��������� ������������� ������ � ������ ������������� ����.
// ����� ��������, ��� ��������� �� ������� json � ������� ����� � ������� ������ HasJsonContentType() - �� ���������� true, ���� ������ ������� json.

//��������� ������������
// ������, ����� ������ json �� ������ ������������� ����������� ����, � ������� ���� ��������� �������������� (���������� ����� ����� Name - UserName)
// ������ ������ Person ��� ����� ����� ������, ������ ��� �������� ������� �������� �� ��������� (null ��� �������� Name � 0 ��� �������� Age).
// � ����� ��� �������� �������� ������ �� ������� ��������� ���������� ���� System.Text.Json.JsonException, � ������ ������� ���������� �� ����������.
//  �� ������ ����� ������������ ��������� ������� - ����������� ����������, ���������� �������������� middleware ��� ������ �������� �������� � ��� �����.
//  ����� �� ������� �������� ������� ����� ����� ���� ��������� ������������/�������������� � ������� ��������� ���� JsonSerializerOptions,
//  ������� ����� ������������ � ����� ReadFromJsonAsync().
//public class PersonConverter : JsonConverter<Person>
//{
//    public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        var personName = "Undefined";
//        var personAge = 0;
//        while (reader.Read())
//        {
//            if (reader.TokenType == JsonTokenType.PropertyName)
//            {
//                var propertyName = reader.GetString();
//                reader.Read();
//                switch (propertyName)
//                {
//                    // ���� �������� Age/age � ��� �������� �����
//                    case "age" or "Age" when reader.TokenType == JsonTokenType.Number:
//                        personAge = reader.GetInt32();  // ��������� ����� �� json
//                        break;
//                    // ���� �������� Age/age � ��� �������� ������
//                    case "age" or "Age" when reader.TokenType == JsonTokenType.String:
//                        string? stringValue = reader.GetString();
//                        // �������� �������������� ������ � �����
//                        if (int.TryParse(stringValue, out int value))
//                        {
//                            personAge = value;
//                        }
//                        break;
//                    case "Name" or "name":  // ���� �������� Name/name
//                        string? name = reader.GetString();
//                        if (name != null)
//                            personName = name;
//                        break;
//                }
//            }
//        }
//        return new Person(personName, personAge);
//    }
//    // ����������� ������ Person � json
//    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
//    {
//        writer.WriteStartObject();
//        writer.WriteString("name", person.Name);
//        writer.WriteNumber("age", person.Age);

//        writer.WriteEndObject();
//    }
//}
////� �������� ����� Run
//app.Run(async (context) =>
//{
//    var response = context.Response;
//    var request = context.Request;
//    if (request.Path == "/api/user")
//    {
//        var responseText = "������������ ������";   // ���������� ��������� �� ���������

//        if (request.HasJsonContentType())
//        {
//            // ���������� ��������� ������������/��������������
//            var jsonoptions = new JsonSerializerOptions();
//            // ��������� ��������� ���� json � ������ ���� Person
//            jsonoptions.Converters.Add(new PersonConverter());
//            // ������������� ������ � ������� ���������� PersonConverter
//            var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
//            if (person != null)
//                responseText = $"Name: {person.Name}  Age: {person.Age}";
//        }
//        await response.WriteAsJsonAsync(new { text = responseText });
//    }
//    else
//    {
//        response.ContentType = "text/html; charset=utf-8";
//        await response.SendFileAsync("html/index.html");
//    }
//});