using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photo_Manager.ViewModels
{
    public class BaseResource
    {
        public static string ChosenImage { get; set; }
        public static string ChosenPath { get; set; }

        public static List<string> CurrentGallery = new List<string>();


    }
}
