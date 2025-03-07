using AssetRipper.Core.Classes.Shader.SerializedShader.Enum;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Parser.Files;

namespace AssetRipper.Core.Classes.Shader.SerializedShader
{
	public sealed class SerializedShaderState : IAssetReadable
	{
		/// <summary>
		/// 2017.2 and greater
		/// </summary>
		public static bool HasZClip(UnityVersion version) => version.IsGreaterEqual(2017, 2);
		/// <summary>
		/// 2020 and greater
		/// </summary>
		public static bool HasConservative(UnityVersion version) => version.IsGreaterEqual(2020);

		public void Read(AssetReader reader)
		{
			Name = reader.ReadString();
			RtBlend0.Read(reader);
			RtBlend1.Read(reader);
			RtBlend2.Read(reader);
			RtBlend3.Read(reader);
			RtBlend4.Read(reader);
			RtBlend5.Read(reader);
			RtBlend6.Read(reader);
			RtBlend7.Read(reader);
			RtSeparateBlend = reader.ReadBoolean();
			reader.AlignStream();

			if (HasZClip(reader.Version))
			{
				ZClip.Read(reader);
			}
			ZTest.Read(reader);
			ZWrite.Read(reader);
			Culling.Read(reader);
			if (HasConservative(reader.Version))
			{
				Conservative.Read(reader);
			}
			OffsetFactor.Read(reader);
			OffsetUnits.Read(reader);
			AlphaToMask.Read(reader);
			StencilOp.Read(reader);
			StencilOpFront.Read(reader);
			StencilOpBack.Read(reader);
			StencilReadMask.Read(reader);
			StencilWriteMask.Read(reader);
			StencilRef.Read(reader);
			FogStart.Read(reader);
			FogEnd.Read(reader);
			FogDensity.Read(reader);
			FogColor.Read(reader);

			FogMode = (FogMode)reader.ReadInt32();
			GpuProgramID = reader.ReadInt32();
			Tags.Read(reader);
			LOD = reader.ReadInt32();
			Lighting = reader.ReadBoolean();
			reader.AlignStream();
		}

		public string Name { get; set; }
		public bool RtSeparateBlend { get; set; }
		public FogMode FogMode { get; set; }
		public int GpuProgramID { get; set; }
		public int LOD { get; set; }
		public bool Lighting { get; set; }

		public ZClip ZClipValue => (ZClip)ZClip.Val;
		public ZTest ZTestValue => (ZTest)ZTest.Val;
		public ZWrite ZWriteValue => (ZWrite)ZWrite.Val;
		public CullMode CullingValue => (CullMode)Culling.Val;
		public bool AlphaToMaskValue => AlphaToMask.Val > 0;
		public string LightingValue => Lighting ? "On" : "Off";

		public SerializedShaderRTBlendState RtBlend0 = new();
		public SerializedShaderRTBlendState RtBlend1 = new();
		public SerializedShaderRTBlendState RtBlend2 = new();
		public SerializedShaderRTBlendState RtBlend3 = new();
		public SerializedShaderRTBlendState RtBlend4 = new();
		public SerializedShaderRTBlendState RtBlend5 = new();
		public SerializedShaderRTBlendState RtBlend6 = new();
		public SerializedShaderRTBlendState RtBlend7 = new();
		public SerializedShaderFloatValue ZClip = new();
		public SerializedShaderFloatValue ZTest = new();
		public SerializedShaderFloatValue ZWrite = new();
		public SerializedShaderFloatValue Culling = new();
		public SerializedShaderFloatValue Conservative = new();
		public SerializedShaderFloatValue OffsetFactor = new();
		public SerializedShaderFloatValue OffsetUnits = new();
		public SerializedShaderFloatValue AlphaToMask = new();
		public SerializedStencilOp StencilOp = new();
		public SerializedStencilOp StencilOpFront = new();
		public SerializedStencilOp StencilOpBack = new();
		public SerializedShaderFloatValue StencilReadMask = new();
		public SerializedShaderFloatValue StencilWriteMask = new();
		public SerializedShaderFloatValue StencilRef = new();
		public SerializedShaderFloatValue FogStart = new();
		public SerializedShaderFloatValue FogEnd = new();
		public SerializedShaderFloatValue FogDensity = new();
		public SerializedShaderVectorValue FogColor = new();
		public SerializedTagMap Tags = new();
	}
}
