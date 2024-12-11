namespace WebMVC.Models
{
    public class FilterViewModel
    {
        public string? Title { get; set; }
        public string? GenresSlected { get; set; }
        public int? PageIndex { get; set; } // Tổng số lượng truyện
        public int? PageSize { get; set; }  // Trang hiện tại

    }
}
