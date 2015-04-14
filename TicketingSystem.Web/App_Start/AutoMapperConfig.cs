using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using TicketingSystem.Models;
using TicketingSystem.Web.ViewModels;
using TicketingSystem.Web.Models;
using TicketingSystem.Web.Areas.Admin.ViewModels;
using TicketingSystem.Web.Areas.Admin.Models;

namespace TicketingSystem.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            #region Ticket
            Mapper.CreateMap<Ticket, TicketIndex>()
                .ForMember(t => t.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(t => t.Author, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(t => t.CommentsCount, opt => opt.MapFrom(s => s.Comments.Count));

            Mapper.CreateMap<Ticket, TicketDetails>()
                .ForMember(t => t.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(t => t.Author, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(t => t.Priority, opt => opt.MapFrom(s => s.Priority))
                .ForMember(t => t.Comments, opt => opt.MapFrom(s => s.Comments));

            Mapper.CreateMap<Ticket, TicketInTicketsIndex>()
                .ForMember(t => t.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(t => t.AuthorName, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(t => t.Priority, opt => opt.MapFrom(s => s.Priority));

            Mapper.CreateMap<Ticket, TicketInUser>()
                .ForMember(t => t.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(t => t.Priority, opt => opt.MapFrom(s => s.Priority));

            Mapper.CreateMap<TicketInput, Ticket>();
            #endregion

            #region Category

            Mapper.CreateMap<Category, CategoryIndex>()
                .ForMember(c => c.TicketsCount, opt => opt.MapFrom(s => s.Tickets.Count));

            Mapper.CreateMap<CategoryInput, Category>();

            Mapper.CreateMap<Category, CategoryEdit>();

            Mapper.CreateMap<CategoryEdit, Category>();
            #endregion

            #region Comment
            Mapper.CreateMap<Comment, CommentInTicket>()
                    .ForMember(c => c.Author, opt => opt.MapFrom(s => s.Author.UserName));

            Mapper.CreateMap<CommentInput, Comment>();

            Mapper.CreateMap<Comment, CommentIndex>()
                .ForMember(c => c.AuthorName, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(c => c.TicketTitle, opt => opt.MapFrom(s => s.Ticket.Title));

            Mapper.CreateMap<Comment, CommentEdit>();

            Mapper.CreateMap<Comment, CommentInUser>()
                .ForMember(c => c.Ticket, opt => opt.MapFrom(s => s.Ticket.Title));

            Mapper.CreateMap<CommentEdit, Comment>(); 
            #endregion

            #region User
            Mapper.CreateMap<User, UserIndex>();

            Mapper.CreateMap<User, UserEditRole>();

            Mapper.CreateMap<User, UserDetails>()
                .ForMember(u => u.Tickets, opt => opt.MapFrom(s => s.Tickets))
                .ForMember(u => u.Comments, opt => opt.MapFrom(s => s.Comments)); 
            #endregion
        }
    }
}