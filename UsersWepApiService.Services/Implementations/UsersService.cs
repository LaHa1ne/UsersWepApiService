using UsersWepApiService.Services.Interfaces;
using UsersWepApiService.DataAccessLayer.Interfaces;
using UsersWepApiService.DataLayer.DTO;
using UsersWepApiService.DataLayer.Responses;
using UsersWepApiService.DataLayer.Helpers;
using AutoMapper;
using UsersWepApiService.DataLayer.Entities;
using UsersWepApiService.DataLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace UsersWepApiService.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersService(IMapper mapper, IUserRepository userRepository )
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseRepsonse<bool>> CreateUser(CreatedUserInfoDTO CreatedUserInfo, string RequesterUserGuid)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckCreatedUserInfoIsValid(CreatedUserInfo);
                if (!IsDataValid) return new BaseRepsonse<bool>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var requesterUser = await _userRepository.GetUserByGuid(Guid.Parse(RequesterUserGuid));

                var user = await _userRepository.GetUserByLogin(CreatedUserInfo.Login);
                if (user != null) return new BaseRepsonse<bool>(Description: "Пользователь с таким логином уже существует", StatusCode: StatusCode.BadRequest);

                var new_user = _mapper.Map<User>(CreatedUserInfo);
                new_user.CreatedOn = DateTime.Now;
                new_user.CreatedBy = requesterUser.Login;

                await _userRepository.Create(new_user);

                return new BaseRepsonse<bool>()
                {
                    Description = "Пользователь успешно создан",
                    Data = true,
                    StatusCode = StatusCode.Created
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<bool>> UpdateMainUserData(UserPersonalInfoDTO UserPersonalInfo, string RequesterUserGuid)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserNewMainPersonalInfoIsValid(UserPersonalInfo);
                if (!IsDataValid) return new BaseRepsonse<bool>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var requesterUser = await _userRepository.GetUserByGuid(Guid.Parse(RequesterUserGuid));
                if (!(requesterUser.Admin || (requesterUser.Guid == Guid.Parse(UserPersonalInfo.Guid) && requesterUser.RevokedOn == null)))
                {
                    return new BaseRepsonse<bool>(Description: "Отказано в доступе", StatusCode: StatusCode.Forbidden);
                }

                var user = await _userRepository.GetUserByGuid(Guid.Parse(UserPersonalInfo.Guid));
                if (user != null)
                {
                    if (UserPersonalInfo.Name is not null) user.Name = UserPersonalInfo.Name;
                    if (UserPersonalInfo.Gender is not null) user.Gender = int.Parse(UserPersonalInfo.Gender);
                    if (UserPersonalInfo.Birthday is not null) user.Birthday = DateTime.Parse(UserPersonalInfo.Birthday);
                    user.ModifiedBy = requesterUser.Login;
                    user.ModifiedOn = DateTime.Now;

                    await _userRepository.Update(user);

                    return new BaseRepsonse<bool>()
                    {
                        Description = "Данные пользователя успешно обновлены",
                        Data = true,
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseRepsonse<bool>(Description: "Пользователь с заданным Guid не существует", StatusCode: StatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<bool>> UpdateUserPassword(UserPersonalInfoDTO UserPersonalInfo, string RequesterUserGuid)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserNewPasswordIsValid(UserPersonalInfo);
                if (!IsDataValid) return new BaseRepsonse<bool>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var requesterUser = await _userRepository.GetUserByGuid(Guid.Parse(RequesterUserGuid));
                if (!(requesterUser.Admin || (requesterUser.Guid == Guid.Parse(UserPersonalInfo.Guid) && requesterUser.RevokedOn == null)))
                {
                    return new BaseRepsonse<bool>(Description: "Отказано в доступе", StatusCode: StatusCode.Forbidden);
                }

                var user = await _userRepository.GetUserByGuid(Guid.Parse(UserPersonalInfo.Guid));
                if (user != null)
                {
                    if (user.Password == HashPasswordHelper.GetHashPassword(UserPersonalInfo.Password))
                        return new BaseRepsonse<bool>(Description: "Пароль совпадает со старым", StatusCode: StatusCode.BadRequest);

                    user.Password = HashPasswordHelper.GetHashPassword(UserPersonalInfo.Password);
                    user.ModifiedBy = requesterUser.Login;
                    user.ModifiedOn = DateTime.Now;

                    await _userRepository.Update(user);

                    return new BaseRepsonse<bool>()
                    {
                        Description = "Пароль пользователя успешно обновлен",
                        Data = true,
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseRepsonse<bool>(Description: "Пользователь с заданным Guid не существует", StatusCode: StatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);

            }
        }

        public async Task<BaseRepsonse<bool>> UpdateUserLogin(UserPersonalInfoDTO UserPersonalInfo, string RequesterUserGuid)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserNewLoginIsValid(UserPersonalInfo);
                if (!IsDataValid) return new BaseRepsonse<bool>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var requesterUser = await _userRepository.GetUserByGuid(Guid.Parse(RequesterUserGuid));
                if (!(requesterUser.Admin || (requesterUser.Guid == Guid.Parse(UserPersonalInfo.Guid) && requesterUser.RevokedOn == null)))
                {
                    return new BaseRepsonse<bool>(Description: "Отказано в доступе", StatusCode: StatusCode.Forbidden);
                }

                var user = await _userRepository.GetUserByGuid(Guid.Parse(UserPersonalInfo.Guid));
                if (user != null)
                {
                    var userWithSameLogin = _userRepository.GetUserByLogin(UserPersonalInfo.Login);
                    if (userWithSameLogin != null) return new BaseRepsonse<bool>(Description: "Пользователь с таким логином уже существует", StatusCode: StatusCode.BadRequest);

                    user.Login = UserPersonalInfo.Login;
                    user.ModifiedBy = requesterUser.Login;
                    user.ModifiedOn = DateTime.Now;

                    await _userRepository.Update(user);

                    return new BaseRepsonse<bool>()
                    {
                        Description = "Логин пользователя успешно обновлен",
                        Data = true,
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseRepsonse<bool>(Description: "Пользователь с заданным Guid не существует", StatusCode: StatusCode.NotFound);

            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<IEnumerable<UserInfoDTO>>> GetActiveUsers()
        {
            try
            {
                var users = _mapper.Map<List<UserInfoDTO>>(await _userRepository.GetActiveUsers());

                return new BaseRepsonse<IEnumerable<UserInfoDTO>>()
                {
                    Description = "Список активных пользователей успешно получен",
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<IEnumerable<UserInfoDTO>>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<UserMainPersonalInfoDTO>> GetUserByLogin(string Login)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserLoginIsValid(Login);
                if (!IsDataValid) return new BaseRepsonse<UserMainPersonalInfoDTO>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var user = await _userRepository.GetUserByLogin(Login);
                if (user == null) return new BaseRepsonse<UserMainPersonalInfoDTO>(Description: "Пользователя с таким логином не существует", StatusCode: StatusCode.NotFound);

                return new BaseRepsonse<UserMainPersonalInfoDTO>()
                {
                    Description = "Данные о пользователе успешно получены",
                    Data = _mapper.Map<UserMainPersonalInfoDTO>(user),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<UserMainPersonalInfoDTO>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }

        }

        public async Task<BaseRepsonse<UserMainPersonalInfoDTO>> GetUserByLoginAndPassword(string Login, string Password, string RequesterUserGuid)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserLoginAndPasswordIsValid(Login,Password);
                if (!IsDataValid) return new BaseRepsonse<UserMainPersonalInfoDTO>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var requesterUser = await _userRepository.GetUserByGuid(Guid.Parse(RequesterUserGuid));
                if (requesterUser.Login!=Login) return new BaseRepsonse<UserMainPersonalInfoDTO>(Description: "Отказано в доступе", StatusCode: StatusCode.Forbidden);

                var user = await _userRepository.GetUserByLoginAndPassword(Login, HashPasswordHelper.GetHashPassword(Password));
                if (user == null) return new BaseRepsonse<UserMainPersonalInfoDTO>(Description: "Неправильный логин или пароль", StatusCode: StatusCode.NotFound);

                return new BaseRepsonse<UserMainPersonalInfoDTO>()
                {
                    Description = "Данные о пользователе успешно получены",
                    Data = _mapper.Map<UserMainPersonalInfoDTO>(user),
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<UserMainPersonalInfoDTO>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<IEnumerable<UserInfoDTO>>> GetUsersOverCertainAge(string Age)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckAgeIsValid(Age);
                if (!IsDataValid) return new BaseRepsonse<IEnumerable<UserInfoDTO>>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var users = _mapper.Map<List<UserInfoDTO>>(await _userRepository.GetUsersOverCertainAge(int.Parse(Age)));

                return new BaseRepsonse<IEnumerable<UserInfoDTO>>()
                {
                    Description = "Список пользователей старше определенного возраста получен",
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<IEnumerable<UserInfoDTO>>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<bool>> SoftDeleteUser(string LoginOfDeletedUser, string RequesterUserGuid)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserLoginIsValid(LoginOfDeletedUser);
                if (!IsDataValid) return new BaseRepsonse<bool>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var requesterUser = await _userRepository.GetUserByGuid(Guid.Parse(RequesterUserGuid));

                var user = await _userRepository.GetUserByLogin(LoginOfDeletedUser);
                if (user == null) return new BaseRepsonse<bool>(Description: "Пользователя с таким логином не существует", StatusCode: StatusCode.NotFound);

                user.RevokedBy = requesterUser.Login;
                user.RevokedOn = DateTime.Now;

                await _userRepository.Update(user);

                return new BaseRepsonse<bool>()
                {
                    Description = "Пользователь успешно (мягко) удален",
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<bool>> DeleteUser(string LoginOfDeletedUser)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserLoginIsValid(LoginOfDeletedUser);
                if (!IsDataValid) return new BaseRepsonse<bool>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var user = await _userRepository.GetUserByLogin(LoginOfDeletedUser);
                if (user == null) return new BaseRepsonse<bool>(Description: "Пользователя с таким логином не существует", StatusCode: StatusCode.NotFound);

                await _userRepository.Delete(user);

                return new BaseRepsonse<bool>()
                {
                    Description = "Пользователь успешно удален",
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<bool>> RevokeUser(string LoginOfRevokedUser)
        {
            try
            {
                var (IsDataValid, ErrorDescription) = DataValidationHelper.CheckUserLoginIsValid(LoginOfRevokedUser);
                if (!IsDataValid) return new BaseRepsonse<bool>(Description: ErrorDescription, StatusCode: StatusCode.UnprocessableContent);

                var user = await _userRepository.GetUserByLogin(LoginOfRevokedUser);
                if (user == null) return new BaseRepsonse<bool>(Description: "Пользователя с таким логином не существует", StatusCode: StatusCode.NotFound);

                user.RevokedBy = null;
                user.RevokedOn = null;

                await _userRepository.Update(user);

                return new BaseRepsonse<bool>()
                {
                    Description = "Пользователь успешно восстановлен",
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }

        public async Task<BaseRepsonse<bool>> CheckIsRequesterUserExists(string RequesterUserGuid)
        {
            try
            {
                var user = await _userRepository.GetUserByGuid(Guid.Parse(RequesterUserGuid));
                if (user != null)
                {
                    return new BaseRepsonse<bool>()
                    {
                        Description = "Пользователь, выполняющий запрос, существует",
                        Data = true,
                        StatusCode = StatusCode.OK
                    };
                }
                return new BaseRepsonse<bool>(Description: "Пользователя, выполняющего запрос, не существует", StatusCode: StatusCode.NotFound);

            }
            catch (Exception ex)
            {
                return new BaseRepsonse<bool>(Description: ex.Message, StatusCode: StatusCode.InternalServerError);
            }
        }
    }
}
