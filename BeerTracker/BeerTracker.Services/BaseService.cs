namespace BeerTracker.Services
{
    using AutoMapper;
    using Models.DataModels;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Geo;
    using Models.ViewModels.User;
    using PagedList;
    using UnitOfWork.Contracts;
    using UnitOfWork.UoW;

    public abstract class BaseService
    {
        protected IUnitOfWork db;
        protected IMapper mapper;
        protected MapperConfiguration configuration;

        public BaseService(IUnitOfWork db)
        {
            this.db = db;
            this.ConfigureMapper();
            this.CreateMapper();
        }

        private void CreateMapper()
        {
            this.mapper = configuration.CreateMapper();
        }

        private void ConfigureMapper()
        {
            configuration = new MapperConfiguration(m =>
            {
                m.CreateMap<Location, BeerLocationViewModel>();

                m.CreateMap<Beer, MyFoundBeerViewModel>()
                .ForMember(mfbvm => mfbvm.Manufacturer, member => member.MapFrom(b => b.Manufacturer.ToString()))
                .ForMember(mfbvm => mfbvm.Miner, member => member.MapFrom(b => b.Miner.AppUser.UserName));

                m.CreateMap<Beer, MyHiddenBeerViewModel>()
                .ForMember(mfbvm => mfbvm.Manufacturer, member => member.MapFrom(b => b.Manufacturer.ToString()))
                .ForMember(mfbvm => mfbvm.Founder, member => member.MapFrom(b => b.Founder.AppUser.UserName));

                m.CreateMap<ApplicationUser, UserViewModel>();

            });
        }

    }
}
