using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons.Magic
{
    public class FocusSouls : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Focus Souls");
            Tooltip.SetDefault("Fires multiple focused beams of souls");
        }
        public override void SetDefaults()
        {
            Item.damage = 180;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 100;
            Item.width = 36;
            Item.height = 36;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.reuseDelay = 5;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 2;
            Item.value = 500000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.noMelee = true;
            Item.shoot = Mod.Find<ModProjectile>("FocusSouls").Type;
            Item.shootSpeed = 5f;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Item.shoot] > 0)
            {
                return false;
            }
            else return true;
        }
    }
}

