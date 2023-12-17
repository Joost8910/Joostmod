using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace JoostMod.Items.Weapons.Hybrid
{
    public class SoulSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Spear");
            Tooltip.SetDefault("Fire a piercing soul spear\n" + "Goes through blocks for a short distance");
        }
        public override void SetDefaults()
        {
            Item.damage = 225;
            Item.DamageType = DamageClass.Throwing;
            Item.CountsAsClass(DamageClass.Magic);
            Item.mana = 20;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 25;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 6.5f;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item8;
            Item.shoot = ModContent.ProjectileType<Projectiles.Hybrid.SoulSpear>();
            Item.shootSpeed = 15f;
        }
        /*
        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += (player.magicDamage - 1f);
            mult *= player.magicDamageMult;
        }
        */
        /*
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += player.GetCritChance(DamageClass.Throwing);
            crit /= 2;
        }
        */
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            Player player = Main.player[Main.myPlayer];
            int dmg = list.FindIndex(x => x.Name == "Damage");
            list.RemoveAt(dmg);
            list.Insert(dmg, new TooltipLine(Mod, "Damage", player.GetWeaponDamage(Item) + " throwing and magic damage"));
        }
        public override void HoldItem(Player player)
        {
            if (player.itemAnimation > 1 && player.itemAnimation > Item.useTime)
            {
                int dustType = 92;
                Vector2 pos = player.MountedCenter + new Vector2(6 * player.direction - 9, -16);
                int dustIndex = Dust.NewDust(pos, 18, 18, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.noGravity = true;
            }
            base.HoldItem(player);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.itemAnimation > Item.useTime)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_book_staff_cast_2"), player.Center); // 203
                return false;
            }
            SoundEngine.PlaySound(SoundID.Item28, player.Center);
            return true;
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            if (Main.rand.NextBool(3))
            {
                switch (rand.Next(12))
                {
                    case 1:
                        return PrefixID.Mystic;
                    case 2:
                        return PrefixID.Adept;
                    case 3:
                        return PrefixID.Masterful;
                    case 4:
                        return PrefixID.Inept;
                    case 5:
                        return PrefixID.Ignorant;
                    case 6:
                        return PrefixID.Deranged;
                    case 7:
                        return PrefixID.Intense;
                    case 8:
                        return PrefixID.Taboo;
                    case 9:
                        return PrefixID.Celestial;
                    case 10:
                        return PrefixID.Furious;
                    case 11:
                        return PrefixID.Manic;
                    default:
                        return PrefixID.Mythical;
                }
            }
            else if (Main.rand.NextBool(2))
            {
                switch (rand.Next(12))
                {
                    case 1:
                        return PrefixID.Rapid;
                    case 2:
                        return PrefixID.Hasty;
                    case 3:
                        return PrefixID.Intimidating;
                    case 4:
                        return PrefixID.Deadly2;
                    case 5:
                        return PrefixID.Staunch;
                    case 6:
                        return PrefixID.Awful;
                    case 7:
                        return PrefixID.Lethargic;
                    case 8:
                        return PrefixID.Awkward;
                    case 9:
                        return PrefixID.Powerful;
                    case 10:
                        return PrefixID.Frenzying;
                    case 11:
                        return PrefixID.Sighted;
                    default:
                        return PrefixID.Unreal;
                }
            }
            return base.ChoosePrefix(rand);
        }
    }
}


