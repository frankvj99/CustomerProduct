using AutoMapper;
using Interview.Data.Entities;
using Interview.Data.Models;

namespace Interview.Data.AutoMapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerEntity>();
        CreateMap<CustomerEntity, Customer>();
    }
}
