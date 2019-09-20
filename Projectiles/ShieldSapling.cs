using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class ShieldSapling : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapling - Shield");
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 11;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 20;
            projectile.extraUpdates = 4;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.damage == 0 || (target.knockBackResist <= 0 && target.life > 1))
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = 0;
            knockback = 0;
            crit = false;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = 0;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            knockback = 0;
            crit = false;
            
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].color == CombatText.DamagedHostile && Main.combatText[i].text == damage.ToString() && projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                    break;
                }
            }
            //CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkGreen, "BLOCKED", true, false);
            
            Main.PlaySound(3, projectile.Center, 4);
            if (target.knockBackResist > 0)
            {
                target.velocity.X = projectile.direction * projectile.knockBack;
            }
            if (target.life < target.lifeMax)
            {
                target.life++;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            crit = false;
            
            for (int i = 0; i < 100; i++)
            {
                if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                    break;
                }
            }
            //CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkGreen, "BLOCKED", true, false);

            if (target.statLife < target.statLifeMax2)
            {
                target.statLife++;
            }
            Main.PlaySound(3, projectile.Center, 4);
            target.velocity.X = projectile.direction * projectile.knockBack;
        }
        public override void AI()
		{
            Player player = Main.player[projectile.owner];
            projectile.position.X = player.MountedCenter.X - projectile.width/2;
			projectile.position.Y = player.position.Y + (player.height / 2) - projectile.height/2;
            projectile.velocity.X = -player.direction * 8;
            projectile.direction = -player.direction;
			projectile.velocity.Y = 4 * player.gravDir;
			projectile.rotation = 0;
            projectile.position = projectile.Center.RotatedBy(player.fullRotation, player.MountedCenter) + new Vector2(-projectile.width/2, -projectile.height/2);
            projectile.velocity = projectile.velocity.RotatedBy(player.fullRotation);
            if (player.GetModPlayer<JoostPlayer>(mod).shieldSapling && !player.dead)
            {
                projectile.timeLeft = 4;
            }
            /*for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.getRect().Intersects(projectile.getRect()) && proj.hostile && proj.penetrate == 1 && proj.active)
                {
                    proj.Kill();
                    Main.PlaySound(3, projectile.Center, 4);
                }
            }*/
		}

	}
}

