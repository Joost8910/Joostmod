//TODO: Make into 1.4 Summon Whip, but with a funky flail thing as a right click function
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class TailWhip : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tail Whip");
            Tooltip.SetDefault("Envenoms struck targets");
		}
		public override void SetDefaults()
		{
			Item.damage = 38;
			Item.DamageType = DamageClass.SummonMeleeSpeed/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.scale = 1f;
			Item.noUseGraphic = true;
			Item.width = 30;
			Item.height = 32;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4.5f;
			Item.value = 80000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.channel = true;
			Item.shoot = Mod.Find<ModProjectile>("TailWhip").Type;
			Item.shootSpeed = 20f;
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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            velocity.X += player.velocity.X;
            velocity.Y += player.velocity.Y;
            return true;
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

