﻿using AssetRipper.Core.Classes;
using AssetRipper.Core.Classes.EditorBuildSettings;
using AssetRipper.Core.Classes.EditorSettings;
using AssetRipper.Core.Interfaces;
using AssetRipper.Core.IO;
using AssetRipper.Core.Parser.Files;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Core.Project.Exporters;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AssetRipper.Core.Project.Collections
{
	public sealed class BuildSettingsExportCollection : ManagerExportCollection
	{
		public BuildSettingsExportCollection(IAssetExporter assetExporter, VirtualSerializedFile file, IUnityObjectBase asset) : this(assetExporter, file, (IBuildSettings)asset) { }

		public BuildSettingsExportCollection(IAssetExporter assetExporter, VirtualSerializedFile virtualFile, IBuildSettings asset) : base(assetExporter, asset)
		{
			EditorBuildSettings = CreateVirtualEditorBuildSettings(virtualFile);
			EditorSettings = CreateVirtualEditorSettings(virtualFile);
		}

		public override bool Export(ProjectAssetContainer container, string dirPath)
		{
			string subPath = Path.Combine(dirPath, ProjectSettingsName);
			string fileName = $"{EditorBuildSettings.ClassID}.asset";
			string filePath = Path.Combine(subPath, fileName);

			Directory.CreateDirectory(subPath);

			IBuildSettings buildSettings = (IBuildSettings)Asset;
			InitializeEditorBuildSettings(EditorBuildSettings, buildSettings, container);
			AssetExporter.Export(container, EditorBuildSettings, filePath);

			fileName = $"{EditorSettings.ClassID}.asset";
			filePath = Path.Combine(subPath, fileName);

			AssetExporter.Export(container, EditorSettings, filePath);

			if (buildSettings.GetType().FullName == "AssetRipper.Core.Classes.BuildSettings")
			{
				//SaveDefaultProjectVersion(subPath);
				SaveMaxProjectVersion(subPath, buildSettings);
			}
			else
			{
				SaveExactProjectVersion(subPath, buildSettings);
			}
			return true;
		}

		private static void SaveDefaultProjectVersion(string projectSettingsDirectory)
		{
			SaveProjectVersion(projectSettingsDirectory, UnityVersion.DefaultVersion);
		}
		private static void SaveMaxProjectVersion(string projectSettingsDirectory, IBuildSettings buildSettings)
		{
			UnityVersion projectVersion = UnityVersion.Max(UnityVersion.DefaultVersion, buildSettings.SerializedFile.Version);
			SaveProjectVersion(projectSettingsDirectory, projectVersion);
		}
		private static void SaveExactProjectVersion(string projectSettingsDirectory, IBuildSettings buildSettings)
		{
			SaveProjectVersion(projectSettingsDirectory, buildSettings.SerializedFile.Version);
		}
		private static void SaveProjectVersion(string projectSettingsDirectory, UnityVersion version)
		{
			using Stream fileStream = System.IO.File.Create(Path.Combine(projectSettingsDirectory, "ProjectVersion.txt"));
			using StreamWriter writer = new InvariantStreamWriter(fileStream, new UTF8Encoding(false));
			writer.Write($"m_EditorVersion: {version}");
		}

		public static IEditorSettings CreateVirtualEditorSettings(VirtualSerializedFile virtualFile)
		{
			IEditorSettings result = virtualFile.CreateAsset<IEditorSettings>(ClassIDType.EditorSettings);
			result.SetToDefaults();
			return result;
		}

		public static IEditorBuildSettings CreateVirtualEditorBuildSettings(VirtualSerializedFile virtualFile)
		{
			return virtualFile.CreateAsset<IEditorBuildSettings>(ClassIDType.EditorBuildSettings);
		}

		public static void InitializeEditorBuildSettings(IEditorBuildSettings editorBuildSettings, IBuildSettings buildSettings, ProjectAssetContainer container)
		{
			int numScenes = buildSettings.Scenes.Length;
			editorBuildSettings.InitializeScenesArray(numScenes);
			IEditorScene[] scenes = editorBuildSettings.Scenes;
			for (int i = 0; i < numScenes; i++)
			{
				string scenePath = buildSettings.Scenes[i].String;
				IEditorScene scene = scenes[i];
				scene.Enabled = true;
				scene.Path = scenePath;
				scene.GUID = container.SceneNameToGUID(scenePath);
			}
		}

		public override bool IsContains(IUnityObjectBase asset)
		{
			if (asset is IEditorBuildSettings)
			{
				return asset == EditorBuildSettings;
			}
			else if (asset is IEditorSettings)
			{
				return asset == EditorSettings;
			}
			else
			{
				return base.IsContains(asset);
			}
		}

		public override long GetExportID(IUnityObjectBase asset)
		{
			return 1;
		}

		public override IEnumerable<IUnityObjectBase> Assets
		{
			get
			{
				yield return Asset;
				yield return EditorBuildSettings;
				yield return EditorSettings;
			}
		}

		public IEditorBuildSettings EditorBuildSettings { get; }
		public IEditorSettings EditorSettings { get; }
	}
}
