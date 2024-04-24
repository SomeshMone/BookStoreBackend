using Businesslayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslayer.Services
{
    public class ReviewBusiness:IReviewBusiness
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewBusiness(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public ReviewModel AddReview(ReviewModel model)
        {
            return _reviewRepository.AddReview(model);
        }
        public List<ReviewsList> GetReviews(int bookid)
        {
            return _reviewRepository.GetReviews(bookid);
        }
    }
}
