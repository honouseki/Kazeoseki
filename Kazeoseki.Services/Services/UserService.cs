using Kazeoseki.Data;
using Kazeoseki.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kazeoseki.Services.Services
{
    public class UserService : BaseService
    {
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
                    result.Add(model);
                }
            );
            return result;
        }
    }
}
