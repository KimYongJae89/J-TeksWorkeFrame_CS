using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detector
{
    public interface IDetectorBasic
    {
        int Width { get; set; }
        int Height { get; set; }

        bool Initialize();
        void Open();
        void Close();
        void AcquisitionStart();
        void AcquisitionStop();
    }
}
