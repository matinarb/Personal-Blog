using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonalBlog.DataLayer.Entities
{
    public class Category : BaseEntity
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public string? MetaTag { get; set; }
        public string? MetaDescription { get; set; }
        


        #region relations
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category? Parent { get; set; }
        public ICollection<Post> posts { get; set; }
        #endregion


    }
}


