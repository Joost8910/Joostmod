using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using JoostMod.Items.Legendaries.Weps;

namespace JoostMod.Tiles
{
	public class LegendGrave : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileOreFinderPriority[Type] = 660;
            Main.tileSpelunker[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);
            TileID.Sets.DisableSmartCursor[Type] = true;
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(0, 145, 100), name);
            DustType = 42;
            MinPick = 100;
            MineResist = 3f;
            RegisterItemDrop(ModContent.ItemType<StaffofDavid>(), 0);
            RegisterItemDrop(ModContent.ItemType<LarkusTome>(), 1);
            RegisterItemDrop(ModContent.ItemType<GnunderGlove>(), 2);
            RegisterItemDrop(ModContent.ItemType<BoookBulletHell>(), 3);
            RegisterItemDrop(ModContent.ItemType<GrognakHammer>(), 4);
            RegisterItemDrop(ModContent.ItemType<UncleCariusPole>(), 5);
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }
	}
}
