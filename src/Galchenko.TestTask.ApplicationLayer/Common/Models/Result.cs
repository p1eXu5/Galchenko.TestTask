using System.Collections.Generic;
using System.Linq;

// ReSharper disable once IdentifierTypo
namespace Galchenko.TestTask.ApplicationLayer.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

        public static Result Failure( string error )
        {
            return new Result(false, new string[] { error });
        }

        /// <summary>
        /// Returns errors aggregate.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if ( Errors.Any() ) {
                return  Errors.Aggregate( "", (acc, err) => acc + err + "; " )[..^2];
            }
            return "failed.";
        }
    }
}
