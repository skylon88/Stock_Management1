using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Priority
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
