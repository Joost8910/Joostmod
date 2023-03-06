using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
{
	public class HomingSoulmass : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Homing Soulmass");
			Tooltip.SetDefault("Summons homing soulmasses that stay above you before seeking enemies\n" + 
                "Right click to target an enemy");
		}
		public override void SetDefaults()
		{
			Item.damage = 72;
			Item.DamageType = DamageClass.MagicSummonHybrid;
            Item.mana = 20;
			Item.width = 36;
			Item.height = 36;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.noMelee = true;
            Item.noUseGraphic = true;
			Item.knockBack = 3.5f;
			Item.value = 500000;
			Item.rare = ItemRarityID.Yellow;
			Item.autoReuse = false;
			Item.UseSound = SoundID.Item28;
			Item.shoot = Mod.Find<ModProjectile>("HomingSoulmass").Type;
			Item.shootSpeed = 0f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.magicDamage - 1f);
            mult *= player.magicDamageMult;
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += player.GetCritChance(DamageClass.Magic);
        }
        */

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " summon and magic damage"));
        }
        public override bool AltFunctionUse(Player player)
		{
			return true;
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(false);
            }
            return null;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                for (int l = 0; l < 200; l++)
                {
                    Projectile p = Main.projectile[l];
                    if (p.type == type && p.active && p.owner == player.whoAmI && p.localAI[1] == 0)
                    {
                        p.timeLeft = 0;
                    }
                }
                for (int i = 0; i < 5;  i++) 
                    Projectile.NewProjectile(source, player.Center.X, player.Center.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 45f * i, 0f);
			}
			return false;
		}
	}
}


