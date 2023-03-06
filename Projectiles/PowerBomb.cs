using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class PowerBomb : ModProjectile
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
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 138;
            Projectile.light = 0.15f;
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
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft == 138)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.position.X, (int)Projectile.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PowerBombCharge1"));
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 6;
            }
            if (Projectile.timeLeft == 108)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.position.X, (int)Projectile.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PowerBombCharge2"));
            }

            if (Projectile.timeLeft == 78)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.position.X, (int)Projectile.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PowerBombCharge3"));
            }
            if (Projectile.timeLeft < 78)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.rotation = 0;
            }
            if (Projectile.timeLeft == 48)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.position.X, (int)Projectile.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PowerBombCharge4"));
            }

            if (Projectile.timeLeft == 18)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.position.X, (int)Projectile.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/PowerBombCharge5"));
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, 0, 0, Mod.Find<ModProjectile>("PowerBombExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
            SoundEngine.PlaySound(SoundLoader.customSoundType, (int)Projectile.position.X, (int)Projectile.position.Y, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/FusionPowerBombExplosion"));
        }


    }
}
