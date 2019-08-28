using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace JoostMod.Items.Weapons
{
    public class Sunfish : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sunfish");
            Tooltip.SetDefault("Creates a controllable sunbeam\n" +
                "Fished in the Lihzahrd Temple after the Golem has been defeated");
        }
        public override void SetDefaults()
        {
            item.damage = 60;
            item.magic = true;
            item.noMelee = true;
            item.channel = true;
            item.mana = 10;
            item.width = 42;
            item.height = 42;
            item.useTime = 16;
            item.useAnimation = 16;
            item.useStyle = 4;
            item.noMelee = true;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 8, 5, 0);
            item.rare = 5;
            item.UseSound = SoundID.DD2_DarkMageCastHeal;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SunLaser");
            item.shootSpeed = 14f;
        }
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                mult *= BattleRodsFishingDamage / player.magicDamage;
            }
        }
        public override void GetWeaponCrit(Player player, ref int crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.magicCrit;
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

    }
}
