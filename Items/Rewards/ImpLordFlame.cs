using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class ImpLordFlame : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lord's Flame");
            Tooltip.SetDefault("Hold attack to charge a bigger fireball");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
		public override void SetDefaults()
		{
			Item.damage = 60;
            Item.mana = 10;
            Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.scale = 1f;
			Item.noUseGraphic = true;
			Item.width = 26;
			Item.height = 26;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3.5f;
			Item.value = 80000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.channel = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Magic.ImpLordFlame>();
			Item.shootSpeed = 12f;
            Item.useTurn = true;
		}
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(230, 204, 128);
                }
            }
        }

    }
}

