using DTO;
using Entities;

namespace Business.Helper.Extensions
{
    internal static class MappingExtention
    {
        internal static TEntity ToEntity<TEntity>(this BaseDTO dto) where TEntity : BaseEntity
        {
            return AutoMapperConfiguration.Mapper.Map<TEntity>(dto);
        }
        internal static IList<TEntity> ToEntityList<TEntity>(this IEnumerable<BaseDTO> dtos)
            where TEntity : BaseEntity
        {
            return AutoMapperConfiguration.Mapper.Map<IList<TEntity>>(dtos);
        }
        internal static TDTO ToDTO<TDTO>(this BaseEntity entity) where TDTO : BaseDTO
        {
            return AutoMapperConfiguration.Mapper.Map<TDTO>(entity);
        }

        internal static List<TDTO> ToDTOList<TDTO>(this IEnumerable<BaseEntity> entity)
            where TDTO : BaseDTO
        {
            return AutoMapperConfiguration.Mapper.Map<List<TDTO>>(entity);
        }
    }
}
