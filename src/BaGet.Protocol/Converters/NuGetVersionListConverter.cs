﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NuGet.Versioning;

namespace BaGet.Protocol.Converters
{
    public class NuGetVersionListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IReadOnlyList<NuGetVersion>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var versions = ((IReadOnlyList<NuGetVersion>)value);

            serializer.Serialize(writer, versions.Select(v => v.ToString()));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<IReadOnlyList<string>>(reader)
                .Select(NuGetVersion.Parse)
                .ToList();
        }
    }
}
