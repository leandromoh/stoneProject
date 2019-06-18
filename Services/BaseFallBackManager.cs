using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BaseFallBackManager<TSource>
    {
        protected readonly List<TSource> _source;

        public BaseFallBackManager(List<TSource> Source)
        {
            _source = Source;
        }

        protected TResult TryForAllSources<TResult>(Func<TSource, TResult> resultSelector)
        {
            foreach (var s in _source)
            {
                try
                {
                    return resultSelector(s);
                }
                catch (Exception e)
                {
                    //TODO gravar log
                    continue;
                }
            }

            string options = string.Join("\n", _source.Select(x => x.GetType().Name));
            throw new Exception($"all options of source are unavailable. options = {options}");
        }
    }
}
