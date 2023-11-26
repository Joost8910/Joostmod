using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Melee
{
    public class TwinClaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twin Claws");
            Tooltip.SetDefault("Right click for a powerful leaping slash");
        }
        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 26;
            Item.height = 24;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 3.5f;
            Item.value = 50000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item7;
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Claw").Type;
            Item.shootSpeed = 4f;
            Item.channel = true;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}


