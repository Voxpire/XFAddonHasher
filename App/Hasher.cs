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

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace XFAddonHasher
{
	public static class Hasher
	{
		private static HashAlgorithm? _SHA;

		public static JsonSerializerOptions JsonOptions { get; } = new()
		{
			WriteIndented = true,
		};

		public static bool SerializeHashes(DirectoryInfo root, out Dictionary<string, string>? hashes, out string? json, out string? aggregate, out Exception? status)
		{
			hashes = null;
			json = null;
			aggregate = null;
			status = null;

			try
			{
				if (!GenerateHashes(root, out hashes, out var hx))
				{
					status = hx;

					return false;
				}

				using var stream = new MemoryStream();

				JsonSerializer.Serialize(stream, hashes, JsonOptions);

				stream.Position = 0;

				Span<byte> data = stackalloc byte[(int)stream.Length];

				stream.ReadExactly(data);

				json = Encoding.UTF8.GetString(data);

				return GenerateHash(stream, out aggregate, out status);
			}
			catch (Exception x)
			{
				status = x;

				return false;
			}
		}

		public static bool GenerateHashes(DirectoryInfo root, out Dictionary<string, string>? hashes, out Exception? status)
		{
			hashes = null;
			status = null;

			try
			{
				var files = root.EnumerateFiles("*", SearchOption.AllDirectories);

				foreach (var file in files.OrderBy(f => f.FullName.Replace('_', '\uFFFF')))
				{
					if (String.Equals(file.Name, "hashes.json", StringComparison.InvariantCultureIgnoreCase))
					{
						continue;
					}

					hashes ??= new();

					var relative = Path.GetRelativePath(root.FullName, file.FullName);

					relative = relative.Replace('\\', '/');

					if (!GenerateHash(file, out var hash, out var hx))
					{
						hashes[relative] = $"HASH_FAILED: {hx?.Message}";
					}
					else
					{
						hashes[relative] = $"{hash}";
					}
				}

				return true;
			}
			catch (Exception x)
			{
				status = x;

				return false;
			}
		}

		public static bool GenerateHash(FileInfo file, out string? hash, out Exception? status)
		{
			hash = null;
			status = null;

			try
			{
				using var stream = new FileStream(file.FullName, new FileStreamOptions
				{
					Mode = FileMode.Open,
					Access = FileAccess.Read,
					Share = FileShare.Read,
					BufferSize = 0,
					Options = FileOptions.SequentialScan
				});

				return GenerateHash(stream, out hash, out status);
			}
			catch (Exception x)
			{
				status = x;

				return false;
			}
		}

		private static bool GenerateHash(Stream stream, out string? hash, out Exception? status)
		{
			hash = null;
			status = null;

			try
			{
				stream.Position = 0;

				_SHA ??= SHA256.Create();

				var inputLength = (int)Math.Min(stream.Length, 8192);
				var outputLength = _SHA.HashSize / 8;

				Span<byte> inputBuffer = stackalloc byte[inputLength];
				Span<byte> outputBuffer = stackalloc byte[outputLength];

				int read, write = 0;

				while (write < outputLength && (read = stream.Read(inputBuffer)) > 0)
				{
					int index = -1, replaced = 0;

					while (++index < read)
					{
						if (inputBuffer[index] == 0x0D)
						{
							++replaced;

							if (index + replaced < read)
							{
								inputBuffer[index] = inputBuffer[index + replaced];
							}
						}
						else if (replaced > 0)
						{
							inputBuffer[index - replaced] = inputBuffer[index];
						}
					}

					read -= replaced;

					if (_SHA.TryComputeHash(inputBuffer.Slice(0, read), outputBuffer, out var written))
					{
						write += written;
					}
					else
					{
						throw new OutOfMemoryException();
					}
				}

				Span<char> hashBuffer = stackalloc char[outputBuffer.Length * 2];
				
				for (int i = 0, j = 0, c; i < outputBuffer.Length; j = ++i * 2)
				{
					c = outputBuffer[i] >> 4;

					hashBuffer[j++] = (char)(c >= 0x0A ? (0x61 + (c - 0x0A)) : (0x30 + c));

					c = outputBuffer[i] & 0x0F;

					hashBuffer[j++] = (char)(c >= 0x0A ? (0x61 + (c - 0x0A)) : (0x30 + c));
				}

				hash = new string(hashBuffer);

				return true;
			}
			catch (Exception x)
			{
				status = x;

				return false;
			}
			finally
			{
				if (status == null)
				{
					_SHA?.Initialize();
				}
				else
				{
					_SHA?.Dispose();
					_SHA = null;
				}
			}
		}
	}
}
