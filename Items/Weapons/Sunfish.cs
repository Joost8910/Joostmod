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
            Item.damage = 60;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            Item.channel = true;
            Item.mana = 10;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 8, 5, 0);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.DD2_DarkMageCastHeal;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("SunLaser").Type;
            Item.shootSpeed = 14f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                damage *= BattleRodsFishingDamage / player.GetDamage(DamageClass.Magic);
            }
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.GetCritChance(DamageClass.Magic);
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
    }
}
