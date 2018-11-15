using CollegeStats.DataAccess.DataTransfer;
using System.Collections.Generic;

namespace CollegeStats.DataAccess.Interface
{
    public interface IAnnualCostDao
    {
        AnnualCostDto GetAnnualCost(string collegeName);
        IList<AnnualCostDto> GetAll();
    }
}
