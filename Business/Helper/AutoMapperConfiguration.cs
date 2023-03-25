using AutoMapper;

namespace Business.Helper
{
    public static class AutoMapperConfiguration
    {
        public static void Init()
        {
        }
        public static IMapper Mapper { get; set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }

    }
}
