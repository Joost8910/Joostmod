using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SAXPowerBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Power Bomb");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 138;
            Projectile.light = 0.15f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Projectile.velocity = Vector2.Zero;
            Projectile.rotation = 0;
            if (Projectile.timeLeft == 138)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/PowerBombCharge1"), Projectile.Center);
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 6;
            }
            if (Projectile.timeLeft == 108)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/PowerBombCharge2"), Projectile.Center);
            }

            if (Projectile.timeLeft == 78)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/PowerBombCharge3"), Projectile.Center);
            }

            if (Projectile.timeLeft == 48)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/PowerBombCharge4"), Projectile.Center);
            }

            if (Projectile.timeLeft == 18)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/PowerBombCharge5"), Projectile.Center);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<SAXPowerBombExplosion>(), Projectile.damage, Projectile.knockBack);
            }
            SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/FusionPowerBombExplosion"), Projectile.Center);
        }
    }
}
