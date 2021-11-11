using saavor.Shared.DTO.Login;
using saavor.Shared.ViewModel;
using System;
using System.Threading.Tasks;

namespace saavor.Shared.Interfaces.SignUp
{
    public interface IGetLoginQuery
    {
        Task<LoginVm> Login(LoginInputDTO inputDTO);
        Task<CommonVm> GetRequestStatus(Int64 inputDTO);
        Task<CommonVm> CheckUser(string emailAddress);
        Task<CommonVm> ForgotPassword(ForgotPasswordDTO inputDTO);
        Task<string> SessionToken(Int64 UserKitchenId);
    }
}
