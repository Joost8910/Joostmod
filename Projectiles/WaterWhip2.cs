using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class WaterWhip2 : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grasping Water Tendril");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.alpha = 100;
            Projectile.extraUpdates = 1;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 20;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            return false;
        }
        int nextProj = -1;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.localAI[1] == 0 && target.knockBackResist > 0 && target.type != NPCID.TargetDummy)
            {
                Projectile.localAI[1] = target.whoAmI + 1;
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
            writer.Write(Projectile.localAI[1]);
            writer.Write(nextProj);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadSingle();
            Projectile.localAI[1] = reader.ReadSingle();
            nextProj = reader.ReadInt32();
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            int max = 30;
            Projectile.scale = 1f - (Projectile.ai[0] / max);
            Projectile.width = (int)(32 * Projectile.scale);
            Projectile.height = (int)(32 * Projectile.scale);
            bool channeling = player.controlUseTile && !player.noItems && !player.CCed && !player.dead;
            int mana = 5;
            if (player.inventory[player.selectedItem].type == Mod.Find<ModItem>("WaterWhip").Type)
            {
                mana = player.inventory[player.selectedItem].mana / 2;
            }
            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0]++;
            }
            if (channeling && player.CheckMana(mana))
            {
                if (Projectile.ai[1] == -1 && Projectile.localAI[0] % 40 == 0)
                {
                    player.CheckMana(mana, true);
                }
                Projectile.localAI[0]++;
                if (nextProj >= 0 && Main.projectile[nextProj].type != Projectile.type && Main.projectile[nextProj].owner != Projectile.owner)
                {
                    Projectile.localAI[0] = 5;
                }
                if (player.ownedProjectileCounts[Projectile.type] < max - 1 && Projectile.localAI[0] == 5)
                {
                    nextProj = Projectile.NewProjectile(Projectile.Center, Projectile.velocity, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, Projectile.ai[0] + 1, Projectile.identity);
                    player.ownedProjectileCounts[Projectile.type]++;
                }
                if (Projectile.ai[1] != -1)
                {
                    if (Main.projectile[(int)Projectile.ai[1]].type == Projectile.type && Main.projectile[(int)Projectile.ai[1]].owner == Projectile.owner)
                    {
                        vector = Main.projectile[(int)Projectile.ai[1]].Center;
                    }
                    else
                    {
                        Projectile.Kill();
                    }
                }
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor = 20f * Projectile.scale;
                    /*
                    for (int i = 0; i < projectile.ai[0]; i++)
                    {
                        scaleFactor += 8f * (1f - (i / max));
                    }
                    */
                    Vector2 dir = Main.MouseWorld - vector;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    dir *= scaleFactor;
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    if (Projectile.ai[1] == -1)
                    {
                        Projectile.velocity = dir;
                    }
                    else
                    {
                        if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, Projectile.Center + dir, 1, 1))
                        {
                            Vector2 move = vector + dir - Projectile.Center;
                            if (move.Length() > 0)
                            {
                                move *= scaleFactor / move.Length();
                            }
                            float home = 40f - Projectile.ai[0];
                            Projectile.velocity = ((home - 1f) * Projectile.velocity + move) / home;
                            if (Projectile.velocity.Length() < scaleFactor)
                            {
                                Projectile.velocity *= (scaleFactor / Projectile.velocity.Length());
                            }
                        }
                        else
                        {
                            Projectile.velocity = Projectile.oldVelocity;
                        }
                    }
                }
            }
            else
            {
                Projectile.Kill();
            }
            for (int i = 0; i < Main.item.Length; i++)
            {
                if (Main.item[i].active)
                {
                    Item I = Main.item[i];
                    if (Projectile.Hitbox.Intersects(I.Hitbox))
                    {
                        Vector2 vel = I.DirectionTo(player.Center) * 2;
                        I.velocity = vel;
                        I.position += I.velocity;
                    }
                }
            }
            if (Projectile.localAI[1] > 0)
            {
                NPC target = Main.npc[(int)Projectile.localAI[1] - 1];
                bool tooClose = player.Distance(target.Center) <= (target.width > target.height ? target.width / 2 : target.height / 2) + 30;
                if (target.active && target.life > 0 && target.knockBackResist > 0 && !target.dontTakeDamage && !target.friendly && target.type != NPCID.TargetDummy)
                {
                    if (tooClose)
                    {
                        Projectile.localAI[1] = 0;
                        target.velocity = player.DirectionTo(target.Center) * Projectile.knockBack;
                    }
                    else
                    {
                        target.position = Projectile.Center - target.Size / 2;
                    }
                }
                else
                {
                    Projectile.localAI[1] = 0;
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
            Projectile.position = (vector) - Projectile.Size / 2f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            //projectile.spriteDirection = projectile.direction;
            Projectile.timeLeft = 2;
            if (Projectile.ai[0] == 0)
            {
                player.ChangeDir(Projectile.direction);
                player.itemTime = 10;
                player.itemAnimation = 10;
                player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            }
            if (Projectile.localAI[0] % 5 == 0)
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 33).noGravity = true;
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = lightColor;
            spriteBatch.Draw(tex, drawPosition, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            return false;
        }
    }
}