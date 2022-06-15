namespace GraphQLAPI.Extensions
{
    public static class QueryExtension
    {
        public static string GetEdgeName(this string source, string target)
        {
            string edgeName = string.Empty;
            switch (source)
            {
                case "Person":
                    {
                        if (target.Equals("Song"))
                        {
                            edgeName = "HasSong";
                            
                        }
                        edgeName = "knows";
                        
                        break;
                    }
                case "Song":
                    {
                        if (target.Equals("Person"))
                        {
                            edgeName = "HasSong";
                        }
                        break;
                    }
            }
            return edgeName;
        }

        public static bool InE(this string source, string target)
        {
            bool inE = true;
            switch (source)
            {
                case "Person":
                    {

                        inE = true;

                        break;
                    }
                case "Song":
                    {
                        if (target.Equals("Person"))
                        {
                            inE = false;
                        }
                        break;
                    }
            }
            return inE;
        }
    }
}
