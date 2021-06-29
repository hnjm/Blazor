using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlazorDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlazorDemo.DataProviders {
    class ContosoRetailDataProvider : EntityQueryableDataProvider<ContosoRetailContext>, IContosoRetailDataProvider {
        public ContosoRetailDataProvider(IDbContextFactory<ContosoRetailContext> contextFactory, IConfiguration configuration) : base(contextFactory, configuration) { }

        public async Task<IEnumerable<Sale>> GetSalesAsync(CancellationToken ct = default) {
            return await LoadQueryableDataAsync<Sale>(ct);
        }

        public override Task<IObservable<int>> GetLoadingStateAsync() {
            if(ConnectionStringUtils.GetPivotGridLargeDataConnectionString(Configuration) == null)
                return Task.FromResult<IObservable<int>>(null);
            return base.GetLoadingStateAsync();
        }
    }
}
