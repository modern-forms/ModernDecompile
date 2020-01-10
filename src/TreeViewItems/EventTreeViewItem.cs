using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public class EventTreeViewItem : BaseTreeViewItem
    {
        public EventDefinition Event { get; }

        public override string GetBody (DecompileLanguage language)
            => DecompilerHelper.Decompile (Event, GetLanguage (language));

        public EventTreeViewItem (EventDefinition ev)
        {
            Event = ev;
            Text = BuildEventDisplayName ();
            Image = ResourceManager.GetDefinitionImage (ev.AddMethod ?? ev.RemoveMethod, "event.png");
        }

        protected override void DoExpand ()
        {
            if (Event.AddMethod != null)
                Items.Add (new MethodTreeViewItem (Event.AddMethod));
            if (Event.RemoveMethod != null)
                Items.Add (new MethodTreeViewItem (Event.RemoveMethod));
        }

        private string BuildEventDisplayName ()
        {
            var sb = new StringBuilder ();

            sb.Append (Event.Name);
            sb.Append (" : ");
            sb.Append (Event.EventType.Name);

            return sb.ToString ();
        }
    }
}
