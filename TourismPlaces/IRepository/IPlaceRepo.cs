using TourismPlaces.Data;
using TourismPlaces.Data.Models;
using TourismPlaces.ViewModels;


namespace TourismPlaces.IRepository
{
    public interface IPlaceRepo
    {

        Task CreatPlace(PlacesCreateViewModel placeView);
        Task<IEnumerable<Government>> GetGovernmetsAsync();

        Task<Place> GetPlaceByIdAsync(int placeId);

        IEnumerable<PlacesRetrieveViewModel> GetAllPlaces();
        IEnumerable<PlacesRetrieveViewModelForUser> GetAllPlacesForUser();

        IEnumerable<PhotoViewModel> GetPhotoByPlaceId(int placeId);

        Task GoPlaceLiveAsync(int placeId);

       Task AddMoreImagesAsync(int placeId, List<IFormFile> MorePhotos);

        Task<bool> RemovePhotoAsync(List<PhotoSelectViewModel> MorePhotos);

        Task UpdatePlaceAsync(EditPlaceViewwModel model, int PlaceId);
        
    }
}
