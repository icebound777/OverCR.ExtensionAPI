using System;
using System.Linq;

namespace OverCR.ExtensionSystem.API
{
    internal static class LanguageExtensions
    {
        internal static string WordBreak(this string text, int lineLength)
        {
            var charCount = 0;
            var lines = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                            .GroupBy(w => (charCount += w.Length + 1) / lineLength)
                            .Select(x => string.Join(" ", x.ToArray()));

            return string.Join("\n", lines.ToArray());
        }
    }
}
