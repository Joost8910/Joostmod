using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;

namespace JoostMod.Items.Weapons.Summon
{
    public class PufferfishStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pufferfish Staff");
            Tooltip.SetDefault("Summons a floating pufferfish to fight for you");
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 27000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<fishMinion>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.FishMinionBuff>();
            Item.buffTime = 3600;
        }
        /*
		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			if (JoostMod.instance.battleRodsLoaded)
			{
				damage *= BattleRodsFishingDamage / player.GetDamage(DamageClass.Summon);
			}
		}
		public override void ModifyWeaponCrit(Player player, ref float crit)
		{
			if (JoostMod.instance.battleRodsLoaded)
			{
				crit += BattleRodsCrit;
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
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }
        public override bool CanShoot(Player player) => player.altFunctionUse != 2;

    }
}


