using Dy.Core.CrossCuttingConcerns.Caching;
using NHibernate.Cache;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dy.Core.Aspects.PostSharp.CacheAspects
{
    [Serializable]
    public class CacheAsepct:MethodInterceptionAspect
    {
        private Type _cacheType; //Hangisi Redis vs..
        private int _cacheByMinute;
        ICacheManager _cacheManager; //Hangisi Redis vs..

        public CacheAsepct(Type cacheType,int cacheByMinute=60)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
        }
        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType)==false) //Defensing proggraming
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);
        }
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = string.Format("{0}.{1}{2}", args.Method.ReflectedType.Namespace, args.Method.ReflectedType.Name, args.Method.Name);
            var arguments = args.Arguments.ToList();

            var key = string.Format("{0}({1})", methodName, string.Join(",", arguments.Select(x => x != null ? x.ToString():"<Null>")));

            if (_cacheManager.IsAdd(key))
            {
                args.ReturnValue = _cacheManager.Get<object>(key);
            }
            _cacheManager.Add(key, args.ReturnValue,_cacheByMinute);
        }
    }
}
