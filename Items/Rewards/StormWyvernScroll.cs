using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
			item.damage = 28;
			item.summon = true;
			item.mana = 10;
			item.width = 36;
			item.height = 36;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 4;
			item.noMelee = true; 
			item.knockBack = 0;
            item.value = 90000;
            item.rare = 3;
            item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("StormWyvernMinion");
			item.shootSpeed = 12.5f;
			item.buffType = mod.BuffType("StormWyvernMinion");
			item.buffTime = 3600;
            item.autoReuse = false;
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(230, 204, 128);
                }
            }
        }
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
            
            if (player.altFunctionUse != 2)
            {
                Projectile projectile = Projectile.NewProjectileDirect(position, Vector2.Zero, type, damage, knockBack, player.whoAmI);
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
                        latestProj = Projectile.NewProjectile(projectile.Center, Vector2.Zero, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, i, latestProj);
                        Main.projectile[latestProj].minionSlots = 0;
                    }
                    projectile.ai[0] = 0;
                    projectile.netUpdate = true;
                }
            }
            return false;
        }
		
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
	}
}


