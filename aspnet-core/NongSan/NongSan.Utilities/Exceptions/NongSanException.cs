using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Utilities.Exceptions
{
    public class NongSanException : Exception
    {
        // StatusCode Http: 200, 201, 400, 404...
        public int Status { get; set; } = 200;

        public NongSanException(int status = 200)
        {
            Status = status;
        }

        public NongSanException(string message, int status = 200)
            : base(message)
        {
            Status = status;
        }

        public NongSanException(string message, Exception inner, int status = 200)
            : base(message, inner)
        {
            Status = status;
        }

        public NongSanException(Exception ex, int status = 200) : base(ex.Message)
        {
            Status = status;
        }
    }
}
