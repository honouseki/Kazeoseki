using Kazeoseki.Data;
using Kazeoseki.Models.Domain;
using Kazeoseki.Models.ViewModels;
using Kazeoseki.Services.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Services.Services
{
    public class UserService : BaseService
    {
        private Base64StringCryptographyService _cryptographyService = new Base64StringCryptographyService();
        private const int HASH_ITERATION_COUNT = 1;
        private const int RAND_LENGTH = 15;

        // Select All Users
        public List<User> SelectAll()
        {
            List<User> result = new List<User>();
            this.DataProvider.ExecuteCmd(
                "Users_SelectAll",
                inputParamMapper: null,
                singleRecordMapper: delegate(IDataReader reader, short set)
                {
                    User model = new User();
                    int index = 0;
                    model.UserId = reader.GetSafeInt32(index++);
                    model.Username = reader.GetSafeString(index++);
                    model.Email = reader.GetSafeString(index++);
                    model.Salt = reader.GetSafeString(index++);
                    model.HashPassword = reader.GetSafeString(index++);
                    model.RoleId = reader.GetSafeInt32(index++);
                    model.Confirmed = reader.GetSafeBool(index++);
                    model.Suspended = reader.GetSafeBool(index++);
                    model.CreatedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedBy = reader.GetSafeString(index++);
                    model.LoginTypeId = reader.GetSafeInt32(index++);
                    result.Add(model);
                }
            );
            return result;
        }

        // Select By Username
        public LoginUser SelectByUsername(string username)
        {
            LoginUser model = new LoginUser();
            this.DataProvider.ExecuteCmd(
                "Users_SelectByUsername",
                inputParamMapper: delegate(SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Username", username);
                },
                singleRecordMapper: delegate(IDataReader reader, short set)
                {
                    int index = 0;
                    model.UserId = reader.GetSafeInt32(index++);
                    model.Username = reader.GetSafeString(index++);
                    model.Email = reader.GetSafeString(index++);
                    model.Salt = reader.GetSafeString(index++);
                    model.HashPassword = reader.GetSafeString(index++);
                    model.RoleId = reader.GetSafeInt32(index++);
                    model.Confirmed = reader.GetSafeBool(index++);
                    model.Suspended = reader.GetSafeBool(index++);
                    model.CreatedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedDate = reader.GetSafeDateTime(index++);
                    model.ModifiedBy = reader.GetSafeString(index++);
                    model.LoginTypeId = reader.GetSafeInt32(index++);
                }
            );
            return model;
        }

        // Insert User; Register
        public int Insert(LoginUser model)
        {
            LoginUser loginModel = SelectByUsername(model.Username);
            if (loginModel.UserId == 0)
            {
                int userId = 0;
                string salt;
                string hashPassword;
                string password = model.Password;

                salt = _cryptographyService.GenerateRandomString(RAND_LENGTH);
                hashPassword = _cryptographyService.Hash(password, salt, HASH_ITERATION_COUNT);
                model.Salt = salt;
                model.HashPassword = hashPassword;

                this.DataProvider.ExecuteNonQuery(
                    "Users_Insert",
                    inputParamMapper: delegate(SqlParameterCollection paramCol)
                    {
                        SqlParameter paramId = new SqlParameter();
                        paramId.ParameterName = "@UserId";
                        paramId.SqlDbType = SqlDbType.Int;
                        paramId.Direction = ParameterDirection.Output;
                        paramCol.Add(paramId);

                        paramCol.AddWithValue("@Username", model.Username);
                        paramCol.AddWithValue("@Email", model.Email);
                        paramCol.AddWithValue("@Salt", model.Salt);
                        paramCol.AddWithValue("@HashPassword", model.HashPassword);
                        paramCol.AddWithValue("@LoginTypeId", model.LoginTypeId);
                    },
                    returnParameters: delegate(SqlParameterCollection paramCol)
                    {
                        userId = (int)paramCol["@UserId"].Value;
                    }
                );
                return userId;
            } else
            {
                loginModel.UserId = -1;
                return loginModel.UserId;
            }

        }

        // Login
        public bool Login(LoginUser model, bool remember)
        {
            bool isSuccessful = false;
            model.Username = model.Username.ToLower();
            LoginUser loginModel = SelectByUsername(model.Username);
            if (loginModel.UserId != 0 && !String.IsNullOrEmpty(loginModel.Salt))
            {
                int multOf4 = loginModel.Salt.Length % 4;
                if (multOf4 > 0)
                {
                    loginModel.Salt += new string('=', 4 - multOf4);
                }

                string hashPassword = _cryptographyService.Hash(model.Password, loginModel.Salt, HASH_ITERATION_COUNT);

                UserBase resp = new UserBase()
                {
                    UserId = loginModel.UserId,
                    Roles = new[] { "User" },
                    Username = loginModel.Username,
                    Email = loginModel.Email,
                    Remember = remember,
                    RoleId = loginModel.RoleId
                };

                // To create the cookie? Will do later
                //Claim emailClaim = new Claim(userData.Email.ToString(), "LPGallery");
                //_authenticationService.LogIn(response, new Claim[] { emailClaim });
                // This is where we use remember to store in the cookie^

                if (model.Username == loginModel.Username && hashPassword == loginModel.HashPassword)
                {
                    isSuccessful = true;
                }
            }
                return isSuccessful;
        }





    }
}
