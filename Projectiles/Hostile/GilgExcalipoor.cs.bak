using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class GilgExcalipoor : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Excalipoor... somehow");
        }
        public override void SetDefaults()
        {
            Projectile.width = 102;
            Projectile.height = 102;
            Projectile.aiStyle = 2;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            AIType = ProjectileID.Shuriken;
        }
        public override void AI()
        {
            Projectile.damage = 1;
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.Center);
            Item.NewItem(Projectile.GetSource_Death(), Projectile.Center, Mod.Find<ModItem>("BrokenExcalipoor").Type);
            Vector2 dir = Projectile.velocity * 0.5f;
            dir.Y *= -1;
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, dir, Mod.Find<ModGore>("Excalipoor1").Type, 1f);
            dir.X *= -1;
            Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, dir, Mod.Find<ModGore>("Excalipoor2").Type, 1f);
        }
    }
}
