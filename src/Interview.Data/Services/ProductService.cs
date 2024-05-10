using AutoMapper;
using Interview.Data.Contexts;
using Interview.Data.Entities;
using Interview.Data.Models;
using Interview.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly InterviewContext _context;
        private readonly IMapper _mapper;

        public ProductService(InterviewContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            if (id == null || id == 0)
            {
                return null;
            }

            var productEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productEntity is not null)
            {
                return _mapper.Map<Product>(productEntity);
            }

            return null;
        }

        public async Task AddOrUpdateProductAsync(Product product)
        {
            var existingProductEntity = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if (existingProductEntity is not null)
            {
                existingProductEntity.Name = product.Name;
                existingProductEntity.Description = product.Description;
            }
            else
            {
                var productEntity = _mapper.Map<ProductEntity>(product);

                await _context.Products.AddAsync(productEntity);
            }

            await _context.SaveChangesAsync();
        }

    }
}
