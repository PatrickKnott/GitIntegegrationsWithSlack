namespace GitIntegrationsWithSlack
{
    public static class HelperExtensions
    {
        public static string GetSubstringBeforeString(this string inputString, string endChar)
        {
            if (string.IsNullOrWhiteSpace(inputString))
            {
                return null;
            }
            return inputString.Substring(0, inputString.IndexOf(endChar));
        }

        public static string ResolveRetailSuccess(this string inputString)
        {
            if (inputString == "RetailSuccess")
            {
                return "Retail Success";
            }
            return inputString;
        }
    }
}
