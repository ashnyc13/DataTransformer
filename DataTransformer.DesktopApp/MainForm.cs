using DataTransformer.Core.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DataTransformer.DesktopApp
{
    public partial class MainForm : Form
    {
        private readonly IPipelineService _pipelineService;
        private readonly IPipelineExecuter _pipelineExecuter;
        private readonly IPipelineDialogFactory _pipelineDialogFactory;

        public MainForm(IPipelineService pipelineService, IPipelineExecuter pipelineExecuter, IPipelineDialogFactory pipelineDialogFactory)
        {
            _pipelineService = pipelineService ?? throw new ArgumentNullException(nameof(pipelineService));
            _pipelineExecuter = pipelineExecuter ?? throw new ArgumentNullException(nameof(pipelineExecuter));
            _pipelineDialogFactory = pipelineDialogFactory ?? throw new ArgumentNullException(nameof(pipelineDialogFactory));
            _pipelineExecuter.Progress += PipelineService_Progress;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(async () => {
                // Load all pipelines
                statusLabel.Text = "Loading all pipelines...";
                var pipelines = await _pipelineService.GetAllPipelineNames();
                Array.ForEach(pipelines.ToArray(),
                    pipeline => pipelinesList.Items.Add(pipeline));
                pipelinesList.SelectedIndices.Add(0);
                statusLabel.Text = "Ready.";
            }));
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            transformButton.BeginInvoke(new Action(async () => {
                // execute the selected pipeline
                var selectedPipelineName = pipelinesList.SelectedItems[0].Text;
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
            var dialog = _pipelineDialogFactory.Create();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(this, "Save clicked");
            }
        }
    }
}
