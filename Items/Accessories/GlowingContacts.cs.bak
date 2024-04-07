using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace JoostMod.Items.Accessories
{
    //[AutoloadEquip(EquipType.Face)]
    public class GlowingContacts : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glow-In-The-Dark Contacts");
            Tooltip.SetDefault("Toggle visiblity to disable glow effect\n" +
                "Place in slot 2 to wear over helmets\n" +
                "Place in slot 3 for raised eye texture\n" +
                "Place in slot 4 for a closed-helm texture\n" +
                "Place in slot 5 for a raised closed-helm texture\n" +
                "Place in slot 6 for genji helm texture");
            //ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[Item.faceSlot] = true;
		}
		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 36;
			Item.value = 5000;
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;
            Item.vanity = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
		}
        /*
        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.GetModPlayer<JoostPlayer>().glowContacts = true;
        }
        */
        public override void UpdateVanity(Player player)
        {
            player.GetModPlayer<JoostPlayer>().glowContacts = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<JoostPlayer>().glowContacts = true;
            /*
            Vector2 pos = new Vector2(player.Center.X + player.direction * 3, player.position.Y + 8);
            if (player.gravDir < 0)
            {
                pos.Y = player.position.Y + player.height - 8;
            }
            Lighting.AddLight((int)pos.X, (int)pos.Y, 0.05f, 0.19f, 0.02f);
            */
            Vector2 pos = new Vector2(player.Center.X + player.direction * 3, player.position.Y + 8);
            if (player.gravDir < 0)
            {
                pos.Y = player.position.Y + player.height - 8;
            }
            Lighting.AddLight((int)(pos.X / 16f), (int)(pos.Y / 16f), 0.02f, 0.1f, 0.01f);
            if (hideVisual)
            {
                player.GetModPlayer<JoostPlayer>().glowEyeNoGlow = true;
            }
        }
    }
}