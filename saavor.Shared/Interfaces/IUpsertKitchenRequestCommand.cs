using saavor.Shared.DTO.Kitchen;
using saavor.Shared.ViewModel;
using System.Threading.Tasks;

namespace saavor.Shared.Interfaces
{
    public interface IUpsertKitchenRequestCommand
    {
        Task<CommonVm> KitchenRequest(KitchenRequestDTO inputDTO);
    }
}
