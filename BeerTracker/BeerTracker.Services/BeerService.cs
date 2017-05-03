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
    using UnitOfWork.Contracts;

    public class BeerService : BaseService, IBeerService
    {
        public BeerService(IUnitOfWork db) : base(db)
        {
        }

        public IEnumerable<BeerLocationViewModel> GetLocations()
        {
            IEnumerable<Location> locations = this.db.Locations.FindMany(
                l => l.Beer.IsFound == false 
                && l.Beer.IsDeleted == false 
                && l.Beer.Hider != null);

            var models = this.mapper.Map<IEnumerable<Location>, IEnumerable<BeerLocationViewModel>>(locations);

            return models;
        }

        public bool HideBeer(HideFindBeerBindingModel model, string name)
        {
            var user = this.db.RegularUsers.FindFirst(ru => ru.AppUser.UserName == name);

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
                    Hider = user,
                    Manufacturer = (BeerMake)Enum.Parse(typeof(BeerMake), model.Manufacturer)

                }
            });

            try
            {
                this.db.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                return false;
            }

            return true;
        }

        public bool FindBeer(HideFindBeerBindingModel model, string username)
        {
            var loggedUser = this.db.RegularUsers.FindFirst(u => u.AppUser.UserName == username);
            var appUser = loggedUser.AppUser;

            var foundBeer = this.db.Beers.FindFirst(b => b.IsFound == false && 
            b.EndOfSerialNumber == model.EndOfSerialNumber &&
            b.Manufacturer.ToString() == model.Manufacturer);

            if (foundBeer != null)
            {
                if (foundBeer.Hider == loggedUser)
                {
                    return false;
                }

                var distance = this.GetDistanceDifference(foundBeer.Location.Latitude, foundBeer.Location.Longitude, 
                               model.Latitude, model.Longitude);

                if (IfDistanceIsValid(distance, 0.3))
                {
                    foundBeer.IsFound = true;
                    foundBeer.Founder = loggedUser;
                    loggedUser.Points++;

                    try
                    {
                        this.db.SaveChanges();
                    }
                    catch (DbEntityValidationException)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
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

        public IEnumerable<BeerLocationViewModel> GetBeersOfContest(int id)
        {
            IEnumerable<Location> locations = this.db.Contests.FindFirst(c => c.Id == id)
                .Beers.Where(b => b.IsFound == false && b.IsDeleted == false).Select(b => b.Location);

            return this.mapper.Map<IEnumerable<Location>, IEnumerable<BeerLocationViewModel>>(locations);
        }

        public string GetUserIdByUsername(string name)
        {
            return this.db.AppUsers.FindFirst(u => u.UserName == name).Id;
        }

        public bool FindContestBeer(string userId, HideFindBeerBindingModel model)
        {
            var loggedUser = this.db.RegularUsers.FindFirst(u => u.AppUserId == userId);

            var appUser = loggedUser.AppUser;

            var foundBeer = this.db.Beers.FindFirst(b => b.IsFound == false &&
            b.EndOfSerialNumber == model.EndOfSerialNumber &&
            b.Manufacturer.ToString() == model.Manufacturer);

            if (foundBeer != null)
            {
                var distance = this.GetDistanceDifference(foundBeer.Location.Latitude, foundBeer.Location.Longitude,
                               model.Latitude, model.Longitude);

                if (IfDistanceIsValid(distance, 0.3))
                {
                    foundBeer.IsFound = true;
                    foundBeer.Founder = loggedUser;
                    loggedUser.Contests.FirstOrDefault(c => c.ContestId == model.ContestId).UserScores++;
                    loggedUser.Points++;

                    try
                    {
                        this.db.SaveChanges();
                    }
                    catch (DbEntityValidationException)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
