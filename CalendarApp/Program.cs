using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;

namespace GoogleAPITest
{
    public class Program
    {


        static void Main()
        {
            // CalendarService インスタンスの作成
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyACDT2kmVWYcSNd_yS2xKSBXj71EwA5iNw",
            });

            // カレンダーイベントの取得
            Events events = service.Events.List("ja.japanese#holiday@group.v.calendar.google.com").Execute();

            // 取得したイベントの処理
            foreach (var calendarEvent in events.Items)
            {
                Console.WriteLine($"{calendarEvent.Summary} - {calendarEvent.Start.Date}");
            }

        }
    }
}