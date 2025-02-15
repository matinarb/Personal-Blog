using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.DataLayer;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDelete { get; set; }
}