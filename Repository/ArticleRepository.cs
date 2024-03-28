using TheComputerShop.DATA;
using TheComputerShop.Models;
using TheComputerShop.Repository.IRepository;

namespace TheComputerShop.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateArticle(Articles article)
        {
            _db.Articles.Add(article);

            return Save();
        }

        public bool DeleteArticle(Articles articles)
        {
            _db.Articles.Remove(articles);
            return Save();
        }

        public bool ExistArticle(string name)
        {
            bool exists = _db.Articles.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return exists;
        }

        public bool ExistArticleId(int id)
        {
            return _db.Articles.Any(a => a.Id.Equals(id));
        }

        public ICollection<Articles> GetAllArticles()
        {
            return _db.Articles.OrderBy(a => a.Name).ToList();
        }

        public Articles GetArticles(int id)
        {
            return _db.Articles.FirstOrDefault(a => a.Id.Equals(id));
        }

        public ICollection<Articles> SearchArticle(string name)
        {
            IQueryable<Articles> query = _db.Articles;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }

            return query.ToList();
        }

        public bool UpdateArticle(Articles articles)
        {
            _db.Articles.Update(articles);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
