using System.ComponentModel.DataAnnotations;

namespace WebApi.Domain.Base
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
    }
}