using System.ComponentModel.DataAnnotations;

namespace BookingRevamp.Models
{
    public enum AmenityCategory
    {
        [Display(Name = "Основні зручності")]
        Basic = 1,

        [Display(Name = "Спальня та зберіганя")]
        Bedroom = 2,

        [Display(Name = "Кухня та харчування")]
        Kitchen = 3,

        [Display(Name = "Ванна кімната та прання")]
        Bathroom = 4,

        [Display(Name = "Територія та вигляд")]
        Territory = 5,

        [Display(Name = "Для дітей")]
        Kids = 6,

        [Display(Name = "Додаткові можливості")]
        Bonus = 7,

        [Display(Name = "Безпека")]
        Safety = 8,
    }
}
