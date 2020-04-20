﻿namespace ChessSystem.Application.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public static Result Success
            => new Result(true, Array.Empty<string>());

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Failure(IEnumerable<string> errors)
            => new Result(false, errors);
    }
}