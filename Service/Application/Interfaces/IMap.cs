using AutoMapper;

namespace Application.Interfaces;

public interface IMap<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}

public interface IMap
{
    void Mapping(Profile profile);
}