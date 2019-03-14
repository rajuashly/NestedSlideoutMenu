using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NestedSlideoutMenu.Models {

    public class MasterPageItem {

        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
        public Color TextColor { get; set; } = Color.White;
        public string Count { get; set; }
        public Color BorderColor { get; set; } = Color.White;
        public int BorderThickness { get; set; } = 1;
    }
}
