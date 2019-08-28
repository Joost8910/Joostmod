using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class CorruptedSoul : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Corrupted Soul");
            Main.projFrames[projectile.type] = 4;
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 300;
            projectile.alpha = 120;
            aiType = ProjectileID.Bullet;
		}
        public override void AI()
        {
            float max = 800;
            if (projectile.timeLeft % 5 == 0)
			{
            	Dust.NewDust(projectile.position, projectile.width, projectile.height, 14, Main.rand.Next(-20, 20) * 0.01f, 3f, 0, default(Color), (6+Main.rand.Next(5)) * 0.1f);
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (!target.friendly && target.type != 488 && !target.dontTakeDamage && target.active && Collision.CanHitLine(projectile.Center, 1, 1, target.Center, 1, 1) && Vector2.Distance(projectile.Center, target.Center) < max)
                    {
                        projectile.velocity = projectile.DirectionTo(target.Center) * 8;
                        max = Vector2.Distance(projectile.Center, target.Center);
                    }
                }
                projectile.frame = (projectile.frame + 1) % 4;
			}
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (damage - (target.defense / 2) > 9999)
            {
                damage = 9999 + target.defense / 2;
            }
            crit = false;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (damage - (target.statDefense / 2) > 9999)
            {
                damage = 9999 + target.statDefense / 2;
            }
            crit = false;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (damage - (target.statDefense / 2) > 9999)
            {
                damage = 9999 + target.statDefense / 2;
            }
            crit = false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                }
            }
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkViolet, damage, true, false);
            float damag = target.lifeMax * 0.25f;
            if ((int)damag > 0 && target.type != NPCID.TargetDummy && target.life <= 0 && !target.HasBuff(mod.BuffType("CorruptSoul")))
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, -5, mod.ProjectileType("CorruptedSoul"), (int)damag, 0, projectile.owner);
            }
            target.AddBuff(mod.BuffType("CorruptSoul"), 1200, false);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                }
            }
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkViolet, damage, true, false);
            float damag = target.statLifeMax2 * 0.25f;
            if ((int)damag > 0 && target.statLife <= 0 && !target.HasBuff(mod.BuffType("CorruptSoul")))
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, -5, mod.ProjectileType("CorruptedSoul"), (int)damag, 0, projectile.owner);
            }
            target.AddBuff(mod.BuffType("CorruptSoul"), 1200, false);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                }
            }
            CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkViolet, damage, true, false);
            float damag = target.statLifeMax2 * 0.25f;
            if ((int)damag > 0 && target.statLife <= 0 && !target.HasBuff(mod.BuffType("CorruptSoul")))
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0, -5, mod.ProjectileType("CorruptedSoul"), (int)damag, 0, projectile.owner);
            }
            target.AddBuff(mod.BuffType("CorruptSoul"), 1200, false);
        }
    }
}

