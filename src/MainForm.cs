using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Modern.Forms;
using Mono.Cecil;

namespace ModernDecompile
{
    public partial class MainForm : Form
    {
        private DecompileLanguage current_language = DecompileLanguage.CSharp;
        private readonly string[] initial_assemblies;

        public MainForm (string[] assemblies)
        {
            InitializeComponent ();

            initial_assemblies = assemblies;

            tree.BeforeExpand += (o, e) => { if (e.Value is BaseTreeViewItem item) item.OnDoExpand (); };
            tree.ItemSelected += Tree_ItemSelected;

            Shown += MainForm_Shown;
        }

        protected override Size DefaultSize => new Size (1200, 768);

        private void MainForm_Shown (object? sender, EventArgs e)
        {
            // Load any assemblies from the command line
            foreach (var assembly in initial_assemblies)
                if (File.Exists (assembly))
                    LoadAssembly (assembly);
        }

        private async void OpenAssembly_Clicked (object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog {
                Title = "Open Assembly",
                AllowMultiple = true
            };

            ofd.AddFilter ("Assembly files", "dll", "exe");

            if ((await ofd.ShowDialog (this)) == DialogResult.OK)
                foreach (var file in ofd.FileNames)
                    LoadAssembly (file);
        }

        private void CloseAssembly (AssemblyDefinition assembly)
        {
            var item = tree.Items.Cast<AssemblyTreeViewItem> ().First (i => i.Assembly == assembly);

            tree.Items.Remove (item);

            if (status_bar.Text == item.ToString ())
                status_bar.Text = string.Empty;
        }

        private void Tree_ItemSelected (object? sender, EventArgs<TreeViewItem> e)
        {
            UpdateDecompilation (e.Value);
        }

        private void ChangeLanguage (DecompileLanguage language)
        {
            current_language = language;

            if (tree.SelectedItem != null)
                UpdateDecompilation (tree.SelectedItem);
        }

        private void UpdateDecompilation (TreeViewItem item)
        {
            view.Text = ((BaseTreeViewItem)item).GetBody (current_language).Replace ("\t", "    ");

            var root = (TreeViewItem?)item;

            while (root != null && !(root is AssemblyTreeViewItem))
                root = root.Parent;

            if (root is AssemblyTreeViewItem atvi) {
                var assembly = atvi.Assembly;
                status_bar.Text = assembly.ToString ();
            } else
                status_bar.Text = string.Empty;
        }

        private void LoadAssembly (string filename)
        {
            try {
                var assembly = DecompilerHelper.GetAssembly (filename);

                if (assembly != null) {
                    var item = tree.Items.Add (new AssemblyTreeViewItem (assembly));

                    item.ContextMenu = new ContextMenu ();
                    var close_menu = item.ContextMenu.Items.Add ("Close Assembly");

                    close_menu.Click += (o, ev) => CloseAssembly (assembly);
                }
            } catch (Exception ex) {
                Application.RunOnUIThread (() => {
                    var mb = new MessageBoxForm ($"Unable to load assembly {Path.GetFileName (filename)}", ex.ToString ());
                    mb.ShowDialog (this);
                });
            }
        }
    }
}
