using System;
using System.ComponentModel.DataAnnotations;


namespace PersonalBlog.DataLayer.Entities
{
    public class Category : BaseEntity
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public string MetaTag { get; set; }
        public string MetaDescription { get; set; }


        #region relations
        public ICollection<Post> posts { get; set; }
        #endregion


    }
}


