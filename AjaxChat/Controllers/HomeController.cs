using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxChat.Models;

namespace AjaxChat.Controllers
{
    public class HomeController : Controller
    {
        static ChatModel chatModel;

        public ActionResult Index(string user, string password, bool? logOn, bool? logOff, bool? newUser, bool? oldUser, string chatMessage)
        {
            try
            {
                if (chatModel == null) chatModel = new ChatModel();
                if (!Request.IsAjaxRequest())
                {
                    return View(chatModel);
                }
                else if (logOn != null && (bool)logOn)
                {
                    if (oldUser != null && (bool)oldUser)
                    {
                        if (chatModel.Users.FirstOrDefault(u => u.login == user) == null|| 
                            (chatModel.Users.FirstOrDefault(u => u.login == user) != null && 
                            chatModel.Users.FirstOrDefault(u => u.login == user).password != password))
                        {
                            throw new Exception("Такого пользователя не существует. Проверьте логин и пароль");
                        }  
                    }
                    else if (newUser != null && (bool)newUser)
                    {
                        if (chatModel.Users.FirstOrDefault(u => u.login == user) != null)
                        {
                            throw new Exception("Пользователь с таким ником уже существует");
                        }
                        else
                        {
                            chatModel.Users.Add(new User()
                            {
                                login = user,
                                password = password,
                            });
                        }
                    }
                    return PartialView("ChatRoom", chatModel);
                }
                // если передан параметр logOff
                else if (logOff != null && (bool)logOff)
                {
                    LogOff(chatModel.Users.FirstOrDefault(u => u.login == user));
                    return PartialView("ChatRoom", chatModel);
                }
                else
                {
                    User currentUser = chatModel.Users.FirstOrDefault(u => u.login == user);

                    //для каждлого пользователя запоминаем воемя последнего обновления
                    //currentUser.LastPing = DateTime.Now;

                    // удаляем неавтивных пользователей, если время простоя больше 15 сек
                    //List<User> toRemove = new List<User>();
                    //foreach (Models.User usr in chatModel.Users.Users)
                    //{
                    // TimeSpan span = DateTime.Now - usr.LastPing;
                    // if (span.TotalSeconds > 15)
                    //  toRemove.Add(usr);
                    //}
                    // foreach (User u in toRemove)
                    //{
                    // LogOff(u);
                    // }

                    // добавляем в список сообщений новое сообщение
                    if (!string.IsNullOrEmpty(chatMessage))
                    {
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            User = currentUser,
                            Text = chatMessage,
                            Date = DateTime.Now
                        });
                    }

                    return PartialView("History", chatModel);
                }
            }
            catch (Exception ex)
            {
                //в случае ошибки посылаем статусный код 500
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }

        // при выходе пользователя удаляем его из списка
        public void LogOff(User user)
        {
            //chatModel.Users.Remove(user);            
        }
    }
}