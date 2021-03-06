﻿using System.ComponentModel;
using System.Windows.Forms;
using MongoGUICtl;
namespace FunctionForm.Aggregation
{
    partial class FrmAddStage
    {

            /// <summary>
            /// Required designer variable.
            /// </summary>
            private IContainer components = null;

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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.trvNewStage = new MongoGUICtl.CtlTreeViewColumns();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdPreview = new System.Windows.Forms.Button();
            this.txtDocument = new ICSharpCode.TextEditor.TextEditorControl();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(607, 311);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(72, 23);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Tag = "Common_OK";
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(684, 311);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(72, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Tag = "Common_Cancel";
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // trvNewStage
            // 
            this.trvNewStage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(173)))), ((int)(((byte)(179)))));
            this.trvNewStage.Location = new System.Drawing.Point(453, 41);
            this.trvNewStage.Margin = new System.Windows.Forms.Padding(4);
            this.trvNewStage.Name = "trvNewStage";
            this.trvNewStage.Padding = new System.Windows.Forms.Padding(1);
            this.trvNewStage.Size = new System.Drawing.Size(480, 263);
            this.trvNewStage.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Stage Expression:";
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(530, 311);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(72, 23);
            this.cmdPreview.TabIndex = 5;
            this.cmdPreview.Tag = "Common_Preview";
            this.cmdPreview.Text = "Preview";
            this.cmdPreview.UseVisualStyleBackColor = true;
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // txtDocument
            // 
            this.txtDocument.AutoScroll = true;
            this.txtDocument.IsReadOnly = false;
            this.txtDocument.Location = new System.Drawing.Point(14, 41);
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new System.Drawing.Size(412, 263);
            this.txtDocument.TabIndex = 6;
            // 
            // FrmAddStage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(946, 346);
            this.Controls.Add(this.txtDocument);
            this.Controls.Add(this.cmdPreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trvNewStage);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmAddStage";
            this.Text = "Add Stage(s)";
            this.Load += new System.EventHandler(this.frmAddStage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion
            private Button cmdOK;
            private Button cmdCancel;
            private CtlTreeViewColumns trvNewStage;
            private Label label1;
            private Button cmdPreview;
        private ICSharpCode.TextEditor.TextEditorControl txtDocument;
    }
}