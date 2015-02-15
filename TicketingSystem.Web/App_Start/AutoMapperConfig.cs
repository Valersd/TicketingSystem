using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using TicketingSystem.Models;
using TicketingSystem.Web.ViewModels;
using TicketingSystem.Web.Models;

namespace TicketingSystem.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Ticket, TicketIndex>()
                .ForMember(t => t.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(t => t.Author, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(t => t.CommentsCount, opt => opt.MapFrom(s => s.Comments.Count));

            Mapper.CreateMap<Comment, CommentInTicket>()
                .ForMember(c => c.Author, opt => opt.MapFrom(s => s.Author.UserName));

            Mapper.CreateMap<Ticket, TicketDetails>()
                .ForMember(t => t.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(t => t.Author, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(t => t.Priority, opt => opt.MapFrom(s => s.Priority))
                .ForMember(t=>t.Comments,opt => opt.MapFrom(s=>s.Comments));

            Mapper.CreateMap<Ticket, TicketInTicketsIndex>()
                .ForMember(t => t.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(t => t.AuthorName, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(t => t.Priority, opt => opt.MapFrom(s => s.Priority));

            Mapper.CreateMap<TicketInput, Ticket>();
        }
    }
}