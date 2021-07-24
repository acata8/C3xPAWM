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
        private readonly IOptionsMonitor<CacheOptions> expirationCache;

        public MemoryCacheNegoziService(INegoziService negozioService, IMemoryCache memoryCache, IOptionsMonitor<CacheOptions> expirationCache)
        {
            this.expirationCache = expirationCache;
            this.memoryCache = memoryCache;
            this.negozioService = negozioService;

        }

        //RICORDA DI USARE memoryCache.Remove($"..") quando aggiorni qualcosa

        public Task<List<string>> GetListaCittaDistinct()
        {
            return memoryCache.GetOrCreateAsync($"citta", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetListaCittaDistinct();
            });

        }

        public Task<List<string>> GetListaRegioniDistinct()
        {

            return memoryCache.GetOrCreateAsync($"regioni", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetListaRegioniDistinct();
            });
        }

        public Task<List<NegozioViewModel>> GetNegoziAsync(string search)
        {
            return memoryCache.GetOrCreateAsync($"negozi{search}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetNegoziAsync(search);
            });

        }

        public Task<List<NegozioViewModel>> GetNegoziByCittaAsync(string citta)
        {
            return memoryCache.GetOrCreateAsync($"{citta}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetNegoziByCittaAsync(citta);
            });
        }

        public Task<List<NegozioViewModel>> GetNegoziByRegioneAsync(string regione)
        {
            return memoryCache.GetOrCreateAsync($"{regione}", cacheEntry =>
            {
                cacheEntry.SetSize(1);
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(GetExpirationTime()));
                return negozioService.GetNegoziByRegioneAsync(regione);
            });
        }


        private int GetExpirationTime(){
            return expirationCache.CurrentValue.TimeExpirationCache;
        }
    }
}