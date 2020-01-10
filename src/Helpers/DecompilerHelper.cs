using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mono.Cecil;
using Telerik.JustDecompiler;
using Telerik.JustDecompiler.Decompiler.Caching;
using Telerik.JustDecompiler.Decompiler.WriterContextServices;
using Telerik.JustDecompiler.Languages;

namespace ModernDecompile
{
    public class DecompilerHelper
    {
        public static AssemblyDefinition GetAssembly (string assemblyPath)
        {
            var assemblyResolver = new WeakAssemblyResolver (GlobalAssemblyResolver.CurrentAssemblyPathCache);
            var readerParameters = new ReaderParameters (assemblyResolver);

            assemblyResolver.AssemblyDefinitionFailure += (o, e) => { throw e.InnerException ?? e; };

            return assemblyResolver.LoadAssemblyDefinition (assemblyPath, readerParameters, loadPdb: true);
        }

        public static string Decompile (IMemberDefinition type, ILanguage language)
        {
            var theWriter = new StringWriter ();
            var formatter = new PlainTextFormatter (theWriter);
            var settings = new WriterSettings (true, false, true, false, true, false);
            var writer = language.GetWriter (formatter, SimpleExceptionFormatter.Instance, settings);
            var writerContextService = new TypeCollisionWriterContextService (new ProjectGenerationDecompilationCacheService (), false);

            writer.Write (type, writerContextService);

            return theWriter.ToString ();
        }
    }
}
