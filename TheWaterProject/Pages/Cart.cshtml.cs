using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheWaterProject.Infrastructure;
using TheWaterProject.Models;

namespace TheWaterProject.Pages
{
    public class CartModel : PageModel
    {
        private IWaterRepository _repo;

        public CartModel(IWaterRepository repo)
        {
            _repo = repo;
        }
        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet()
        {
            ReturnUrl = ReturnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public void OnPost(int projectId, string returnUrl)
        {
            Project proj = _repo.Projects.FirstOrDefault(x => x.ProjectId == projectId);

            if (proj != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(proj, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage(new {returnUrl = returnUrl});
        }
    }
}
