using build9ja.admin.Dto;

namespace build9ja.admin.Extensions
{
    public static class HttpExtensions
    {
        public static DataTableRequestDto GetDataTableRequestForm(this HttpRequest request){
            DataTableRequestDto dataTableRequestDto = new DataTableRequestDto();
             dataTableRequestDto.Draw = request.Form["draw"].FirstOrDefault();
            dataTableRequestDto.Start = request.Form["start"].FirstOrDefault();
            dataTableRequestDto.Length = request.Form["length"].FirstOrDefault();
            dataTableRequestDto.SortColumn = request.Form["columns[" + request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            dataTableRequestDto.SortColumnDirection = request.Form["order[0][dir]"].FirstOrDefault();
            dataTableRequestDto.SearchValue = request.Form["search[value]"].FirstOrDefault();
            return dataTableRequestDto;
        }
    }
}