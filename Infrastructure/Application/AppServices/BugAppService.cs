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
    public partial class BugAppService : IBugAppService
    {
        private readonly IBugRepository _BugRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator _entityValidator;

        public BugAppService(IBugRepository BugRepository, IMapper mapper, IEntityValidator entityValidator)
        {
            _BugRepository = BugRepository ?? throw new ArgumentNullException(nameof(BugRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(BugDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _BugRepository.AddAsync(_mapper.Map<Bug>(item));
                commited = await _BugRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }
        
        public async Task<List<BugDto>> FindAllBySpecificationPatternAsync(Specification<BugDto> specification = null, List<string> includes = null, Dictionary<string, bool> order = null)
        {
            return _mapper.Map<List<BugDto>>(
                await _BugRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Bug, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), includes, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<BugDto> specification = null)
        {
            var count = await _BugRepository.FindCountByExpressionAsync(specification?.MapToExpressionOfType<Bug>());
            return count;
        }

        public async Task<BugDto> FindOneBySpecificationPatternAsync(Specification<BugDto> specification = null, List<string> includes = null)
        {
            var item = await _BugRepository.FindOneByExpressionAsync(specification?.MapToExpressionOfType<Bug>(), includes);
            return _mapper.Map<BugDto>(item);
        }

        public async Task<List<BugDto>> FindPageBySpecificationPatternAsync(Specification<BugDto> specification = null, List<string> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            return _mapper.Map<List<BugDto>>(
                await _BugRepository.FindPageByExpressionAsync(
                    specification?.MapToExpressionOfType<Bug>(), includes, order, pageSize, pageGo));
        }

        
        public BugDto Get(int id, List<string> includes = null)
        {
            return _mapper.Map<BugDto>(_BugRepository.Get(id, includes));
        }

        public async Task<List<BugDto>> GetAllAsync(List<string> includes = null, Dictionary<string, bool> order = null)
        {
            var items = await _BugRepository.GetAllAsync(includes, order);
            var dtoItems = _mapper.Map<List<BugDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<BugDto> GetAsync(int id, List<string> includes = null)
        {
            return _mapper.Map<BugDto>(await _BugRepository.GetAsync(id, includes));
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var item = await _BugRepository.GetAsync(id);
            await _BugRepository.DeleteAsync(item);
            var commited = await _BugRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(BugDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _BugRepository.UpdateAsync(_mapper.Map<Bug>(item));
                commited = await _BugRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
