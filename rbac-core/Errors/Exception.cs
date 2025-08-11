namespace rbac_core.Errors
{
    public abstract class AppException : Exception
    {
        public AppException() { }

        public AppException(string message)
            : base(message) { }

        public AppException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class ValidationException(string message) : AppException(message) { }

    public class UnAuthorizedException(string message) : AppException(message) { }

    public class NotFoundException(string message) : AppException(message) { }

    public class AlreadyExistsException(string message) : AppException(message) { }

    public class NotAllowedException(string message) : AppException(message) { }
}
