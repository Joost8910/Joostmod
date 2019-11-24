using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons
{
	public class MightyBambooShoot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mighty Bamboo Shoot");
			Tooltip.SetDefault("Left click to swing\n" + 
                "Right click to charge a seed barrage\n" + 
                "Allows the collection of seeds for ammo");
		}
		public override void SetDefaults()
		{
			item.damage = 17;
			item.melee = true;
            item.ranged = true;
			item.width = 100;
			item.height = 12;
			item.noMelee = true;
			item.useTime = 21;
			item.useAnimation = 21;
			item.useStyle = 5;
			item.autoReuse = true;
			item.knockBack = 6;
			item.value = 20000;
			item.rare = 1;
			item.UseSound = SoundID.Item7;
			item.noUseGraphic = true;
			item.channel = true;
			item.shoot = 10;
			item.shootSpeed = 11f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.rangedDamage - 1f);
            mult *= player.rangedDamageMult;
        }
        */
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += player.rangedCrit;
            crit /= 2;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " melee and ranged damage"));
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useAmmo = AmmoID.Dart;
            }
            else
            {
                item.useAmmo = AmmoID.None;
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("BambooShoot")] < 1)
            {
                return base.CanUseItem(player);
            }
            return false;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (Main.rand.NextBool(3))
            {
                switch (rand.Next(12))
                {
                    case 1:
                        return PrefixID.Rapid;
                    case 2:
                        return PrefixID.Hasty;
                    case 3:
                        return PrefixID.Intimidating;
                    case 4:
                        return PrefixID.Deadly2;
                    case 5:
                        return PrefixID.Staunch;
                    case 6:
                        return PrefixID.Awful;
                    case 7:
                        return PrefixID.Lethargic;
                    case 8:
                        return PrefixID.Awkward;
                    case 9:
                        return PrefixID.Powerful;
                    case 10:
                        return PrefixID.Frenzying;
                    case 11:
                        return PrefixID.Sighted;
                    default:
                        return PrefixID.Unreal;
                }
            }
            else if (Main.rand.NextBool(2))
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
                        return mod.PrefixType("Impractically Oversized");
                    case 17:
                        return mod.PrefixType("Miniature");
                    default:
                        return PrefixID.Legendary;
                }
            }
            return base.ChoosePrefix(rand);
        }
        public override bool ConsumeAmmo(Player player)
        {
            return false;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int mode = 0;
            if (player.altFunctionUse == 2)
            {
                mode = 1;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("BambooShoot"), damage, knockBack, player.whoAmI, mode, type);
            return false;
        }
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Blowpipe);
			recipe.AddIngredient(ItemID.BreathingReed);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}

