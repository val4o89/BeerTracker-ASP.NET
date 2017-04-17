namespace BeerTracker.Services
{
    using Contracts;
    using Models.DataModels;
    using Models.ViewModels.Geo;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Models.BindingModels.Geo;
    using System.Linq;
    using Models.DataModels.Enums;
    using System.Data.Entity.Validation;

    public class BeerService : BaseService, IBeerService
    {
        public BeerService() : base()
        {
        }

        public IEnumerable<BeerLocationViewModel> GetLocations()
        {
            IEnumerable<Location> locations = this.db.Locations.Where(l => l.Beer.IsFound == false);

            var loc = new Location();

            var models = this.mapper.Map<IEnumerable<Location>, IEnumerable<BeerLocationViewModel>>(locations);

            return models;
        }

        public void HideBeer(HideFindBeerBindingModel model, string name)
        {
            var user = this.db.RegularUsers.FirstOrDefault(ru => ru.AppUser.UserName == name);

            user.Points++;

            //Code porn
            user.AppUser = user.AppUser;

            db.Locations.Add(new Location
            {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Beer = new Beer
                {
                    EndOfSerialNumber = model.EndOfSerialNumber,
                    Miner = user,
                    Manufacturer = (BeerMake)Enum.Parse(typeof(BeerMake), model.Manufacturer)

                }
            });

            this.db.SaveChanges();
        }

        public void FindBeer(HideFindBeerBindingModel model, string username)
        {
            var loggedUser = this.db.RegularUsers.FirstOrDefault(u => u.AppUser.UserName == username);

            var foundBeer = this.db.Beers.FirstOrDefault(b => b.IsFound == false && 
            b.EndOfSerialNumber == model.EndOfSerialNumber &&
            b.Manufacturer.ToString() == model.Manufacturer);

            if (foundBeer != null)
            {
                if (foundBeer.Miner == loggedUser)
                {
                    return;
                }

                var distance = this.GetDistanceDifference(foundBeer.Location.Latitude, foundBeer.Location.Longitude, 
                                                                model.Latitude, model.Longitude);
                if (IfDistanceIsValid(distance, 0.3))
                {
                    foundBeer.IsFound = true;
                    foundBeer.Founder = loggedUser;
                    loggedUser.Points++;

                    //Code porn
                    loggedUser.AppUser = loggedUser.AppUser;

                    this.db.SaveChanges();
                }

            }
        }

        private double GetDistanceDifference(double firstPointLat, double firstPointLong, double secondPointLat, double secondPointLong)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(firstPointLat - secondPointLat), 2) +
                                    Math.Pow(Math.Abs(firstPointLong - secondPointLong), 2)) * 100;
        }

        private bool IfDistanceIsValid(double distance, double allowedDistance)
        {
            if (distance <= allowedDistance)
            {
                return true;
            }

            return false;
        }
    }
}
