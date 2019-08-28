using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class Fishhook : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BobberReinforced);
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 10;
		}
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fish Hook");
		}
		public override bool? CanHitNPC(NPC target)
		{
            return !target.friendly;
		}
        public override bool CanHitPvp(Player target)
        {
            return true;
        }
        int hitMob = -1;
        bool pvp = false;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!projectile.wet)
            {
                if (hitMob < 0)
                {
                    hitMob = target.whoAmI;
                    pvp = false;
                }
                if (!target.active || target.friendly || target.life <= 0)
                {
                    projectile.ai[0] = 1;
                }
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (!projectile.wet)
            {
                if (hitMob < 0)
                {
                    hitMob = target.whoAmI;
                    pvp = true;
                }
                if (!target.active || target.dead)
                {
                    projectile.ai[0] = 1;
                }
            }
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (hitMob > 0 && projectile.Distance(player.Center) < 2000 && !player.controlUseItem)
            {
                projectile.ai[0] = 0;
            }
            if (projectile.ai[0] > 0)
            {
                hitMob = -1;
            }
            if (pvp && hitMob > 0 && hitMob < 255 && projectile.ai[0] == 0)
            {
                Player P = Main.player[hitMob];
                projectile.velocity = P.velocity;
                projectile.position = P.Center - (projectile.Size / 2);
                if (!P.active || P.dead)
                {
                    hitMob = -1;
                    pvp = false;
                    projectile.ai[0] = 1;
                }
                projectile.netUpdate = true;
            }
            else if (hitMob >= 0 && projectile.ai[0] == 0)
            {
                NPC npc = Main.npc[hitMob];
                projectile.velocity = npc.velocity;
                projectile.position = npc.Center - (projectile.Size / 2);
                if (!npc.active || npc.friendly || npc.life <= 0)
                {
                    hitMob = -1;
                    projectile.ai[0] = 1;
                }
                projectile.netUpdate = true;
            }
            else
            {
                for (int i = 0; i < Main.item.Length; i++)
                {
                    if (Main.item[i].active)
                    {
                        Item I = Main.item[i];
                        if (projectile.Hitbox.Intersects(I.Hitbox))
                        {
                            I.velocity = projectile.velocity;
                            I.position = projectile.Center - (I.Size / 2);
                        }
                    }
                }
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(hitMob);
            writer.Write(pvp);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            hitMob = reader.ReadInt16();
            pvp = reader.ReadBoolean();
        }
        public override bool PreDrawExtras(SpriteBatch spriteBatch)      //this draws the fishing line correctly
        {
            Player player = Main.player[projectile.owner];
            if (projectile.bobber && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].holdStyle > 0)
            {
                float pPosX = player.MountedCenter.X;
                float pPosY = player.MountedCenter.Y;
                pPosY += Main.player[projectile.owner].gfxOffY;
                int type = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type;
                float gravDir = Main.player[projectile.owner].gravDir;
 
                if (type == mod.ItemType("FishingGun")) //add your Fishing Pole name here
                {
                    pPosX += (float)(50 * Main.player[projectile.owner].direction);
                    if (Main.player[projectile.owner].direction < 0)
                    {
                        pPosX -= 13f;
                    }
                    pPosY -= 12f * gravDir;
                }
 
                if (gravDir == -1f)
                {
                    pPosY -= 12f;
                }
                Vector2 value = new Vector2(pPosX, pPosY);
                value = Main.player[projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
                float projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
                float projPosY = projectile.position.Y + (float)projectile.height * 0.5f - value.Y;
                Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
                bool flag2 = true;
                if (projPosX == 0f && projPosY == 0f)
                {
                    flag2 = false;
                }
                else
                {
                    float projPosXY = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                    projPosXY = 12f / projPosXY;
                    projPosX *= projPosXY;
                    projPosY *= projPosXY;
                    value.X -= projPosX;
                    value.Y -= projPosY;
                    projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
                    projPosY = projectile.position.Y + (float)projectile.height * 0.5f - value.Y;
                }
                while (flag2)
                {
                    float num = 12f;
                    float num2 = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                    float num3 = num2;
                    if (float.IsNaN(num2) || float.IsNaN(num3))
                    {
                        flag2 = false;
                    }
                    else
                    {
                        if (num2 < 20f)
                        {
                            num = num2 - 8f;
                            flag2 = false;
                        }
                        num2 = 12f / num2;
                        projPosX *= num2;
                        projPosY *= num2;
                        value.X += projPosX;
                        value.Y += projPosY;
                        projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
                        projPosY = projectile.position.Y + (float)projectile.height * 0.1f - value.Y;
                        if (num3 > 12f)
                        {
                            float num4 = 0.3f;
                            float num5 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                            if (num5 > 16f)
                            {
                                num5 = 16f;
                            }
                            num5 = 1f - num5 / 16f;
                            num4 *= num5;
                            num5 = num3 / 80f;
                            if (num5 > 1f)
                            {
                                num5 = 1f;
                            }
                            num4 *= num5;
                            if (num4 < 0f)
                            {
                                num4 = 0f;
                            }
                            num5 = 1f - projectile.localAI[0] / 100f;
                            num4 *= num5;
                            if (projPosY > 0f)
                            {
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            }
                            else
                            {
                                num5 = Math.Abs(projectile.velocity.X) / 3f;
                                if (num5 > 1f)
                                {
                                    num5 = 1f;
                                }
                                num5 -= 0.5f;
                                num4 *= num5;
                                if (num4 > 0f)
                                {
                                    num4 *= 2f;
                                }
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            }
                        }
                        rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
                        Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Microsoft.Xna.Framework.Color(200, 200, 200, 100));    //this is the fishing line color in RGB, 200 is red, 12 is green, 50 blue
 
                        Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num)), color2, rotation2, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            return false;
        }
	}
}