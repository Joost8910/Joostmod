using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class DerangedMutantKillerMonsterSnowGoon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deranged Mutant Killer Monster Snow Goon");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.Zombie];
        }
        public override void SetDefaults()
        {
            npc.width = 26;
            npc.height = 40;
            npc.damage = 60;
            npc.defense = 10;
            npc.lifeMax = 140;
            npc.HitSound = SoundID.NPCHit11;
            npc.DeathSound = SoundID.NPCDeath15;
            npc.value = Item.buyPrice(0, 0, 7, 50);
            npc.knockBackResist = 0.75f;
            npc.aiStyle = 3;
            npc.coldDamage = true;
            aiType = NPCID.SnowFlinx;
            animationType = NPCID.Zombie;
            banner = npc.type;
            bannerItem = mod.ItemType("DerangedMutantKillerMonsterSnowGoonBanner");
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 593, 10);
            if (Main.rand.Next(40) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1921);
            }
            if (Main.rand.Next(25) == 0 && Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1921);
            }
            if (Main.rand.Next(50) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThirdAnniversary"), 1);
            }

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DerangedMutantKillerMonsterSnowGoon1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DerangedMutantKillerMonsterSnowGoon1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DerangedMutantKillerMonsterSnowGoon2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DerangedMutantKillerMonsterSnowGoon2"), 1f);
            }

        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
            return !spawnInfo.playerInTown && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && (tile.type == 147 || tile.type == 161 || tile.type == 163 || tile.type == 164 || tile.type == 200) && spawnInfo.spawnTileY <= Main.worldSurface && Main.hardMode ? 0.2f : 0f;
        }
        public override void AI()
        {
            npc.ai[0]++;
            Player P = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;

            npc.ai[1]++;
            if (npc.ai[1] >= 300)
            {
                float Speed = 6f;
                Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2));
                int damage = 10;
                int type = 109;
                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 1);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                }
                npc.ai[1] -= 300;
            }
        }
    }
}

