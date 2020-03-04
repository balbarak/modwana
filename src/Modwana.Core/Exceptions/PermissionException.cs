using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Exceptions
{
    [Serializable]
    public class PermissionException : Exception
    {
        public PermissionException() { }

        public PermissionException(string message) : base(message) { }

        public PermissionException(string message, Exception inner) : base(message, inner) { }

        protected PermissionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
