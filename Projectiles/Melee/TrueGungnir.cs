using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.GameContent.Drawing;

namespace JoostMod.Projectiles.Melee
{
    public class TrueGungnir : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Gungnir");
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.scale = 1.2f;
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

        readonly float timePoint = 0.6f;
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
                //speed = 27f / player.itemAnimationMax * Projectile.scale;
                //Projectile.localNPCHitCooldown = (int)(6 / (speed / Projectile.scale));
                Projectile.width = (int)(34 * Projectile.scale);
                Projectile.height = (int)(34 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            //Projectile.localNPCHitCooldown = player.itemAnimationMax / 2;
            float length = 180f * Projectile.scale;
            float lengthBase = Projectile.width;
            float stabAmount = length / (player.itemAnimationMax * timePoint);
            if (Projectile.ai[0] == 0f)
            {
                Projectile.velocity.Normalize();
                Projectile.ai[0] = lengthBase;
                Projectile.netUpdate = true;
            }
            //float stabMult = 23.1f;
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter) - Projectile.Size / 2;
            Projectile.position += Projectile.velocity * Projectile.ai[0];
            if (Projectile.ai[1] == 0)
            {
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity * stabAmount * 0.75f, ModContent.ProjectileType<TrueGungnirBeam>(), (int)(Projectile.damage * 1f), Projectile.knockBack / 2, Projectile.owner, Projectile.whoAmI, stabAmount * 0.75f);
                Projectile.ai[1]++;
                //SoundEngine.PlaySound(SoundID.Item8, Projectile.Center);
            }
            if (player.itemAnimation < player.itemAnimationMax * (1f - timePoint))
            {
                Projectile.ai[0] -= stabAmount;
            }
            else
            {
                Projectile.ai[0] += stabAmount;
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
            if (Main.rand.NextBool(3))
            {
                int num22 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 200, default(Color), 1.2f);
                Main.dust[num22].velocity += Projectile.velocity * 0.3f;
                Main.dust[num22].velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                int num23 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TintableDustLighted, 0f, 0f, 254, default(Color), 0.3f);
                Main.dust[num23].velocity += Projectile.velocity * 0.5f;
                Main.dust[num23].velocity *= 0.5f;
            }
        }
        public override bool? CanDamage()
        {
            Player player = Main.player[Projectile.owner];
            if (player.itemAnimation > player.itemAnimationMax * (1f - timePoint))
            {
                return false;
            }
            return base.CanDamage();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Hitbox.ClosestPointInRect(Projectile.Center)
            }, default(int?));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, new ParticleOrchestraSettings
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
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition - vel * 110 * Projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}