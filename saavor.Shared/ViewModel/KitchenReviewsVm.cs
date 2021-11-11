using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace saavor.Shared.ViewModel
{
    public class KitchenReviewsVm
    {
        [Key]
        public Int64 ReviewId { get; set; }
        public int Stars { get; set; }
        public string ReviewMessage { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string CreateDate { get; set; }
        public string AverageRating { get; set; }
        public int TotalRecord { get; set; }
        public Int32 UserId { get; set; }
        public string ProfileImgPath { get; set; }
    } 
    public class KitchenReviewsModel
    {
        public Int64 ReviewId { get; set; }
        public int Stars { get; set; }
        public string ReviewMessage { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string CreateDate { get; set; }
        public string AverageRating { get; set; }
        public string ProfileImg { get; set; }
        public Int32 UserId { get; set; }
        public List<KitchenReviewsReplyModel> ReplyReviewList { get; set; }
    }
    public class KitchenReviewsReplyModel
    {
        [Key]
        public Int64 ReviewReplyId { get; set; }
        public string ReplyMessage { get; set; }
        public string CreateDate { get; set; }
         
    }
}
