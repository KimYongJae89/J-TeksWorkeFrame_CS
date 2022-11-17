using KiyLib.General;
using Matrox.MatroxImagingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMatrox
{
    public class JMapp : IDisposable
    {
        private bool _initialized;
        private MIL_ID milApp = MIL.M_NULL;
        private MIL_ID milSys = MIL.M_NULL;
        private MIL_ID milDisp = MIL.M_NULL;

        public bool Initialized
        {
            get { return _initialized; }
        }
        public MIL_ID MilApp
        {
            get { return milSys; }
        }
        public MIL_ID MilSys
        {
            get { return milSys; }
        }
        public MIL_ID MilDisp
        {
            get { return milDisp; }
        }

        private static volatile JMapp uniqInstance;
        private static object syncRoot = new Object();
        public static JMapp Inst
        {
            get
            {
                if (uniqInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (uniqInstance == null)
                            uniqInstance = new JMapp();
                    }
                }

                return uniqInstance;
            }
        }


        private JMapp()
        {
            Allocate();
        }


        private void Allocate()
        {
            if (!_initialized)
            {
                MIL.MappAllocDefault(MIL.M_DEFAULT, ref milApp, ref milSys, ref milDisp, MIL.M_NULL, MIL.M_NULL);
                _initialized = true;
            }
            //else
            //    throw new Exception("[JMapp] - Allocate(), Already Initialized");
        }


        public void Dispose()
        {
            if (_initialized)
            {
                MIL.MappFreeDefault(milApp, milSys, milDisp, MIL.M_NULL, MIL.M_NULL);
                _initialized = false;
            }
            else
                throw new Exception("[JMapp] - ResourceFree(), Not Initialized");
        }
    }
}
