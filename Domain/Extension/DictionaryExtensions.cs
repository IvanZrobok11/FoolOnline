namespace Domain.Extension
{
    public static class DictionaryExtensions
    {
        public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            return dict == null || dict.Count == 0;
        }
        public static bool IsKeyWithoutValue<TKey, TValue>(this Dictionary<TKey, TValue> dict) where TValue : struct 
        {
            foreach (var key in dict.Keys)
            {
                if (dict[key].Equals(default(TValue)))
                {
                    return true;
                }
            }

            return false;
        }
        public static Dictionary<TKeyResult, TValueResult> AsDictionary<TKeySource, TValueSource, TKeyResult, TValueResult>(this Dictionary<TKeySource, TValueSource> dict, Func<KeyValuePair<TKeySource, TValueSource>, KeyValuePair<TKeyResult, TValueResult>> selector)
        {
            var resultDict = new Dictionary<TKeyResult, TValueResult>(dict.Count);
            foreach (var kvp in dict)
            {
                var func = selector(kvp);
                resultDict[func.Key] = func.Value;
            }

            return resultDict;
        }
    }
}
