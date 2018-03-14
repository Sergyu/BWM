using Common;
using DesktopClient.UIControls;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Debugger = Common.Singleton<DesktopClient.ModelDebugger>;

namespace DesktopClient
{
    public partial class MainForm : Form
    {

        public const string SolutionLocation = @"E:\Projects\BWMT\BWM\DesktopClient\SigningModel";

        public static IUIModel Model
        {
            get
            {
                return Singleton<SigningModel.SigningModel>.Instance;
            }
        }

        public MainForm()
        {
            InitializeComponent();
            this.fileSystemTree.AfterCollapse += FileSystemTree_AfterCollapse;
            this.fileSystemTree.AfterExpand += FileSystemTree_AfterExpand;
            btnPauseDebug.Enabled = false;
            btnStopDebug.Enabled = false;

            Debugger.Instance.SetParent(Model.ModelImage);
            Debugger.Instance.OnStart += Debugger_OnStart;
            Debugger.Instance.OnStop += Debugger_OnStop;
            Debugger.Instance.OnPause += Debugger_OnPause;
            Debugger.Instance.OnContinue += Debugger_OnContinue;
        }

       

        private void FileSystemTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex = 1;
        }

        private void FileSystemTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.ImageIndex = 0;
            e.Node.SelectedImageIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListDirectory(fileSystemTree,SolutionLocation);
        }

        private void Designer1_Click(object sender, EventArgs e)
        {

        }

        private void ControlsTab_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            treeView.Nodes[0].Text = $"Solution '{treeView.Nodes[0].Text}'";
            treeView.ExpandAll();

            treeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.panelNodeEditor.Visible = false;
            var uiNode = (ModelTreeNode)e.Node;
            var ctrlToRender = uiNode.Renderer.GetRawEditor();
            addNewTab(e.Node.Text, ctrlToRender);
            ctrlToRender.Dock = DockStyle.Fill;
        }

        private void addNewTab(string fileName, Control tabArea)
        {
            var tab = new FATabStripItem(Path.GetFileName(fileName), tabArea);
            tab.Tag = fileName;
            tsFiles.AddTab(tab);
            tsFiles.SelectedItem = tab;
        }


        FastColoredTextBox CurrentTB
        {
            get
            {
                if (tsFiles.SelectedItem == null || tsFiles.SelectedItem.Controls.Count == 0 || null == (tsFiles.SelectedItem.Controls[0]))
                    return null;
                return (tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
            }

            set
            {
                tsFiles.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
        }

      
        private bool Save(FATabStripItem tab)
        {
            var tb = (tab.Controls[0] as FastColoredTextBox);
            try
            {
                File.WriteAllText(tab.Tag as string, tb.Text);
                tb.IsChanged = false;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return Save(tab);
                else
                    return false;
            }

            tb.Invalidate();

            return true;
        }

       


        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var dir = CreateDirectoryNode(directory);
                directoryNode.Nodes.Add(dir);
            }
                

            foreach (var file in directoryInfo.GetFiles())
            {
                var uiCtrl = Model.Nodes.FirstOrDefault(n => n.FileName.Equals(file.Name));
                if (null != uiCtrl)
                {
                    var node = new ModelTreeNode(file.Name, uiCtrl);
                    node.ImageIndex = node.SelectedImageIndex = uiCtrl.ImageIndex;
                    directoryNode.Nodes.Add(node);
                }
            }
                
            return directoryNode;
        }

        #region Debugger

        private void btnDebug_Click(object sender, EventArgs e)
        {
            Debugger.Instance.Start(Model);
        }

        private void btnPauseDebug_Click(object sender, EventArgs e)
        {
            Debugger.Instance.Pause();
        }

        private void btnStopDebug_Click(object sender, EventArgs e)
        {
            Debugger.Instance.Stop();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            Debugger.Instance.Continue();
        }


        private void Debugger_OnStart(object sender, EventArgs e)
        {
            while (tsFiles.Items.Count > 0)
            {
                tsFiles.RemoveTab(tsFiles.Items[0] as FATabStripItem);
            }

            var ctrlToRender = Model.GetRawEditor();
            addNewTab(Model.Name, ctrlToRender);
            ctrlToRender.Dock = DockStyle.Fill;

            btnDebug.Visible = false;
            btnContinueDebug.Visible = true;
            btnContinueDebug.Enabled = false;
            btnPauseDebug.Enabled = true;
            btnStopDebug.Enabled = true;
        }

        private void Debugger_OnPause(object sender, EventArgs e)
        {
            btnContinueDebug.Enabled = true;
            btnPauseDebug.Enabled = false;
        }

        private void Debugger_OnContinue(object sender, EventArgs e)
        {
            btnContinueDebug.Enabled = false;
            btnPauseDebug.Enabled = true;
        }


        private void Debugger_OnStop(object sender, EventArgs e)
        {
            btnDebug.Visible = true;
            btnContinueDebug.Visible = false;
            btnPauseDebug.Enabled = false;
            btnStopDebug.Enabled = false;
        }
        #endregion

    }
}
