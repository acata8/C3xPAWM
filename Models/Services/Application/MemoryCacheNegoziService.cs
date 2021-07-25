using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        //RICORDA DI USARE memoryCache.Remove($"..") quando aggiorni qualcosa


        public Task<List<NegozioViewModel>> GetNegoziAsync(string search, int page)
        {
            return memoryCache.GetOrCreateAsync($"negozi{search}-{page}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetNegoziAsync(search, page);
            });

        }

        public Task<List<NegozioViewModel>> GetNegoziByCittaAsync(string citta, int page)
        {
            return memoryCache.GetOrCreateAsync($"{citta}-{page}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetNegoziByCittaAsync(citta,page);
            });
        }

        public Task<List<NegozioViewModel>> GetNegoziByProvinciaAsync(string provincia)
        {
            return memoryCache.GetOrCreateAsync($"{provincia}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetNegoziByProvinciaAsync(provincia);
            });
        }


        private int GetExpirationTime(){
            return cacheOptions.CurrentValue.TimeExpirationCache;
        }
    }
}