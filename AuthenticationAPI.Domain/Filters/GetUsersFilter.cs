using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AuthenticationAPI.Domain.Base;
using AuthenticationAPI.Domain.Enums;

namespace AuthenticationAPI.Domain.Filters
{
    public class GetUsersFilter : BaseFilter
    {
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? Name { get; set; }
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? Email { get; set; }
        [QueryOperator(Operator = WhereOperator.Equals)]
        public RoleType Role { get; set; }
    }
}
