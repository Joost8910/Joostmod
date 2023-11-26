using JoostMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
{
    public class NatureStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Staff of Nature");
            Tooltip.SetDefault("Summons a swirling shield of leaves\n" + "Right click to send the leaves outwards");
        }
        public override void SetDefaults()
        {
            Item.damage = 44;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 12;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 4.5f;
            Item.value = 250000;
            Item.rare = ItemRarityID.Pink;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item78;
            Item.shoot = Mod.Find<ModProjectile>("Leaf2").Type;
            Item.shootSpeed = 9f;
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
                for (int i = 0; i < 8; i++)
                    Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 45f * i, 0f);
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ForestWand>()
                .AddIngredient(ItemID.JungleSpores, 8)
                .AddIngredient(ItemID.SoulofFright, 12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}


