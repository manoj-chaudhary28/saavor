using saavor.Shared.DTO.SignUp;
using saavor.Shared.ViewModel;
using System;
using System.Threading.Tasks;

namespace saavor.Shared.Interfaces.SignUp
{
    public interface IUpsertSignupCommand
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        Task<LoginVm> Create(SignupInputDTO inputDTO);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="inputDTO"></param>
        /// <returns></returns>
        Task<LoginVm> Update(SignupUpdateInputDTO inputDTO);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserVm> Get(Int64 userId);
    }
}
