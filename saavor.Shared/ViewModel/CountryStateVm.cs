using saavor.Shared.DTO.Country;
using saavor.Shared.DTO.State;
using System.Collections.Generic;

namespace saavor.Shared.ViewModel
{
   public class CountryStateVm
    {
        public List<CountryDTO> Countries { get; set; }
        public List<StateDTO> States { get; set; }
    }
}
