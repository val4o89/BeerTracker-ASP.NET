namespace BeerTracker.Services
{
    using AutoMapper;
    using Models.BindingModels.Partner;
    using Models.DataModels;
    using Models.ViewModels.Admin;
    using Models.ViewModels.Geo;
    using Models.ViewModels.Partner;
    using Models.ViewModels.User;
    using UnitOfWork.Contracts;

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
                .ForMember(mfbvm => mfbvm.Miner, member => member.MapFrom(b => b.Hider.AppUser.UserName));

                m.CreateMap<Beer, MyHiddenBeerViewModel>()
                .ForMember(mfbvm => mfbvm.Manufacturer, member => member.MapFrom(b => b.Manufacturer.ToString()))
                .ForMember(mfbvm => mfbvm.Founder, member => member.MapFrom(b => b.Founder.AppUser.UserName));

                m.CreateMap<ApplicationUser, UserViewModel>();

                m.CreateMap<Beer, ManageBeerViewModel>()
                .ForMember(mbvm => mbvm.Manufacturer, member => member.MapFrom(b => b.Manufacturer.ToString()))
                .ForMember(mbvm => mbvm.HidersUsername, member => member.MapFrom(b => b.Hider.AppUser.UserName));

                m.CreateMap<AddContestBindingModel, Contest>();

                m.CreateMap<Contest, ContestViewModel>();

                m.CreateMap<Contest, ManageContestBindingModel>();
            });
        }

    }
}
