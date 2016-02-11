using System.Net.Http;
using System.Threading.Tasks;

namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {
        public static Task<long?> GetPageLength()
        {
            HttpClient client=new HttpClient();
            var httpTask = client.GetAsync("http://apress.com");
            // Во время ожидания завершения HTTP-запроса
            // Можно было бы выполнить другую работу
            // Запрос будет выполнен в фоновом режиме и его результатом будет
            // HttpResponseMessage
            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent)=>
            {
                return antecedent.Result.Content.Headers.ContentLength;
            });
        }

        public static async Task<long?> GetPageLengthAsync()
        {
            HttpClient client=new HttpClient();
            var httpMessage = await client.GetAsync("http://apress.com");
            // Во время ожидания завершения HTTP-запроса
            // Можно было бы выполнить другую работу
            // Await используется при вызове асинхронного метода
            // Сообщает компилятору, что надо дождаться результата 
            // Task и затем продолжить работу
            return httpMessage.Content.Headers.ContentLength;
        }
    }
}