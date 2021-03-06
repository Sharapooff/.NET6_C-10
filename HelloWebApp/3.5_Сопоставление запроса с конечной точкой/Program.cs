var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//??? ???? ???????? ????????????? ???? ??????? "/hello". ?????? ?????? ???????? "/{message}" ????? ????? - ?????? ????????? message ????? ???? ??? ??????.
//????? ??? ?????? "/hello" ??????? ?? ???????????? ???????? ? ?????????????? ????? ??????????, ????? ???????????,
//??????? ???????? ????? ????? ??????? ???????? ? ????? ??????? ??? ????????? ??????? ?? ???? "/hello".
app.Map("/hello", () => "Hello world");
app.Map("/{message}", (string message) => $"Message: {message}");
//????? ?????? ?????? ???????? ????????? ?????????????? ???????? message. ?????? ?????? ???????? ????????????? ????? ???-??????????.
//? ? ???????? ??? ???? ??????? ????????????? ???? ??????? "/". ?????? ?????? ?????? ???????????? ??????????? ???????,
//??????? ??? ???????? ????? ????? ??????? ? ???????? ????? ??? ????????? ???????
app.Map("/{message?}", (string? message) => $"Message: {message}");
//????????? ?? ?????? ???????? ?????? ??????? ???????????? ??????????? ???????, ?? ?????? ?????? ???????? ????? ????? ?????????? ??? ????????? ????????
app.Map("/{controller}/Index/5", (string controller) => $"Controller: {controller}");
app.Map("/Home/{action}/{id}", (string action) => $"Action: {action}");
app.Map("/", () => "Index Page");

app.Run();




//????????????? ?????? URL ??? URL matching ???????????? ??????? ????????????? ??????? ? ???????? ??????.
// ?????? ??????? ???????????? ?? ???? ??????? ? ?????????? ? ??????? ??????????.
// ?????? ??????? ???????? ??? ??????:
// ??????? ?????????? ??? ???????? ?????, ?????? ???????? ??????? ????????? ? ????? ???????
// ????? ?? ??????????? ?? ?????????? ????? ?????? ???????? ????? ????????? ??, ??????? ?? ????????????? ???????????? ????????
// ????? ?? ??????????? ?? ?????????? ????? ?????? ???????? ????? ????????? ??, ??????? ?? ????????????? ???????? ??????? MatcherPolicy
// (???????: ????? MatcherPolicy ????????? ?????????? ??????? ????????? ???????? ????? ? ?????? URL)
// ? ? ????? ????? ??????????? ?????? EndpointSelector ??? ?????? ?? ??????????? ?? ?????????? ????? ?????? ???????? ?????, ??????? ? ???????? ????? ? ????? ???????????? ??????

//????????? ???????? ????? ??????? ?? ???? ????????:
// ??????? ?????????? ? ?????? ???????? ?????
// ?????????????? ??????? ????????

// ?????????????? ???????? ???????? ??????? ?? ????????????? ???????.
// ????????????? ??????? ???????????? ?? ?????? ????????? ?????????:
// ?????? ???????? ? ??????? ??????????? ????????? ????? ??????????, ??? ?????? ??????? ??????????? ?????????
// ??????? ? ????????? ????????? (??????????? ???????) ????? ??????????, ??? ??????? ? ?????????? ????????
// ??????? ? ??????????, ? ???????? ??????????? ??????????? ????????, ????? ??????????, ??? ??????? ? ?????????? ??? ???????????
// ??????????? ??????? ????? ??????????, ??? ??????? ? ?????????? ? ????????????
// ???????? catch-all (????????, ??????? ????????????? ??????????????? ?????????? ?????????) ???????? ??????????
// ???? ? ???????? ????? ???????? ??? ? ????? ???????? ?????, ??????? ????????????? ???????????? ??????, ? ?????????????? ??????? ????????????? ?? ????? ???????,
// ????? ?? ???? ???????? ????? ?????? ???????????? ???????, ?? ???????????? ??????????.