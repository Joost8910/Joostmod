using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class GrogWaveFlipped1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y - 13, 0, 1, ModContent.ProjectileType<GrogWaveFlipped2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0], Projectile.ai[1]);
            return true;
        }

    }
}
