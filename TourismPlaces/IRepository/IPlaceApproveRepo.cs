using TourismPlaces.ViewModels.Admin;

namespace TourismPlaces.IRepository
{
    public interface IPlaceApproveRepo
    {
       IEnumerable<AdminApprovePlaceViewModel> GetApprovedPlaces();
    }
}
