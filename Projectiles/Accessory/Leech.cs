using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Accessory
{
    public class Leech : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leechy Boi");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            AIType = ProjectileID.Bullet;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            float lifeStoled = damage * 0.05f;
            if (lifeStoled < 1)
            {
                lifeStoled = 1;
            }
            if ((int)lifeStoled > 0 && !player.moonLeech && target.type != NPCID.TargetDummy && target.life > 2)
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
            }
            SoundEngine.PlaySound(SoundID.NPCHit9, player.Center);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            float lifeStoled = damage * 0.05f;
            if (lifeStoled < 1)
            {
                lifeStoled = 1;
            }
            if ((int)lifeStoled > 0 && !player.moonLeech)
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, player.whoAmI, lifeStoled);
            }
            SoundEngine.PlaySound(SoundID.NPCHit9, player.Center);
        }
        public override void AI()
        {
            if (Projectile.timeLeft > 298)
            {
                Player player = Main.player[Projectile.owner];
                if (player.HasMinionAttackTargetNPC)
                {
                    NPC target = Main.npc[player.MinionAttackTargetNPC];
                    if (Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
                    {
                        Projectile.velocity = Projectile.DirectionTo(target.Center) * 10;
                    }
                }
                else
                {
                    for (int i = 0; i < 200; i++)
                    {
                        NPC target = Main.npc[i];
                        if (!target.friendly && target.type != NPCID.TargetDummy && !target.dontTakeDamage && target.active && Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1) && target.life > 5)
                        {
                            Projectile.velocity = Projectile.DirectionTo(target.Center) * 10;
                        }
                    }
                }
            }
        }

    }
}

