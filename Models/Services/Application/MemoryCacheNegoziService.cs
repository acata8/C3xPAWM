using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C3xPAWM.Models.InputModel;
using C3xPAWM.Models.Options;
using C3xPAWM.Models.ViewModel;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace C3xPAWM.Models.Services.Application
{
    public class MemoryCacheNegoziService : ICachedNegoziService
    {
        private readonly INegoziService negozioService;
        private readonly IMemoryCache memoryCache;
        private readonly IOptionsMonitor<CacheOptions> cacheOptions;

        public MemoryCacheNegoziService(INegoziService negozioService, IMemoryCache memoryCache, IOptionsMonitor<CacheOptions> cacheOptions)
        {
            this.cacheOptions = cacheOptions;
            this.memoryCache = memoryCache;
            this.negozioService = negozioService;

        }

        public Task<ListViewModel<NegozioViewModel>> ByTipologia(ElencoListInputModel input)
        {
            throw new NotImplementedException();
        }

        public Task<List<NegozioViewModel>> GetListaNegozi(ElencoListInputModel model)
        {
            throw new NotImplementedException();
        }

        //RICORDA DI USARE memoryCache.Remove($"..") quando aggiorni qualcosa


        public Task<ListViewModel<NegozioViewModel>> GetNegozi(ElencoListInputModel model)
        {

            var page = model.Page;
            var search = model.Search;

            return memoryCache.GetOrCreateAsync($"negozi{search}-{page}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetNegozi(model);
            });

        }

        private int GetExpirationTime(){
            return cacheOptions.CurrentValue.TimeExpirationCache;
        }
    }
}