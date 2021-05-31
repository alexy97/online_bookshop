
using AutoMapper;
using online_bookshop_API.Model;
using online_bookshop_API.Model.Requests;

namespace online_bookshop_API
{
    
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<BookRequest, Book>();
            }
        }
    
}
