using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;
using Telerik.JustDecompiler.Languages;
using Telerik.JustDecompiler.Languages.CSharp;
using Telerik.JustDecompiler.Languages.VisualBasic;

namespace ModernDecompile
{
    public abstract class BaseTreeViewItem : TreeViewItem
    {
        private bool is_expanded;

        public virtual string GetBody (DecompileLanguage language) => string.Empty;

        public void OnDoExpand ()
        {
            if (is_expanded)
                return;

            DoExpand ();

            is_expanded = true;
        }

        protected virtual void DoExpand () { }

        protected ILanguage GetLanguage (DecompileLanguage language)
        {
            return language == DecompileLanguage.CSharp ? LanguageFactory.GetLanguage (CSharpVersion.V7) : LanguageFactory.GetLanguage (VisualBasicVersion.V10);
        }
    }
}
