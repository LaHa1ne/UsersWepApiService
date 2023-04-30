using UsersWepApiService.DataLayer.Enums;

namespace UsersWepApiService.DataLayer.Responses
{
    public class AuthorizationRequiredResponse : BaseRepsonse<bool>
    {
        public AuthorizationRequiredResponse() : base()
        {
            Description = "Требуется авторизация";
            Data = false;
            StatusCode = StatusCode.Unauthorized;
        }
    }
}
