using DataTransformer.Core.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DataTransformer
{
    public partial class MainForm : Form
    {
        private readonly IPipelineService _pipelineService;

        public MainForm(IPipelineService pipelineService)
        {
            _pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            _pipelineService.Progress += PipelineService_Progress;
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            // Load all pipelines
            statusLabel.Text = "Loading all pipelines...";
            var pipelines = await _pipelineService.GetAllPipelineNames();
            Array.ForEach(pipelines.ToArray(),
                pipeline => pipelinesList.Items.Add(pipeline));
            pipelinesList.SelectedIndices.Add(0);
            statusLabel.Text = "Ready.";
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            transferButton.BeginInvoke(new Action(async () => {
                // execute the selected pipeline
                var selectedPipelineName = pipelinesList.SelectedItems[0].Text;
                var output = await _pipelineService.Execute(selectedPipelineName, inputTextBox.Text);
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
    }
}
