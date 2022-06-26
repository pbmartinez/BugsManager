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
    public class BugsByUserSpecification : Specification<BugDto>
    {
        public int? UserId { get; set; }

        public BugsByUserSpecification(IMapper mapper, int? userId) : base(mapper)
        {
            UserId = userId;
        }
        //Expression that defines the predicate that object of type(EntityDto) must comply to fullfill the specification
        public override Expression<Func<BugDto, bool>> ToExpression()
        {
            if (UserId == null)
                return e => true;
            return e => e.UserId == UserId.Value;
        }
    }
}
