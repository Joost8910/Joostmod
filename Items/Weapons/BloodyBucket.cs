using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace JoostMod.Items.Weapons
{
    public class BloodyBucket : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloody Bucket");
            Tooltip.SetDefault("Fished in the Crimson after the Brain of Cthulhu has been defeated");
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 30;
            Item.height = 28;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = 75000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item21;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("BloodyWater").Type;
            Item.shootSpeed = 12f;
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
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                perturbedSpeed *= 1f - (Main.rand.NextFloat() * .3f); 
                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
