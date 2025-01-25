using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;
using Terraria.Graphics.Shaders;
using Terraria.DataStructures;

namespace JoostMod.Projectiles.Magic
{
    public class DavidLaser : ModProjectile
    {
        private const int MAX_CHARGE = 50;
        private const float MOVE_DISTANCE = 100f;       //The distance charge particle from the player center
        private int sound = 0;
        public float Distance
        {
            get { return Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }

        public float WidthScale
        {
            get { return Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }
        public float Charge
        {
            get { return Projectile.localAI[0]; }
            set { Projectile.localAI[0] = value; }
        }
        public float ManaCounter
        {
            get { return Projectile.localAI[1]; }
            set { Projectile.localAI[1] = value; }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laser of David");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 3;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.hide = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Charge == MAX_CHARGE)
            {
                Vector2 unit = Projectile.velocity;
                DrawLaser(TextureAssets.Projectile[Projectile.type].Value,
                    Main.player[Projectile.owner].Center, unit, 10, Projectile.damage,
                    -1.57f, 1f, 1000f, new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f)), (int)MOVE_DISTANCE);
            }
            return false;

        }

        /// <summary>
        /// The core function of drawing a laser
        /// </summary>
        public void DrawLaser(Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default, int transDist = 50)
        {
            Vector2 origin = start;
            float r = unit.ToRotation() + rotation;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);

            //color = Main.hslToRgb((ManaCounter * 0.01f) % 1f, 1f, 0.5f, 255);
            //color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);

            MiscShaderData shaderData = GameShaders.Misc["JoostLaserBeam"];
            shaderData.UseColor(Color.White);
            shaderData.UseSecondaryColor(color);
            shaderData.UseImage0(TextureAssets.Projectile[Projectile.type]);
            shaderData.UseOpacity(0.5f);

            Vector2 scale2D = new(WidthScale, 1);

            #region Draw laser body
            for (int i = transDist + 3; i <= Distance - step; i += (int)step)
            {
                origin = start + i * unit;

                DrawData dataTrail = new DrawData(texture, origin - Main.screenPosition,
                    new Rectangle(0, 26, 38, 10), i < transDist ? Color.Transparent : color, r,
                    new Vector2(38 / 2, 0), scale2D, 0, 0);
                shaderData.Apply(dataTrail);
                dataTrail.Draw(Main.spriteBatch);
            }
            #endregion

            #region Draw laser tail
            DrawData data = new DrawData(texture, start + unit * (transDist - step) - Main.screenPosition,
                new Rectangle(0, 0, 38, 26), color, r, new Vector2(38 / 2, 26 / 2), scale2D, 0, 0);
            shaderData.Apply(data);
            data.Draw(Main.spriteBatch);
            #endregion

            #region Draw laser head
            data = new DrawData(texture, start + Distance * unit - Main.screenPosition,
                new Rectangle(0, 54, 38, 30), color, r, new Vector2(38 / 2, 30 / 2), scale2D, 0, 0);
            shaderData.Apply(data);
            data.Draw(Main.spriteBatch);
            #endregion

            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
        }

