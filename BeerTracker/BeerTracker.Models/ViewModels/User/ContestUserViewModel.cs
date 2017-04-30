namespace BeerTracker.Models.ViewModels.User
{
    using System;

    public class ContestUserViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsParticipant { get; set; }
    }
}
