using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class ReferenceTreeViewItem : BaseTreeViewItem
    {
        public AssemblyNameReference Reference { get; }

        public ReferenceTreeViewItem (AssemblyNameReference reference)
        {
            Text = reference.FullName;
            Image = ResourceManager.GetEmbeddedImage ("reference.png");
            Reference = reference;

            Items.Clear ();
        }
    }
}
