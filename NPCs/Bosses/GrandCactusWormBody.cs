using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
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
            NPC.CloneDefaults(NPCID.DiggerBody);
            NPC.aiStyle = -1;
            NPC.damage = 35;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;
            NPC.width = 82;
            NPC.height = 98;
            NPC.behindTiles = true;
            NPC.noTileCollide = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.dontCountMe = true;
            NPC.lavaImmune = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Vector2.Distance(target.Center, NPC.Center) > 50)
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
            if (projectile.type == Mod.Find<ModProjectile>("DoomSkull3").Type)
            {
                damage /= 2; 
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.damage = (int)(NPC.damage * 0.7f);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("GrandCactusWormBody").Type);
            }

            //The HitEffect hook is client side, these bits will need to be moved
            if (Main.npc[NPC.realLife].ai[3] == 0)
            {
                Main.npc[NPC.realLife].ai[2] = 1;
                Main.npc[NPC.realLife].netUpdate = true;
            }
        }
        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(28, -1, -1, null, NPC.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                    }
                }
                if (Main.npc[(int)NPC.ai[3]].ai[1] >= 225 && Main.npc[(int)NPC.ai[3]].ai[1] < 425 || Main.npc[(int)NPC.ai[3]].ai[1] >= 624 && Main.npc[(int)NPC.ai[3]].ai[1] < 820)
                {
                    if (Main.npc[(int)NPC.ai[3]].ai[1] % 45 == 0)
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("CactusThorn").Type);
                    }
                }
                if (Main.npc[(int)NPC.ai[3]].ai[0] >= 2)
                {
                    //npc.dontTakeDamage = true;
                    NPC.defense = 1000;
                    NPC.HitSound = SoundID.NPCHit4;
                    NPC.netUpdate = true;
                }
                else
                {
                    //npc.dontTakeDamage = false;
                    NPC.defense = 0;
                    NPC.HitSound = SoundID.NPCHit1;
                    NPC.netUpdate = true;
                }
            }

            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                float dirX = Main.npc[(int)NPC.ai[1]].Center.X + Main.npc[(int)NPC.ai[1]].velocity.X - NPC.Center.X;
                float dirY = Main.npc[(int)NPC.ai[1]].Center.Y + Main.npc[(int)NPC.ai[1]].velocity.Y - NPC.Center.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - (float)NPC.width) / length;
                if (NPC.ai[1] == NPC.ai[3])
                {
                    dist = (length - NPC.width/2) / length;
                }
                float posX = dirX * dist;
                float posY = dirY * dist;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
                NPC.netUpdate = true;
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            if (NPC.defense >= 1000)
            {
                texture = Mod.Assets.Request<Texture2D>("NPCs/Bosses/GrandCactusWormBodyInvincible").Value;
            }
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, new Rectangle?(), drawColor, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0);
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