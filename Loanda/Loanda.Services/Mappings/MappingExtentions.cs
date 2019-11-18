using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Loanda.Services.Mappings
{
    internal static class MappingExtensions
    {
        public static IReadOnlyCollection<TOut> MapCollection<TIn, TOut>(this IReadOnlyCollection<TIn> input, Func<TIn, TOut> mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (input is null || input.Count == 0)
            {
                return Array.Empty<TOut>();
            }

            var output = new TOut[input.Count];
            var i = 0;
            foreach (var entry in input)
            {
                output[i] = mapper(entry);
                ++i;
            }

            return new ReadOnlyCollection<TOut>(output);
        }
    }
}
