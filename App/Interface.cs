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

using System.Diagnostics;
using System.Reflection;

namespace XFAddonHasher
{
	public partial class Interface : Form
	{
		private static readonly char[] _PathSeparators =
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		private static readonly HashSet<object> _Flickering = new();

		private string? _Aggregate;

		public Interface()
		{
			InitializeComponent();

			var asm = Assembly.GetEntryAssembly();
			var asmName = asm?.GetName();
			var asmVersion = asmName?.Version?.ToString();

			HashViewActions_Version.Text = $"v{asmVersion ?? "1.0.0.0"}";
		}

		private void AddonTree_Update()
		{
			if (InvokeRequired)
			{
				Invoke(AddonTree_Update);

				return;
			}

			var selectedPath = AddonTree.SelectedNode?.FullPath;

			TreeNode? rootNode = null, selectedNode = null;

			var path = AddonTreeWatcher.Path;

			if (Directory.Exists(path))
			{
				var root = new DirectoryInfo(path);

				if (Hasher.SerializeHashes(root, out var hashes, out var json, out var aggregate, out var status))
				{
					if (aggregate == _Aggregate)
					{
						return;
					}

					_Aggregate = aggregate;

					var rootName = "upload";

					var rootSplit = path.LastIndexOfAny(_PathSeparators);

					if (rootSplit >= 0)
					{
						rootName = path.Substring(rootSplit + 1);
					}

					rootNode = new TreeNode(rootName);

					if (hashes?.Count > 0)
					{
						foreach (var (relative, hash) in hashes)
						{
							var node = rootNode;

							if (relative.IndexOfAny(_PathSeparators) >= 0)
							{
								var segments = relative.Split(_PathSeparators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

								foreach (var segment in segments)
								{
									var child = node.Nodes[segment];

									child ??= node.Nodes.Add(segment, segment);

									node = child;
								}

								node.Checked = true;

								node.ToolTipText = hash;

								node.ImageIndex = 1;
								node.SelectedImageIndex = 1;
								node.StateImageIndex = 1;

								if (selectedPath?.EndsWith(relative) == true)
								{
									selectedNode = node;
								}
							}
						}
					}
				}

				HashView.Text = json ?? String.Empty;
			}
			else
			{
				HashView.Text = String.Empty;
			}

			AddonTree.BeginUpdate();

			AddonTree.Nodes.Clear();

			if (rootNode != null)
			{
				_ = AddonTree.Nodes.Add(rootNode);

				rootNode.ExpandAll();

				selectedNode ??= rootNode;

				selectedNode.EnsureVisible();

				AddonTree.SelectedNode = selectedNode;
			}

			AddonTree.EndUpdate();
		}

		private void HashView_UpdateHighlight(string? search, bool isFile)
		{
			if (InvokeRequired)
			{
				_ = Invoke(HashView_UpdateHighlight, search, isFile);

				return;
			}

			HashView.SuspendLayout();

			HashView.SelectionStart = 0;

			HashView.SelectAll();

			HashView.SelectionBackColor = HashView.BackColor;

			if (search != null)
			{
				var offset = 0;

				while (offset < HashView.TextLength)
				{
					var index = HashView.Find(search, offset, RichTextBoxFinds.None);

					var length = 0;

					if (index < 0)
					{
						break;
					}

					var line = HashView.GetLineFromCharIndex(index);

					index = HashView.GetFirstCharIndexFromLine(line);

					if (++line < HashView.Lines.Length)
					{
						length = HashView.GetFirstCharIndexFromLine(line) - index;
					}
					else
					{
						length = HashView.TextLength - index;
					}

					if (offset == 0 && !HashView.ClientRectangle.Contains(HashView.GetPositionFromCharIndex(index)))
					{
						HashView.Select(index, 0);

						HashView.ScrollToCaret();
					}

					HashView.Select(index, length);

					HashView.SelectionBackColor = Color.LawnGreen;

					if (!isFile)
					{
						offset += length;
					}
					else
					{
						break;
					}
				}
			}

			HashView.Select(0, 0);

			HashView.ResumeLayout(true);
		}

		private void AddonTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			string? search = null;

			if (e.Node != null)
			{
				search = e.Node.FullPath;

				var index = search.IndexOf('/');

				if (index >= 0)
				{
					search = search.Substring(index + 1);
				}
			}

			HashView_UpdateHighlight(search, e.Node?.Checked ?? false);
		}

