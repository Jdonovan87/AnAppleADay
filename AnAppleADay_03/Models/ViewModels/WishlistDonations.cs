using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnAppleADay_03.Models.ViewModels
{
    public class WishlistDonations
    {
        public int listId { get; set; }

        public string itemName { get; set; }

        [DataType(DataType.MultilineText)]
        public string itemAbout { get; set; }

        [DataType(DataType.Currency)]
        public decimal cost { get; set; }

        [DataType(DataType.Currency)]
        public decimal current { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime expDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal donation { get; set; }

        public virtual Teacher Teacher { get; set; }

        public WishlistDonations() { }

        public WishlistDonations(TeachersWishlist list)
        {
            this.listId = list.listId;
            this.itemName = list.itemName;
            this.itemAbout = list.itemAbout;
            this.cost = list.cost;
            this.current = list.current;
            this.expDate = list.expDate;
            this.Teacher = list.Teacher;
            this.donation = 0;
        }
    }

}
    