using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gncerp.Utility
{
    public static class HContext
    {
        public static HttpContext _current() => new HttpContextAccessor().HttpContext;

    }

}
