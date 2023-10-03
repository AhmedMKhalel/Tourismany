using TourismPlaces.Data;
using TourismPlaces.ViewModels;

public class PlacesRetrieveViewModelForUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal EntrancePrice { get; set; }
    public decimal Rate { get; set; }
    public string Details { get; set; }
    public string GovernmentName { get; set; }
    public string MainPhoto { get; set; }
    public ICollection<PhotoViewModel> Photos { get; set; }
    public string UserId { get; set; }


    public PlacesRetrieveViewModelForUser(Place place, string userId)
    {
        Id = place.Id;
        Name = place.Name;
        Address = place.Address;
        EntrancePrice = place.EntrancePrice;
        Rate = place.rate;
        Details = place.Details;
        GovernmentName = place.Government.Name;
        MainPhoto = place.Photos.FirstOrDefault(ph => ph.IsMain)?.PhotoPath;
        Photos = place.Photos.Select(ph => new PhotoViewModel
        {
            Id = ph.Id,
            PhotoPath = ph.PhotoPath,
            IsMain = ph.IsMain
        }).ToList();
        UserId = userId;
    }
}