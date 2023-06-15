using DemoExam_Type4_Variant1.Application.Models.Entities;
using System;
using System.Linq;

namespace DemoExam_Type4_Variant1.Application.Common
{
    public static class SessionData
    {
        public static User? CurrentUser { get; private set; }

        public static Order? CurrentOrder { get; private set; }

        public static bool TryToContinueAsUser(string login, string password)
        {
            var user = DemoExamDataContext.Instance.Users.FirstOrDefault(user =>
                                                                         user.Login == login &&
                                                                         user.Password == password);
            if (user == null)
            {
                return false;
            }
            else
            {
                CurrentUser = user;
                GenerateNewOrder();

                return true;
            }
        }

        public static void ContinueAsGuest()
        {
            CurrentUser = null;
            GenerateNewOrder();
        }

        public static void LogOutFromAccount()
        {
            CurrentUser = null;
            CurrentOrder = null;
        }

        public static void GenerateNewOrder()
        {
            CurrentOrder = new Order()
            {
                UserId = CurrentUser?.Id,
                StatusId = Status.DefaultStatusId,
                TakeCode = new Random().Next(100, 1000)
            };
        }
    }
}
