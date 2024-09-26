﻿using esWMS.Application.Contracts.Persistence;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.SystemActors
{
    internal class EmployeeRepository
        (EsWmsDbContext context,
        ILogger<EmployeeRepository> logger,
        ISieveProcessor sieveProcessor)
        : IEmployeeRepository
    {

        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<EmployeeRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity");
                throw;
            }
        }

        public async Task<PagedResult<Employee>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var employees = _context.Employees
                .AsNoTracking()
                .AsQueryable();

            var filteredEmployees = await _sieveProcessor
                .Apply(sieveModel, employees)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, employees, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Employee>
                (filteredEmployees, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
