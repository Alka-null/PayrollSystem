using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ConfigurationEntities
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public double JwtExpiryTime { get; set; }
    }
}
