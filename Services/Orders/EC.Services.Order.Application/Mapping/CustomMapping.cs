﻿using AutoMapper;
using EC.Services.Order.Application.Commands;
using EC.Services.Order.Application.Dtos;
using EC.Services.Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.Order.Application.Mapping
{
    public class CustomMapping:Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<CreateOrderCommand, OrderCreateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();


        }

    }
}