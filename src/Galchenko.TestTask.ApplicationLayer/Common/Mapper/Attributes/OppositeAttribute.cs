using System;

namespace Galchenko.TestTask.ApplicationLayer.Common.Mapper.Attributes
{
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = true, Inherited = false )]
    public class OppositeAttribute : Attribute
    {
        public OppositeAttribute( string property )
        {
            Property = property;
        }

        public string Property { get; }
    }
}
