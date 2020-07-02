using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using ClientAPi.Models;

namespace ClientAPi.App_Code
{
    public class Code_credencial
    {
        public void setCrearCreden(ClsCredencial login, string token)
        {
            var cache = MemoryCache.Default;
            MemoryCache.Default.AddOrGetExisting(login.Key, token, DateTime.Now.AddMinutes(10));
        }

        public bool getBuscarMemoria(ClsCredencial login)
        {
            bool estado = false;
            var cacheT = MemoryCache.Default;
            if (cacheT != null && cacheT.Contains(login.Key) && cacheT.Get(login.Key) != null)
            {
                estado = true;
            }
            return estado;
        }
    }
}