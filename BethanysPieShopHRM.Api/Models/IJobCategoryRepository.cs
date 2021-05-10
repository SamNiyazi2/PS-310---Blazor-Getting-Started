using System.Collections.Generic;


// 05/10/2021 03:37 am - SSN - [20210510-0323] - [007] - M03-03 - Demo: Exploring the API
// using BethanysPieShopHRM.Shared;
using ps_310_BethantysPieShopHRM.Shared;


namespace BethanysPieShopHRM.Api.Models
{
    public interface IJobCategoryRepository
    {
        IEnumerable<JobCategory> GetAllJobCategories();
        JobCategory GetJobCategoryById(int jobCategoryId);
    }
}
