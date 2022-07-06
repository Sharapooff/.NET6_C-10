namespace _1_15_Классы_middleware
{
    public static class TokenExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenMiddleware>();
        }
    }
}
//Здесь создается метод расширения для типа IApplicationBuilder. И этот метод встраивает компонент TokenMiddleware
//в конвейер обработки запроса. Как правило, подобные методы возвращают объект IApplicationBuilder.