using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Backend.Communication.Messenger
{
    public class MessengerClasses
    {
        public class NavigatePageTo
        {
            public string PageName { get; set; }

            public NavigatePageTo(string pageName)
            {
                PageName = pageName;
            }

        }

    }
}
