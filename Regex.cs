using System;
using System.Collections.Generic;

using System;
using System.Text.RegularExpressions;

public class Regex
{
    private static readonly string[] listRegex = {
        @"[Aa4@]?",
        "[Bb8]",
        @"[Cc(]",
        "[Dd]",
        "[Ee3]?",
        "[Ff]",
        "[Gg6]",
        "[Hh]",
        "[Ii1!]?",
        "[Jj]",
        "[Kk]",
        "[Ll1!]",
        "[Mm]",
        "[Nn]",
        "[Oo0]",
        "[Pp]",
        "[Qq]",
        "[Rr]",
        @"[Ss5$]",
        "[Tt7]",
        "[Uu]?",
        "[Vv]",
        "[Ww]",
        "[Xx]",
        "[Yy]",
        "[Zz2]",
        @"[\s]*"
    };

    public static string StringToRegex_TrueToAlay(string input)
    {
        string result = "";
        foreach (char c in input)
        {
            if (char.ToUpper(c) == ' ')
            {
                result += listRegex[26];
            }
            else
            {
                int index = char.ToUpper(c) - 'A';
                if (index >= 0 && index < listRegex.Length - 1)
                {
                    result += listRegex[index];
                }
            }
        }
        return result;
    }

    public static bool IsMatch(string input, string pattern)
    {
        string regexPattern = StringToRegex(pattern);
        return System.Text.RegularExpressions.Regex.IsMatch(input, regexPattern);
    }

    public static List<string> FindMatches(string input, string pattern)
    {
        string regexPattern = StringToRegex(pattern);
        MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(input, regexPattern);
        List<string> matchList = new List<string>();

        foreach (Match match in matches)
        {
            matchList.Add(match.Value);
        }

        return matchList;
    }
}