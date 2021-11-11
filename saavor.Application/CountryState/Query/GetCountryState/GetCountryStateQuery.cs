using Microsoft.EntityFrameworkCore;
using saavor.EntityFrameworkCore.Context;
using saavor.Shared.DTO.Country;
using saavor.Shared.DTO.State;
using saavor.Shared.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saavor.Application.CountryState.Query.GetCountryState
{
    public class GetCountryStateQuery
    {
        private readonly ApplicationDBContext context;
        public GetCountryStateQuery(ApplicationDBContext dbContextInstance)
        {
            context = dbContextInstance;
        }
        /// <summary>
        /// Get Country and State
        /// </summary>
        /// <returns></returns>
        public CountryStateVm GetCountryState(int countryId)
        {
            var countries = (from country in context.ScCountries
                             orderby country.CountryName ascending
                                   select new CountryDTO
                                   {
                                       Id = country.CountryId,
                                       Name = country.CountryName

                                   }
                  ).ToList();
            countries.Insert(0, new CountryDTO { Id = 0, Name = " Select Country" });

            var states =  (from state in context.ScStates
                   .Where(x => x.CountryId == (countryId == 0 ? 231 : countryId))
                                select new StateDTO
                                {
                                    Id = state.StateId,
                                    Name = state.StateName,
                                    CountryId = state.CountryId
                                }
                 ).ToList();
            states.Insert(0, new StateDTO { Id = 0, Name = " Select State" });
            return new CountryStateVm
            {
                Countries = countries,
                States = states
            };


        }

        /// <summary>
        /// Bind state using country id
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public List<StateDTO> GetState(int countryId)
        {         
            var states = (from state in context.ScStates
                  .Where(x => x.CountryId == countryId)
                          select new StateDTO
                          {
                              Id = state.StateId,
                              Name = state.StateName,
                              CountryId = state.CountryId
                          }
                 ).ToList();
            states.Insert(0, new StateDTO { Id = 0, Name = " Select State" });
            return states;


        }
    }
}