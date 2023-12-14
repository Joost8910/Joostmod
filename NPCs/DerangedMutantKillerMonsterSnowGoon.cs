using System;
using JoostMod.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class DerangedMutantKillerMonsterSnowGoon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deranged Mutant Killer Monster Snow Goon");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
        }
        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 40;
            NPC.damage = 60;
            NPC.defense = 10;
            NPC.lifeMax = 140;
            NPC.HitSound = SoundID.NPCHit11;
            NPC.DeathSound = SoundID.NPCDeath15;
            NPC.value = Item.buyPrice(0, 0, 7, 50);
            NPC.knockBackResist = 0.75f;
            NPC.aiStyle = 3;
            NPC.coldDamage = true;
            AIType = NPCID.SnowFlinx;
            AnimationType = NPCID.Zombie;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<DerangedMutantKillerMonsterSnowGoonBanner>();
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.SnowBlock, 1, 5, 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ThirdAnniversary>(), 50));
            npcLoot.Add(ItemDropRule.NormalvsExpert(ItemID.HandWarmer, 40, 25));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("DerangedMutantKillerMonsterSnowGoon1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("DerangedMutantKillerMonsterSnowGoon1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("DerangedMutantKillerMonsterSnowGoon2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("DerangedMutantKillerMonsterSnowGoon2").Type);
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
            return !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && (tile.TileType == 147 || tile.TileType == 161 || tile.TileType == 163 || tile.TileType == 164 || tile.TileType == 200) && spawnInfo.SpawnTileY <= Main.worldSurface && Main.hardMode ? 0.2f : 0f;
        }
        public override void AI()
        {
            NPC.ai[0]++;
            Player P = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;

            NPC.ai[1]++;
            if (NPC.ai[1] >= 300)
            {
                float Speed = 6f;
                Vector2 vector8 = new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2));
                int damage = 10;
                int type = 109;
                SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                float rotation = (float)Math.Atan2(vector8.Y - (P.position.Y + (P.height * 0.5f)), vector8.X - (P.position.X + (P.width * 0.5f)));
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                }
                NPC.ai[1] -= 300;
            }
        }
    }
}

