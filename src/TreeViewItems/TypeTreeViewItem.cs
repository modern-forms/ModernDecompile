using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;
using Mono.Cecil;
using SkiaSharp;

namespace ModernDecompile
{
    public class TypeTreeViewItem : BaseTreeViewItem
    {
        public TypeDefinition Type { get; }

        public TypeTreeViewItem (TypeDefinition type)
        {
            Type = type;
            Text = type.Name;
            Image = GetImage ();
        }

        public override string GetBody (DecompileLanguage language)
            => DecompilerHelper.Decompile (Type, GetLanguage (language));

        protected override void DoExpand ()
        {
            foreach (var field in Type.Fields.Where (f => ShouldShowDefinition (f)).OrderBy (f => f.Name))
                Items.Add (new FieldTreeViewItem (field));

            foreach (var property in Type.Properties.Where (p => ShouldShowDefinition (p)).OrderBy (p => p.Name))
                Items.Add (new PropertyTreeViewItem (property));

            foreach (var method in Type.Methods.Where (m => ShouldShowMethod (m)).OrderBy (m => m.Name))
                Items.Add (new MethodTreeViewItem (method));

            foreach (var ev in Type.Events.Where (e => ShouldShowDefinition (e)).OrderBy (p => p.Name))
                Items.Add (new EventTreeViewItem (ev));
        }

        private SKBitmap GetImage ()
        {
            if (Type.IsEnum)
                return ResourceManager.GetTypeImage (Type, "enum.png");
            if (Type.IsValueType)
                return ResourceManager.GetTypeImage (Type, "structure.png");
            if (Type.IsInterface)
                return ResourceManager.GetTypeImage (Type, "interface.png");

            return ResourceManager.GetTypeImage (Type, "class.png");
        }

        private bool ShouldShowDefinition (ICustomAttributeProvider provider)
        {
            if (!provider.HasCustomAttributes)
                return true;

            return !provider.CustomAttributes.Any (a => a.AttributeType.Name == "CompilerGeneratedAttribute");
        }

        private bool ShouldShowMethod (MethodDefinition method)
        {
            if (method.IsAddOn || method.IsRemoveOn || method.IsGetter || method.IsSetter)
                return false;
            
            return ShouldShowDefinition (method);
        }
    }
}
