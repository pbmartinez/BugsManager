using Application.Dtos;
using AutoMapper;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Application.Specifications
{
    //Miningfull name that denotes the specification
    public class BugsByDateRangeSpecification : Specification<BugDto>
    {
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }

        public BugsByDateRangeSpecification(IMapper mapper, DateTime? startDate, DateTime? endDate) : base(mapper)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        //Expression that defines the predicate that object of type(EntityDto) must comply to fullfill the specification
        public override Expression<Func<BugDto, bool>> ToExpression()
        {
            if (StartDate == null && EndDate == null)
                return e => true;
            if (StartDate != null && EndDate == null)
                return e => e.CreationDate >= StartDate.Value;
            if (StartDate == null && EndDate != null)
                return e => e.CreationDate <= EndDate.Value;
            return e => e.CreationDate >= StartDate.Value && e.CreationDate <= EndDate.Value;
        }
    }
}
