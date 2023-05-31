using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        //Navigation properties
        public List<Cadre> Cadres { get; set;}

        //public Tax Tax { get; set; }
    }
}
