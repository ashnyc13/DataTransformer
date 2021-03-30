using DataTransformer.Core.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DataTransformer.DesktopApp
{
    public partial class PipelineDialog : Form
    {
        private readonly IPluginLoader _pluginLoader;

        public PipelineDialog(IPluginLoader pluginLoader)
        {
            InitializeComponent();
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
        }

        private async void PipelineDialog_Load(object sender, EventArgs e)
        {
            var allPlugins = await _pluginLoader.LoadAllPlugins();
            foreach (var plugin in allPlugins)
            {
                var listViewItem = new ListViewItem(plugin.Name, "plugin")
                {
                    Tag = plugin
                };
                availablePluginsList.Items.Add(listViewItem);
            }
        }

        private void AddPluginsButton_Click(object sender, EventArgs e)
        {
            addPluginsButton.BeginInvoke(new Action(() =>
            {
                // Check if any items have been selected
                var selectedItems = availablePluginsList.SelectedItems.Cast<ListViewItem>().ToArray();
                if (selectedItems == null || !selectedItems.Any())
                {
                    MessageBox.Show(this, "No items selected", "Add selected plugins",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Add those items to the right side
                foreach (var item in selectedItems)
                {
                    pipelinePluginsList.Items.Add(item.Clone() as ListViewItem);
                }
            }));
        }

        private void PipelineDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
