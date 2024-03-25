using Microsoft.AspNetCore.Mvc;
using TheWaterProject.Models;

namespace TheWaterProject.Components
{
    public class ProjectTypesViewComponent : ViewComponent
    {
        private IWaterRepository _repo;
        
        //constructor
        public ProjectTypesViewComponent(IWaterRepository temp) {
            _repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedProjectType = RouteData?.Values["projectType"];

            var projectTypes = _repo.Projects
                .Select(x => x.ProjectType)
                .Distinct()
                .OrderBy(x => x);

            return View(projectTypes);
        }
    }
}
