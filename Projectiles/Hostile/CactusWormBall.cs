using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;

namespace JoostMod.Projectiles.Hostile
{
    public class CactusWormBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Ball");
        }
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.aiStyle = 1;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            AIType = ProjectileID.Bullet;
        }
        public override void PostDraw(Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), Color.GreenYellow, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, SpriteEffects.None, 0);
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 26;
            height = 26;
            return true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X += 4;
            hitbox.Y += 4;
            hitbox.Width = 30;
            hitbox.Height = 30;
        }
        public override void AI()
        {
            if (Projectile.velocity.Length() > 0)
            {
                Projectile.rotation = Projectile.timeLeft * -Projectile.direction * 0.0174f * 12;
            }
            if (Projectile.timeLeft < 280)
            {
                Projectile.tileCollide = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.timeLeft -= 100;
            Projectile.damage -= 10;
            return false;
        }
        /*public override void Kill(int timeLeft)
        {
            if (Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 4; i++)
                {
                    NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, mod.NPCType("CactusThorn"));
                }
            }
        }*/
    }
}

