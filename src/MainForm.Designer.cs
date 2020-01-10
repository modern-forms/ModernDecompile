using System;
using System.Collections.Generic;
using System.Text;
using Modern.Forms;

namespace ModernDecompile
{
    public partial class MainForm : Form
    {
        private ToolBar toolbar;
        private TreeView tree;
        private TextBox view;
        private StatusBar status_bar;

        public void InitializeComponent ()
        {
            Text = "Decompile.Core";
            Image = ResourceManager.GetEmbeddedImage ("module.png");

            // SplitContainer
            var split_container = Controls.Add (new SplitContainer { SplitterWidth = 3 });
            split_container.Panel1.Width = 300;

            // TextBox
            view = split_container.Panel2.Controls.Add (new TextBox { Dock = DockStyle.Fill, MultiLine = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical });

            view.Style.Border.Left.Width = 1;
            view.Style.Border.Top.Width = 0;
            view.Style.Border.Bottom.Width = 0;

            // TreeControl
            tree = new TreeView {
                Dock = DockStyle.Fill,
                VirtualMode = true
            };

            tree.Style.Border.Top.Width = 0;
            tree.Style.Border.Left.Width = 0;
            tree.Style.Border.Bottom.Width = 0;

            split_container.Panel1.Controls.Add (tree);

            // ToolBar
            toolbar = Controls.Add (new ToolBar ());

            toolbar.Items.Add (new MenuItem ("Open Assembly", ResourceManager.GetEmbeddedImage ("folder-closed.png"), new EventHandler<MouseEventArgs> (OpenAssembly_Clicked)));

            var language_dropdown = toolbar.Items.Add (new MenuItem ("Language", ResourceManager.GetEmbeddedImage ("widgets.png")));

            var csharp = language_dropdown.Items.Add ("C#");
            csharp.Click += (o, e) => ChangeLanguage (DecompileLanguage.CSharp);

            var vb = language_dropdown.Items.Add ("Visual Basic");
            vb.Click += (o, e) => ChangeLanguage (DecompileLanguage.VisualBasic);

            // StatusBar
            status_bar = Controls.Add (new StatusBar ());
        }
    }
}
