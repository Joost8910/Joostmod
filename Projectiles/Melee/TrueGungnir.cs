using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class TrueGungnir : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Gungnir");
        }
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.scale = 1.2f;
            Projectile.aiStyle = 19;
            Projectile.timeLeft = 90;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.light = 0.5f;
            Projectile.ownerHitCheck = true;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 8;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.direction = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            float speed = player.GetAttackSpeed(DamageClass.Melee);
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
                speed = 27f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee) * Projectile.scale;
                Projectile.localNPCHitCooldown = (int)(6 / (speed / Projectile.scale));
                Projectile.width = (int)(54 * Projectile.scale);
                Projectile.height = (int)(54 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            Projectile.position += Projectile.velocity * speed * Projectile.ai[0];
            Projectile.position = player.RotatedRelativePoint(player.MountedCenter) - Projectile.Size / 2;
            Projectile.position += Projectile.velocity * Projectile.ai[0]; if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[0] = 3f;
                Projectile.netUpdate = true;
            }
            if (player.itemAnimation < player.itemAnimationMax / 2)
            {
                if (Projectile.ai[1] == 0)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity, ModContent.ProjectileType<TrueGungnirBeam>(), Projectile.damage, Projectile.knockBack / 2, Projectile.owner);
                    Projectile.ai[1]++;
                }
                Projectile.ai[0] -= 2.3f;
            }
            else
            {
                Projectile.ai[0] += 2.3f;
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
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition - vel * 90 * Projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}