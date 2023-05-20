#region Header
//               _,-'/-'/
//   .      __,-; ,'( '/
//    \.    `-.__`-._`:_,-._       _ , . ``
//     `:-._,------' ` _,`--` -: `_ , ` ,' :
//        `---..__,,--'  (C) 2023  ` -'. -'
//        #                Vita-Nex                 #
//  {o)xxx|================-   #   -================|xxx(o}
//        #  https://vita-nex.com/xf-addon-hasher/  #
#endregion

namespace XFAddonHasher
{
	partial class Interface
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
			components = new System.ComponentModel.Container();
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
			ViewContainer = new SplitContainer();
			AddonTree = new TreeView();
			AddonTreeIcons = new ImageList(components);
			AddonTreeActions = new ToolStrip();
			AddonTreeActions_Root = new ToolStripButton();
			HashView = new RichTextBox();
			HashViewActions = new ToolStrip();
			HashViewActions_Hash = new ToolStripButton();
			HashViewActions_Copy = new ToolStripButton();
			HashViewActions_Save = new ToolStripButton();
			HashViewActions_Version = new ToolStripLabel();
			HashViewActions_About = new ToolStripLabel();
			AddonTreeWatcher = new FileSystemWatcher();
			AddonTreeBrowser = new FolderBrowserDialog();
			HashViewSaveDialog = new SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)ViewContainer).BeginInit();
			ViewContainer.Panel1.SuspendLayout();
			ViewContainer.Panel2.SuspendLayout();
			ViewContainer.SuspendLayout();
			AddonTreeActions.SuspendLayout();
			HashViewActions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)AddonTreeWatcher).BeginInit();
			SuspendLayout();
			// 
			// ViewContainer
			// 
			ViewContainer.Dock = DockStyle.Fill;
			ViewContainer.Location = new Point(0, 0);
			ViewContainer.Name = "ViewContainer";
			// 
			// ViewContainer.Panel1
			// 
			ViewContainer.Panel1.Controls.Add(AddonTree);
			ViewContainer.Panel1.Controls.Add(AddonTreeActions);
			ViewContainer.Panel1MinSize = 200;
			// 
			// ViewContainer.Panel2
			// 
			ViewContainer.Panel2.Controls.Add(HashView);
			ViewContainer.Panel2.Controls.Add(HashViewActions);
			ViewContainer.Panel2MinSize = 400;
			ViewContainer.Size = new Size(784, 561);
			ViewContainer.SplitterDistance = 200;
			ViewContainer.TabIndex = 0;
			// 
			// AddonTree
			// 
			AddonTree.Dock = DockStyle.Fill;
			AddonTree.HideSelection = false;
			AddonTree.HotTracking = true;
			AddonTree.ImageIndex = 0;
			AddonTree.ImageList = AddonTreeIcons;
			AddonTree.Indent = 8;
			AddonTree.Location = new Point(0, 0);
			AddonTree.Name = "AddonTree";
			AddonTree.PathSeparator = "/";
			AddonTree.SelectedImageIndex = 0;
			AddonTree.ShowLines = false;
			AddonTree.ShowNodeToolTips = true;
			AddonTree.ShowPlusMinus = false;
			AddonTree.ShowRootLines = false;
			AddonTree.Size = new Size(200, 524);
			AddonTree.TabIndex = 1;
			AddonTree.AfterSelect += AddonTree_AfterSelect;
			// 
			// AddonTreeIcons
			// 
			AddonTreeIcons.ColorDepth = ColorDepth.Depth32Bit;
			AddonTreeIcons.ImageStream = (ImageListStreamer)resources.GetObject("AddonTreeIcons.ImageStream");
			AddonTreeIcons.TransparentColor = Color.Transparent;
			AddonTreeIcons.Images.SetKeyName(0, "folder.png");
			AddonTreeIcons.Images.SetKeyName(1, "document.png");
			// 
			// AddonTreeActions
			// 
			AddonTreeActions.CanOverflow = false;
			AddonTreeActions.Dock = DockStyle.Bottom;
			AddonTreeActions.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
			AddonTreeActions.GripStyle = ToolStripGripStyle.Hidden;
			AddonTreeActions.Items.AddRange(new ToolStripItem[] { AddonTreeActions_Root });
			AddonTreeActions.Location = new Point(0, 524);
			AddonTreeActions.Name = "AddonTreeActions";
			AddonTreeActions.Size = new Size(200, 37);
			AddonTreeActions.TabIndex = 0;
			AddonTreeActions.Text = "Addon Tree Actions";
			// 
			// AddonTreeActions_Root
			// 
			AddonTreeActions_Root.Image = Properties.Resources.folder;
			AddonTreeActions_Root.ImageTransparentColor = Color.White;
			AddonTreeActions_Root.Name = "AddonTreeActions_Root";
			AddonTreeActions_Root.Size = new Size(39, 34);
			AddonTreeActions_Root.Text = "Open";
			AddonTreeActions_Root.TextImageRelation = TextImageRelation.ImageAboveText;
			AddonTreeActions_Root.ToolTipText = "Select the addon root directory, usually named 'upload'";
			AddonTreeActions_Root.Click += AddonTreeActions_Root_Click;
			// 
			// HashView
			// 
			HashView.BackColor = SystemColors.Window;
			HashView.Dock = DockStyle.Fill;
			HashView.Location = new Point(0, 0);
			HashView.Name = "HashView";
			HashView.ReadOnly = true;
			HashView.Size = new Size(580, 524);
			HashView.TabIndex = 1;
			HashView.Text = "";
			HashView.WordWrap = false;
			// 
			// HashViewActions
			// 
			HashViewActions.AllowMerge = false;
			HashViewActions.CanOverflow = false;
			HashViewActions.Dock = DockStyle.Bottom;
			HashViewActions.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
			HashViewActions.GripStyle = ToolStripGripStyle.Hidden;
			HashViewActions.Items.AddRange(new ToolStripItem[] { HashViewActions_Hash, HashViewActions_Copy, HashViewActions_Save, HashViewActions_Version, HashViewActions_About });
			HashViewActions.Location = new Point(0, 524);
			HashViewActions.Name = "HashViewActions";
			HashViewActions.Size = new Size(580, 37);
			HashViewActions.Stretch = true;
			HashViewActions.TabIndex = 0;
			HashViewActions.Text = "Hash View Actions";
			// 
			// HashViewActions_Hash
			// 
			HashViewActions_Hash.Image = Properties.Resources.recycle;
			HashViewActions_Hash.ImageTransparentColor = Color.White;
			HashViewActions_Hash.Name = "HashViewActions_Hash";
			HashViewActions_Hash.Size = new Size(39, 34);
			HashViewActions_Hash.Text = "Hash";
			HashViewActions_Hash.TextImageRelation = TextImageRelation.ImageAboveText;
			HashViewActions_Hash.ToolTipText = "Regenerate the entire Json output";
			HashViewActions_Hash.Click += HashViewActions_Hash_Click;
			// 
			// HashViewActions_Copy
			// 
			HashViewActions_Copy.Image = Properties.Resources.documents;
			HashViewActions_Copy.ImageTransparentColor = Color.White;
			HashViewActions_Copy.Name = "HashViewActions_Copy";
			HashViewActions_Copy.Size = new Size(39, 34);
			HashViewActions_Copy.Text = "Copy";
			HashViewActions_Copy.TextImageRelation = TextImageRelation.ImageAboveText;
			HashViewActions_Copy.ToolTipText = "Copy the entire Json output to the clipboard";
			HashViewActions_Copy.Click += HashViewActions_Copy_Click;
			// 
			// HashViewActions_Save
			// 
			HashViewActions_Save.Image = Properties.Resources.diskette;
			HashViewActions_Save.ImageTransparentColor = Color.White;
			HashViewActions_Save.Name = "HashViewActions_Save";
			HashViewActions_Save.Size = new Size(39, 34);
			HashViewActions_Save.Text = "Save";
			HashViewActions_Save.TextImageRelation = TextImageRelation.ImageAboveText;
			HashViewActions_Save.ToolTipText = "Save the entire Json output to a file";
			HashViewActions_Save.Click += HashViewActions_Save_Click;
			// 
			// HashViewActions_Version
			// 
			HashViewActions_Version.Alignment = ToolStripItemAlignment.Right;
			HashViewActions_Version.Image = Properties.Resources.tag;
			HashViewActions_Version.ImageTransparentColor = Color.White;
			HashViewActions_Version.Name = "HashViewActions_Version";
			HashViewActions_Version.Size = new Size(63, 34);
			HashViewActions_Version.Text = "v1.0.0.0";
			HashViewActions_Version.TextImageRelation = TextImageRelation.ImageAboveText;
			// 
			// HashViewActions_About
			// 
			HashViewActions_About.Alignment = ToolStripItemAlignment.Right;
			HashViewActions_About.Image = Properties.Resources.world;
			HashViewActions_About.ImageTransparentColor = Color.White;
			HashViewActions_About.IsLink = true;
			HashViewActions_About.LinkBehavior = LinkBehavior.HoverUnderline;
			HashViewActions_About.LinkColor = Color.FromArgb(0, 0, 255);
			HashViewActions_About.Name = "HashViewActions_About";
			HashViewActions_About.Size = new Size(42, 34);
			HashViewActions_About.Text = "About";
			HashViewActions_About.TextImageRelation = TextImageRelation.ImageAboveText;
			HashViewActions_About.ToolTipText = "https://vita-nex.com/xf-addon-hasher/";
			HashViewActions_About.VisitedLinkColor = Color.FromArgb(0, 0, 255);
			HashViewActions_About.Click += HashViewActions_About_Click;
			// 
			// AddonTreeWatcher
			// 
			AddonTreeWatcher.EnableRaisingEvents = true;
			AddonTreeWatcher.IncludeSubdirectories = true;
			AddonTreeWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
			AddonTreeWatcher.SynchronizingObject = AddonTree;
			AddonTreeWatcher.Changed += AddonTreeWatcher_Updated;
			AddonTreeWatcher.Created += AddonTreeWatcher_Updated;
			AddonTreeWatcher.Deleted += AddonTreeWatcher_Updated;
			AddonTreeWatcher.Renamed += AddonTreeWatcher_Updated;
			// 
			// AddonTreeBrowser
			// 
			AddonTreeBrowser.Description = "Select Addon Root Directory";
			AddonTreeBrowser.RootFolder = Environment.SpecialFolder.Recent;
			AddonTreeBrowser.ShowNewFolderButton = false;
			AddonTreeBrowser.UseDescriptionForTitle = true;
			// 
			// HashViewSaveDialog
			// 
			HashViewSaveDialog.DefaultExt = "json";
			HashViewSaveDialog.FileName = "hashes.json";
			HashViewSaveDialog.Filter = "Json files|*.json";
			HashViewSaveDialog.Title = "XenForo Addon Hashes";
			// 
			// Interface
			// 
			AutoScaleDimensions = new SizeF(7F, 14F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(784, 561);
			Controls.Add(ViewContainer);
			Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
			Icon = (Icon)resources.GetObject("$this.Icon");
			MinimumSize = new Size(600, 400);
			Name = "Interface";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "XenForo Addon Hasher";
			ViewContainer.Panel1.ResumeLayout(false);
			ViewContainer.Panel1.PerformLayout();
			ViewContainer.Panel2.ResumeLayout(false);
			ViewContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ViewContainer).EndInit();
			ViewContainer.ResumeLayout(false);
			AddonTreeActions.ResumeLayout(false);
			AddonTreeActions.PerformLayout();
			HashViewActions.ResumeLayout(false);
			HashViewActions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)AddonTreeWatcher).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private SplitContainer ViewContainer;
		private ToolStrip AddonTreeActions;
		private ToolStrip HashViewActions;
		private TreeView AddonTree;
		private RichTextBox HashView;
		private ToolStripButton AddonTreeActions_Root;
		private FileSystemWatcher AddonTreeWatcher;
		private FolderBrowserDialog AddonTreeBrowser;
		private ToolStripButton HashViewActions_Copy;
		private ToolStripButton HashViewActions_Save;
		private SaveFileDialog HashViewSaveDialog;
		private ImageList AddonTreeIcons;
		private ToolStripLabel HashViewActions_About;
		private ToolStripButton HashViewActions_Hash;
		private ToolStripLabel HashViewActions_Version;
	}
}
