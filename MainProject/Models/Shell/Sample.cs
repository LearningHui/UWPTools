using Microsoft.Toolkit.Uwp;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;

namespace MainProject
{
    public class Sample
    {
        internal static async Task<Sample> FindAsync(string category, string name)
        {
            var categories = await Samples.GetCategoriesAsync();
            return categories?
                .FirstOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase))?
                .Samples
                .FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string About { get; set; }

        public string CodeUrl { get; set; }

        public string CodeFile { get; set; }

        public string JavaScriptCodeFile { get; set; }

        public string XamlCodeFile { get; set; }

        public string XamlCode { get; private set; }

        public string DocumentationUrl { get; set; }

        public string Icon { get; set; }

        public string BadgeUpdateVersionRequired { get; set; }

        public string ApiCheck { get; set; }

        public bool HasXAMLCode => !string.IsNullOrEmpty(XamlCodeFile);

        public bool HasCSharpCode => !string.IsNullOrEmpty(CodeFile);

        public bool HasJavaScriptCode => !string.IsNullOrEmpty(JavaScriptCodeFile);

        public bool HasDocumentation => !string.IsNullOrEmpty(DocumentationUrl);

        public bool IsSupported
        {
            get
            {
                if (ApiCheck == null)
                {
                    return true;
                }

                return ApiInformation.IsTypePresent(ApiCheck);
            }
        }

        public async Task<string> GetCSharpSourceAsync()
        {
            using (var codeStream = await StreamHelper.GetPackagedFileStreamAsync($"SamplePages/{Name}/{CodeFile}"))
            {
                return await codeStream.ReadTextAsync();
            }
        }

        public async Task<string> GetJavaScriptSourceAsync()
        {
            using (var codeStream = await StreamHelper.GetPackagedFileStreamAsync($"SamplePages/{Name}/{JavaScriptCodeFile}"))
            {
                return await codeStream.ReadTextAsync();
            }
        }

        //public async Task<string> GetDocumentationAsync()
        //{
        //    try
        //    {
        //        using (var request = new HttpHelperRequest(new Uri(DocumentationUrl), HttpMethod.Get))
        //        {
        //            using (var response = await HttpHelper.Instance.SendRequestAsync(request).ConfigureAwait(false))
        //            {
        //                if (response.Success)
        //                {
        //                    var result = await response.Content.ReadAsStringAsync();

        //                    // Need to do some cleaning
        //                    // Rework code tags
        //                    var regex = new Regex("```(xaml|xml|csharp)(?<code>.+?)```", RegexOptions.Singleline);

        //                    foreach (Match match in regex.Matches(result))
        //                    {
        //                        var code = match.Groups["code"].Value;
        //                        var lines = code.Split('\n');
        //                        var newCode = new StringBuilder();
        //                        foreach (var line in lines)
        //                        {
        //                            newCode.AppendLine("    " + line);
        //                        }

        //                        result = result.Replace(match.Value, newCode.ToString());
        //                    }

        //                    // Images
        //                    regex = new Regex("## Example Image.+?##", RegexOptions.Singleline);
        //                    result = regex.Replace(result, "##");

        //                    return result;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //    return string.Empty;
        //}


        private static Type LookForTypeByName(string typeName)
        {
            // First search locally
            var result = System.Type.GetType(typeName);

            if (result != null)
            {
                return result;
            }

            // Search in Windows
            var proxyType = VerticalAlignment.Center;
            var assembly = proxyType.GetType().GetTypeInfo().Assembly;

            foreach (var typeInfo in assembly.ExportedTypes)
            {
                if (typeInfo.Name == typeName)
                {
                    return typeInfo;
                }
            }

            // Search in Microsoft.Toolkit.Uwp.UI.Controls
            var controlsProxyType = GridSplitter.GridResizeDirection.Auto;
            assembly = controlsProxyType.GetType().GetTypeInfo().Assembly;

            foreach (var typeInfo in assembly.ExportedTypes)
            {
                if (typeInfo.Name == typeName)
                {
                    return typeInfo;
                }
            }

            return null;
        }
    }
}
