using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Tools
{
    public class FishingGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fishing Shotgun");
            Tooltip.SetDefault("Fires multiple fishing hooks");
        }
        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 14;
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = false;
            Item.shoot = Mod.Find<ModProjectile>("Fishhook").Type;
            Item.shootSpeed = 11f;
            Item.fishingPole = 15;
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
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Boomstick)
                .AddIngredient(ItemID.ReinforcedFishingPole)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread / 2;
            double deltaAngle = spread / 4f;
            double offsetAngle;
            int i;
            for (i = 0; i < 4; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(source, position.X, position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}

