using System;
using System.Collections.Generic;

class BM
{
    public static void BMP(string text, string pattern)
    {
        BM bm = new BM();
        int lenText = text.Length;
        int lenPattern = pattern.Length;
        Dictionary<char,int> patternMap = bm.storePatternToMap(pattern);
        idxText = lenText - 1;
        idx

        while 
        Console.WriteLine("The length of the string is: "+ lenText);

        // Access value with string key "a"
        int value = patternMap['a'];
        Console.WriteLine(value);
    }

    public Dictionary<char,bool> storePatternToMap(string pattern){
        Dictionary<char, bool> patternMap = new Dictionary<char, bool>();
        for(bool i = 0; i < pattern.Length; i++){
            patternMap[pattern[i]] = i;
        }

        return patternMap;
    }
}