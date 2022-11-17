using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XManager.Util
{
    public class ImageCoordTransformer
    {
        public int OrgWidth { get; set; }
        public int OrgHeight { get; set; }
        public int NewWidth { get; set; }
        public int NewHeight { get; set; }
        public int Scale { get; set; }

        public ImageCoordTransformer()
        {
        }
    }
}
