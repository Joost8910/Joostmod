using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Projectiles.Minions;
using JoostMod.Buffs;
using Terraria.Localization;
using System.Collections.Generic;

namespace JoostMod.Items.Weapons.Summon
{
    public class GnomeStaff : ModItem
    {
        private int projDamageLimit;
        public static LocalizedText MaxBlockDamageText { get; private set; }
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TailWhipDebuff.TagDamage);

        public override void SetStaticDefaults()
        {
            MaxBlockDamageText = this.GetLocalization("MaxBlockDamage");
            // DisplayName.SetDefault("Gnome Staff");
            /* Tooltip.SetDefault("'With silver beard and crimson hat the gnome warriors fight valiantly for their people'\n" +
                "Summons a Gnome warrior\n" +
                "Hold right click to direct the gnome warriors to block\n" +
                "Gnome warriors cannot block projectiles with higher base damage than double their own damage\n" +
                "Left click while they're blocking to direct them to shield bash"); */
        }
        public override void SetDefaults()
        {
            Item.damage = 38;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
            Item.width = 52;
            Item.height = 50;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 3;
            Item.value = 64000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item44;
            Item.shoot = ModContent.ProjectileType<Gnome>();
            Item.shootSpeed = 7f;
            Item.buffType = ModContent.BuffType<Buffs.GnomeBuff>();
            Item.buffTime = 3600;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            projDamageLimit = (int)(damage.ApplyTo(Item.damage) * Main.GameModeInfo.EnemyDamageMultiplier * 2f);
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            list.Add(new TooltipLine(Mod, "MaxDamageCanBlock", MaxBlockDamageText.Format(projDamageLimit)));
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }
        public override bool CanShoot(Player player) => player.altFunctionUse != 2;
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GardenGnome)
                .AddRecipeGroup(nameof(ItemID.AdamantiteBar), 8)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

    }
}


