using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Drawing;

namespace JoostMod.Projectiles.Melee
{
    public class TrueDarkLance : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Dark Lance");
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.scale = 1.1f;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 190;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.light = 0.5f;
            Projectile.ownerHitCheck = true;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        float timePoint = 0.6f;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.direction = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            //float speed = player.GetAttackSpeed(DamageClass.Melee);
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
                //speed = 36f / player.itemAnimationMax * Projectile.scale;
                //Projectile.localNPCHitCooldown = (int)(10 / (speed / Projectile.scale));
                Projectile.width = (int)(28 * Projectile.scale);
                Projectile.height = (int)(28 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            //Projectile.localNPCHitCooldown = player.itemAnimationMax / 2;
            float length = 150f * Projectile.scale;
            float lengthBase = Projectile.width;
            float stabAmount = length / (player.itemAnimationMax * timePoint);
            if (Projectile.ai[0] == 0f)
            {
                Projectile.velocity.Normalize();
                Projectile.ai[0] = lengthBase;
                Projectile.netUpdate = true;
            }
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter) - Projectile.Size / 2;
            Projectile.position += Projectile.velocity * Projectile.ai[0];
            if (Projectile.ai[1] == 0)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    /*
                    Vector2 velA = Projectile.velocity.RotatedBy(9 * Math.PI / 180);
                    Vector2 velB = Projectile.velocity.RotatedBy(-9 * Math.PI / 180);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velA * speed * stabMult, ModContent.ProjectileType<TrueDarkLanceBeam>(), Projectile.damage, Projectile.knockBack / 2, Projectile.owner, Projectile.whoAmI, 22);
                    */
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity * stabAmount, ModContent.ProjectileType<TrueDarkLanceBeam>(), (int)(Projectile.damage * 1f), Projectile.knockBack / 2, Projectile.owner, Projectile.whoAmI);
                    //Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, velB * speed * stabMult, ModContent.ProjectileType<TrueDarkLanceBeam>(), Projectile.damage, Projectile.knockBack / 2, Projectile.owner, Projectile.whoAmI, -22);
                }
                Projectile.ai[1]++;
            }
            if (player.itemAnimation < player.itemAnimationMax * (1f - timePoint))
            { //Backwards
                Projectile.ai[0] -= stabAmount;
                if (Main.rand.NextBool(2))
                {
                    int num21 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.2f + (float)(Projectile.direction * 3), Projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
                    Main.dust[num21].noGravity = true;
                    Main.dust[num21].velocity /= 2f;
                    num21 = Dust.NewDust(Projectile.position - Projectile.velocity * 2f, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 150, default(Color), 1.4f);
                    Main.dust[num21].velocity /= 5f;
                }
            }
            else //Forwards
            {
                Projectile.ai[0] += stabAmount;
                int num21 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.2f + (float)(Projectile.direction * 3), Projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
                Main.dust[num21].noGravity = true;
                Main.dust[num21].velocity /= 2f;
                num21 = Dust.NewDust(Projectile.position - Projectile.velocity * 2f, Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 150, default(Color), 1.4f);
                Main.dust[num21].velocity /= 5f;
            }
            if (player.itemAnimation == 0)
            {
                Projectile.Kill();
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Demonite, 0f, 0f, 150, default(Color), 1.4f);
            }
        }
        public override bool? CanDamage()
        {
            Player player = Main.player[Projectile.owner];
            if (player.itemAnimation > player.itemAnimationMax * (1f - timePoint) * 0.5f)
            {
                return false;
            }
            return base.CanDamage();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.NightsEdge, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Hitbox.ClosestPointInRect(Projectile.Center)
            }, default(int?));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.NightsEdge, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Hitbox.ClosestPointInRect(Projectile.Center)
            }, default(int?));
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            Vector2 vel = Projectile.velocity;
            vel.Normalize();
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition - vel * 100 * Projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}