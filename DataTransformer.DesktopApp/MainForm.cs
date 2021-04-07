using DataTransformer.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTransformer.DesktopApp
{
    public partial class MainForm : Form
    {
        private readonly IPipelineRepository _pipelineRepository;
        private readonly IPipelineExecuter _pipelineExecuter;
        private readonly IPipelineDialogFactory _pipelineDialogFactory;

        public MainForm(IPipelineRepository pipelineRepository, IPipelineExecuter pipelineExecuter, IPipelineDialogFactory pipelineDialogFactory)
        {
            _pipelineRepository = pipelineRepository ?? throw new ArgumentNullException(nameof(pipelineRepository));
            _pipelineExecuter = pipelineExecuter ?? throw new ArgumentNullException(nameof(pipelineExecuter));
            _pipelineDialogFactory = pipelineDialogFactory ?? throw new ArgumentNullException(nameof(pipelineDialogFactory));
            _pipelineExecuter.Progress += PipelineService_Progress;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () =>
            {
                // Load all pipelines
                statusLabel.Text = "Loading all pipelines...";
                var pipelines = await _pipelineRepository.GetAllPipelineNames();
                BindPipelinesList(pipelines);

                statusLabel.Text = "Ready.";
            }));
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            transformButton.BeginInvoke(new Action(async () =>
            {
                var selectedPipelineItem = GetSelectedPipelineItem();
                if (selectedPipelineItem == null) return;

                // execute the selected pipeline
                var selectedPipelineName = selectedPipelineItem.Text;
                var output = await _pipelineExecuter.Execute(selectedPipelineName, inputTextBox.Text);
                outputTextBox.Text = output;
            }));
        }

        private void PipelineService_Progress(object sender, Models.PipelineProgressEventArgs e)
        {
            progress.Value = e.PercentProgress;
            statusLabel.Text = e.StatusMessage;
        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text = outputTextBox.Text;
        }

        private void AddPipelineButton_Click(object sender, EventArgs e)
        {
            addPipelineButton.Invoke(new Action(async () =>
            {
                var dialog = _pipelineDialogFactory.Create();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Save pipeline
                    var pipeline = dialog.Tag as Models.Pipeline;
                    await DoPipelineOperation(repo => repo.SavePipeline(pipeline));
                }
            }));
        }

        private void EditPipelineButton_Click(object sender, EventArgs e)
        {
            editPipelineButton.Invoke(new Action(async () => {
                // Get selected pipeline
                var selectedPipelineItem = GetSelectedPipelineItem();
                if (selectedPipelineItem == null) return;

                // Show dialog to edit it
                var pipeline = await _pipelineRepository.GetPipelineByName(selectedPipelineItem.Text);
                var dialog = _pipelineDialogFactory.Create();
                dialog.Tag = pipeline;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Save pipeline
                    pipeline = dialog.Tag as Models.Pipeline;
                    await DoPipelineOperation(repo => repo.SavePipeline(pipeline));
                }
            }));
        }

        private void RemovePipelineButton_Click(object sender, EventArgs e)
        {
            removePipelineButton.Invoke(new Action(async () => {
                // Get selected pipeline
                var selectedPipelineItem = GetSelectedPipelineItem();
                if (selectedPipelineItem == null) return;

                // Remove pipeline
                var pipelineName = selectedPipelineItem.Text;
                await DoPipelineOperation(repo => repo.DeletePipeline(pipelineName));
            }));
        }

        private void BindPipelinesList(IEnumerable<string> pipelineNames)
        {
            pipelinesList.Items.Clear();
            Array.ForEach(pipelineNames.ToArray(), pipeline => pipelinesList.Items.Add(pipeline));
            if(pipelinesList.Items.Count > 0) pipelinesList.SelectedIndices.Add(0);
        }

        private ListViewItem GetSelectedPipelineItem()
        {
            // check if any pipeline is selected
            if (pipelinesList.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Must select a pipeline.", "No pipeline selected.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return pipelinesList.SelectedItems[0];
        }

        private async Task DoPipelineOperation(Func<IPipelineRepository, Task> operation)
        {
            // Save/Delete pipeline
            await operation(_pipelineRepository);

            // Reload list
            var pipelines = await _pipelineRepository.GetAllPipelineNames();
            BindPipelinesList(pipelines);
        }
    }
}
