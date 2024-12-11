using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IIndexService
    {
        Task<IndexViewModel> GetInformationIndex();
        Task<IndexFilterViewModel> GetInformationIndexFilter(FilterViewModel filterViewModel);

    }
}
