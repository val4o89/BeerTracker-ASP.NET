namespace BeerTracker.Services
{
    using AutoMapper;
    using Models.BindingModels.Geo;
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
                .ForMember(mfbvm => mfbvm.Hider, member => member.MapFrom(b => b.Hider != null ?b.Hider.AppUser.UserName : b.Contest.Title + " Contest"));

                m.CreateMap<Beer, MyHiddenBeerViewModel>()
                .ForMember(mfbvm => mfbvm.Manufacturer, member => member.MapFrom(b => b.Manufacturer.ToString()))
                .ForMember(mfbvm => mfbvm.Founder, member => member.MapFrom(b => b.Founder.AppUser.UserName));

                m.CreateMap<User, UserViewModel>();

                m.CreateMap<Beer, ManageBeerViewModel>()
                .ForMember(mbvm => mbvm.Manufacturer, member => member.MapFrom(b => b.Manufacturer.ToString()))
                .ForMember(mbvm => mbvm.HidersUsername, member => member.MapFrom(b => b.Hider.AppUser.UserName));

                m.CreateMap<AddContestBindingModel, Contest>();

                m.CreateMap<Contest, ContestViewModel>();

                m.CreateMap<Contest, ContestUserViewModel>().ForMember(cuwm => cuwm.Description, member => member
                .MapFrom(c => c.Description.Substring(0, c.Description.Length < 150 ? c.Description.Length : 150) + "..."));

                m.CreateMap<Contest, ManageContestBindingModel>();

                m.CreateMap<Beer, ContestBeerViewModel>()
                .ForMember(cbvm => cbvm.Manufacturer, member => member.MapFrom(b => b.Manufacturer.ToString()));

                m.CreateMap<ContestRegularUser, UserRankViewModel>()
                .ForMember(urvm => urvm.UserScores, member => member.MapFrom(cr => cr.UserScores))
                .ForMember(urvm => urvm.Username, member => member.MapFrom(cr => cr.RegularUser.AppUser.UserName));

            });
        }

    }
}
