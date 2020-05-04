using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HavelShield : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Havel's Greatshield");
		}
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 46;
			projectile.aiStyle = 1;
            projectile.melee = true;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.timeLeft = 11;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 15;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if ((projectile.localAI[1] <= 0 && target.damage == 0) || Main.player[projectile.owner].shieldParryTimeLeft > 0)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.localAI[1] <= 0)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (projectile.localAI[1] <= 0)
            {
                damage = 0;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            float KB = projectile.knockBack;
            if (projectile.localAI[1] <= 0)
            {
                KB /= 4;
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
                if (target.life < target.lifeMax)
                {
                    target.life++;
                }
            }
            if (target.knockBackResist > 0)
            {
                target.velocity.X = projectile.direction * (KB + Math.Abs(player.velocity.X));
                target.velocity.Y--;
                if (!target.noTileCollide)
                {
                    float push = projectile.Center.X + 16;
                    if (projectile.direction < 0)
                    {
                        push = (projectile.Center.X - 16) - target.width;
                    }
                    Vector2 pos = target.position;
                    pos.X = push + player.velocity.X;
                    if (Collision.SolidCollision(pos, target.width, target.height))
                    {
                        player.velocity.X = -projectile.direction * KB;
                        projectile.localAI[1] = 0;
                        if (player.immuneTime < 2)
                        {
                            player.immune = true;
                            player.immuneNoBlink = true;
                            player.immuneTime = 2;
                        }
                    }
                }
                if (Main.netMode != 0)
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write((byte)JoostModMessageType.NPCpos);
                    packet.Write(target.whoAmI);
                    packet.WriteVector2(target.position);
                    packet.WriteVector2(target.velocity);
                    ModPacket netMessage = packet;
                    netMessage.Send();
                }
            }
            else
            {
                if ((target.velocity.X < 0 && projectile.direction > 0) || (target.velocity.X > 0 && projectile.direction < 0))
                {
                    player.velocity.X = -projectile.direction * (KB + Math.Abs(target.velocity.X));
                }
                else
                {
                    player.velocity.X = -projectile.direction * KB;
                }
                projectile.localAI[1] = 0;
                if (player.immuneTime < 2)
                {
                    player.immune = true;
                    player.immuneNoBlink = true;
                    player.immuneTime = 2;
                }
            }
            Main.PlaySound(21, (int)projectile.Center.X, (int)projectile.Center.Y, 1, 1, -0.2f);
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
            if ((projectile.localAI[1] > 0 || projectile.localAI[0] > 0 || (player.controlUseTile && player.itemAnimation == 0 && !ItemLoader.AltFunctionUse(player.HeldItem, player) && player.itemAnimation == 0)) && !player.dead)
            {
                projectile.timeLeft = 2;
                if (player.dashDelay < 0)
                {
                    player.dashDelay = 0;
                }
                if (projectile.localAI[1] > 0)
                {
                    projectile.localAI[1]--;
                    player.ChangeDir(projectile.direction);
                    player.velocity.X = player.direction * player.moveSpeed * 6;
                    player.dashDelay = -1;
                    projectile.localNPCHitCooldown = 15;
                }
                else
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        if (Main.MouseWorld.X < player.Center.X)
                        {
                            player.ChangeDir(-1);
                        }
                        if (Main.MouseWorld.X > player.Center.X)
                        {
                            player.ChangeDir(1);
                        }
                        projectile.direction = player.direction;
                    }
                    projectile.localNPCHitCooldown = 5;
                    if (projectile.localAI[0] > 0)
                    {
                        projectile.localAI[0]--;
                    }
                    else if (player.controlUseItem)
                    {
                        Main.PlaySound(42, (int)projectile.position.X, (int)projectile.position.Y, 213);
                        projectile.localAI[0] = 30 * player.meleeSpeed;
                        projectile.localAI[1] = 15;
                    }
                }
                player.controlUseItem = false;
            }
            if (player.parryDamageBuff && projectile.localAI[1] <= 0)
            {
                projectile.melee = false;
            }
            else
            {
                projectile.melee = true;
            }
            projectile.position.X = player.MountedCenter.X - projectile.width / 2;
            projectile.position.Y = player.position.Y + (player.height / 2) - projectile.height / 2;
            projectile.velocity.X = player.direction * 11;
            projectile.velocity.Y = 0;
            projectile.rotation = 0;
            projectile.position = projectile.Center.RotatedBy(player.fullRotation, player.MountedCenter) + new Vector2(-projectile.width / 2, -projectile.height / 2);
            projectile.velocity = projectile.velocity.RotatedBy(player.fullRotation);
        }

	}
}

