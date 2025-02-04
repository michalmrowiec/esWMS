﻿using Microsoft.EntityFrameworkCore;

namespace esWMS.Infrastructure.UnitTests.Helpers
{
    public class EsWMSContextInMemoryFactory
    {
        public static TestEsWmsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<EsWmsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new TestEsWmsDbContext(options);
        }
    }
}
