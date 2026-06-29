using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicSystem.Shared.CommonResult
{
    public class Result
    {
        private readonly List<Error> _errors = [];

        public bool IsSuccess => _errors.Count == 0; //true
        public bool IsFailure => !IsSuccess; //false
        public IReadOnlyList<Error> Errors => _errors.AsReadOnly();

        //OK-Success
        protected Result() { }
        //Failure-With Error
        protected Result(Error error)
        {
            _errors.Add(error);
        }
        //Failure-With Errors
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }
        //3 Static Factory Methods To Create Results
        public static Result Ok() => new Result();
        public static Result Fail(Error error) => new Result(error);
        public static Result Fail(List<Error> errors) => new Result(errors);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value => IsSuccess ? _value
            : throw new InvalidOperationException("Cannot access the value of a failed result.");

        private Result(TValue value) : base() => _value = value;
        private Result(Error error) : base(error) => _value = default!;
        private Result(List<Error> errors) : base(errors) => _value = default!;

        public static Result<TValue> Ok(TValue value) => new(value);
        public new static Result<TValue> Fail(Error error) => new(error);
        public new static Result<TValue> Fail(List<Error> errors) => new(errors);
    }
}
