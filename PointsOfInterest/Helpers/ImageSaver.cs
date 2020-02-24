using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsOfInterest.Helpers
{
    public static class ImageSaver
    {
        public static string Save(string imgFolder, string imgPath)
        {
            var imgName = System.IO.Path.GetFileName(imgPath);
            var dir = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString();
            var path = System.IO.Path.GetDirectoryName(dir);
            var combinePath = System.IO.Path.Combine(path + $"/Images/{imgFolder}/" + imgName);

            Bitmap b = new Bitmap(imgPath);

            b.Save(combinePath);

            return imgName;
        }
    }
}
