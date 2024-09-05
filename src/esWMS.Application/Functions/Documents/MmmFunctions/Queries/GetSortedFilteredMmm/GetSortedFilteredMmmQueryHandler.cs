﻿using AutoMapper;
using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Application.Functions.Services;
using esWMS.Application.Responses;
using MediatR;

namespace esWMS.Application.Functions.Documents.MmmFunctions.Queries.GetSortedFilteredMmm
{
    internal class GetSortedFilteredMmmQueryHandler
        (IMmmRepository mmmRepository, IMapper mapper)
        : IRequestHandler<GetSortedFilteredMmmQuery, BaseResponse<PagedResult<MmmDto>>>
    {
        private readonly IMmmRepository _mmmRepository = mmmRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<PagedResult<MmmDto>>> Handle(GetSortedFilteredMmmQuery request, CancellationToken cancellationToken)
        {
            return await request.SieveModel.Handle<MmmDto, MMM>(_mapper, _mmmRepository);
        }
    }
}