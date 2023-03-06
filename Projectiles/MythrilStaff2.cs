using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class MythrilStaff2 : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Twilight Staff>();
		}
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = !player.noItems && !player.CCed && Projectile.ai[1] < 30;
            if (channeling)
            {
                Projectile.ai[1]++;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.position = vector - Projectile.Size / 2f;
            Projectile.direction = player.direction;
            Projectile.ai[0] += Projectile.direction;
            Projectile.rotation = Projectile.ai[0] * 0.0174f * 6.5f;
            Projectile.timeLeft = 2;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            player.itemRotation = MathHelper.WrapAngle(Projectile.rotation);
            if (Projectile.ai[1] % 3 == 0)
            {
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, -(float)Math.Cos(Projectile.rotation + 0.785f) * 8, -(float)Math.Sin(Projectile.rotation + 0.785f) * 8, Mod.Find<ModProjectile>("BoltofLight").Type, Projectile.damage, Projectile.knockBack / 2, player.whoAmI);
                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, (float)Math.Cos(Projectile.rotation + 0.785f) * 8, (float)Math.Sin(Projectile.rotation + 0.785f) * 8, Mod.Find<ModProjectile>("BoltofNight").Type, Projectile.damage, Projectile.knockBack / 2, player.whoAmI);
            }

            return false;
        }

    }
}