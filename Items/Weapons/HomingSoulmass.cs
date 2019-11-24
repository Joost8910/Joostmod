using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HomingSoulmass : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Homing Soulmass");
			Tooltip.SetDefault("Summons homing soulmasses that stay above you before seeking enemies\n" + 
                "Right click to target an enemy");
		}
		public override void SetDefaults()
		{
			item.damage = 72;
			item.summon = true;
            item.magic = true;
            item.mana = 20;
			item.width = 36;
			item.height = 36;
			item.useTime = 35;
			item.useAnimation = 35;
			item.useStyle = 4;
			item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 3.5f;
			item.value = 500000;
			item.rare = 8;
			item.autoReuse = false;
			item.UseSound = SoundID.Item28;
			item.shoot = mod.ProjectileType("HomingSoulmass");
			item.shootSpeed = 0f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.magicDamage - 1f);
            mult *= player.magicDamageMult;
        }
        */
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.magicCrit;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " summon and magic damage"));
        }
        public override bool AltFunctionUse(Player player)
		{
			return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse != 2)
            {
                for (int l = 0; l < 200; l++)
                {
                    Projectile p = Main.projectile[l];
                    if (p.type == type && p.active && p.owner == player.whoAmI && p.localAI[1] == 0)
                    {
                        p.timeLeft = 0;
                    }
                }
                Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 45f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 90f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 135f, 0f);
				Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 180f, 0f);
			}
			return false;
		}
	}
}


