using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class PumpkinStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pumpkin Staff");
			Tooltip.SetDefault("Summons a swirling shield of pumpkins\n" + "Right click to send the pumpkins outwards");
		}
		public override void SetDefaults()
		{
			item.damage = 52;
			item.summon = true;
			item.mana = 15;
			item.width = 50;
			item.height = 48;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.knockBack = 8;
			item.value = 120000;
			item.rare = 8;
			item.UseSound = SoundID.Item78;
			item.noMelee = true;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Pumpkin");
			item.shootSpeed = 11f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            bool create = true;
            for (int l = 0; l < 200; l++)
            {
                Projectile p = Main.projectile[l];
                if (p.type == type && p.active && p.owner == player.whoAmI && p.ai[1] != 1)
                {
                    if (player.altFunctionUse == 0)
                    {
                        p.Kill();
                    }
                    else
                    {
                        p.ai[1] = 1f;
                        create = false;
                    }
                    p.netUpdate = true;
                }
            }
            if (create)
            {
                for (int i = 0; i < 12; i++)
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, i * -30);
            }
            return false;
		}		
	}
}


