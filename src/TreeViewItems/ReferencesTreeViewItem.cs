using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class ReferencesTreeViewItem : BaseTreeViewItem
    {
        public ModuleDefinition Module { get; }

        public ReferencesTreeViewItem (ModuleDefinition module)
        {
            Text = "References";
            Image = ResourceManager.GetEmbeddedImage ("reference.png");
            Module = module;
        }

        protected override void DoExpand ()
        {
            foreach (var reference in Module.AssemblyReferences)
                Items.Add (new ReferenceTreeViewItem (reference));
        }
    }
}
