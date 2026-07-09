using BookingRevamp.Models;

namespace BookingRevamp.ViewModels
{
    public class FavoritesViewModel
    {
        public List<Property> Properties { get; set; } = new();

        public HashSet<int> FavoritePropertyIds { get; set; } = new();
    }
}
