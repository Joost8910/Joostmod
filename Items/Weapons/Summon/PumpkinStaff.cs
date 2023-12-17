using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Summon;

namespace JoostMod.Items.Weapons.Summon
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
            Item.damage = 52;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 15;
            Item.width = 50;
            Item.height = 48;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 8;
            Item.value = 120000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item78;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Pumpkin>();
            Item.shootSpeed = 11f;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
                    Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, i * -30);
            }
            return false;
        }
    }
}


