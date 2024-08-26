using AutoMapper;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Contracts.Utilities;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.PwFunctions.Commands.CreatePw
{
    internal class CreatePwCommandHandler
        (IPwRepository repository,
        IMediator mediator,
        IMapper mapper,
        ITransactionManager transactionManager)
        : IRequestHandler<CreatePwCommand, BaseResponse<PwDto>>
    {
        private readonly IPwRepository _repository = repository;
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;
        private readonly ITransactionManager _transactionManager = transactionManager;

        public async Task<BaseResponse<PwDto>> Handle
            (CreatePwCommand request, CancellationToken cancellationToken)
        {
            return new BaseResponse<PwDto>(new PwDto());
        }
    }
}
