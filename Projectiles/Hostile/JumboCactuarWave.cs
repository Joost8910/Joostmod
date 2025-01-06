using JoostMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class JumboCactuarWave : ModProjectile
    {
        public override string Texture => "JoostMod/Projectiles/Hostile/ShockWave";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Wave");
        }
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.aiStyle = 0;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            if (Projectile.localAI[0] < 10)
            {
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.spriteDirection = Projectile.direction;
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[0] = 10;
                Projectile.localAI[1] = Projectile.ai[1];
                Projectile.localAI[2] = Projectile.ai[2];
                Projectile.timeLeft = (int)Projectile.ai[0];
            }
            if (Projectile.timeLeft < 30)
            {
                //Projectile.scale = Projectile.timeLeft * (Projectile.ai[0] / 30f);
                Projectile.ai[1] = Projectile.timeLeft * (Projectile.localAI[1] / 30f);
                Projectile.ai[2] = Projectile.timeLeft * (Projectile.localAI[2] / 30f);
            }
            //Projectile.scale = Projectile.timeLeft * 0.02f;
            Projectile.position.X += Projectile.ai[1] * 16 * Projectile.spriteDirection;
            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.position.Y -= 16 * Projectile.ai[2];
            }

            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position.X, Projectile.position.Y, 0, 15f, ModContent.ProjectileType<JumboCactuarWave1>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.spriteDirection, Projectile.ai[1], Projectile.ai[2]);
        }
    }
    public class JumboCactuarWave1 : ModProjectile
    {
        public override string Texture => "JoostMod/Projectiles/Hostile/ShockWave";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sand Wave");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 2;
            height = 2;
            fallThrough = false;
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            Vector2 posi = new Vector2(Projectile.position.X, Projectile.position.Y + 4);
            Point pos = posi.ToTileCoordinates();
            Tile tileSafely = Framing.GetTileSafely(pos.X, pos.Y);
            if (tileSafely.HasTile)
            {
                Tile tileSafely2 = Framing.GetTileSafely(pos.X, pos.Y - 1);
                if (!tileSafely2.HasTile || !Main.tileSolid[tileSafely2.TileType] || Main.tileSolidTop[tileSafely2.TileType])
                {
                    for (int d = 0; d < 6; d++)
                    {
                        Dust dust = Main.dust[WorldGen.KillTile_MakeTileDust(pos.X, pos.Y, tileSafely)];
                        dust.velocity.Y = (dust.velocity.Y - 5) * Main.rand.NextFloat();
                        dust.velocity.X = 0;
                    }
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y + 32 + (int)(-56 * Projectile.ai[2]), 0, 0, ModContent.ProjectileType<JumboCactuarWave2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0], Projectile.ai[1], Projectile.ai[2]);
                }
            }
        }

    }

    public class JumboCactuarWave2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Golem");
            Main.projFrames[Projectile.type] = 10;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 56;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 11;
            Projectile.tileCollide = false;
            CooldownSlot = 1;
        }
        public override bool PreAI()
        {
            Projectile.direction = (int)Projectile.ai[0];
            //Projectile.scale = Projectile.ai[1];
            Projectile.spriteDirection = Projectile.direction;
            Projectile.width = (int)(18 * Projectile.ai[1]);
            if (Projectile.timeLeft > 10)
            {
                Projectile.frame = 0;
                Projectile.height = (int)(56 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft == 10)
            {
                Projectile.position.Y = Projectile.position.Y - 26 * Projectile.ai[2];
                Projectile.frame = 1;
                Projectile.height = (int)(82 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft == 9)
            {
                Projectile.position.Y = Projectile.position.Y - 6 * Projectile.ai[2];
                Projectile.frame = 2;
                Projectile.height = (int)(88 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft == 8)
            {
                Projectile.frame = 3;
            }
            if (Projectile.timeLeft == 7)
            {
                Projectile.position.Y = Projectile.position.Y + 6 * Projectile.ai[2];
                Projectile.frame = 4;
                Projectile.height = (int)(82 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft == 6)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.ai[2];
                Projectile.frame = 5;
                Projectile.height = (int)(66 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft == 5)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.ai[2];
                Projectile.frame = 6;
                Projectile.height = (int)(50 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft == 4)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.ai[2];
                Projectile.frame = 7;
                Projectile.height = (int)(34 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft == 3)
            {
                Projectile.position.Y = Projectile.position.Y + 16 * Projectile.ai[2];
                Projectile.frame = 8;
                Projectile.height = (int)(18 * Projectile.ai[2]);
            }
            if (Projectile.timeLeft <= 2)
            {
                Projectile.position.Y = Projectile.position.Y + 8 * Projectile.ai[2];
                Projectile.frame = 9;
                Projectile.height = (int)(10 * Projectile.ai[2]);
            }
            return base.PreAI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.velocity.Y -= Projectile.knockBack * target.knockBackResist;
            if (target.knockBackResist > 0)
            {
                target.velocity.X = 0;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)/* tModPorter Note: Removed. Use OnHitPlayer and check info.PvP */
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y -= Projectile.knockBack;
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.DisableKnockback();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor;
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle rectangle = new Rectangle(0, Projectile.frame * 90, texture.Width, texture.Height / Main.projFrames[Projectile.type]);
            Vector2 vector = new Vector2(texture.Width / 2f, texture.Height / Main.projFrames[Projectile.type] / 2f);
            Vector2 scale = new Vector2(Projectile.ai[1], Projectile.ai[2]);

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, color, Projectile.rotation, vector, scale, effects, 0);
            return false;
        }
    }
}
