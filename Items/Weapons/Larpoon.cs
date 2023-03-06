using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace JoostMod.Items.Weapons
{
	public class Larpoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Larpoon");
			Tooltip.SetDefault("Launches a harpoon that shoots lasers at enemies\n" + 
                "Fished in the ocean after a mechanical boss has been defeated\n" + 
                "'Live-Action-Role-Playing Harpoon' - Loki");
		}
		public override void SetDefaults()
		{
			Item.damage = 42;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 46;
			Item.height = 22;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; 
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 6, 5, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("Larpoon").Type; 
			Item.shootSpeed = 18f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                damage *= BattleRodsFishingDamage / player.GetDamage(DamageClass.Ranged);
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
        public override bool CanUseItem(Player player)      
        {
			return player.ownedProjectileCounts[Item.shoot] == 0;
        }
	}
}
