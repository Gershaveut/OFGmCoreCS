using System;

namespace OFGmCoreCS.Util
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException() : base("This object already exists in the list.")
        {

        }
    }
}
