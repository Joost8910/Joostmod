using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class ImpLordFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lord's Flame");
            Main.projFrames[Projectile.type] = 18;
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            //projectile.light = 0.50f;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            if (channeling)
            {
                if (Projectile.ai[0] % 10 < 1 && Projectile.ai[0] < 60 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                {
                    Projectile.Kill();
                }
                Projectile.ai[0]++;
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                }
            }
            else
            {
                if (Projectile.ai[0] >= 60)
                    Projectile.ai[0] = 100;
                Projectile.Kill();
            }
            if (Projectile.ai[0] % 6 == 0 && Projectile.ai[0] < 60)
            {
                Projectile.frame = (Projectile.frame + 1) % 12;
            }
            if (Projectile.ai[0] >= 40)
            {
                player.velocity *= 0.98f;
            }
            if (Projectile.ai[0] >= 60)
            {
                player.velocity *= 0.98f;
                if (Projectile.ai[0] % 3 == 0)
                {
                    Projectile.frame = Projectile.frame >= 17 ? 10 : Projectile.frame + 1;
                    Projectile.ai[0] = 60;
                }
            }
            float light = (Projectile.ai[0] > 60 ? 60 : Projectile.ai[0]) / 60f;
            Lighting.AddLight(Projectile.Center, light, light * 0.5f, light * 0.1f);


            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f + new Vector2(Projectile.direction * -10, -player.gravDir * ((Projectile.ai[0] > 60 ? 12 : Projectile.ai[0] / 5) + 18));
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 15;
            player.itemAnimation = 15;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
        }

        public override void Kill(int timeLeft)
        {
            var source = Projectile.GetSource_Death();
            Player player = Main.player[Projectile.owner];
            Vector2 pos = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 dir = Vector2.Normalize(Projectile.velocity);
            if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
            {
                dir = -Vector2.UnitY;
            }
            if (Main.myPlayer == Projectile.owner)
            {
                if (Projectile.ai[0] < 40)
                {
                    Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 5, dir.Y * 5, Mod.Find<ModProjectile>("BurningSphere").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
                }
                else if (Projectile.ai[0] < 60)
                {
                    Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 12, dir.Y * 12, Mod.Find<ModProjectile>("FireBolt").Type, (int)(Projectile.damage * 2.5f), Projectile.knockBack * 3, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item45, Projectile.Center);
                }
                else if (Projectile.ai[0] == 100)
                {
                    /*
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int n = NPC.NewNPC((int)pos.X, (int)pos.Y + 14, mod.NPCType("FireBall2"), 1, dir.X * 8, dir.Y * 8, projectile.damage * 3, 0, projectile.owner);
                        Main.npc[n].netUpdate = true;
                        projectile.netUpdate = true;
                    }
                    */
                    Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 8, dir.Y * 8, Mod.Find<ModProjectile>("FireBlast").Type, Projectile.damage * 3, Projectile.knockBack * 5, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item73, Projectile.Center);
                }
            }
        }
    }
}