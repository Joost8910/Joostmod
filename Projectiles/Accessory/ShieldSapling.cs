using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Accessory
{
    public class ShieldSapling : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sapling - Shield");
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 11;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
            Projectile.extraUpdates = 4;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.damage == 0 || target.knockBackResist <= 0 && target.life > 1)
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
                if (Main.combatText[i].active && Main.combatText[i].color == CombatText.DamagedHostile && Main.combatText[i].text == damage.ToString() && Projectile.Distance(Main.combatText[i].position) < 250)
                {
                    Main.combatText[i].active = false;
                    break;
                }
            }
            //CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkGreen, "BLOCKED", true, false);

            SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.Center);
            if (target.knockBackResist > 0)
            {
                target.velocity.X = Projectile.direction * Projectile.knockBack;
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
            SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.Center);
            target.velocity.X = Projectile.direction * Projectile.knockBack;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.position.X = player.MountedCenter.X - Projectile.width / 2;
            Projectile.position.Y = player.position.Y + player.height / 2 - Projectile.height / 2;
            Projectile.velocity.X = -player.direction * 8;
            Projectile.direction = -player.direction;
            Projectile.velocity.Y = 4 * player.gravDir;
            Projectile.rotation = 0;
            Projectile.position = Projectile.Center.RotatedBy(player.fullRotation, player.MountedCenter) + new Vector2(-Projectile.width / 2, -Projectile.height / 2);
            Projectile.velocity = Projectile.velocity.RotatedBy(player.fullRotation);
            if (player.GetModPlayer<JoostPlayer>().shieldSaplingItem != null && !player.dead)
            {
                Projectile.timeLeft = 4;
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

