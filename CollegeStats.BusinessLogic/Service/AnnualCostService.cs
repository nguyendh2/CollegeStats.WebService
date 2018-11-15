using CollegeStats.BusinessLogic.Interface;
using CollegeStats.DataAccess.Interface;
using System;

namespace CollegeStats.BusinessLogic.Service
{
    public class AnnualCostService : IAnnualCostService
    {
        private IAnnualCostDao _dao;
        public AnnualCostService(IAnnualCostDao dao)
        {
            _dao = dao;
        }

        public double GetAnnualCost(string collegeName, bool includeRoom, bool isOutOfState)
        {
            if (HasCollege(collegeName))
            {
                var result = _dao.GetAnnualCost(collegeName);

                if (includeRoom && result.Room.HasValue == false)
                {
                    throw new Exception($"Error - Room/Boarding cost not found. College Name = '{collegeName}'.");
                }

                if (isOutOfState && result.TuitionOutOfState.HasValue == false)
                {
                    throw new Exception($"Error - Tuition Out of State not found. College Name = '{collegeName}'.");
                }

                if (isOutOfState == false && result.Tuition.HasValue == false) {
                    throw new Exception($"Error - Tuition In State not found. College Name = '{collegeName}'.");
                }

                var tuition = isOutOfState ? result.TuitionOutOfState.Value : result.Tuition.Value;
                var room = includeRoom ? result.Room.Value : 0;
                var totalCost = tuition + room;

                return totalCost;
            }
            return -1;
        }

        public bool HasCollege(string collegeName)
        {
            var result = _dao.GetAnnualCost(collegeName);
            return result != null;
        }
        
    }
}
