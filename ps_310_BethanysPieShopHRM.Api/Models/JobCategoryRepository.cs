using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// 05/10/2021 03:37 am - SSN - [20210510-0323] - [008] - M03-03 - Demo: Exploring the API
// using BethanysPieShopHRM.Shared;
using ps_310_BethantysPieShopHRM.Shared;


namespace BethanysPieShopHRM.Api.Models
{
    public class JobCategoryRepository: IJobCategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public JobCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<JobCategory> GetAllJobCategories()
        { 
            return _appDbContext.JobCategories.OrderBy(r=>r.JobCategoryName);
        }

        public JobCategory GetJobCategoryById(int jobCategoryId)
        {
            return _appDbContext.JobCategories.FirstOrDefault(c => c.JobCategoryId == jobCategoryId);
        }
    }
}
