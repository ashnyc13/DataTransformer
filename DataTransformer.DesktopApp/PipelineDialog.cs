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
        private readonly IPluginListValidator _pluginListValidator;

        public PipelineDialog(IPluginLoader pluginLoader, IPluginListValidator pluginListValidator)
        {
            InitializeComponent();
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _pluginListValidator = pluginListValidator ?? throw new ArgumentNullException(nameof(pluginListValidator));
        }

        private async void PipelineDialog_Load(object sender, EventArgs e)
        {
            var allPlugins = await _pluginLoader.LoadAllPlugins();
            BindPluginsToListView(allPlugins, availablePluginsList);
            //BindPluginsToListView(_selectedPlugins, pipelinePluginsList);
        }

        private void PipelineDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
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
                var clonedItems = selectedItems.Select(item => item.Clone() as ListViewItem);
                pipelinePluginsList.Items.AddRange(clonedItems.ToArray());
            }));
        }

        private void RemovePluginsButton_Click(object sender, EventArgs e)
        {
            removePluginsButton.BeginInvoke(new Action(() =>
            {
                // Check if any items have been selected
                var selectedItems = pipelinePluginsList.SelectedItems.Cast<ListViewItem>().ToArray();
                if (selectedItems == null || !selectedItems.Any())
                {
                    MessageBox.Show(this, "No items selected", "Remove selected plugins",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Remove selected items
                Array.ForEach(selectedItems, item => item.Remove());
            }));

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Validate plugin list
            var selectedPlugins = pipelinePluginsList.Items.Cast<ListViewItem>().Select(item => item.Tag as IPlugin);
            var result = _pluginListValidator.Validate(selectedPlugins);
            if (!result.IsValid)
            {
                MessageBox.Show(this, result.Error, "Invalid selection.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private static void BindPluginsToListView(IEnumerable<IPlugin> plugins, ListView listView)
        {
            listView.Items.Clear();
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
