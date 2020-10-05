using System;
using AutoMapper;

using InLife.Store.Core.Models;

using InLife.Store.Api.Messages;

namespace InLife.Store.Api
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//CreateMap<Quote, QuoteRequest>()
			//	.ForMember(dest => dest.NamePrefix, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.NameSuffix, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.MiddleName, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.EmailAddress, opts => opts.MapFrom(src => src.Customer.NamePrefix))
			//	.ForMember(dest => dest.MobileNumber, opts => opts.MapFrom(src => src.Customer.NamePrefix));

		}
	}
}
