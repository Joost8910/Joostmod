using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Dyes
{
    //Temporary tomfuckery until ported to 1.4.4
    public class DarkLanceBeamShader : ModItem
    {
        public override string Texture => "JoostMod/Items/Dyes/GhostDye";
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                GameShaders.Armor.BindShader(
                    Item.type,
                    new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Shaders/JuiceProjectileShaders", AssetRequestMode.ImmediateLoad).Value), "GungnirBeamShaderPass").UseColor(new Color(0.7f, 0.21f, 0.79f)).UseSecondaryColor(new Color(0.49f, 0.82f, 0f)) // Be sure to update the effect path and pass name here.
                );
            }
        }

        public override void SetDefaults()
        {
            // Item.dye will already be assigned to this item prior to SetDefaults because of the above GameShaders.Armor.BindShader code in Load().
            // This code here remembers Item.dye so that information isn't lost during CloneDefaults.
            int dye = Item.dye;

            Item.CloneDefaults(ItemID.GelDye); 

            Item.dye = dye;
        }
    }
}