		private void AddonTreeWatcher_Updated(object sender, FileSystemEventArgs e)
		{
			if (String.IsNullOrWhiteSpace(e.Name))
			{
				return;
			}

			if (e.Name.Contains("hashes.json", StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}

			AddonTree_Update();
		}

		private void AddonTreeActions_Root_Click(object sender, EventArgs e)
		{
			var result = DialogResult.Retry;

			while (result == DialogResult.Retry)
			{
				result = AddonTreeBrowser.ShowDialog(this);

				if (result != DialogResult.OK)
				{
					break;
				}

				var path = AddonTreeBrowser.SelectedPath;

				var addons = Path.Combine(path, "src", "addons");

				if (Directory.Exists(addons))
				{
					AddonTreeWatcher.Path = path;

					AddonTree_Update();

					break;
				}

				var status = $"Expected path does not exist:\r\n{addons}";

				result = MessageBox.Show(this, status, "Invalid Path", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
			}
		}

		private void HashViewActions_Hash_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(AddonTreeWatcher.Path))
			{
				try
				{
					AddonTree_Update();

					FlickerColor(HashViewActions_Hash, Color.Green, 250, 3);

					return;
				}
				catch
				{
				}
			}

			FlickerColor(HashViewActions_Hash, Color.Red, 250, 3);
		}

		private void HashViewActions_Copy_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(HashView.Text))
			{
				try
				{
					Clipboard.SetText(HashView.Text);

					FlickerColor(HashViewActions_Copy, Color.Green, 250, 3);

					return;
				}
				catch
				{
				}
			}

			FlickerColor(HashViewActions_Copy, Color.Red, 250, 3);
		}

		private void HashViewActions_Save_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(HashView.Text))
			{
				try
				{
					var path = Path.Combine(AddonTreeWatcher.Path, "src", "addons");

					foreach (var addon in Directory.EnumerateFiles(path, "addon.json", SearchOption.AllDirectories))
					{
						path = Path.GetDirectoryName(addon);

						break;
					}

					HashViewSaveDialog.InitialDirectory = path;

					var result = HashViewSaveDialog.ShowDialog(this);

					if (result == DialogResult.OK)
					{
						using var file = HashViewSaveDialog.OpenFile();
						using var writer = new StreamWriter(file);

						writer.Write(HashView.Text);

						FlickerColor(HashViewActions_Save, Color.Green, 250, 3);

						return;
					}
				}
				catch (Exception x)
				{
					var status = $"Could not save file:\r\n{x.Message}";

					_ = MessageBox.Show(this, status, "Save Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
				}
			}

			FlickerColor(HashViewActions_Save, Color.Red, 250, 3);
		}

		private void HashViewActions_About_Click(object sender, EventArgs e)
		{
			if (OpenBrowser(HashViewActions_About.ToolTipText))
			{
				HashViewActions_About.LinkVisited = true;

				FlickerColor(HashViewActions_About, Color.Green, 250, 3);

				return;
			}

			FlickerColor(HashViewActions_About, Color.Red, 250, 3);
		}

		private static bool OpenBrowser(string link)
		{
			Process? proc;

			try
			{
				var info = new ProcessStartInfo(link)
				{
					UseShellExecute = true
				};

				proc = Process.Start(info);
			}
			catch
			{
				proc = null;
			}

			return proc != null;
		}

		private static void FlickerColor(ToolStripItem control, Color color, int repeatMS, int count)
		{
			if (!_Flickering.Add(control))
			{
				return;
			}

			var oldColor = control.ForeColor;

			var index = 0;

			count *= 2;

			if (count % 2 == 1)
			{
				++count;
			}

			DelayCall(control.Owner, 0, repeatMS, count, control, c =>
			{
				c.ForeColor = ++index % 2 != 0 ? color : oldColor;

				if (index >= count)
				{
					_ = _Flickering.Remove(control);
				}
			});
		}

		private static void DelayCall<T>(Control sync, int delayMS, int repeatMS, int count, T state, Action<T> callback)
		{
			if (repeatMS <= 0 || count <= 0)
			{
				repeatMS = Timeout.Infinite;
			}

			count = Math.Max(1, count);

			System.Threading.Timer? timer = null;

			timer = new System.Threading.Timer(o =>
			{
				if (--count >= 0)
				{
					if (o is T obj)
					{
						_ = sync.Invoke(callback, obj);
					}
				}

				if (count <= 0)
				{
					timer?.Dispose();
				}
			},
			state, delayMS, repeatMS);
		}
	}
}
