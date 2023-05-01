using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using UsersWepApiService.DataLayer.DTO;
using UsersWepApiService.DataLayer.ViewModels.Account;
using UsersWepApiService.Services.Implementations;
using UsersWepApiService.Services.Interfaces;

namespace UsersWepApiService.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST Users/CreateUser
        ///     {
        ///        "login": "Login",
        ///        "password": "Password",
        ///        "name": "Name",
        ///        "gender": "0",
        ///        "birthday": "2012-12-12T00:00:00",
        ///        "admin": "false"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Пользователь успешно создан</response>
        /// <response code="400">Пользователь с заданным логином уже существует</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> CreateUser([FromBody,Required] CreatedUserInfoDTO CreatedUserInfo)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.CreateUser(CreatedUserInfo, RequesterUserGuid);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }


        /// <summary>
        /// Изменение имени, пола или даты рождения пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT Users/UpdatePersonalUserData
        ///     {
        ///        "guid": "08db496e-7ca5-487f-81d2-b147fee21fc4",
        ///        "name": "Name",
        ///        "gender": "0",
        ///        "birthday": "2012-12-12T00:00:00"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Данные успешно обновлены</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="404">Пользователь с заданным Guid не существует</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<JsonResult> UpdatePersonalUserData([FromBody, Required] UserPersonalInfoDTO UserPersonalInfo)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.UpdateMainUserData(UserPersonalInfo, RequesterUserGuid);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Изменение пароля пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT Users/UpdateUserPassword
        ///     {
        ///        "guid": "08db496e-7ca5-487f-81d2-b147fee21fc4",
        ///        "password": "Password",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Данные успешно обновлены</response>
        /// <response code="400">Новый пароль совпадает со старым</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="404">Пользователь с заданным Guid не существует</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<JsonResult> UpdateUserPassword([FromBody, Required] UserPersonalInfoDTO UserPersonalInfo)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.UpdateUserPassword(UserPersonalInfo, RequesterUserGuid);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Изменение логина пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT Users/UpdateUserLogin
        ///     {
        ///        "guid": "08db496e-7ca5-487f-81d2-b147fee21fc4",
        ///        "login": "Login",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Данные успешно обновлены</response>
        /// <response code="400">Логин уже занят</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="404">Пользователь с заданным Guid не существует</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<JsonResult> UpdateUserLogin([FromBody, Required] UserPersonalInfoDTO UserPersonalInfo)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.UpdateUserLogin(UserPersonalInfo, RequesterUserGuid);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Запрос активных пользователей
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET Users/GetActiveUsers
        ///
        /// </remarks>
        /// <response code="200">Данные успешно получены</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetActiveUsers()
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.GetActiveUsers();
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Запрос пользователя по логину
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET Users/GetUserByLogin/Admin
        ///
        /// </remarks>
        /// <response code="200">Данные успешно получены</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="404">Пользователь с заданным логином не существует</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("{Login}")]
        public async Task<JsonResult> GetUserByLogin(string Login)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.GetUserByLogin(Login);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Запрос пользователя по логину и паролю
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST Users/GetUserByLoginAndPassword
        ///     {
        ///        "login": "Admin",
        ///        "password": "Admin"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Данные успешно получены</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="403">Неправильный логин или пароль</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<JsonResult> GetUserByLoginAndPassword([FromBody, Required] LoginViewModel loginModel)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.GetUserByLoginAndPassword(loginModel.Login, loginModel.Password, RequesterUserGuid);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Запрос пользователей старше определенного возвраста
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET Users/GetUsersOverCertainAge/18
        ///
        /// </remarks>
        /// <response code="200">Данные успешно получены</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpGet("{Age}")]
        public async Task<JsonResult> GetUsersOverCertainAge(string Age)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.GetUsersOverCertainAge(Age);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Мягкое удаление пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE Users/SoftDeleteUser
        ///     {
        ///        "login": "Login",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Пользователь успешно (мягко) удален</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="404">Пользователь с заданным логином не существует</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<JsonResult> SoftDeleteUser([FromBody, Required] UserLoginDTO UserLogin)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.SoftDeleteUser(UserLogin.Login, RequesterUserGuid);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE Users/DeleteUser
        ///     {
        ///        "login": "Login",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Пользователь успешно удален</response>
        /// <response code="400">Логин уже занят</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="404">Пользователь с заданным логином не существует</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<JsonResult> DeleteUser([FromBody, Required] UserLoginDTO UserLogin)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.DeleteUser(UserLogin.Login);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }

        /// <summary>
        /// Восстановление пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE Users/RevokeUser
        ///     {
        ///        "login": "Login",
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Пользователь успешно восстановлен</response>
        /// <response code="401">Ошибка авторизации</response>
        /// <response code="403">Отказано в доступе</response>
        /// <response code="404">Пользователь с заданным логином не существует</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<JsonResult> RevokeUser([FromBody, Required] UserLoginDTO UserLogin)
        {
            string RequesterUserGuid = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            var IsRequesterUserExists = await _usersService.CheckIsRequesterUserExists(RequesterUserGuid);
            if (!IsRequesterUserExists.Data) return Json(IsRequesterUserExists);

            var response = await _usersService.RevokeUser(UserLogin.Login);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }
    }
}
