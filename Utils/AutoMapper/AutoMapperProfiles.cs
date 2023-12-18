using AutoMapper;
using POS_service_address.DTOs;
using POS_service_contact.DTOs;
using POS_service_customers.DTOs;
using POS_service_customers.Models;

namespace POS_service_customers.AutoMapper
{
	public class AutoMapperProfiles:Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<CustomerDTO, Customer>().ReverseMap();
            CreateMap<CustomerIdDTO, Customer>().ReverseMap();

            CreateMap<AddressDTO, Address>().ReverseMap();
            CreateMap<AddressIdDTO, Address>().ReverseMap();

            CreateMap<ContactDTO, Contact>().ReverseMap();
            CreateMap<ContactIdDTO, Contact>().ReverseMap();
        }
	}
}

