﻿using uTinyRipper.Classes.BoxCollider2Ds;
using uTinyRipper.Classes.Misc;
using uTinyRipper.Converters;
using uTinyRipper.YAML;

namespace uTinyRipper.Classes
{
	public sealed class PolygonCollider2D : Collider2D
	{
		public PolygonCollider2D(AssetInfo assetInfo):
			base(assetInfo)
		{
		}

		/// <summary>
		/// 5.6.0 and greater
		/// </summary>
		public static bool IsReadSpriteTilingProperty(Version version)
		{
			return version.IsGreaterEqual(5, 6);
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);
			
			if (IsReadSpriteTilingProperty(reader.Version))
			{
				SpriteTilingProperty.Read(reader);
				AutoTiling = reader.ReadBoolean();
				reader.AlignStream(AlignType.Align4);
			}
			Points.Read(reader);
		}

		protected override YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = base.ExportYAMLRoot(container);
			node.Add(SpriteTilingPropertyName, SpriteTilingProperty.ExportYAML(container));
			node.Add(AutoTilingName, AutoTiling);
			node.Add(PointsName, Points.ExportYAML(container));
			return node;
		}

		public bool AutoTiling { get; private set; }

		public const string SpriteTilingPropertyName = "m_SpriteTilingProperty";
		public const string AutoTilingName = "m_AutoTiling";
		public const string PointsName = "m_Points";

		public SpriteTilingProperty SpriteTilingProperty;
		/// <summary>
		/// Poly previously
		/// </summary>
		public Polygon2D Points;
	}
}