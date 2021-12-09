using Gradovi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Gradovi.Models
{
    public class City
    {
        public int IDCity { get; set; }
        public string ImeGrada { get; set; }
        public int CountryID { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image
        {
            get => ImageUtils.ByteArrayToBitmapImage(Picture);
        }
    }
}
