using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Ranged
{
    public class SearingBow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volcanic Longbow");
            Main.projFrames[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        int[] arrowItems = new int[5];
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(arrowItems[0]);
            writer.Write(arrowItems[1]);
            writer.Write(arrowItems[2]);
            writer.Write(arrowItems[3]);
            writer.Write(arrowItems[4]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            arrowItems[0] = reader.ReadInt16();
            arrowItems[1] = reader.ReadInt16();
            arrowItems[2] = reader.ReadInt16();
            arrowItems[3] = reader.ReadInt16();
            arrowItems[4] = reader.ReadInt16();
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            float speed = 25;
            float shootSpeed = 13f;

            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                speed = player.inventory[player.selectedItem].useTime;
                Projectile.netUpdate = true;
            }
            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 dir = Main.MouseWorld - player.MountedCenter;
                dir.Normalize();
                if (dir.HasNaNs())
                {
                    dir = Vector2.UnitX * player.direction;
                }
                if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                {
                    Projectile.netUpdate = true;
                }
                Projectile.velocity = dir;
            }
            if (player.gravDir < 0)
            {
                Projectile.rotation += 3.14f;
                Projectile.spriteDirection = -Projectile.direction;
            }
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            player.itemTime = 2;
            player.itemAnimation = 2;

            float armRot = player.itemRotation - (float)(Math.PI / 2) * Projectile.direction;
            player.SetCompositeArmBack(true, Player.CompositeArmStretchAmount.Full, armRot);
            Vector2 origin = player.GetBackHandPosition(Player.CompositeArmStretchAmount.Full, armRot);

            Projectile.position = new Vector2(origin.X - 8 * Projectile.direction + Projectile.velocity.X, origin.Y + Projectile.velocity.Y) - Projectile.Size / 2f;
            Projectile.position.Y += player.gravDir * 4 + 2;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);

            Projectile.frame = Projectile.ai[1] > 0 || Projectile.localAI[0] >= speed * 0.75f ? 1 : 0;
            if (Projectile.localAI[0] > speed * 0.75f || Projectile.localAI[0] <= speed * 0.25f)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 10;
            }
            else if (Projectile.localAI[0] > speed * 0.5f)
            {
                player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            }
            else if (Projectile.localAI[0] > speed * 0.25f)
            {
                player.bodyFrame.Y = player.bodyFrame.Height;
            }
            if ((Projectile.ai[1] < 5 || Projectile.localAI[0] < speed * 0.2f) && Projectile.localAI[0] < speed)
            {
                Projectile.localAI[0]++;
            }
            if (player.noItems || player.CCed || player.dead || !player.active || Projectile.ai[1] <= 0 && !player.controlUseItem && !player.controlUseTile)
            {
                Projectile.Kill();
            }
            else
            {
                /*
                Item item = new Item();
                bool canShoot = false;
                bool flag = false;
                for (int i = 54; i < 58; i++)
                {
                    if (player.inventory[i].ammo == AmmoID.Arrow && player.inventory[i].stack > 0)
                    {
                        item = player.inventory[i];
                        canShoot = true;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    for (int j = 0; j < 54; j++)
                    {
                        if (player.inventory[j].ammo == AmmoID.Arrow && player.inventory[j].stack > 0)
                        {
                            item = player.inventory[j];
                            canShoot = true;
                            break;
                        }
                    }
                }
                */
                bool canShoot = player.PickAmmo(player.HeldItem, out int t, out float s, out int d, out float k, out int arrowID, true);
                if (canShoot)
                {
                    Projectile.ai[0] = t;
                }
                else if (Projectile.ai[1] == 0)
                {
                    Projectile.Kill();
                }
                if (Projectile.ai[0] == ProjectileID.WoodenArrowFriendly)
                {
                    Projectile.ai[0] = ModContent.ProjectileType<BlazingArrow>();
                }
                if (Projectile.ai[1] < 5 && (player.controlUseTile || Projectile.ai[1] < 1 || Projectile.localAI[0] > speed * 0.25f && Projectile.localAI[0] < speed * 0.9f) && canShoot)
                {
                    if (Projectile.localAI[0] >= (Projectile.ai[1] < 1 ? speed : speed * 0.6f))
                    {
                        player.PickAmmo(player.HeldItem, out t, out s, out d, out k, out arrowID, false);
                        arrowItems[(int)Projectile.ai[1]] = arrowID;
                        Projectile.ai[1]++;
                        Projectile.localAI[0] = 0;
                        SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
                    }
                }
                else if (!(player.controlUseTile && player.controlUseItem) && (Projectile.localAI[0] >= speed * 0.9f || Projectile.localAI[0] <= speed * 0.25f && Projectile.ai[1] < 5 || Projectile.localAI[0] >= speed * 0.2f && Projectile.ai[1] >= 5))
                {
                    if (Projectile.ai[1] > 0)
                    {
                        float rotation = MathHelper.ToRadians(22.5f);
                        if (Main.myPlayer == Projectile.owner)
                        {
                            for (int i = 0; i < Projectile.ai[1]; i++)
                            {
                                float spd = player.inventory[player.selectedItem].shootSpeed;
                                int dmg = player.GetWeaponDamage(player.HeldItem);
                                float kb = player.GetWeaponKnockback(player.HeldItem) * 0.5f;
                                int arrowType = ModContent.ProjectileType<BlazingArrow>();
                                Item item = new Item(arrowItems[i], 0);
                                if (item.ammo == AmmoID.Arrow)
                                {
                                    dmg += item.damage;
                                    kb += item.knockBack;
                                    spd += item.shootSpeed;
                                    arrowType = item.shoot;
                                }
                                if (arrowType == ProjectileID.WoodenArrowFriendly)
                                {
                                    arrowType = ModContent.ProjectileType<BlazingArrow>();
                                    shootSpeed += 4;
                                    dmg = (int)(dmg * 1.5f);
                                    kb += 3;
                                }
                                Vector2 vel = Projectile.velocity * spd;
                                Vector2 perturbedSpeed = Projectile.ai[1] <= 1 ? vel : vel.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (Projectile.ai[1] - 1)));

                                Projectile.NewProjectile(Projectile.GetSource_ItemUse_WithPotentialAmmo(player.HeldItem, AmmoID.Arrow), Projectile.Center, perturbedSpeed, arrowType, dmg, kb, Projectile.owner);
                            }
                        }
                        //Projectile.NewProjectile(projectile.Center, projectile.velocity * shootSpeed, (int)projectile.ai[0], (projectile.damage + item.damage), projectile.knockback + item.knockback, projectile.owner);
                        SoundEngine.PlaySound(SoundID.Item5, Projectile.Center);
                        Projectile.ai[1] = 0;
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item7, Projectile.Center);
                    }
                }
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height / Main.projFrames[Projectile.type] * 0.5f);
            Rectangle? rect = new Rectangle(0, Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), tex.Width, tex.Height / Main.projFrames[Projectile.type]);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, rect, lightColor, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            if (Projectile.ai[0] != 0)
            {
                Main.instance.LoadProjectile((int)Projectile.ai[0]);
                Texture2D arrowTex = TextureAssets.Projectile[(int)Projectile.ai[0]].Value;
                int frames = Main.projFrames[(int)Projectile.ai[0]] < 1 ? 1 : Main.projFrames[(int)Projectile.ai[0]];
                Vector2 arrowOrigin = new Vector2(arrowTex.Width * 0.5f, arrowTex.Height / frames * 0.5f);
                Rectangle? arrowRect = new Rectangle(0, 0, arrowTex.Width, arrowTex.Height / frames);

                Player player = Main.player[Projectile.owner];
                if (Projectile.ai[1] < 5)
                {
                    if (player.bodyFrame.Y == player.bodyFrame.Height)
                    {
                        Vector2 off = new Vector2(-10 * player.direction, player.gravDir * -2);
                        float rot = player.gravDir < 0 ? 3.14f : 0;
                        off += (rot - 1.57f).ToRotationVector2() * arrowOrigin.Y;
                        Main.EntitySpriteDraw(arrowTex, player.MountedCenter + off - Main.screenPosition, arrowRect, lightColor, rot, arrowOrigin, Projectile.scale, effects, 0);
                    }
                    if (player.bodyFrame.Y == player.bodyFrame.Height * 2)
                    {
                        Vector2 off = new Vector2(0, player.gravDir * -6);
                        float rot = player.direction * player.gravDir * 0.785f + (player.gravDir < 0 ? 3.14f : 0);
                        off += (rot - 1.57f).ToRotationVector2() * arrowOrigin.Y;
                        Main.EntitySpriteDraw(arrowTex, player.MountedCenter + off - Main.screenPosition, arrowRect, lightColor, rot, arrowOrigin, Projectile.scale, effects, 0);
                    }
                    if (player.bodyFrame.Y == player.bodyFrame.Height * 3)
                    {
                        Vector2 off = new Vector2(2 * player.direction, player.gravDir * 2);
                        float rot = player.direction * player.gravDir * 1.92f + (player.gravDir < 0 ? 3.14f : 0);
                        off += (rot - 1.57f).ToRotationVector2() * arrowOrigin.Y;
                        Main.EntitySpriteDraw(arrowTex, player.MountedCenter + off - Main.screenPosition, arrowRect, lightColor, rot, arrowOrigin, Projectile.scale, effects, 0);
                    }
                    if (player.bodyFrame.Y == player.bodyFrame.Height * 4)
                    {
                        Vector2 off = new Vector2(0, player.gravDir * 4);
                        float rot = player.direction * player.gravDir * 2.36f + (player.gravDir < 0 ? 3.14f : 0);
                        off += (rot - 1.57f).ToRotationVector2() * arrowOrigin.Y;
                        Main.EntitySpriteDraw(arrowTex, player.MountedCenter + off - Main.screenPosition, arrowRect, lightColor, rot, arrowOrigin, Projectile.scale, effects, 0);
                    }
                }

                float rotOff = MathHelper.ToRadians(22.5f);
                Vector2 vel = Projectile.velocity;
                vel.Normalize();
                float e = Projectile.ai[1] < 1 && player.bodyFrame.Y == player.bodyFrame.Height * 10 && Projectile.frame == 1 ? 1 : Projectile.ai[1];
                for (int i = 0; i < e; i++)
                {
                    Item item = new Item(arrowItems[i], 0);
                    if (item.ammo == AmmoID.Arrow)
                    {
                        int arrowType = item.shoot;
                        if (arrowType == ProjectileID.WoodenArrowFriendly)
                        {
                            arrowType = ModContent.ProjectileType<BlazingArrow>();
                        }

                        Main.instance.LoadProjectile(arrowType);
                        arrowTex = TextureAssets.Projectile[arrowType].Value;
                        frames = Main.projFrames[arrowType] < 1 ? 1 : Main.projFrames[arrowType];
                        arrowOrigin = new Vector2(arrowTex.Width * 0.5f, arrowTex.Height / frames * 0.5f);
                        arrowRect = new Rectangle(0, 0, arrowTex.Width, arrowTex.Height / frames);
                        

                        Vector2 dir = Projectile.ai[1] <= 1 ? vel : vel.RotatedBy(MathHelper.Lerp(-rotOff, rotOff, i / (Projectile.ai[1] - 1)));
                        float rot = dir.ToRotation() + 1.57f;
                        Vector2 offset = dir * arrowOrigin.Y - vel * drawOrigin.X;
                        offset.Y -= player.gravDir;
                        Main.EntitySpriteDraw(arrowTex, Projectile.Center + offset - Main.screenPosition, arrowRect, lightColor, rot, arrowOrigin, Projectile.scale, effects, 0);
                    }
                }
            }
            return false;
        }
    }
}