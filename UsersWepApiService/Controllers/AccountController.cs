using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsersWepApiService.Services.Interfaces;
using UsersWepApiService.DataLayer.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using UsersWepApiService.DataLayer.Responses;
using System.ComponentModel.DataAnnotations;

namespace UsersWepApiService.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST Account/Login
        ///     {
        ///        "login": "Admin",
        ///        "password": "Admin"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Токен успешно получен</response>
        /// <response code="404">Неправильный логин или пароль</response>
        /// <response code="422">Некорректные данные</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> Login([FromBody,Required] LoginViewModel loginModel)
        {
            var response = await _accountService.Login(loginModel);
            Response.StatusCode = (int)response.StatusCode;
            return Json(response);
        }
    }
}
