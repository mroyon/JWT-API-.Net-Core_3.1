using JWTApiExample.InAppResources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JWTApiExample.Services
{
    public class LocalizeService
    {
        private readonly IStringLocalizer _localizer;

        public LocalizeService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }

        public LocalizedString GetLocalizedHtmlStringAllowNull(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                return _localizer[key];
            }

            return new LocalizedString(key, string.Empty);
        }

        public LocalizedString GetLocalizedHtmlString(string key, string parameter)
        {
            return _localizer[key, parameter];
        }
    }
}
