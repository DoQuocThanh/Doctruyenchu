using ApplicationCore.Entities;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IStoryService
    {
        Task<StoryDetailViewModel> GetInformationStoryDetail(int id, PageViewModel page);

    }
}
