using TheComputerShop.Models;

namespace TheComputerShop.Repository.IRepository
{
    public interface IArticleRepository
    {
        Articles GetArticles(int id); 
        bool CreateArticle(Articles articles);
        bool ExistArticle(string name);
        bool ExistArticleId(int id);
        bool UpdateArticle(Articles articles);
        ICollection<Articles> GetAllArticles();
        ICollection<Articles> SearchArticle(string name);
        bool DeleteArticle(Articles articles);
        bool Save();
    }
}
