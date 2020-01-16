using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Newtonsoft.Json;
using BeepBot;
using BeepBot.Model;

namespace BeepBot
{
    public class Program
    {
        /// <summary>  
        /// Here I  Declare Telegrambot object  
        /// </summary>  

        private static readonly TelegramBotClient bot = new TelegramBotClient("1056084097:AAHFXVmaZNg4vDItLMFhN2Za1UzJsfKIBxg");
        public static List<Users> ObjUsers = new List<Users>();

        /// <summary>  
        /// CIS Beep Bot Entry point 
        /// </summary>  
        /// <param name="args"></param>  
        static void Main(string[] args)
        {
            bot.OnMessage += CISbotmessage;
            bot.StartReceiving();
            Console.ReadLine();
            bot.StopReceiving();
        }


        /// <summary>  
        /// Handle bot webhook  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        public static void CISbotmessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                Questionnaires(e);
        }
        public static void Questionnaires(MessageEventArgs e)
        {
            News objNews = new News();
            WeatherInfo objWeatherInfo = new WeatherInfo();
            Users _user = new Users();
            bool IsIdExist = false;
            bool IsLocationExist = false;
            for (int i = 0; i < ObjUsers.Count; i++)
            {
                if (ObjUsers[i].Id == e.Message.From.Id)
                {
                    IsIdExist = true;
                    _user.Username = ObjUsers[i].Username;
                    _user.Location = ObjUsers[i].Location;
                    if (ObjUsers[i].Location != null)
                    {
                        IsLocationExist = true;
                    }
                }
            }

            if (IsIdExist == false && (e.Message.Text.ToLower().Contains("hi") || e.Message.Text.ToLower().Contains("hello")))
            {
                bot.SendTextMessageAsync(e.Message.Chat.Id, "Hello User" + Environment.NewLine + "what's your name");
            }

            else if (e.Message.Text.ToLower().Contains("news") && IsIdExist == true && IsLocationExist == true)
            {
                using (var objClient = new HttpClient())
                {
                    objClient.BaseAddress = new Uri("https://newsapi.org/v2/");
                    var responseTask = objClient.GetAsync("everything?q=" + _user.Location.ToString() + "&from=2019-12-30&sortBy=publishedAt&apiKey=31ed28f12d654adc8c478d0c77f7efce");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<News>();
                        readTask.Wait();
                        objNews = readTask.Result;
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "Thanks ," + _user.Username.ToUpper() + Environment.NewLine);
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "Top news of your ," + _user.Location.ToUpper() + " location - " + Environment.NewLine);
                        for (int i = 0; i < 3; i++)
                        {
                            if (i < 3)
                            {
                                bot.SendTextMessageAsync(e.Message.Chat.Id, objNews.articles[i].title + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "Server error try after some time.");
                    }
                }
            }
            else if (e.Message.Text.ToLower().Contains("weather") && IsIdExist == true && IsLocationExist == true)
            {
                using (var objClient = new HttpClient())
                {
                    objClient.BaseAddress = new Uri("https://samples.openweathermap.org/data/2.5/");
                    var responseTask = objClient.GetAsync("weather?q=" + _user.Location.ToString() + "&appid=b6907d289e10d714a6e88b30761fae22");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<WeatherInfo>();
                        readTask.Wait();
                        objWeatherInfo = readTask.Result;
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "Thanks " + _user.Username.ToString() + ", weather info of your " + _user.Location.ToString() + " location - " + Environment.NewLine);
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "Weather " + objWeatherInfo.weather[0].description + ", Temp " + objWeatherInfo.main.temp + ", Pressure " + objWeatherInfo.main.pressure + ", Humidity " + objWeatherInfo.main.temp);
                    }
                    else
                    {
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "Server error try after some time.");
                    }
                }
            }
            else if (IsIdExist == true && IsLocationExist == false && (!(e.Message.Text.ToLower().Contains("news") && e.Message.Text.ToLower().Contains("weather") && e.Message.Text.ToLower().Contains("hi") && e.Message.Text.ToLower().Contains("hello"))))
            {

                for (int i = 0; i < ObjUsers.Count; i++)
                {
                    if (ObjUsers[i].Id == e.Message.From.Id)
                    {
                        IsIdExist = true;
                        _user.Username = ObjUsers[i].Username;
                        _user.Location = e.Message.Text.ToUpper();
                        ObjUsers[i].Location = e.Message.Text.ToUpper();
                    }
                }
                bot.SendTextMessageAsync(e.Message.Chat.Id, "Thanks " + _user.Username.ToUpper() + Environment.NewLine + " You can asked news and weather of your " + _user.Location.ToUpper() + " location .");
            }
            else if (IsIdExist == false && (!(e.Message.Text.ToLower().Contains("news") && e.Message.Text.ToLower().Contains("weather") && e.Message.Text.ToLower().Contains("hi") && e.Message.Text.ToLower().Contains("hello"))))
            {
                for (int i = 0; i < ObjUsers.Count; i++)
                {
                    if (ObjUsers[i].Id == e.Message.From.Id)
                    {
                        IsIdExist = true;
                        _user.Username = e.Message.Text.ToUpper();
                        _user.Location = ObjUsers[i].Location;
                        ObjUsers[i].Username = e.Message.Text.ToUpper();
                    }
                }
                if (!IsIdExist)
                {
                    _user.Id = e.Message.From.Id;
                    _user.Username = e.Message.Text.ToUpper();
                    _user.Location = null;
                    ObjUsers.Add(_user);
                }

                bot.SendTextMessageAsync(e.Message.Chat.Id, "Hello " + e.Message.Text.ToUpper() + Environment.NewLine + " please enter your location");

            }
            else if (IsIdExist == true && IsLocationExist == true && (e.Message.Text.ToLower().Contains("hi") || e.Message.Text.ToLower().Contains("hello")))
            {
                bot.SendTextMessageAsync(e.Message.Chat.Id, "Hello " + _user.Username.ToUpper() + Environment.NewLine + " You can asked news and weather of your " + _user.Location.ToUpper() + " location .");
            }
        }
        public static string ChatQuestionnaires(int UserId, string Message)
        {
            string replyMessage = null;
            News objNews = new News();
            WeatherInfo objWeatherInfo = new WeatherInfo();
            Users _user = new Users();
            bool IsIdExist = false;
            bool IsLocationExist = false;
            for (int i = 0; i < ObjUsers.Count; i++)
            {
                if (ObjUsers[i].Id == UserId)
                {
                    IsIdExist = true;
                    _user.Username = ObjUsers[i].Username;
                    _user.Location = ObjUsers[i].Location;
                    if (ObjUsers[i].Location != null)
                    {
                        IsLocationExist = true;
                    }
                }
            }

            if ((Message.ToLower().Contains("hi") || Message.ToLower().Contains("hello")))
            {
                return replyMessage += "Hello User, What's your name";
            }
            else if (Message.ToLower().Contains("news") && IsIdExist == true && IsLocationExist == true)
            {
                using (var objClient = new HttpClient())
                {
                    objClient.BaseAddress = new Uri("https://newsapi.org/v2/");
                    var responseTask = objClient.GetAsync("everything?q=" + _user.Location.ToString() + "&from=2020-01-17&sortBy=publishedAt&apiKey=31ed28f12d654adc8c478d0c77f7efce");
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<News>();
                        readTask.Wait();
                        objNews = readTask.Result;
                        for (int i = 0; i < 3; i++)
                        {
                            if (i < 3)
                            {
                                replyMessage += objNews.articles[i].title;
                            }
                        }
                        return replyMessage;
                    }
                    else
                    {
                        return replyMessage = "Server error try after some time.";
                    }
                }
            }
            else if (Message.ToLower().Contains("weather") && IsIdExist == true && IsLocationExist == true)
            {
                using (var objClient = new HttpClient())
                {
                    objClient.BaseAddress = new Uri("https://samples.openweathermap.org/data/2.5/");
                    var responseTask = objClient.GetAsync("weather?q=" + _user.Location.ToString() + "&appid=b6907d289e10d714a6e88b30761fae22");
                    responseTask.Wait();                   
                    var result = responseTask.Result;
                    
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<WeatherInfo>();
                        readTask.Wait();
                        objWeatherInfo = readTask.Result;                      
                        return replyMessage = objWeatherInfo.weather[0].description;
                    }
                    else
                    {                        
                        return replyMessage = "Server error try after some time.";
                    }
                }

            }
            else if (IsIdExist == true && IsLocationExist == false && (!(Message.ToLower().Contains("news") && Message.ToLower().Contains("weather") && Message.ToLower().Contains("hi") && Message.ToLower().Contains("hello"))))
            {

                for (int i = 0; i < ObjUsers.Count; i++)
                {
                    if (ObjUsers[i].Id == UserId)
                    {
                        IsIdExist = true;
                        _user.Username = ObjUsers[i].Username;
                        _user.Location = Message.ToUpper();
                        ObjUsers[i].Location = Message.ToUpper();

                    }
                }               
                return replyMessage = "You can asked news and weather of your location";
            }
            else if (IsIdExist == false && (!(Message.ToLower().Contains("news") && Message.ToLower().Contains("weather") && Message.ToLower().Contains("hi") && Message.ToLower().Contains("hello"))))
            {
                for (int i = 0; i < ObjUsers.Count; i++)
                {
                    if (ObjUsers[i].Id == UserId)
                    {
                        IsIdExist = true;
                        _user.Username = Message.ToUpper();
                        _user.Location = ObjUsers[i].Location;
                        ObjUsers[i].Username = Message.ToUpper();
                    }
                }
                if (!IsIdExist)
                {
                    _user.Id = UserId;
                    _user.Username = Message.ToUpper();
                    _user.Location = null;
                    ObjUsers.Add(_user);
                }
                return replyMessage = "Hello Rakesh, please enter your location";
            }
            else if (IsIdExist == true && IsLocationExist == true && (Message.ToLower().Contains("hi") || Message.ToLower().Contains("hello")))
            {
                return replyMessage = " You can asked news and weather of your location";
            }
            return replyMessage;
        }
    }
}
