using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UsersWepApiService.DataLayer.DTO;

namespace UsersWepApiService.DataLayer.Helpers
{
    public class DataValidationHelper
    {
        public static (bool IsValid, string ErrorDescription) CheckCreatedUserInfoIsValid(CreatedUserInfoDTO CreatedUserInfo)
        {
            bool IsValid = true;
            string ErrorDescription = "";

            if (CreatedUserInfo.Login == null || !IsEngLettersOrDigits(CreatedUserInfo.Login)) 
            {
                IsValid = false;
                ErrorDescription += "Некорректный логин; ";
            }

            if (CreatedUserInfo.Password == null || !IsEngLettersOrDigits(CreatedUserInfo.Password))
            {
                IsValid = false;
                ErrorDescription += "Некорректный пароль; ";
            }

            if (CreatedUserInfo.Name == null || !IsRuOrEngLetters(CreatedUserInfo.Name))
            {
                IsValid = false;
                ErrorDescription += "Некорректное имя; ";
            }

            if (CreatedUserInfo.Gender != null && (!int.TryParse(CreatedUserInfo.Gender, out int Gender) || !IsCorrectGender(Gender)))
            {
                IsValid = false;
                ErrorDescription += "Некорретное указание пола; ";
            }

            if (CreatedUserInfo.Birthday!= null && !DateTime.TryParse(CreatedUserInfo.Birthday, out _))
            {
                IsValid = false;
                ErrorDescription += "Некорректная дата рождения; ";
            }

            return (IsValid, ErrorDescription);
        }

        public static (bool IsValid, string ErrorDescription) CheckUserNewMainPersonalInfoIsValid(UserPersonalInfoDTO UserPersonalInfo)
        {
            bool IsValid = true;
            string ErrorDescription = "";

            if (UserPersonalInfo.Guid == null || !Guid.TryParse(UserPersonalInfo.Guid, out _)) 
            {
                IsValid = false;
                ErrorDescription += "Некорретный Guid пользователя; ";
            }

            if (UserPersonalInfo.Name !=null && !IsRuOrEngLetters(UserPersonalInfo.Name))
            {
                IsValid = false;
                ErrorDescription += "Некорректное имя; ";
            }

            if (UserPersonalInfo.Gender != null && (!int.TryParse(UserPersonalInfo.Gender, out int Gender) || !IsCorrectGender(Gender)))
            {
                IsValid = false;
                ErrorDescription += "Некорретное указание пола; ";
            }

            if (UserPersonalInfo.Birthday != null && !DateTime.TryParse(UserPersonalInfo.Birthday, out _))
            {
                IsValid = false;
                ErrorDescription += "Некорректная дата рождения; ";
            }

            return (IsValid, ErrorDescription);
        }

        public static (bool IsValid, string ErrorDescription) CheckUserNewLoginIsValid(UserPersonalInfoDTO UserPersonalInfo)
        {
            bool IsValid = true;
            string ErrorDescription = "";

            if (UserPersonalInfo.Guid == null || !Guid.TryParse(UserPersonalInfo.Guid, out _))
            {
                IsValid = false;
                ErrorDescription += "Некорретный Guid пользователя; ";
            }

            if (UserPersonalInfo.Login == null || !IsEngLettersOrDigits(UserPersonalInfo.Login))
            {
                IsValid = false;
                ErrorDescription += "Некорректный логин; ";
            }

            return (IsValid, ErrorDescription);
        }

        public static (bool IsValid, string ErrorDescription) CheckUserNewPasswordIsValid(UserPersonalInfoDTO UserPersonalInfo)
        {
            bool IsValid = true;
            string ErrorDescription = "";

            if (UserPersonalInfo.Guid == null || !Guid.TryParse(UserPersonalInfo.Guid, out _))
            {
                IsValid = false;
                ErrorDescription += "Некорретный Guid пользователя; ";
            }

            if (UserPersonalInfo.Password == null || !IsEngLettersOrDigits(UserPersonalInfo.Password))
            {
                IsValid = false;
                ErrorDescription += "Некорректный пароль; ";
            }

            return (IsValid, ErrorDescription);
        }

        public static (bool IsValid, string ErrorDescription) CheckUserLoginIsValid(string Login)
        {
            bool IsValid = true;
            string ErrorDescription = "";

            if (Login == null || !IsEngLettersOrDigits(Login))
            {
                IsValid = false;
                ErrorDescription += "Некорректный логин; ";
            }

            return (IsValid, ErrorDescription);
        }

        public static (bool IsValid, string ErrorDescription) CheckUserLoginAndPasswordIsValid(string Login, string Password)
        {
            bool IsValid = true;
            string ErrorDescription = "";

            if (Login == null || !IsEngLettersOrDigits(Login))
            {
                IsValid = false;
                ErrorDescription += "Некорректный логин; ";
            }

            if (Password == null || !IsEngLettersOrDigits(Password))
            {
                IsValid = false;
                ErrorDescription += "Некорректный пароль; ";
            }

            return (IsValid, ErrorDescription);
        }

        public static (bool IsValid, string ErrorDescription) CheckAgeIsValid(string Age)
        {
            bool IsValid = true;
            string ErrorDescription = "";

            if (Age == null || !int.TryParse(Age, out int ConvertedAge) || ConvertedAge < 0)
            {
                IsValid = false;
                ErrorDescription += "Некорректный возраст; ";
            }

            return (IsValid, ErrorDescription);
        }



        static bool IsEngLettersOrDigits(string str)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(str);
        }

        static bool IsRuOrEngLetters(string str)
        {
            Regex regex = new Regex("^[a-zA-Zа-яА-Я]*$");
            return regex.IsMatch(str);
        }

        static bool IsCorrectGender(int Gender)
        {
            return Gender >= 0 && Gender <= 2;
        }
    }
}
