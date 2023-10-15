using DailyNotesWebApi;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace DayliNotesWebMVC.Classes
{
    public static class GetResponseNote
    {
        public static void ResponseNote()
        {
            Uri baseAddres = new Uri("https://localhost:7210/api");
            string data = "MakkLaud";
            StringContent content = new StringContent(data.ToLower(), Encoding.UTF8, "application/json");
            var _client = new HttpClient();
            HttpResponseMessage response = _client.PostAsync(baseAddres + "/DailyNotes/GetSome", content).Result;
        }
    }
}
