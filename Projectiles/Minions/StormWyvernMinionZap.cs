using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace JoostMod.Projectiles.Minions
{
    public class StormWyvernMinionZap : ModProjectile
    {
        private const float MOVE_DISTANCE = 50f;       //The distance charge particle from the player center
        private const float MAX_DISTANCE = 3200f;       //The maximum length of projectile
        //private int sound = 0;
        public float Distance
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Strike");
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 20;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 unit = Projectile.velocity;
            DrawLaser(TextureAssets.Projectile[Projectile.type].Value,
                Main.projectile[(int)Projectile.ai[0]].Center, unit, 26, Projectile.damage,
                -1.57f, 1f, MAX_DISTANCE, Color.White, (int)MOVE_DISTANCE);
            return false;
        }

        /// <summary>
        /// The core function of drawing a laser
        /// </summary>
        public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 1200f, Color color = default, int transDist = 50)
        {
            Vector2 origin = start;
            float r = unit.ToRotation() + rotation;
            int xFrame = Projectile.timeLeft / 3 % 3;

            #region Draw laser body
            for (float i = transDist; i <= Distance; i += step)
            {
                origin = start + i * unit;
                Main.EntitySpriteDraw(texture, origin - Main.screenPosition,
                    new Rectangle(28 * xFrame, 28, 26, 28), i < transDist ? Color.Transparent : color, r,
                    new Vector2(26 / 2, 28 / 2), scale, 0, 0);
            }
            #endregion

            #region Draw laser tail
            Main.EntitySpriteDraw(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(28 * xFrame, 0, 26, 28), color, r, new Vector2(26 / 2, 28 / 2), scale, 0, 0);
            #endregion

            #region Draw laser head
            Main.EntitySpriteDraw(texture, start + Distance * unit - Main.screenPosition,
                new Rectangle(28 * xFrame, 56, 26, 28), color, r, new Vector2(26 / 2, 28 / 2), scale, 0, 0);
            #endregion
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Projectile owner = Main.projectile[(int)Projectile.ai[0]];
            Vector2 unit = Projectile.velocity;
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), owner.Center, owner.Center + unit * Distance, 18, ref point))
            {
                return true;
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = true;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            crit = true;
        }
        public override void AI()
        {
            Projectile owner = Main.projectile[(int)Projectile.ai[0]];
            Projectile.position = owner.Center + Projectile.velocity * MOVE_DISTANCE - new Vector2(Projectile.width / 2, Projectile.height / 2);

            #region Set laser tail position and dusts
            Vector2 start = owner.Center;
            Vector2 unit = Projectile.velocity;
            unit *= -1;
            for (Distance = MOVE_DISTANCE; Distance <= MAX_DISTANCE; Distance += 5f)
            {
                start = owner.Center + Projectile.velocity * Distance;
                if (!Collision.CanHitLine(owner.Center, 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }

            Vector2 dustPos = owner.Center + Projectile.velocity * Distance;
            //Imported dust code from source because I'm lazy
            for (int i = 0; i < 2; ++i)
            {
                float num1 = Projectile.velocity.ToRotation() + (Main.rand.NextBool(2)? -1.0f : 1.0f) * 1.57f;
                float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
                Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Pixie, dustVel.X, dustVel.Y, 0, Color.White, 1f)];
                dust.noGravity = true;
                dust.scale = 1.2f;
                // At this part, I was messing with the dusts going across the laser beam very fast, but only really works properly horizontally now
                dust = Main.dust[Dust.NewDust(owner.Center + unit * 5f, 0, 0, DustID.Pixie, unit.X, unit.Y, 0, Color.White, 1f)];
                dust.fadeIn = 0f;
                dust.noGravity = true;
                dust.scale = 0.88f;
            }
            #endregion

            //Add lights
            DelegateMethods.v3_1 = new Vector3(1f, 0.8f, 0.2f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = Projectile.velocity;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
        }
    }
}
