using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class DreamNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dream Nail");
            Tooltip.SetDefault("'Cut through the veil between dreams and waking'\n" +
            "Attacks pierce 50% of enemy defense");
        }
        public override void SetDefaults()
        {
            Item.damage = 99;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 50;
            Item.height = 50;
            Item.noMelee = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.knockBack = 8;
            Item.value = 400000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.DD2_SonicBoomBladeSlash;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.shoot = Mod.Find<ModProjectile>("DreamNail").Type;
            Item.shootSpeed = 17f;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DreamNail2").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DreamGreatSlash").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DreamDashSlash").Type] + player.ownedProjectileCounts[Mod.Find<ModProjectile>("DreamSpinSlash").Type] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.statLife >= player.statLifeMax2)
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("DreamBeam").Type, damage / 2, 0, player.whoAmI);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<PureNail>()
                .AddIngredient(ItemID.BrokenHeroSword)
                .AddIngredient(ItemID.SoulofLight, 25)
                .AddIngredient(ItemID.LightShard, 2)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
