using Domain.Enums;

namespace Application.ApproveRequests;


public record ApproveRequestPaginationParams(int? PageIndex = 0, int? PageSize = 10, string SearchString = null)
{
    public StatusApprove DescriptionOrder { get; set; }
}
