using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using JoostMod.Items.Legendaries;
using JoostMod.Projectiles.Fishhooks;
using JoostMod.Projectiles.Ranged;
using Terraria.GameContent.Events;

namespace JoostMod.Items.Legendaries.Weps
{
    public class BrokenFishingPole : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Uncle Carius's Fishing Pole");
            /* Tooltip.SetDefault("'This is the pole of ol' Uncle Carius\n" +
            "Deals more damage and throws more hooks as you kill bosses throughout the game\n" +
            "Fishing power is equivelent to damage\n" +
            "Right click to throw some fish, velocity increases with boss progression"); */
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<UncleCariusPole>();
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 40;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 0;
            Item.rare = ItemRarityID.Gray;
            Item.knockBack = 6;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<BrokenFishHook>();
            Item.shootSpeed = 15f;
            Item.fishingPole = 8; 
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
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

        //TODO: Figure out how to work UnuBattleRods compatability with current version
        /*
        public float BattleRodsFishingDamage
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberDamage; }
        }
        public int BattleRodsCrit
        {
            get { Player player = Main.player[Main.myPlayer]; return player.GetModPlayer<UnuBattleRods.FishPlayer>().bobberCrit; }
        }
        
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (JoostMod.instance.battleRodsLoaded)
            {
                crit += BattleRodsCrit - player.GetCritChance(DamageClass.Ranged);
            }
        }
        */
    }
}

