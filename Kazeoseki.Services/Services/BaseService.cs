using Kazeoseki.Data;
using Kazeoseki.Data.Providers;

namespace Kazeoseki.Services
{
    public abstract class BaseService
    {
        public IDataProvider DataProvider { get; set; }

        public BaseService(IDataProvider dataProvider)
        {
            this.DataProvider = dataProvider;
        }

        public BaseService()
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            this.DataProvider = new SqlDataProvider(connStr);
        }
    }
}
