using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class NamespaceTreeViewItem : BaseTreeViewItem
    {
        public string Namespace { get; }
        private readonly List<TypeDefinition> types;

        public NamespaceTreeViewItem (string ns, IEnumerable<TypeDefinition> types)
        {
            Text = string.IsNullOrWhiteSpace (ns) ? "<Default namespace>" : ns;
            Image = ResourceManager.GetEmbeddedImage ("namespace.png");
            Namespace = ns;
            this.types = types.ToList ();
        }

        protected override void DoExpand ()
        {
            foreach (var type in types.OrderBy (t => t.Name))
                Items.Add (new TypeTreeViewItem (type));
        }
    }
}
