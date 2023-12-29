using addCard_backend.DTOs;
using addCard_backend.Models;
using AutoMapper;

namespace addCard_backend.AutoMapper
{
	public class AutoMapperProfiles:Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<CreditCardDTO, CreditCard>().ReverseMap();
            CreateMap<CreditCardIdDTO, CreditCard>().ReverseMap();
        }
	}
}

