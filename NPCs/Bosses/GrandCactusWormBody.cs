using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Bosses
{
    [AutoloadBossHead]
    public class GrandCactusWormBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Grand Cactus Worm");
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.aiStyle = -1;
            npc.damage = 35;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 82;
            npc.height = 98;
            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(target.Center, npc.Center) > 50)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (damage > 2 && (projectile.penetrate > 2 || projectile.penetrate < 0))
            {
                damage = (int)(damage * 0.6f);
            }
            if (projectile.type == mod.ProjectileType("DoomSkull3"))
            {
                damage /= 2; 
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = (int)(npc.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/GrandCactusWormBody"), npc.scale);
            }
        }
        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                    }
                }
                if (Main.npc[(int)npc.ai[3]].ai[1] >= 225 && Main.npc[(int)npc.ai[3]].ai[1] < 425 || Main.npc[(int)npc.ai[3]].ai[1] >= 624 && Main.npc[(int)npc.ai[3]].ai[1] < 820)
                {
                    if (Main.npc[(int)npc.ai[3]].ai[1] % 30 == 0)
                    {
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("CactusThorn"));
                    }
                }
                if (Main.npc[(int)npc.ai[3]].ai[0] >= 2)
                {
                    npc.dontTakeDamage = true;
                    npc.netUpdate = true;
                }
                else
                {
                    npc.dontTakeDamage = false;
                    npc.netUpdate = true;
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                float dirX = Main.npc[(int)npc.ai[1]].Center.X + Main.npc[(int)npc.ai[1]].velocity.X - npc.Center.X;
                float dirY = Main.npc[(int)npc.ai[1]].Center.Y + Main.npc[(int)npc.ai[1]].velocity.Y - npc.Center.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - (float)npc.width) / length;
                if (npc.ai[1] == npc.ai[3])
                {
                    dist = (length - npc.width/2) / length;
                }
                float posX = dirX * dist;
                float posY = dirY * dist;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
                npc.netUpdate = true;
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            if (npc.dontTakeDamage)
            {
                texture = mod.GetTexture("NPCs/Bosses/GrandCactusWormBodyInvincible");
            }
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(), drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0);
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool CheckActive()
        {
            return false;
        }
    }
}