using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class FieldTreeViewItem : BaseTreeViewItem
    {
        public FieldDefinition Field { get; }

        public override string GetBody (DecompileLanguage language)
            => DecompilerHelper.Decompile (Field, GetLanguage (language));

        public FieldTreeViewItem (FieldDefinition field)
        {
            Field = field;
            Text = BuildMethodDisplayName ();
            Image = ResourceManager.GetDefinitionImage (field, "field.png");

            Items.Clear ();
        }

        private string BuildMethodDisplayName ()
        {
            var sb = new StringBuilder ();

            sb.Append (Field.Name);
            sb.Append (" : ");
            sb.Append (Field.FieldType.Name);

            return sb.ToString ();
        }
    }
}
