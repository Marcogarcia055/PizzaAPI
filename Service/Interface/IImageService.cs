using Pizzeria.Dto;
using Pizzeria.Models;

namespace Pizzeria.Service.Interface
{
    public interface IImageService
{
    Task<string> SaveImageAsync(IFormFile file);
}
}