namespace Blog.Web.ViewModels.Post
{
    using System;
    using AutoMapper;
    using Blog.Web.Infrastructure.Mapping;

    public class PostViewModel : IMapFrom<Blog.Data.Models.Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorID { get; set; }

        public string ImageURL { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Blog.Data.Models.Post, PostViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}
