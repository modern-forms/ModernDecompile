using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;
using Mono.Cecil;
using Telerik.JustDecompiler.Decompiler;
using Telerik.JustDecompiler.Languages;
using Telerik.JustDecompiler.Languages.CSharp;

namespace ModernDecompile
{
    public class MethodTreeViewItem : BaseTreeViewItem
    {
        public MethodDefinition Method { get; }

        public MethodTreeViewItem (MethodDefinition method)
        {
            Method = method;
            Text = BuildMethodDisplayName ();
            Image = ResourceManager.GetDefinitionImage (method, "method.png");

            Items.Clear ();
        }

        public override string GetBody (DecompileLanguage language) 
            => DecompilerHelper.Decompile (Method, GetLanguage (language));

        private string BuildMethodDisplayName ()
        {
            var sb = new StringBuilder ();
            var name = Method.Name;

            if (name == ".ctor" || name == ".cctor")
                name = Method.DeclaringType.Name;

            sb.Append (name);
            sb.Append ("(");
            sb.Append (string.Join (", ", Method.Parameters.Select (p => p.ParameterType.Name)));
            sb.Append (") : ");
            sb.Append (Method.ReturnType.Name);

            return sb.ToString ();
        }
    }
}
