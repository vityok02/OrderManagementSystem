﻿namespace Domain.Abstract;

public class Result<T> : Result
{
    public T Value { get; } = default!;

    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private Result()
    {
        IsSuccess = true;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }

    private Result(string code, string? description = null)
    {
        IsSuccess = false;
        Error = new(code, description);
    }

    public static Result<T> Success(T value) => new(value);
    public new static Result<T> Failure(Error error) => new(error);
    public new static Result<T> Failure(string code, string? description = null) => new(code, description);
}
