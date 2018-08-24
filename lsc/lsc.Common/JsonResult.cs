using System;
using System.Collections.Generic;
using System.Text;

namespace lsc.Common
{
    [Serializable]
    public class JsonResult<T>
    {
        public int code { get; set; }
        public string msg { get; set; }

        public T data { get; set; }
    }
}
