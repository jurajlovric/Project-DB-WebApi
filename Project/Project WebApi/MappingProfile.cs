using AutoMapper;
using Project.Model;
using Project.WebApi.Controllers;

namespace Project.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerRest>().ReverseMap();
            CreateMap<Order, OrderRest>().ReverseMap();

            CreateMap<Customer, CustomersController.GetCustomerRest>().ReverseMap();
            CreateMap<Customer, CustomersController.PostCustomerRest>().ReverseMap();
            CreateMap<Customer, CustomersController.PutCustomerRest>().ReverseMap();
            CreateMap<Customer, CustomersController.DeleteCustomerRest>().ReverseMap();

            CreateMap<Order, OrdersController.GetOrderRest>().ReverseMap();
        }
    }
}
