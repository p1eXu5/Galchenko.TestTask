using System;
using AutoMapper;

namespace Galchenko.TestTask.ApplicationLayer.Common.Mapper.Contracts
{
    [Obsolete("Consider using MapToAttribute and MapFromAttribute attributes instead.")]
    public interface IMap
    {
        void CreateMap( Profile profile );
    }

    [Obsolete("Consider using MapToAttribute and MapFromAttribute attributes instead.")]
    public interface ISourceMap< TS, TD >
    {
        IMappingExpression< TS, TD > CreateMap( Profile profile ) 
            => profile.CreateMap< TS, TD >( MemberList.Source ).IncludeAllDerived();
    }

    [Obsolete("Consider using MapToAttribute and MapFromAttribute attributes instead.")]
    public interface IDestinationMap< TS, TD >
    {
        IMappingExpression< TS, TD > CreateMap( Profile profile ) 
            => profile.CreateMap< TS, TD >( MemberList.Destination ).IncludeAllDerived();
    }

    [Obsolete("Consider using MapToAttribute and MapFromAttribute attributes instead.")]
    public interface IReversedMap
    {
        void CreateReverseMap< TS, TD >( IMappingExpression< TS, TD > expr ) => expr.ReverseMap();
    }
}
