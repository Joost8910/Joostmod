using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class DreamSpinSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dream Spin Slash");
            Main.projFrames[Projectile.type] = 14;
        }
        public override void SetDefaults()
        {
            Projectile.width = 420;
            Projectile.height = 96;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 4;
            Projectile.extraUpdates = 1;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 4;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.ai[0]++;
            bool channeling = Projectile.ai[0] < 75 * 2 && (player.controlUseItem || Projectile.ai[0] < 30 * 2) && !player.noItems && !player.CCed;
            if (!channeling)
            {
                Projectile.Kill();
            }
            Projectile.frame = (Projectile.frame + 1) % 14;
            if (Projectile.ai[0] == 33 * 2)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/hero_nail_art_cyclone_slash_2"), Projectile.Center);
            }
            player.velocity.Y *= 0.9f;
            player.fallStart = (int)(player.position.Y / 16f);
            player.ChangeDir(Projectile.direction * (Projectile.ai[0] % 14 < 7 ? Projectile.direction : -Projectile.direction));
            Projectile.position = vector - Projectile.Size / 2f;
            Projectile.rotation = 0;
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.ai[0] % 14 >= 2 && Projectile.ai[0] % 14 <= 8)
                player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            return false;
        }
    }
}