        /// <summary>
        /// Change the way of collision check of the projectile
        /// </summary>
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Charge == MAX_CHARGE)
            {
                Player p = Main.player[Projectile.owner];
                Vector2 unit = Projectile.velocity;
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), p.Center, p.Center + unit * Distance, 26 * WidthScale, ref point))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Change the behavior after hit a NPC
        /// </summary>
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ScalingArmorPenetration += 1f;
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)/* tModPorter Note: Removed. Use ModifyHitPlayer and check modifiers.PvP */
        {
            modifiers.ScalingArmorPenetration += 1f;
        }

        /// <summary>
        /// The AI of the projectile
        /// </summary>
        public override void AI()
        {

            Vector2 mousePos = Main.MouseWorld;
            Player player = Main.player[Projectile.owner];
            Color dustColor = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));

            #region Set projectile position
            if (Projectile.owner == Main.myPlayer) // Multiplayer support
            {
                Vector2 diff = mousePos - player.Center;
                diff.Normalize();
                float home = Charge >= MAX_CHARGE ? 12f : 8f;
                Projectile.velocity = ((home - 1f) * Projectile.velocity + diff) / home;
                Projectile.velocity.Normalize();
                Projectile.direction = Main.MouseWorld.X > player.Center.X ? 1 : -1;
                Projectile.netUpdate = true;
            }
            Projectile.position = player.Center + Projectile.velocity * MOVE_DISTANCE - new Vector2(Projectile.width / 2, Projectile.height / 2);
            Projectile.timeLeft = 2;
            int dir = Projectile.direction;
            player.ChangeDir(dir);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(Projectile.velocity.Y * dir, Projectile.velocity.X * dir);
            #endregion

            #region Charging process
            // Kill the projectile if the player stops channeling
            if (!player.channel || player.dead || !player.active || player.noItems || player.CCed)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.ai[2] > 0)
                {
                    if (player.CheckMana(player.inventory[player.selectedItem].mana, true))
                    {
                        Projectile.ai[2] = 0;
                    }
                    WidthScale -= 0.05f;
                    if (WidthScale <= 0) 
                    { 
                        Projectile.Kill(); 
                    }
                }
                else
                {
                    if (ManaCounter % 60 == 59 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                    {
                        Projectile.ai[2]++;
                        //Projectile.Kill();
                    }
                }
                Vector2 offset = Projectile.velocity;
                offset *= MOVE_DISTANCE - 20;
                Vector2 pos = player.Center + offset - new Vector2(10, 10);
                if (Charge == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item13, Projectile.position);
                }
                if (Charge < MAX_CHARGE)
                {
                    Charge++;
                }
                if (Charge >= MAX_CHARGE)
                {
                    ManaCounter++;
                    if (WidthScale < 1 && Projectile.ai[2] == 0)
                    {
                        WidthScale += 0.2f;
                    }
                    if (sound == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item15.WithPitchOffset(0.2f).WithVolumeScale(1.2f), Projectile.position);
                    }
                    sound++;
                }
                if (sound >= 16)
                {
                    SoundEngine.PlaySound(SoundID.Item15.WithPitchOffset(0.1f), Projectile.position);
                    sound = 1;
                }
                int chargeFact = (int)(Charge / 20f);
                Vector2 dustVelocity = Vector2.UnitX * 18f;
                dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f, default);
                Vector2 spawnPos = Projectile.Center + dustVelocity;
                for (int k = 0; k < chargeFact + 1; k++)
                {
                    Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
                    Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, DustID.BubbleBurst_Green, Projectile.velocity.X / 2f,
                        Projectile.velocity.Y / 2f, 0, dustColor, 1f)];
                    dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
                    dust.noGravity = true;
                    dust.scale = Main.rand.Next(10, 20) * 0.05f;
                }
            }
            #endregion


            #region Set laser tail position and dusts
            if (Charge < MAX_CHARGE) return;
            Vector2 start = player.Center;
            Vector2 unit = Projectile.velocity;
            unit *= -1;
            for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
            {
                start = player.Center + Projectile.velocity * Distance;
                Vector2 collision = Collision.TileCollision(start - Projectile.velocity * 5, Projectile.velocity * 5, 1, 1);
                float d = (float)Math.Round(collision.Length());
                /*
                if (d < 5)
                {
                    Distance -= 5 - d;
                    break;
                
                */
                if (d < 5 || !Collision.CanHitLine(player.Center, 1, 1, start, 1, 1))
                {
                    Distance -= 5 - d;
                    break;
                }
            }

            Vector2 dustPos = player.Center + Projectile.velocity * Distance;
            for (int i = 0; i < 2; ++i)
            {
                /*float num1 = projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);*/
                Vector2 dustVel = unit;
                Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.BubbleBurst_Green, dustVel.X, dustVel.Y, 0, dustColor, 1f)];
                dust.noGravity = true;
                dust.scale = 2f;


                dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + unit * -65, 0, 0, DustID.BubbleBurst_Green, unit.X * i, unit.Y * i, 0, dustColor, 1f)];
                dust.noGravity = true;
                dust.fadeIn = 0f;
                dust.scale = 0.88f;
            }
            if (Main.rand.NextBool(5))
            {
                Vector2 offset = Projectile.velocity.RotatedBy(1.57f, new Vector2()) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, dustColor, 1.5f)];
                dust.velocity = dust.velocity * 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);

                unit = dustPos - Main.player[Projectile.owner].Center;
                unit.Normalize();
                dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + 65 * unit, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, dustColor, 1.5f)];
                dust.velocity = dust.velocity * 0.5f;
                dust.velocity.Y = -Math.Abs(dust.velocity.Y);
            }
            #endregion

            //Add lights
            DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
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
