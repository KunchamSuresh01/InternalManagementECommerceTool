using InternalManagementECommerceTool.Models;

namespace InternalManagementECommerceTool.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<SliderImages> SliderImages { get; set; }
        public List<Category> Categories { get; set; }
    }
}
