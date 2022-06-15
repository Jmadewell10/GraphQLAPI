namespace GraphQLAPI.Extensions
{
    public static class DataExtension
    {
        public static IEnumerable<dynamic> IterateToDictionaries(this IEnumerable<dynamic> data)
        {
            IList<dynamic> dictionariesToReturn = new List<dynamic>();
            foreach (var dictionaries in data)
            {
                dictionariesToReturn.Add(dictionaries);
            }
            return dictionariesToReturn;
        }
    }
}
