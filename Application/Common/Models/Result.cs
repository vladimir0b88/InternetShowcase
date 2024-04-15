namespace Application.Common.Models
{


    public abstract class Result
    {
        public bool Success { get; protected set; }
        public bool Failure => !Success;
    }

    public abstract class Result<T> : Result
    {
        private T _data;

        protected Result(T data) => _data = data;

        public T Data
        {
            get => _data;
            set => _data = value;
        }
    }
}
