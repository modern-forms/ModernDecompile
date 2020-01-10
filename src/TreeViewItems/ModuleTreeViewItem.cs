using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class ModuleTreeViewItem : BaseTreeViewItem
    {
        public ModuleDefinition Module { get; }

        public ModuleTreeViewItem (ModuleDefinition module)
        {
            Text = module.Name;
            Image = ResourceManager.GetEmbeddedImage ("reference.png");
            Module = module;
        }

        protected override void DoExpand ()
        {
            Items.Add (new ReferencesTreeViewItem (Module));

            foreach (var ns in Module.Types.GroupBy (t => t.Namespace).OrderBy (t => t.Key))
                Items.Add (new NamespaceTreeViewItem (ns.Key, ns));
        }
    }
}
