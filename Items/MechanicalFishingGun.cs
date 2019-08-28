using System; 
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace JoostMod.Items
{
    public class MechanicalFishingGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mechanical Fishing Shotgun");
            Tooltip.SetDefault("Fires multiple fishing hooks");
        }
        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 16;
            item.damage = 20;
            item.ranged = true;
            item.knockBack = 0;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 1;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item36;
            item.autoReuse = false;
            //item.shoot = 365;
            item.shoot = mod.ProjectileType("MechanicalFishHook");
            item.shootSpeed = 15f;
            item.fishingPole = 30;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                mult *= BattleRodsFishingDamage / player.rangedDamage;
            }
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.rangedCrit;
            }
        }
        public float BattleRodsFishingDamage
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>(ModLoader.GetMod("UnuBattleRods")).bobberDamage; }
        }
        public int BattleRodsCrit
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>(ModLoader.GetMod("UnuBattleRods")).bobberCrit; }
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                Player player = Main.player[Main.myPlayer];
                int dmg = list.FindIndex(x => x.Name == "Damage");
                list.RemoveAt(dmg);
                list.Insert(dmg, new TooltipLine(mod, "Damage", player.GetWeaponDamage(item) + " Fishing damage"));
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FishingGun", 1);
            recipe.AddIngredient(ItemID.MechanicsRod, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Boomstick, 1);
            recipe.AddIngredient(ItemID.MechanicsRod, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 50f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 7f;
            double offsetAngle;
            int i;
            for (i = 0; i < 7; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}

