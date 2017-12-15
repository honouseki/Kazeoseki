using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazeosekiApp.Models.Responses
{
    public class ItemsResponse<T> : SuccessResponse
    {
        public List<T> Item { get; set; }
    }
}
