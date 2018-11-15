using CollegeStats.DataAccess.DataTransfer;
using CollegeStats.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.VisualBasic.FileIO;

namespace CollegeStats.DataAccess.Dao
{
    public class AnnualCostDao : IAnnualCostDao
    {
        private const string FilePath = @"C:\college_costs.csv";
        private IList<AnnualCostDto> ListOfAnnualCosts = new List<AnnualCostDto>();
        private bool ListOfAnnualCostsInitialized = false;
        private object ListOfAnnualCostsLock = new object();

        public AnnualCostDao()
        {

        }

        public IList<AnnualCostDto> GetAll()
        {
            LazyInitializer.EnsureInitialized(ref ListOfAnnualCosts,ref ListOfAnnualCostsInitialized,ref ListOfAnnualCostsLock,PopulateListOfAnnualCosts);
            return ListOfAnnualCosts;
        }

        public IList<AnnualCostDto> PopulateListOfAnnualCosts()
        {
            var result = new List<AnnualCostDto>();
            if (File.Exists(FilePath) == false)
                throw new System.Exception("Error - source .csv file not found.");
            using (var streamReader = new StreamReader(FilePath))
            {
                using (var parser = new TextFieldParser(streamReader))
                {
                    parser.HasFieldsEnclosedInQuotes = true;
                    parser.SetDelimiters(",");
                    var lineCount = 0;
                    while (!parser.EndOfData)
                    {
                        lineCount++;
                        var splitValues = parser.ReadFields();
                        if(lineCount > 1 && splitValues != null){//if not blank line && not header line
                            if (splitValues.Length == 4)
                            {
                                var itemToAdd = new AnnualCostDto();
                                itemToAdd.CollegeName = splitValues[0].Trim();
                                itemToAdd.Tuition = string.IsNullOrWhiteSpace(splitValues[1]) ? null : (double?)double.Parse(splitValues[1]);
                                itemToAdd.TuitionOutOfState = string.IsNullOrWhiteSpace(splitValues[2]) ? null : (double?)double.Parse(splitValues[2]);
                                itemToAdd.Room = string.IsNullOrWhiteSpace(splitValues[3]) ? null : (double?)double.Parse(splitValues[3]);
                                result.Add(itemToAdd);
                            }
                            else
                            {
                                throw new System.Exception($"Error - incorrect number of values found on line {lineCount} in the .csv file.");
                            }
                        }
                        
                    }
                }
            }

            return result;
        }

        public AnnualCostDto GetAnnualCost(string collegeName)
        {
            if (string.IsNullOrWhiteSpace(collegeName))
                return null;
            collegeName = collegeName.Trim();

            var all = GetAll();
            if (all.Count == 0)
                return null;

            var result = all.Where(x => x.CollegeName.Equals(collegeName,StringComparison.OrdinalIgnoreCase)).ToList();
            if (result.Count > 1)
            {
                throw new Exception($"Error - duplicate records found for the same collegeName.  collegeName = {collegeName}");
            }
            else if (result.Count == 1)
            {
                return result.First();
            }
            return null;
        }
    }
}
