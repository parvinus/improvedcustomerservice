﻿using System;
using AutoMapper;
using ImprovedCustomerService.Data.Dto;
using ImprovedCustomerService.Data.Dto.Contacts;
using ImprovedCustomerService.Data.Model;

namespace ImprovedCustomerService.Data.Configuration
{
    public static class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Customer, CustomerResponseDto>();
                config.CreateMap<CustomerResponseDto, Customer>();

                config.CreateMap<CustomerSaveDto, Customer>()
                    .ForMember(c => c.Address_Id, opt => opt.MapFrom(a => a.AddressId))
                    .ForMember(c => c.CreatedOn, opt => opt.UseValue(DateTime.UtcNow));
                config.CreateMap<Customer, CustomerSaveDto>()
                    .ForMember(csd => csd.AddressId, opt => opt.MapFrom(c => c.Address_Id));

                config.CreateMap<AddressDto, Address>();
                config.CreateMap<Address, AddressDto>();

                config.CreateMap<Contact, ContactSaveDto>();
                config.CreateMap<ContactSaveDto, Contact>();

                config.CreateMap<Contact, ContactResponseDto>();
                config.CreateMap<ContactResponseDto, Contact>();
            });
        }
    }
}
