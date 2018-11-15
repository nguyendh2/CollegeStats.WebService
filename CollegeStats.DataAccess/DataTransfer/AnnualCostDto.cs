

namespace CollegeStats.DataAccess.DataTransfer
{
    public class AnnualCostDto
    {
        public string CollegeName { get; set; }
        public double? Tuition { get; set; }
        public double? TuitionOutOfState { get; set; }
        public double? Room { get; set; }
    }
}
