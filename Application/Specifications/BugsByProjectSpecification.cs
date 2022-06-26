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
    public class BugsByProjectSpecification : Specification<BugDto>
    {
        public int? ProjectId { get; set; }

        public BugsByProjectSpecification(IMapper mapper, int? projectId) : base(mapper)
        {
            ProjectId = projectId;
        }
        //Expression that defines the predicate that object of type(EntityDto) must comply to fullfill the specification
        public override Expression<Func<BugDto, bool>> ToExpression()
        {
            if (ProjectId == null)
                return e => true;
            return e => e.ProjectId == ProjectId.Value;
        }
    }
}
