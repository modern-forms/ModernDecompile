using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Mono.Cecil;
using SkiaSharp;

namespace ModernDecompile
{
    public static class ResourceManager
    {
        private static readonly Assembly assembly = typeof (ResourceManager).Assembly;

        private static readonly ConcurrentDictionary<string, SKBitmap> image_resources = new ConcurrentDictionary<string, SKBitmap> ();

        public static SKBitmap GetEmbeddedImage (string filename)
        {
            filename = filename.ToLowerInvariant ();

            return image_resources.GetOrAdd (filename, (f) => {
                var name = assembly.GetManifestResourceNames ().FirstOrDefault (n => n.EndsWith (f, StringComparison.OrdinalIgnoreCase)) ?? f;
                var stream = assembly.GetManifestResourceStream (name);

                if (stream == null)
                    throw new InvalidOperationException ($"Could not find embedded image {f}");

                return SKBitmap.Decode (stream);
            });
        }

        public static SKBitmap GetDefinitionImage (IVisibilityDefinition definition, string filename)
        {
            if (definition.IsPublic)
                return GetEmbeddedImage ("pub" + filename);
            if (definition.IsFamily)
                return GetEmbeddedImage ("prot" + filename);
            if (definition.IsPrivate)
                return GetEmbeddedImage ("priv" + filename);

            return GetEmbeddedImage ("pub" + filename);
        }

        public static SKBitmap GetTypeImage (TypeDefinition definition, string filename)
        {
            if (definition.IsPublic)
                return GetEmbeddedImage ("pub" + filename);

            return GetEmbeddedImage ("priv" + filename);
        }
    }
}
