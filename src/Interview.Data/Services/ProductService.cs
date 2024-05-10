using AutoMapper;
using Interview.Data.Contexts;
using Interview.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Data.Services
{
    public class ProductService
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
    }
}
