using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IReviewRepository
    {
        public ReviewModel AddReview(ReviewModel model);
        public List<ReviewsList> GetReviews(int bookid);
    }
}
