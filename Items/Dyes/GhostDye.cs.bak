using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Items.Dyes
{
    public class GhostDye : ModItem
    {
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                GameShaders.Armor.BindShader(
                    Item.type,
                    new ArmorShaderData(new Ref<Effect>(Mod.Assets.Request<Effect>("Shaders/JuiceArmorShaders", AssetRequestMode.ImmediateLoad).Value), "GhostShaderPass").UseColor(Color.Aquamarine) // Be sure to update the effect path and pass name here.
                );
            }
            DisplayName.SetDefault("Ghostly Dye"); 
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
