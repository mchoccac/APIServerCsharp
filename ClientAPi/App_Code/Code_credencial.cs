using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public void setIdentiﬁer(ClsToken tangs)
        {
            var cache = MemoryCache.Default;

            if (cache != null &&
            cache.Contains(tangs.X_Key + tangs.Id) &&
            cache.Get(     tangs.X_Key + tangs.Id) != null)
            {
                cache.Remove(tangs.X_Key + tangs.Id);
                MemoryCache.Default.AddOrGetExisting(
                tangs.X_Key + tangs.Id,
                tangs.Id + "#" + tangs.Message,
                DateTime.Now.AddMinutes(10));
            }else {
                MemoryCache.Default.AddOrGetExisting(
                tangs.X_Key + tangs.Id,
                tangs.Id + "#" + tangs.Message,
                DateTime.Now.AddMinutes(10));
            }


        }

        public string getIdentiﬁerId(ClsToken tangs)
        {
            string id = "";
            var cacheT = MemoryCache.Default;
            if (cacheT != null && 
                cacheT.Contains(tangs.X_Key + tangs.Id) && 
                cacheT.Get(     tangs.X_Key + tangs.Id) != null)
            {
                id = cacheT.Get(tangs.X_Key + tangs.Id).ToString().Split('#')[0];
            }
            return id;
        }

        public string getIdentiﬁerMessage(ClsToken tangs)
        {
            string id = "";
            var cacheT = MemoryCache.Default;
            if (cacheT != null &&
                cacheT.Contains(tangs.X_Key + tangs.Id) &&
                cacheT.Get(tangs.X_Key + tangs.Id) != null)
            {
                id = cacheT.Get(tangs.X_Key + tangs.Id).ToString().Split('#')[1];
            }
            return id;
        }

        public bool getBuscarKey(ClsCredencial login)
        {
            bool estado = false;
            var cacheT = MemoryCache.Default;
            if (cacheT != null && cacheT.Contains(login.Key) && cacheT.Get(login.Key) != null)
            {
                estado = true;
            }
            return estado;
        }

        public string getToken(ClsCredencial login)
        {
            string  token ="";
            var cacheT = MemoryCache.Default;
            if (cacheT != null && cacheT.Contains(login.Key) && cacheT.Get(login.Key) != null)
            {
                token = cacheT.Get(login.Key).ToString();
            }
            return token;
        }

        public List<string> getAllKey() {
            return MemoryCache.Default.Select(mkey => mkey.Key).ToList();
            //return MemoryCache.Default.ToList();
        }

        public List<object> getAllValue()
        {
            return MemoryCache.Default.Select(mValue => mValue.Value).ToList();
            //return MemoryCache.Default.ToList();
        }
    }
}