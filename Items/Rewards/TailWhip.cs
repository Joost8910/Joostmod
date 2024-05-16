//TODO: Make into 1.4 Summon Whip, but with a funky flail thing as a right click function
using JoostMod.Buffs;
using JoostMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JoostMod.Items.Rewards
{
	public class TailWhip : ModItem
	{
        private int projDamageLimit;
        public static LocalizedText MaxDeflectDamageText { get; private set; }
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TailWhipDebuff.TagDamage); 
        public override void SetStaticDefaults()
		{
            MaxDeflectDamageText = this.GetLocalization("MaxDeflectDamage");
			// DisplayName.SetDefault("Tail Whip");
            // Tooltip.SetDefault("Envenoms struck targets");
		}
		public override void SetDefaults()
        {
            Item.DefaultToWhip(ModContent.ProjectileType<Projectiles.Summon.TailWhip>(), 42, 3, 4f);
            Item.width = 30;
            Item.height = 32;
			Item.value = 80000;
			Item.rare = ItemRarityID.Orange;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2) 
            { 
                type = ModContent.ProjectileType<TailWhip2>();
                knockback *= 4;
            }
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.UseSound = new("Terraria/Sounds/Custom/dd2_sky_dragons_fury_swing_1");
            }
            else
            {
                Item.UseSound = SoundID.Item152;
            }
            if (player.ownedProjectileCounts[Item.shoot] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<TailWhip2>()] > 0)
            {
                return false;
            }
            return base.CanUseItem(player);
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            projDamageLimit = (int)(damage.ApplyTo(Item.damage) * Main.GameModeInfo.EnemyDamageMultiplier * 2f);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(230, 204, 128);
                }
            }
            list.Add(new TooltipLine(Mod, "MaxDamageCanDeflect", MaxDeflectDamageText.Format(projDamageLimit)));
        }
        public override void HoldItem(Player player)
        {
            if (player.altFunctionUse == 2 || player.itemAnimation == 0)
            {
                float mul = Main.GameModeInfo.EnemyDamageMultiplier;
                if (Main.GameModeInfo.IsJourneyMode)
                {
                    CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
                    if (power.GetIsUnlocked())
                    {
                        mul = power.StrengthMultiplierToGiveNPCs;
                    }
                }
                int damage = player.GetWeaponDamage(Item);
                foreach (Projectile proj in Main.ActiveProjectiles)
                {
                    if (proj.Distance(player.Center) < 200 && TailWhip2.ProjCheck(proj) && (proj.hostile && proj.damage <= damage || 
                        player.whoAmI != proj.owner && player.hostile && Main.player[proj.owner].hostile && (player.team == 0 || player.team != Main.player[proj.owner].team) && CombinedHooks.CanHitPvpWithProj(proj, player) && proj.damage * mul <= damage))
                    {
                        Dust.NewDustDirect(proj.position, proj.width, proj.height, 31, proj.velocity.X, proj.velocity.Y, 0, default(Color), 1f);
                    }
                }
            }
        }
    }
}

