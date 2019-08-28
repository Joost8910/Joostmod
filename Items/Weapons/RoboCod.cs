using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace JoostMod.Items.Weapons
{
    public class RoboCod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Robocod");
            Tooltip.SetDefault("Fires a spread of lasers\n" +
                "Fished in the Jungle or Tundra after a mechanical boss has been defeated");
        }
        public override void SetDefaults()
        {
            item.damage = 35;
            item.ranged = true;
            item.width = 34;
            item.height = 34;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 5, 5, 0);
            item.rare = 5;
            item.UseSound = SoundID.Item75;
            item.autoReuse = true;
            item.shoot = 440;
            item.shootSpeed = 16f;
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3 + Main.rand.Next(4);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(12));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
