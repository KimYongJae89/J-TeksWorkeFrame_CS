using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNPI
{
    public class Config
    {
        private static volatile Config uniqInstance;
        private static object syncRoot = new Object();

        public static Config Inst
        {
            get
            {
                if (uniqInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (uniqInstance == null)
                            uniqInstance = new Config();
                    }
                }

                return uniqInstance;
            }
        }

        private Config() { }


        public Color BackGroundClr = Color.FromArgb(234, 239, 245);
        public Color ButtonClr = Color.FromArgb(90, 107, 135);
        public Color TextClr = Color.FromArgb(116, 130, 192);
    }
}