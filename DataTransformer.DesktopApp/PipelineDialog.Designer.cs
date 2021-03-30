
namespace DataTransformer.DesktopApp
{
    partial class PipelineDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PipelineDialog));
            this.nameLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.availablePluginsGroup = new System.Windows.Forms.GroupBox();
            this.availablePluginsList = new System.Windows.Forms.ListView();
            this.pipelineDialogImageList = new System.Windows.Forms.ImageList(this.components);
            this.pipelinePluginsGroup = new System.Windows.Forms.GroupBox();
            this.pipelinePluginsList = new System.Windows.Forms.ListView();
            this.addPluginsButton = new System.Windows.Forms.Button();
            this.removePluginsButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.availablePluginsGroup.SuspendLayout();
            this.pipelinePluginsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(13, 13);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(84, 15);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Pipeline Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 10);
            this.textBox1.MaxLength = 100;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(685, 23);
            this.textBox1.TabIndex = 1;
            // 
            // availablePluginsGroup
            // 
            this.availablePluginsGroup.Controls.Add(this.availablePluginsList);
            this.availablePluginsGroup.Location = new System.Drawing.Point(13, 64);
            this.availablePluginsGroup.Name = "availablePluginsGroup";
            this.availablePluginsGroup.Size = new System.Drawing.Size(299, 333);
            this.availablePluginsGroup.TabIndex = 2;
            this.availablePluginsGroup.TabStop = false;
            this.availablePluginsGroup.Text = "Available Plugins";
            // 
            // availablePluginsList
            // 
            this.availablePluginsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.availablePluginsList.HideSelection = false;
            this.availablePluginsList.Location = new System.Drawing.Point(3, 19);
            this.availablePluginsList.Name = "availablePluginsList";
            this.availablePluginsList.Size = new System.Drawing.Size(293, 311);
            this.availablePluginsList.SmallImageList = this.pipelineDialogImageList;
            this.availablePluginsList.TabIndex = 0;
            this.availablePluginsList.UseCompatibleStateImageBehavior = false;
            this.availablePluginsList.View = System.Windows.Forms.View.List;
            // 
            // pipelineDialogImageList
            // 
            this.pipelineDialogImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.pipelineDialogImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("pipelineDialogImageList.ImageStream")));
            this.pipelineDialogImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.pipelineDialogImageList.Images.SetKeyName(0, "plugin");
            // 
            // pipelinePluginsGroup
            // 
            this.pipelinePluginsGroup.Controls.Add(this.pipelinePluginsList);
            this.pipelinePluginsGroup.Location = new System.Drawing.Point(489, 64);
            this.pipelinePluginsGroup.Name = "pipelinePluginsGroup";
            this.pipelinePluginsGroup.Size = new System.Drawing.Size(299, 330);
            this.pipelinePluginsGroup.TabIndex = 3;
            this.pipelinePluginsGroup.TabStop = false;
            this.pipelinePluginsGroup.Text = "Plugins in pipeline";
            // 
            // pipelinePluginsList
            // 
            this.pipelinePluginsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pipelinePluginsList.HideSelection = false;
            this.pipelinePluginsList.Location = new System.Drawing.Point(3, 19);
            this.pipelinePluginsList.Name = "pipelinePluginsList";
            this.pipelinePluginsList.Size = new System.Drawing.Size(293, 308);
            this.pipelinePluginsList.SmallImageList = this.pipelineDialogImageList;
            this.pipelinePluginsList.TabIndex = 1;
            this.pipelinePluginsList.UseCompatibleStateImageBehavior = false;
            this.pipelinePluginsList.View = System.Windows.Forms.View.List;
            // 
            // addPluginsButton
            // 
            this.addPluginsButton.Location = new System.Drawing.Point(318, 128);
            this.addPluginsButton.Name = "addPluginsButton";
            this.addPluginsButton.Size = new System.Drawing.Size(165, 23);
            this.addPluginsButton.TabIndex = 4;
            this.addPluginsButton.Text = "Add selected Plugins >";
            this.addPluginsButton.UseVisualStyleBackColor = true;
            this.addPluginsButton.Click += new System.EventHandler(this.AddPluginsButton_Click);
            // 
            // removePluginsButton
            // 
            this.removePluginsButton.Location = new System.Drawing.Point(318, 167);
            this.removePluginsButton.Name = "removePluginsButton";
            this.removePluginsButton.Size = new System.Drawing.Size(165, 23);
            this.removePluginsButton.TabIndex = 5;
            this.removePluginsButton.Text = "< Remove selected Plugins";
            this.removePluginsButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(634, 415);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(715, 415);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // PipelineDialog
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.removePluginsButton);
            this.Controls.Add(this.addPluginsButton);
            this.Controls.Add(this.pipelinePluginsGroup);
            this.Controls.Add(this.availablePluginsGroup);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PipelineDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "{Operation} Pipeline";
            this.Load += new System.EventHandler(this.PipelineDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PipelineDialog_KeyDown);
            this.availablePluginsGroup.ResumeLayout(false);
            this.pipelinePluginsGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox availablePluginsGroup;
        private System.Windows.Forms.GroupBox pipelinePluginsGroup;
        private System.Windows.Forms.ListView availablePluginsList;
        private System.Windows.Forms.ListView pipelinePluginsList;
        private System.Windows.Forms.Button addPluginsButton;
        private System.Windows.Forms.Button removePluginsButton;
        private System.Windows.Forms.ImageList pipelineDialogImageList;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}