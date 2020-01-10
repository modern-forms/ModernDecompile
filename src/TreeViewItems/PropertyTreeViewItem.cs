using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class PropertyTreeViewItem : BaseTreeViewItem
    {
        public PropertyDefinition Property { get; }

        public PropertyTreeViewItem (PropertyDefinition property)
        {
            Property = property;
            Text = BuildPropertyDisplayName ();
            Image = ResourceManager.GetDefinitionImage (property.GetMethod ?? property.SetMethod, "property.png");
        }

        public override string GetBody (DecompileLanguage language)
            => DecompilerHelper.Decompile (Property, GetLanguage (language));

        protected override void DoExpand ()
        {
            if (Property.GetMethod != null)
                Items.Add (new MethodTreeViewItem (Property.GetMethod));
            if (Property.SetMethod != null)
                Items.Add (new MethodTreeViewItem (Property.SetMethod));
        }

        private string BuildPropertyDisplayName ()
        {
            var sb = new StringBuilder ();

            sb.Append (Property.Name);
            sb.Append ("(");
            sb.Append (string.Join (", ", Property.Parameters.Select (p => p.ParameterType.Name)));
            sb.Append (") : ");
            sb.Append (Property.PropertyType.Name);

            return sb.ToString ();
        }
    }
}
