namespace DataTransformer.DesktopApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.inputGroupBox = new System.Windows.Forms.GroupBox();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.transferButton = new System.Windows.Forms.Button();
            this.pipelinesGroup = new System.Windows.Forms.GroupBox();
            this.pipelinesList = new System.Windows.Forms.ListView();
            this.transformButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.managePipelines = new System.Windows.Forms.GroupBox();
            this.removePipelineButton = new System.Windows.Forms.Button();
            this.editPipelineButton = new System.Windows.Forms.Button();
            this.addPipelineButton = new System.Windows.Forms.Button();
            this.buttonsImageList = new System.Windows.Forms.ImageList(this.components);
            this.reverseCheckbox = new System.Windows.Forms.CheckBox();
            this.inputGroupBox.SuspendLayout();
            this.outputGroupBox.SuspendLayout();
            this.pipelinesGroup.SuspendLayout();
            this.managePipelines.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputGroupBox
            // 
            this.inputGroupBox.Controls.Add(this.inputTextBox);
            this.inputGroupBox.Location = new System.Drawing.Point(13, 13);
            this.inputGroupBox.Name = "inputGroupBox";
            this.inputGroupBox.Size = new System.Drawing.Size(517, 196);
            this.inputGroupBox.TabIndex = 0;
            this.inputGroupBox.TabStop = false;
            this.inputGroupBox.Text = "Input";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTextBox.Location = new System.Drawing.Point(3, 19);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.PlaceholderText = "Type / paste input data here";
            this.inputTextBox.Size = new System.Drawing.Size(511, 174);
            this.inputTextBox.TabIndex = 0;
            // 
            // outputGroupBox
            // 
            this.outputGroupBox.Controls.Add(this.outputTextBox);
            this.outputGroupBox.Location = new System.Drawing.Point(13, 310);
            this.outputGroupBox.Name = "outputGroupBox";
            this.outputGroupBox.Size = new System.Drawing.Size(517, 196);
            this.outputGroupBox.TabIndex = 1;
            this.outputGroupBox.TabStop = false;
            this.outputGroupBox.Text = "Output";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Location = new System.Drawing.Point(3, 19);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.PlaceholderText = "Output data will be available here after processing";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(511, 174);
            this.outputTextBox.TabIndex = 1;
            // 
            // transferButton
            // 
            this.transferButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.transferButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.transferButton.ImageKey = "upArrow";
            this.transferButton.ImageList = this.buttonsImageList;
            this.transferButton.Location = new System.Drawing.Point(87, 227);
            this.transferButton.Name = "transferButton";
            this.transferButton.Size = new System.Drawing.Size(65, 64);
            this.transferButton.TabIndex = 2;
            this.transferButton.Text = "Copy to input";
            this.transferButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.transferButton.UseVisualStyleBackColor = true;
            this.transferButton.Click += new System.EventHandler(this.TransferButton_Click);
            // 
            // pipelinesGroup
            // 
            this.pipelinesGroup.Controls.Add(this.pipelinesList);
            this.pipelinesGroup.Location = new System.Drawing.Point(544, 13);
            this.pipelinesGroup.Name = "pipelinesGroup";
            this.pipelinesGroup.Size = new System.Drawing.Size(362, 378);
            this.pipelinesGroup.TabIndex = 3;
            this.pipelinesGroup.TabStop = false;
            this.pipelinesGroup.Text = "Available pipelines";
            // 
            // pipelinesList
            // 
            this.pipelinesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pipelinesList.HideSelection = false;
            this.pipelinesList.Location = new System.Drawing.Point(3, 19);
            this.pipelinesList.MultiSelect = false;
            this.pipelinesList.Name = "pipelinesList";
            this.pipelinesList.Size = new System.Drawing.Size(356, 356);
            this.pipelinesList.TabIndex = 0;
            this.pipelinesList.UseCompatibleStateImageBehavior = false;
            this.pipelinesList.View = System.Windows.Forms.View.List;
            // 
            // transformButton
            // 
            this.transformButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.transformButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.transformButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.transformButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.transformButton.ImageKey = "downArrow";
            this.transformButton.ImageList = this.buttonsImageList;
            this.transformButton.Location = new System.Drawing.Point(16, 227);
            this.transformButton.Name = "transformButton";
            this.transformButton.Size = new System.Drawing.Size(65, 64);
            this.transformButton.TabIndex = 4;
            this.transformButton.Text = "Execute Pipeline";
            this.transformButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.transformButton.UseVisualStyleBackColor = true;
            this.transformButton.Click += new System.EventHandler(this.TransformButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(547, 459);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(42, 15);
            this.statusLabel.TabIndex = 5;
            this.statusLabel.Text = "Ready.";
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(544, 479);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(359, 23);
            this.progress.TabIndex = 6;
            // 
            // managePipelines
            // 
            this.managePipelines.Controls.Add(this.removePipelineButton);
            this.managePipelines.Controls.Add(this.editPipelineButton);
            this.managePipelines.Controls.Add(this.addPipelineButton);
            this.managePipelines.Location = new System.Drawing.Point(547, 398);
            this.managePipelines.Name = "managePipelines";
            this.managePipelines.Size = new System.Drawing.Size(356, 58);
            this.managePipelines.TabIndex = 7;
            this.managePipelines.TabStop = false;
            this.managePipelines.Text = "Manage pipelines";
            // 
            // removePipelineButton
            // 
            this.removePipelineButton.Location = new System.Drawing.Point(169, 22);
            this.removePipelineButton.Name = "removePipelineButton";
            this.removePipelineButton.Size = new System.Drawing.Size(75, 23);
            this.removePipelineButton.TabIndex = 2;
            this.removePipelineButton.Text = "Remove";
            this.removePipelineButton.UseVisualStyleBackColor = true;
            this.removePipelineButton.Click += new System.EventHandler(this.RemovePipelineButton_Click);
            // 
            // editPipelineButton
            // 
            this.editPipelineButton.Location = new System.Drawing.Point(88, 23);
            this.editPipelineButton.Name = "editPipelineButton";
            this.editPipelineButton.Size = new System.Drawing.Size(75, 23);
            this.editPipelineButton.TabIndex = 1;
            this.editPipelineButton.Text = "Edit";
            this.editPipelineButton.UseVisualStyleBackColor = true;
            this.editPipelineButton.Click += new System.EventHandler(this.EditPipelineButton_Click);
            // 
            // addPipelineButton
            // 
            this.addPipelineButton.Location = new System.Drawing.Point(7, 23);
            this.addPipelineButton.Name = "addPipelineButton";
            this.addPipelineButton.Size = new System.Drawing.Size(75, 23);
            this.addPipelineButton.TabIndex = 0;
            this.addPipelineButton.Text = "Add";
            this.addPipelineButton.UseVisualStyleBackColor = true;
            this.addPipelineButton.Click += new System.EventHandler(this.AddPipelineButton_Click);
            // 
            // buttonsImageList
            // 
            this.buttonsImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.buttonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("buttonsImageList.ImageStream")));
            this.buttonsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.buttonsImageList.Images.SetKeyName(0, "upArrow");
            this.buttonsImageList.Images.SetKeyName(1, "downArrow");
            // 
            // reverseCheckbox
            // 
            this.reverseCheckbox.AutoSize = true;
            this.reverseCheckbox.Location = new System.Drawing.Point(159, 271);
            this.reverseCheckbox.Name = "reverseCheckbox";
            this.reverseCheckbox.Size = new System.Drawing.Size(165, 19);
            this.reverseCheckbox.TabIndex = 8;
            this.reverseCheckbox.Text = "Execute pipeline in reverse";
            this.reverseCheckbox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 518);
            this.Controls.Add(this.reverseCheckbox);
            this.Controls.Add(this.managePipelines);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.transformButton);
            this.Controls.Add(this.pipelinesGroup);
            this.Controls.Add(this.transferButton);
            this.Controls.Add(this.outputGroupBox);
            this.Controls.Add(this.inputGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.inputGroupBox.ResumeLayout(false);
            this.inputGroupBox.PerformLayout();
            this.outputGroupBox.ResumeLayout(false);
            this.outputGroupBox.PerformLayout();
            this.pipelinesGroup.ResumeLayout(false);
            this.managePipelines.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox inputGroupBox;
        private System.Windows.Forms.GroupBox outputGroupBox;
        private System.Windows.Forms.Button transferButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.GroupBox pipelinesGroup;
        private System.Windows.Forms.ListView pipelinesList;
        private System.Windows.Forms.Button transformButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.GroupBox managePipelines;
        private System.Windows.Forms.Button addPipelineButton;
        private System.Windows.Forms.Button editPipelineButton;
        private System.Windows.Forms.Button removePipelineButton;
        private System.Windows.Forms.ImageList buttonsImageList;
        private System.Windows.Forms.CheckBox reverseCheckbox;
    }
}

