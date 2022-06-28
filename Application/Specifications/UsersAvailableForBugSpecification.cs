using Application.Dtos;
using AutoMapper;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Specifications
{
    public class UsersAvailableForBugSpecification : Specification<UserDto>
    {
        public UsersAvailableForBugSpecification(IMapper mapper) : base(mapper)
        {
        }

        public override Expression<Func<UserDto, bool>> ToExpression()
        {
            return u => u.Bug == null;
        }
    }
}
