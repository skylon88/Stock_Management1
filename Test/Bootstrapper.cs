using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewServices;
using Unity;

namespace Test
{
    public class Bootstrapper
    {
        public static void Init()
        {
            DependencyInjector.Register<IRequestService, RequestService>();
            DependencyInjector.AddExtension<DependencyOfDependencyExtension>();
        }
    }
}
