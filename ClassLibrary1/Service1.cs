using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Service1
    {
        private readonly Service2 _service2;

        public Service1(Service2 service2)
        {
            _service2 = service2;
        }

        public string CallService2PrintFromService1()
        {
            return _service2.PrintMyName();
        }
    }
}
