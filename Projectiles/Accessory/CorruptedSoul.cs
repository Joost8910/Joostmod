using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JoostMod.Buffs;

namespace JoostMod.Projectiles.Accessory
{
    public class CorruptedSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Soul");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 120;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            float max = 800;
            if (Projectile.timeLeft % 5 == 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 14, Main.rand.Next(-20, 20) * 0.01f, 3f, 0, default, (6 + Main.rand.Next(5)) * 0.1f);
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (!target.friendly && target.type != NPCID.TargetDummy && !target.dontTakeDamage && target.active && Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1) && Vector2.Distance(Projectile.Center, target.Center) < max)
                    {
                        Projectile.velocity = Projectile.DirectionTo(target.Center) * 8;
                        max = Vector2.Distance(Projectile.Center, target.Center);
                    }
                }
                Projectile.frame = (Projectile.frame + 1) % 4;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (damage - target.defense / 2 > 9999)
            {
                damage = 9999 + target.defense / 2;
            }
            crit = false;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (damage - target.statDefense / 2 > 9999)
            {
                damage = 9999 + target.statDefense / 2;
            }
            crit = false;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (damage - target.statDefense / 2 > 9999)
            {
                damage = 9999 + target.statDefense / 2;
            }
            crit = false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && Projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                }
            }
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkViolet, damage, true, false);
            float damag = target.lifeMax * 0.25f;
            if ((int)damag > 0 && target.type != NPCID.TargetDummy && target.life <= 0 && !target.HasBuff(ModContent.BuffType<CorruptSoul>()))
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0, -5, ModContent.ProjectileType<CorruptedSoul>(), (int)damag, 0, Projectile.owner);
            }
            target.AddBuff(ModContent.BuffType<CorruptSoul>(), 1200, false);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && Projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                }
            }
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkViolet, damage, true, false);
            float damag = target.statLifeMax2 * 0.25f;
            if ((int)damag > 0 && target.statLife <= 0 && !target.HasBuff(ModContent.BuffType<CorruptSoul>()))
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0, -5, ModContent.ProjectileType<CorruptedSoul>(), (int)damag, 0, Projectile.owner);
            }
            target.AddBuff(ModContent.BuffType<CorruptSoul>(), 1200, false);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && Projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                }
            }
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkViolet, damage, true, false);
            float damag = target.statLifeMax2 * 0.25f;
            if ((int)damag > 0 && target.statLife <= 0 && !target.HasBuff(ModContent.BuffType<CorruptSoul>()))
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center.X, target.Center.Y, 0, -5, ModContent.ProjectileType<CorruptedSoul>(), (int)damag, 0, Projectile.owner);
            }
            target.AddBuff(ModContent.BuffType<CorruptSoul>(), 1200, false);
        }
    }
}

