﻿using esWMS.Domain.Entities.SystemActors;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.SystemActors
{
    internal class ContractorSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Contractor>(x => x.ContractorId)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.VatId)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.ContractorName)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.EmailAddress)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.PhoneNumber)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.City)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.Address)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.PostalCode)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.Region)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.IsActive)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.IsSupplier)
                .CanSort()
                .CanFilter();
            mapper.Property<Contractor>(x => x.IsRecipient)
                .CanSort()
                .CanFilter();
        }
    }
}
