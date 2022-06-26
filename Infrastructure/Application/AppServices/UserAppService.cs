using Application.Dtos;
using Application.IAppServices;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.IRepositories;
using Domain.Specification;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Specifications;
using Application.Interfaces;
using Application.Exceptions;

namespace Infrastructure.Application.AppServices
{
    public partial class UserAppService : IUserAppService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator _entityValidator;

        public UserAppService(IUserRepository UserRepository, IMapper mapper, IEntityValidator entityValidator)
        {
            _UserRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(UserDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _UserRepository.AddAsync(_mapper.Map<User>(item));
                commited = await _UserRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }
        
        public async Task<List<UserDto>> FindAllBySpecificationPatternAsync(Specification<UserDto> specification = null, List<string> includes = null, Dictionary<string, bool> order = null)
        {
            return _mapper.Map<List<UserDto>>(
                await _UserRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<User, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), includes, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<UserDto> specification = null)
        {
            var count = await _UserRepository.FindCountByExpressionAsync(specification?.MapToExpressionOfType<User>());
            return count;
        }

        public async Task<UserDto> FindOneBySpecificationPatternAsync(Specification<UserDto> specification = null, List<string> includes = null)
        {
            var item = await _UserRepository.FindOneByExpressionAsync(specification?.MapToExpressionOfType<User>(), includes);
            return _mapper.Map<UserDto>(item);
        }

        public async Task<List<UserDto>> FindPageBySpecificationPatternAsync(Specification<UserDto> specification = null, List<string> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            return _mapper.Map<List<UserDto>>(
                await _UserRepository.FindPageByExpressionAsync(
                    specification?.MapToExpressionOfType<User>(), includes, order, pageSize, pageGo));
        }

        
        public UserDto Get(int id, List<string> includes = null)
        {
            return _mapper.Map<UserDto>(_UserRepository.Get(id, includes));
        }

        public async Task<List<UserDto>> GetAllAsync(List<string> includes = null, Dictionary<string, bool> order = null)
        {
            var items = await _UserRepository.GetAllAsync(includes, order);
            var dtoItems = _mapper.Map<List<UserDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<UserDto> GetAsync(int id, List<string> includes = null)
        {
            return _mapper.Map<UserDto>(await _UserRepository.GetAsync(id, includes));
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var item = await _UserRepository.GetAsync(id);
            await _UserRepository.DeleteAsync(item);
            var commited = await _UserRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(UserDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _UserRepository.UpdateAsync(_mapper.Map<User>(item));
                commited = await _UserRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
