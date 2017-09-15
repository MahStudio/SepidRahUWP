using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepidrah.UWP.Entities
{
    
        public class VMUserLevelOne
        {
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("lastname")]
            public string LastName { get; set; }
        }
    
}
