
using System;
using System.Collections.Generic;

namespace Sitecore.Support.ContentTesting.Analytics.Calculation
{
    public class KPIsCalculation
    {
        public int GetIntegerPercentage<TKey>(long total, List<KeyValuePair<TKey, long>> values, TKey key)
        {
            if (values != null)
            {
                int num = 100;
                for (int i = 0; i < values.Count; i++)
                {
                    KeyValuePair<TKey, long> pair = values[i];
                    int rate = this.GetRate(pair.Value, total);
                    num -= rate;
                    if (i == (values.Count - 1))
                    {
                        rate += num;
                    }
                    if (pair.Key.Equals(key))
                    {
                        return rate;
                    }
                }
            }
            return 0;
        }


        int GetRate(long value, long totalValue)
        {
            if (totalValue == 0L)
            {
                return 0;
            }
            double a = (((double)value) / ((double)totalValue)) * 100.0;
            return (int)Math.Round(a);
        }

    }
}

