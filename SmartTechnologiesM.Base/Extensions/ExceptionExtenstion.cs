using System;

namespace SmartTechnologiesM.Base.Extensions
{
    public static class ExceptionExtenstion
    {
        public static Exception GetInnerException(this Exception exception) => 
            exception.InnerException == null ? exception : GetInnerException(exception.InnerException);
    }
}
