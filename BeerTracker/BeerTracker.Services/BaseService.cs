namespace BeerTracker.Services
{
    using AutoMapper;
    using Data;
    using Models.DataModels;
    using Models.ViewModels.Geo;
    using System;

    public class BaseService
    {
        protected ApplicationDbContext db;
        protected IMapper mapper;
        protected MapperConfiguration configuration;

        public BaseService()
        {
            this.db = new ApplicationDbContext();
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
            });
        }
    }
}
