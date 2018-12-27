using AutoMapper;

namespace FantasyLeague.Services
{
    public abstract class BaseService
    {
        protected readonly IMapper mapper;

        protected BaseService(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}
