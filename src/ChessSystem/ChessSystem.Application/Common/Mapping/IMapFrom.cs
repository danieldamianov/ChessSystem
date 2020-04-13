using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Application.Common.Mapping
{
    using AutoMapper;

    public interface IMapFrom<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), GetType());
    }
}
