using Core.Dtos;

namespace EC.Services.LangResourceAPI.Dtos.LangResourceDtos
{
    public class LangResourceGetAllByLangIdPagingDto:PagingDto
    {

        public int LangId { get; set; }

    }
}
