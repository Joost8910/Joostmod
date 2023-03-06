using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class AwokenDreamSpinSlash : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Awoken Dream Spin Slash");
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
			Projectile.idStaticNPCHitCooldown = 3;
            Projectile.extraUpdates = 1;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += (int)(target.defense / 2);
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
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.Center.X, (int)Projectile.Center.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/hero_nail_art_cyclone_slash_2"));
            }
            Dust.NewDust(Projectile.position, Projectile.width,Projectile.height, 71, Projectile.velocity.X*20, 0, 100, default(Color), (0.8f + (Main.rand.Next(5)/10)));
            Dust.NewDust(Projectile.position, Projectile.width,Projectile.height, 71, Projectile.velocity.X*-20, 0, 100, default(Color), (0.8f + (Main.rand.Next(5)/10)));
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
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            return false;
        }
    }
}