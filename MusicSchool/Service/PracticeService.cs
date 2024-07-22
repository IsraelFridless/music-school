using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSchool.Service
{
    internal static class PracticeService
    {
        public static Func<List<string>, bool> StartWithA = (list) => 
        list.Any(str => str.ToLower().StartsWith("a"));

        public static Func<List<string>, bool> HasEmptyStr = (list) =>
        list.Any(str => string.IsNullOrEmpty(str));

        public static Func<List<string>, bool> IsAllContainsA = (list) =>
        list.All(str => str.ToLower().Contains("a"));

        public static Func<List<string>, List<string>> AllToUpper = (list) =>
        list.Select(str => str.ToUpper()).ToList();

        public static Func<List<string>, List<string>> LinqToUpper = (list) =>
        (from str in list select str.ToUpper()).ToList();

        public static Func<List<string>, List<string>> LongerThan3 = (list) =>
        list.Where(str => str.Length > 3).ToList();

        public static Func<List<string>, List<string>> LinqLongerThan3 = (list) =>
        (from str in list where str.Length > 3 select str).ToList();

        public static Func<List<string>, string> ToOneString = (list) =>
        list.Aggregate("", (res, str) => $"{res} {str}");

        public static Func<List<string>, int> ToInt = (list) =>
        list.Aggregate(0, (res, str) => res + str.Length);

        public static Func<List<string>, List<string>> AggLongerThan3 = (list) =>
        list.Aggregate(new List<string>(), (current, next) => next.Length > 3? [.. current, next] : current);
    }
}
