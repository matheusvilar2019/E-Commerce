﻿namespace E_Commerce.ViewModels;
public class ResultDTO<T>
{
    public ResultDTO(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }

    public ResultDTO(T data)
    {
        Data = data;
    }

    public ResultDTO(List<string> errors)
    {
        Errors = errors;
    }

    public ResultDTO(string error)
    {
        Errors.Add(error);
    }

    public T Data { get; set; }
    public List<string> Errors { get; private set; } = new();
}
