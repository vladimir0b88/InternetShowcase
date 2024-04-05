
using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITypePropertyService
    {
        Task<Result<List<TypeProperty>>> GetPropertiesByTypeId(long typeId);


    }
}
