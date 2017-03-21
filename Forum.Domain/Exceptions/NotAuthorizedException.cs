using System;

namespace Forum.Domain.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base() { }
    }
}
