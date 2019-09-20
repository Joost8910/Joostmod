using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Weapons
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
			item.damage = 180;
			item.magic = true;
            item.mana = 100;
			item.width = 36;
			item.height = 36;
			item.useTime = 48;
			item.useAnimation = 48;
			item.reuseDelay = 5;
			item.useStyle = 4;
			item.knockBack = 2;
			item.value = 500000;
			item.rare = 8;
			item.UseSound = SoundID.Item8;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = mod.ProjectileType("FocusSouls");
			item.shootSpeed = 5f;
        }
        public override bool CanUseItem(Player player)
        {
            if ((player.ownedProjectileCounts[item.shoot]) > 0)
            {
                return false;
            }
            else return true;
        }
    }
}

