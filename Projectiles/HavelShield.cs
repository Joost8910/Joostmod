using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
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
			Projectile.width = 18;
			Projectile.height = 46;
			Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 11;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if ((Projectile.localAI[1] <= 0 && target.damage == 0) || Main.player[Projectile.owner].shieldParryTimeLeft > 0)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.localAI[1] <= 0)
            {
                damage = 0;
                knockback = 0;
                crit = false;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.localAI[1] <= 0)
            {
                damage = 0;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            float KB = Projectile.knockBack;
            if (Projectile.localAI[1] <= 0)
            {
                KB /= 4;
                knockback = 0;
                crit = false;

                for (int i = 0; i < 100; i++)
                {
                    if (Main.combatText[i].active && Main.combatText[i].color == CombatText.DamagedHostile && Main.combatText[i].text == damage.ToString() && Projectile.Distance(Main.combatText[i].position) < 250)
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
                target.velocity.X = Projectile.direction * (KB + Math.Abs(player.velocity.X));
                target.velocity.Y--;
                if (!target.noTileCollide)
                {
                    float push = Projectile.Center.X + 16;
                    if (Projectile.direction < 0)
                    {
                        push = (Projectile.Center.X - 16) - target.width;
                    }
                    Vector2 pos = target.position;
                    pos.X = push + player.velocity.X;
                    if (Collision.SolidCollision(pos, target.width, target.height))
                    {
                        player.velocity.X = -Projectile.direction * KB;
                        Projectile.localAI[1] = 0;
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
                    ModPacket packet = Mod.GetPacket();
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
                if ((target.velocity.X < 0 && Projectile.direction > 0) || (target.velocity.X > 0 && Projectile.direction < 0))
                {
                    player.velocity.X = -Projectile.direction * (KB + Math.Abs(target.velocity.X));
                }
                else
                {
                    player.velocity.X = -Projectile.direction * KB;
                }
                Projectile.localAI[1] = 0;
                if (player.immuneTime < 2)
                {
                    player.immune = true;
                    player.immuneNoBlink = true;
                    player.immuneTime = 2;
                }
            }
            SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.2f), Projectile.Center);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Projectile.localAI[1] > 0)
            {
                target.velocity.X = Projectile.direction * Projectile.knockBack;
            }
            else
            { 
                crit = false;

                for (int i = 0; i < 100; i++)
                {
                    if (Main.combatText[i].active && Main.combatText[i].text == damage.ToString() && Projectile.Distance(Main.combatText[i].position) < 250)
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
                target.velocity.X = Projectile.direction * Projectile.knockBack / 4;
            }
            SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.2f), Projectile.Center);
        }
        public override void AI()
		{
            Player player = Main.player[Projectile.owner];
            if ((Projectile.localAI[1] > 0 || Projectile.localAI[0] > 0 || (player.controlUseTile && player.itemAnimation == 0 && !ItemLoader.AltFunctionUse(player.HeldItem, player) && player.itemAnimation == 0)) && !player.dead)
            {
                Projectile.timeLeft = 2;
                if (player.dashDelay < 0)
                {
                    player.dashDelay = 0;
                }
                if (Projectile.localAI[1] > 0)
                {
                    Projectile.localAI[1]--;
                    player.ChangeDir(Projectile.direction);
                    player.velocity.X = player.direction * player.moveSpeed * 6;
                    player.dashDelay = -1;
                    Projectile.localNPCHitCooldown = 15;
                }
                else
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        if (Main.MouseWorld.X < player.Center.X)
                        {
                            player.ChangeDir(-1);
                        }
                        if (Main.MouseWorld.X > player.Center.X)
                        {
                            player.ChangeDir(1);
                        }
                        Projectile.direction = player.direction;
                    }
                    Projectile.localNPCHitCooldown = 5;
                    if (Projectile.localAI[0] > 0)
                    {
                        Projectile.localAI[0]--;
                    }
                    else if (player.controlUseItem)
                    {
                        SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
                        Projectile.localAI[0] = 30 * player.GetAttackSpeed(DamageClass.Melee);
                        Projectile.localAI[1] = 15;
                    }
                }
                player.controlUseItem = false;
            }
            if (player.parryDamageBuff && Projectile.localAI[1] <= 0)
            {
                Projectile.melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
            }
            else
            {
                Projectile.DamageType = DamageClass.Melee;
            }
            Projectile.position.X = player.MountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = player.position.Y + (player.height / 2) - Projectile.height / 2;
            Projectile.velocity.X = player.direction * 11;
            Projectile.velocity.Y = 0;
            Projectile.rotation = 0;
            Projectile.position = Projectile.Center.RotatedBy(player.fullRotation, player.MountedCenter) + new Vector2(-Projectile.width / 2, -Projectile.height / 2);
            Projectile.velocity = Projectile.velocity.RotatedBy(player.fullRotation);
        }

	}
}

