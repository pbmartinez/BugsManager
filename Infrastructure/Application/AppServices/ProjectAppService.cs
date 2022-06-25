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
    public partial class ProjectAppService : IProjectAppService
    {
        private readonly IProjectRepository _ProjectRepository;
        private readonly IMapper _mapper;
        private readonly IEntityValidator _entityValidator;

        public ProjectAppService(IProjectRepository ProjectRepository, IMapper mapper, IEntityValidator entityValidator)
        {
            _ProjectRepository = ProjectRepository ?? throw new ArgumentNullException(nameof(ProjectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _entityValidator = entityValidator ?? throw new ArgumentNullException(nameof(entityValidator));
        }

        public async Task<bool> AddAsync(ProjectDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _ProjectRepository.AddAsync(_mapper.Map<Project>(item));
                commited = await _ProjectRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }
        
        public async Task<List<ProjectDto>> FindAllBySpecificationPatternAsync(Specification<ProjectDto> specification = null, List<string> includes = null, Dictionary<string, bool> order = null)
        {
            return _mapper.Map<List<ProjectDto>>(
                await _ProjectRepository.FindAllByExpressionAsync(
                    _mapper.MapExpression<Expression<Func<Project, bool>>>(
                        specification == null ? a => true : specification.ToExpression()), includes, order));
        }

        public async Task<int> FindCountBySpecificationPatternAsync(Specification<ProjectDto> specification = null)
        {
            var count = await _ProjectRepository.FindCountByExpressionAsync(specification?.MapToExpressionOfType<Project>());
            return count;
        }

        public async Task<ProjectDto> FindOneBySpecificationPatternAsync(Specification<ProjectDto> specification = null, List<string> includes = null)
        {
            var item = await _ProjectRepository.FindOneByExpressionAsync(specification?.MapToExpressionOfType<Project>(), includes);
            return _mapper.Map<ProjectDto>(item);
        }

        public async Task<List<ProjectDto>> FindPageBySpecificationPatternAsync(Specification<ProjectDto> specification = null, List<string> includes = null, Dictionary<string, bool> order = null, int pageSize = 0, int pageGo = 0)
        {
            return _mapper.Map<List<ProjectDto>>(
                await _ProjectRepository.FindPageByExpressionAsync(
                    specification?.MapToExpressionOfType<Project>(), includes, order, pageSize, pageGo));
        }

        
        public ProjectDto Get(Guid id, List<string> includes = null)
        {
            return _mapper.Map<ProjectDto>(_ProjectRepository.Get(id, includes));
        }

        public async Task<List<ProjectDto>> GetAllAsync(List<string> includes = null, Dictionary<string, bool> order = null)
        {
            var items = await _ProjectRepository.GetAllAsync(includes, order);
            var dtoItems = _mapper.Map<List<ProjectDto>>(items.ToList());
            return dtoItems;
        }

        public async Task<ProjectDto> GetAsync(Guid id, List<string> includes = null)
        {
            return _mapper.Map<ProjectDto>(await _ProjectRepository.GetAsync(id, includes));
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var item = await _ProjectRepository.GetAsync(id);
            await _ProjectRepository.DeleteAsync(item);
            var commited = await _ProjectRepository.UnitOfWork.CommitAsync();

            return commited > 0;
        }

        public async Task<bool> UpdateAsync(ProjectDto item)
        {
            int commited;
            if (_entityValidator.IsValid(item))
            {
                await _ProjectRepository.UpdateAsync(_mapper.Map<Project>(item));
                commited = await _ProjectRepository.UnitOfWork.CommitAsync();
            }
            else
                throw new ApplicationValidationErrorsException(_entityValidator.GetInvalidMessages(item));
            return commited > 0;
        }

    }
}
