using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
	public class IronFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Iron Flail");
		}
		public override void SetDefaults()
		{
			Item.damage = 8;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.scale = 0.875f;
			Item.noUseGraphic = true;
			Item.width = 30;
			Item.height = 32;
			Item.useTime = 44;
			Item.useAnimation = 44;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.channel = true;
            Item.useTurn = true;
            Item.shoot = Mod.Find<ModProjectile>("IronFlail").Type;
			Item.shootSpeed = 9f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position.Y -= (Item.scale * 30) - 30;
            return true;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (Main.rand.NextBool(2))
            {
                switch (rand.Next(18))
                {
                    case 1:
                        return PrefixID.Large;
                    case 2:
                        return PrefixID.Massive;
                    case 3:
                        return PrefixID.Dangerous;
                    case 4:
                        return PrefixID.Savage;
                    case 5:
                        return PrefixID.Sharp;
                    case 6:
                        return PrefixID.Pointy;
                    case 7:
                        return PrefixID.Tiny;
                    case 8:
                        return PrefixID.Terrible;
                    case 9:
                        return PrefixID.Small;
                    case 10:
                        return PrefixID.Dull;
                    case 11:
                        return PrefixID.Unhappy;
                    case 12:
                        return PrefixID.Bulky;
                    case 13:
                        return PrefixID.Shameful;
                    case 14:
                        return PrefixID.Heavy;
                    case 15:
                        return PrefixID.Light;
                    case 16:
                        return Mod.Find<ModPrefix>("Impractically Oversized").Type;
                    case 17:
                        return Mod.Find<ModPrefix>("Miniature").Type;
                    default:
                        return PrefixID.Legendary;
                }
            }
            return base.ChoosePrefix(rand);
        }
        public override void AddRecipes()
		{
			CreateRecipe()
                .AddIngredient(ItemID.IronBar, 8)
                .AddTile(TileID.Anvils)
                .Register();
		}

	}
}

