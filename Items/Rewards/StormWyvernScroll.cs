using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Rewards
{
	public class StormWyvernScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scroll of the Storm");
			Tooltip.SetDefault("Summons a young Storm Wyvern to fight for you\n" +
                "Hold Right Click to command the Storm Wyvern to charge a lightning blast");
		}
		public override void SetDefaults()
		{
			Item.damage = 28;
			Item.DamageType = DamageClass.Summon;
			Item.mana = 10;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true; 
			Item.knockBack = 0;
            Item.value = 90000;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item44;
			Item.shoot = ModContent.ProjectileType<StormWyvernMinion>();
			Item.shootSpeed = 12.5f;
			Item.buffType = ModContent.BuffType<Buffs.StormWyvernMinionBuff>();
			Item.buffTime = 3600;
            Item.autoReuse = false;
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
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
            
            if (player.altFunctionUse != 2)
            {
                Projectile projectile = Projectile.NewProjectileDirect(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI);
                bool foundWyvern = false;
                int tail = -1;
                float slots = 0f;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == player.whoAmI && p.minion)
                    {
                        slots += p.minionSlots;
                        //Main.NewText(p.ai[0], Color.Green);
                        //Main.NewText(p.minionSlots);
                        if (i != projectile.identity && p.type == type && tail < 0)
                        {
                            foundWyvern = true;
                            if (p.ai[0] == 3)
                            {
                                tail = i;
                            }
                        }
                    }
                }
                //Main.NewText(slots, Color.Red);
                if (tail > 0 && slots <= player.maxMinions)
                {
                    Projectile p = Main.projectile[tail];
                    Main.projectile[(int)p.ai[1]].ai[0] = 4;
                    p.ai[0] = 2;
                    projectile.ai[0] = 3;
                    projectile.ai[1] = tail;
                    projectile.netUpdate = true;
                }
                if (!foundWyvern)
                {
                    int latestProj = projectile.identity;
                    for (int i = 1; i < 4; i++)
                    {
                        latestProj = Projectile.NewProjectile(source, projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, i, latestProj);
                        Main.projectile[latestProj].minionSlots = 0;
                    }
                    projectile.ai[0] = 0;
                    projectile.netUpdate = true;
                }
            }
            return false;
        }
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim(false);
			}
			return base.UseItem(player);
		}
	}
}


