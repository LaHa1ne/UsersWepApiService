using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersWepApiService.DataLayer.Enums
{
    public enum StatusCode
    {
        OK = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        UnprocessableContent = 422,
        InternalServerError = 500,
    }
}
