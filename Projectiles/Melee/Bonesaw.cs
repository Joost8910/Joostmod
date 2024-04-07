using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Bonesaw : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bonesaw");
        }
        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = 20;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HitSound == SoundID.NPCHit2 || target.HitSound == SoundID.DD2_SkeletonHurt)
            {
                modifiers.SetCrit();
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if(target.boneArmor)
            {
                modifiers.FinalDamage *= 2;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var source = Projectile.GetSource_OnHit(target);
            if (target.life <= 0)
            {
                for (int i = 0; i < target.width / 12; i++)
                {
                    for (int j = 0; j < target.height / 12; j++)
                    {
                        Vector2 pos = target.position + new Vector2(i * 12, j * 12);
                        //Vector2 dir = pos - npc.Center;
                        //dir.Normalize();
                        Vector2 vel = new Vector2(Main.rand.Next(9) - 4, Main.rand.Next(9) - 4);
                        Projectile.NewProjectile(source, pos, vel, ProjectileID.Bone, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    }
                }
            }
        }

    }
}