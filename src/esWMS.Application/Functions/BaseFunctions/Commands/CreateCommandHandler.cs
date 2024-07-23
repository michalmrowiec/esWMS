﻿using AutoMapper;
using esWMS.Application.Contracts.Persistence;
using esWMS.Application.Functions.Responses;
using FluentValidation;
using MediatR;

namespace esWMS.Application.Functions.BaseFunctions.Commands
{
    internal class CreateCommandHandler<TRequest, TEntity, TId, TEntityDto>
        : IRequestHandler<TRequest, BaseResponse<TEntityDto>>
        where TRequest : IRequest<BaseResponse<TEntityDto>>
        where TEntity : class
        where TEntityDto : class
    {
        private AbstractValidator<TRequest> _validator;
        private readonly IBaseRepository<TEntity, TId> _repository;
        private readonly IMapper _mapper;
        public CreateCommandHandler
            (AbstractValidator<TRequest> validator,
            IBaseRepository<TEntity, TId> repository,
            IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<BaseResponse<TEntityDto>> Handle
            (TRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new BaseResponse<TEntityDto>(validationResult);
            }

            var entity = _mapper.Map<TEntity>(request);

            var createdEntity = await _repository.CreateAsync(entity);

            return new BaseResponse<TEntityDto>(createdEntity);
        }
    }
}
