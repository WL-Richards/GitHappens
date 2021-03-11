using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventIT
{
    public class PictureConverter : AxHost
    {
        private PictureConverter() : base(String.Empty)
        {
        }

        /// <summary>
        /// Simply convert a .NET image resource into a IPicture to be used in Inventor
        /// </summary>
        /// <param name="image">The .NET resource to convert</param>
        /// <returns>Converted IPicture</returns>
        public static stdole.IPictureDisp ImageToPictureDisp(Image image)
        {
            return (stdole.IPictureDisp)GetIPictureDispFromPicture(image);
        }
    }
}
