using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class ResourcesTreeViewItem : BaseTreeViewItem
    {
        public AssemblyDefinition Assembly { get; }

        public ResourcesTreeViewItem (AssemblyDefinition assembly)
        {
            Text = "Resources";
            Image = ResourceManager.GetEmbeddedImage ("folder.png");
            Assembly = assembly;
        }

        protected override void DoExpand ()
        {
            foreach (var resource in Assembly.MainModule.Resources)
                Items.Add (new ResourceTreeViewItem (resource));
        }
    }
}
