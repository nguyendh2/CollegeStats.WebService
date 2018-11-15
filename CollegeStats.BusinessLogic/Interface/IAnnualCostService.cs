

namespace CollegeStats.BusinessLogic.Interface
{
    public interface IAnnualCostService
    {
        bool HasCollege(string collegeName);
        double GetAnnualCost(string collegeName, bool includeRoom, bool isOutOfState);
    }
}
