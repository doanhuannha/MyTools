
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace BlueMoon.Common
{
    public static class ExtensionUtilities
    {
        public static string GetExceptionDetail(this Exception ex, string note = null)
        {
            StringBuilder sb = new StringBuilder();
            if (note != null) { sb.Append(note); }
            do
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.StackTrace);
                ex = ex.InnerException;
            }
            while (ex != null);
            return sb.ToString();
        }
        public static string[] MaskAttributes { get; set; }
        public static string MaskJsonAttributes(this string json)
        {
            if (MaskAttributes != null)
            {
                foreach (var attName in MaskAttributes)
                {
                    json = Regex.Replace(json, @"""(?<a>" + attName + @")"":\s*""(?<v>(?:\\""|[^""])*)""", m =>
                    {
                        return string.Format("\"{0}\":\"{1}\"", m.Groups["a"].Value, "[xxxXxxx]");
                    }, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                }
            }
            return json;
        }

        public static string MaskSensitiveData(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            text = MaskJsonAttributes(text);
            return text;
        }
        public static string RemoveCRLF(this string s)
        {
            return s.Replace("\r", "").Replace("\n", " ");
        }
        const byte MAX = 0xFF;
        const string ENC_PREFIX = "[protected::", ENC_SURFIX = "]";
        
        public static string ProtectValue(this string val)
        {
            if (string.IsNullOrEmpty(val)) return val;
            byte k = (byte)new Random((int)DateTime.Now.Ticks).Next(0, MAX + 1);
            var bb = Encoding.UTF8.GetBytes(val);
            bb = bb.Select(d =>
            {
                var v = d + k;
                if (v > MAX) v = v - MAX;
                return (byte)v;
            }).Append(k).ToArray();
            return $"{ENC_PREFIX}{Convert.ToBase64String(bb)}{ENC_SURFIX}";
        }
        public static bool IsProtectedValue(this string val)
        {
            return !string.IsNullOrEmpty(val) && val.StartsWith(ENC_PREFIX) && val.EndsWith(ENC_SURFIX);
        }
        public static string ReadProtectedValue(this string val)
        {
            if (string.IsNullOrEmpty(val)) return val;
            if (!val.IsProtectedValue()) return val;
            val = val.Substring(ENC_PREFIX.Length, val.Length - ENC_PREFIX.Length - ENC_SURFIX.Length);
            var bb = Convert.FromBase64String(val);
            byte k = bb.Last();
            bb = bb.Take(bb.Length - 1).Select(d =>
            {
                var v = d - k;
                if (v < 0) v = MAX + v;
                return (byte)v;
            }).ToArray();
            return Encoding.UTF8.GetString(bb);
        }
        static Regex s_reg_SensitiveValues = new Regex(@"<<(.*?)>>", RegexOptions.Singleline | RegexOptions.Compiled);
        static Regex s_reg_ProtectedValues = new Regex(@"\[protected::(.*?)\]", RegexOptions.Singleline | RegexOptions.Compiled);
        
        public static string ReadSecuredFile(this string jsonFilePath)
        {
            var s = File.ReadAllText(jsonFilePath);
            string ss = s_reg_SensitiveValues.Replace(s, m => $"{m.Groups[1].Value.ProtectValue()}");
            if (ss.Length > s.Length) File.WriteAllText(jsonFilePath, ss);
            s = s_reg_ProtectedValues.Replace(ss, m => $"{m.Value.ReadProtectedValue()}");
            return s;
        }
    }
}
