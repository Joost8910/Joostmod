using JoostMod.NPCs.Bosses.SAX;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class SAXSuperExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Explosion");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 184;
            Projectile.height = 184;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18;
            Projectile.tileCollide = false;
            Projectile.light = 0.95f;
            Projectile.ignoreWater = true;
            AIType = ProjectileID.Bullet;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            CooldownSlot = ImmunityCooldownID.Bosses;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= JoostFunctions.GameDamageMult() * 2;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!(target.type == ModContent.NPCType<SAX>() || target.type == ModContent.NPCType<XParasite>()))
            {
                return true;
            }
            return base.CanHitNPC(target);
        }
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
        }
    }
}
