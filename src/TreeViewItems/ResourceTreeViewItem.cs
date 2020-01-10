using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class ResourceTreeViewItem : BaseTreeViewItem
    {
        public Resource Resource { get; }

        public ResourceTreeViewItem (Resource resource)
        {
            Text = resource.Name;
            Image = ResourceManager.GetEmbeddedImage ("picture.png");
            Resource = resource;

            Items.Clear ();
        }
    }
}
