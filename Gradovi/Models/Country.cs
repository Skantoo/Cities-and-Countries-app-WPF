using Gradovi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Gradovi.Models
{
    public class Country
    {
        public int IDCountry { get; set; }
        public string ImeDrzave { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image  
        {
            get => ImageUtils.ByteArrayToBitmapImage(Picture);
        }
        public override string ToString() => $"{ImeDrzave}";
    }
}
