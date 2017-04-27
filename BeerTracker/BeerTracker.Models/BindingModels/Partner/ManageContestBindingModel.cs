namespace BeerTracker.Models.BindingModels.Partner
{
    using System;

    public class ManageContestBindingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

    }
}
