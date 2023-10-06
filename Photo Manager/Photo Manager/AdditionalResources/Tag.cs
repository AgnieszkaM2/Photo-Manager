using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo_Manager.AdditionalResources
{
    public class Tag
    {
        public required string Name { get; set; }
        public required string PhotoPath { get; set; }
        public required string ParentDirectory { get; set; }
    }
}
