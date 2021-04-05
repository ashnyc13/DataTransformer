using DataTransformer.Core.Plugin;
using DataTransformer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DataTransformer.DesktopApp
{
    public partial class PipelineDialog : Form
    {
        private readonly IPluginLoader _pluginLoader;
        private readonly List<IPlugin> _selectedPlugins = new();

        public PipelineDialog(IPluginLoader pluginLoader)
        {
            InitializeComponent();
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
        }

        private async void PipelineDialog_Load(object sender, EventArgs e)
        {
            var allPlugins = await _pluginLoader.LoadAllPlugins();
            BindPluginsToListView(allPlugins, availablePluginsList);
            BindPluginsToListView(_selectedPlugins, pipelinePluginsList);
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

                // Add those items to the selected list
                _selectedPlugins.AddRange(selectedItems.Select(item => item.Tag as IPlugin));

                // Validate the list

                // Re-bind
                BindPluginsToListView(_selectedPlugins, pipelinePluginsList);
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

        private static void BindPluginsToListView(IEnumerable<IPlugin> plugins, ListView listView)
        {
            listView.Clear();
            foreach (var plugin in plugins)
            {
                var listViewItem = new ListViewItem(plugin.Name, "plugin")
                {
                    Tag = plugin
                };
                listView.Items.Add(listViewItem);
            }
        }
    }
}
