using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.General
{
    /// <summary>
    /// Dispose함수(리소스해제용도)를 호출하지 않더라도 소멸자에서 Dispose함수를 호출하기위한 클래스
    /// Dispose함수를 따로 호출하면 소멸자내에서 리소스해제를 실행하지 않는다
    /// 또한 Dispose함수를 호출하지 않으면 소멸자 호출시에 리소스해제를 실행한다
    /// </summary>
    public abstract class ResourceFreeRequired : IDisposable
    {
        private bool _disposed = false;


        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                ResourceFree();
                _disposed = true;
            }

            if (!disposing)
                GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 사용된 리소스를 해제한다
        /// </summary>
        public void Dispose()
        {
            Dispose(false);
        }

        ~ResourceFreeRequired()
        {
            Dispose(true);
        }


        // 리소스 해제 코드
        protected abstract void ResourceFree();
    }
}
