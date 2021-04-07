using DataTransformer.Core.Pipeline;
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
        private readonly IPipelineValidator _pipelineValidator;
        private readonly IPipelineFactory _pipelineFactory;
        private readonly IPluginMetadataRepository _pluginMetadataRepository;

        public PipelineDialog(IPluginLoader pluginLoader, IPipelineValidator pipelineValidator,
            IPipelineFactory pipelineFactory, IPluginMetadataRepository pluginMetadataRepository)
        {
            InitializeComponent();
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _pipelineValidator = pipelineValidator ?? throw new ArgumentNullException(nameof(pipelineValidator));
            _pipelineFactory = pipelineFactory ?? throw new ArgumentNullException(nameof(pipelineFactory));
            _pluginMetadataRepository = pluginMetadataRepository ?? throw new ArgumentNullException(nameof(pluginMetadataRepository));
        }

        private async void PipelineDialog_Load(object sender, EventArgs e)
        {
            // Populate all plugins list.
            var allPlugins = await _pluginLoader.LoadAllPlugins();
            BindPluginsToListView(allPlugins, availablePluginsList);

            // Set dialog title.
            var pipeline = Tag as Pipeline;
            Text = Text.Replace("{Operation}", pipeline == null ? "Create" : "Edit");

            // Set the pipeline name
            pipelineNameTextBox.Text = pipeline.Name;

            // Populate pipeline plugins list.
            if (pipeline == null) pipeline = _pipelineFactory.CreateNew();
            BindPluginsToListView(pipeline.Plugins, pipelinePluginsList);
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
            saveButton.Invoke(new Action(async () =>
            {
                saveButton.Enabled = false;

                // Create pipeline instance
                var selectedPlugins = pipelinePluginsList.Items.Cast<ListViewItem>().Select(item => item.Tag as IPlugin);
                var pipelineName = pipelineNameTextBox.Text.Trim();
                var pipeline = _pipelineFactory.Create(pipelineName, selectedPlugins);

                // Validate it
                var result = await _pipelineValidator.Validate(pipeline);

                // Show error
                if (!result.IsValid)
                {
                    saveButton.Enabled = true;
                    MessageBox.Show(this, result.Error, "Save pipeline.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Show warning
                if (!string.IsNullOrEmpty(result.Warning) &&
                    MessageBox.Show(this, result.Warning, "Are you sure to proceed with saving?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    saveButton.Enabled = true;
                    return;
                }

                // Return it
                Tag = pipeline;
                DialogResult = DialogResult.OK;
                Close();
            }));
        }

        private void BindPluginsToListView(IEnumerable<IPlugin> plugins, ListView listView)
        {
            listView.Items.Clear();
            if (plugins == null) return;
            foreach (var plugin in plugins)
            {
                var metadata = _pluginMetadataRepository.Get(plugin);
                var listViewItem = new ListViewItem(new[] { plugin.Name, metadata.InputType.Name, metadata.OutputType.Name },
                    "plugin")
                {
                    Tag = plugin
                };
                listView.Items.Add(listViewItem);
            }
        }
    }
}
