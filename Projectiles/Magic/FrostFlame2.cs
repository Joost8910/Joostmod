using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class FrostFlame2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("FrostFlame");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 18;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 110;
            Projectile.tileCollide = false;
            Projectile.alpha = 35;
            Projectile.light = 0.75f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.coldDamage = true;
        }
        public override void AI()
        {
            if (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidAmount > 80 && (Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 0 || Main.tile[Projectile.Center.ToTileCoordinates().X, Projectile.Center.ToTileCoordinates().Y].LiquidType == 2))
            {
                Projectile.Kill();
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 4;
            }
            if (Projectile.timeLeft < 60)
            {
                Projectile.scale = Projectile.timeLeft * 0.016f;
                Projectile.position.X = Projectile.Center.X - (int)(16 * Projectile.scale) / 2;
                Projectile.position.Y = Projectile.Center.Y - (int)(18 * Projectile.scale) / 2;
                Projectile.width = (int)(16 * Projectile.scale);
                Projectile.height = (int)(18 * Projectile.scale);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + new Vector2(0f, Projectile.height * 1.5f), new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type])), Color.White, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
        public override void OnHitNPC(NPC n, int damage, float knockback, bool crit)
        {
            Player owner = Main.player[Projectile.owner];
            n.AddBuff(44, 60);
        }
    }
}

