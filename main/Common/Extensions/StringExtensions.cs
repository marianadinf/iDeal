using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace UIT.iDeal.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToProperCase(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }

        public static string ToWords(this string text)
        {
            return NetToString.Convert(text);
        }

        public static string ToProperCaseWords(this string text)
        {
            var words = ToWords(text);
            return words.ToProperCase();
        }

        public static string ToFormat(this string stringFormat, params object[] args)
        {
            return String.Format(stringFormat, args);
        }

        public static string TestableId(this string value)
        {
            return Regex.Replace(value, @"[^-_:A-Za-z0-9]", "_");
        }

        public static string ToolBarButtonId(this string buttonName)
        {
            return buttonName.TestableId() + "Button";
        }

        public static string DialogButtonIdFor(this string dialogId, string buttonName)
        {
            return dialogId.TestableId() + buttonName.TestableId() + "Button";
        }

        public static String RemoveSuffix(this string subject, string suffix = "List")
        {
            var index = subject.IndexOf(suffix);
            var subjectWithoutSuffix = subject;

            if (index > 0)
            {
                subjectWithoutSuffix = subject.Substring(0, index);
            }

            return subjectWithoutSuffix;
        }
    }
}