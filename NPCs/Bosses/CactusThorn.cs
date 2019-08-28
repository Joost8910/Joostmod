using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JoostMod.NPCs.Bosses
{
    public class CactusThorn : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Thorn");
        }
        public override void SetDefaults()
        {
            npc.width = 10;
            npc.height = 10;
            npc.defense = 0;
            npc.lifeMax = 1;
            if (Main.expertMode)
            {
                npc.damage = 60;
            }
            else
            {
                npc.damage = 30;
            }
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath9;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0;
            npc.behindTiles = true;
            npc.aiStyle = -1;
            npc.noGravity = false;
            npc.noTileCollide = false;
        }
        public override void PostDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.npcTexture[npc.type];
            sb.Draw(tex, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), Color.GreenYellow, npc.rotation, new Vector2(tex.Width / 2, tex.Height / 2), npc.scale, SpriteEffects.None, 0f);
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override void AI()
        {
            npc.rotation = (float)System.Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (npc.ai[0] <= 1)
            {
                npc.velocity = new Vector2(Main.rand.Next(-3, 4), Main.rand.Next(2) - 2);
                npc.netUpdate = true;
            }
            else
            {
                if (npc.velocity.X != npc.oldVelocity.X)
                {
                    npc.velocity.X = -npc.oldVelocity.X;
                }
                if (npc.velocity.Y != npc.oldVelocity.Y)
                {
                    npc.velocity.Y = -npc.oldVelocity.Y * 0.9f;
                    npc.ai[0] += 20;
                }
            }
            npc.ai[0]++;
            if (npc.ai[0] > 180)
            {
                npc.active = false;
            }
        }
    }
}

