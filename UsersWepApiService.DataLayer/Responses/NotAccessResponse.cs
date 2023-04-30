using UsersWepApiService.DataLayer.Enums;

namespace UsersWepApiService.DataLayer.Responses
{
    public class NotAccessResponse : BaseRepsonse<bool>
    {
        public NotAccessResponse() : base()
        {
            Description = "Недостаточно прав доступа";
            Data = false;
            StatusCode = StatusCode.Forbidden;
        }
    }
}
