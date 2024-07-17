using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Models
{
    public class UserBasicInfo()
    {

        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public List<UserSubs> Subscriptions { get; set; } = [];


    }
}
