﻿using AssetRipper.Core.Classes.TrailRenderer;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Math;
using AssetRipper.Core.Math.Vectors;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;

namespace AssetRipper.Core.Classes.LineRenderer
{
	public sealed class LineRenderer : Renderer.Renderer
	{
		public LineRenderer(AssetInfo assetInfo) : base(assetInfo) { }

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			Positions = reader.ReadAssetArray<Vector3f>();
			Parameters.Read(reader);
			UseWorldSpace = reader.ReadBoolean();
			Loop = reader.ReadBoolean();
		}

		public override void Write(AssetWriter writer)
		{
			base.Write(writer);

			Positions.Write(writer);
			Parameters.Write(writer);
			writer.Write(UseWorldSpace);
			writer.Write(Loop);
		}

		protected override YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = base.ExportYAMLRoot(container);
			node.InsertSerializedVersion(1);
			node.Add("m_Positions", Positions.ExportYAML(container));
			node.Add("m_Parameters", Parameters.ExportYAML(container));
			node.Add("m_UseWorldSpace", UseWorldSpace);
			node.Add("m_Loop", Loop);
			return node;
		}

		public Vector3f[] Positions { get; set; }
		public LineParameters Parameters = new();
		public bool UseWorldSpace { get; set; }
		public bool Loop { get; set; }
	}
}
