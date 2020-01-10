using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class AssemblyTreeViewItem : BaseTreeViewItem
    {
        public AssemblyDefinition Assembly { get; }

        public AssemblyTreeViewItem (AssemblyDefinition assembly)
        {
            Text = assembly.Name.Name;
            Image = ResourceManager.GetEmbeddedImage ("reference.png");
            Assembly = assembly;
        }

        protected override void DoExpand ()
        {
            foreach (var module in Assembly.Modules)
                Items.Add (new ModuleTreeViewItem (module));

            Items.Add (new ResourcesTreeViewItem (Assembly));
        }
    }
}
