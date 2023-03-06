using Microsoft.Xna.Framework;
using System; 
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace JoostMod.Items.Tools
{
    public class LunarFishingGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Fishing Shotgun");
            Tooltip.SetDefault("Fires many fishing hooks\n" + "Can fish up Lunar Fragments");
        }
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 20;
            Item.damage = 75;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 2000000;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("LunarFishHook").Type;
            Item.shootSpeed = 19f;
            Item.fishingPole = 100;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                damage *= BattleRodsFishingDamage / player.GetDamage(DamageClass.Ranged).Multiplicative;
            }
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.GetCritChance(DamageClass.Ranged);
            }
        }
        public float BattleRodsFishingDamage
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberDamage; }
        }
        public int BattleRodsCrit
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberCrit; }
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                Player player = Main.player[Main.myPlayer];
                int dmg = list.FindIndex(x => x.Name == "Damage");
                list.RemoveAt(dmg);
                list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " Fishing damage"));
            }
        }
        */
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<JoostPlayer>().lunarRod = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<LunarRod>()
                .AddIngredient<SuperFishingGun>()
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient<DukeFishingGun>()
                .AddIngredient<LunarRod>()
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient<GoldenFishingGun>()
                .AddIngredient<LunarRod>()
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient<MechanicalFishingGun>()
                .AddIngredient<LunarRod>()
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient<FishingGun>()
                .AddIngredient<LunarRod>()
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Boomstick, 1)
                .AddIngredient<LunarRod>()
                .AddTile(TileID.WorkBenches)
                .Register();

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 70f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 20f;
            double offsetAngle;
            int i;
            for (i = 0; i < 20; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}

