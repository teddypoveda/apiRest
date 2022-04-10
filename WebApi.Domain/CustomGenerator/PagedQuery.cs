using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Domain.CustomGenerator
{
    public class PagedQuery
    {
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}