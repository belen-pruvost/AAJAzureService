using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAJAzureService.Utilities
{
    public static class Common
    {
        public static int GenerateRandomNo()
        {
            int _min = 00000;
            int _max = 99999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

    }
}
