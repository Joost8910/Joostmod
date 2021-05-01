using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
    public class JoostGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public bool lifeRend = false;
        public bool corruptSoul = false;
        public bool bonesHurt = false;
        public bool infectedRed = false;
        public bool infectedGreen = false;
        public bool infectedBlue = false;
        public bool infectedYellow = false;
        public float boneHurtDamage = 1;
        public int immunePlayer = -1;
        public override void ResetEffects(NPC npc)
        {
            lifeRend = false;
            corruptSoul = false;
            bonesHurt = false;
            infectedRed = false;
            infectedGreen = false;
            infectedBlue = false;
            infectedYellow = false;
            if (!npc.HasBuff(mod.BuffType("BoneHurt")))
            {
                boneHurtDamage = 1;
            }
            immunePlayer = -1;
        }
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            Player player = Main.player[Main.myPlayer];
            switch (type)
            {
                case NPCID.GoblinTinkerer:
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Spikyballclump"));
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(mod.ItemType("Airplane"));
                        nextSlot++;
                    }
                    break;
                case NPCID.Merchant:
                    shop.item[nextSlot].SetDefaults(mod.ItemType("Scooter"));
                    nextSlot++;
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("GiantKnife"));
                        nextSlot++;
                    }
                    break;
                case NPCID.Dryad:
                    shop.item[nextSlot].SetDefaults(208);
                    nextSlot++;
                    break;
                case NPCID.Truffle:
                    if (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("ShroomStaff"));
                        nextSlot++;
                    }
                    break;
                case NPCID.Steampunker:
                    if (player.ZoneDesert)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("DesertSolution"));
                        nextSlot++;
                    }
                    else if (player.ZoneSnow)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("WinterSolution"));
                        nextSlot++;
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("TemperateSolution"));
                        nextSlot++;
                    }
                    break;
                case NPCID.TravellingMerchant:
                    shop.item[nextSlot].SetDefaults(mod.ItemType("WonderWaffle"));
                    shop.item[nextSlot].SetDefaults(mod.ItemType("GrabGlove"));
                    nextSlot++;
                    break;
            }
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            Player player = Main.player[npc.lastInteraction];
            if (npc.lastInteraction == 255)
            {
                player = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];
            }
            if (corruptSoul)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 20;
                damage = 10;
            }
            if (bonesHurt)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                boneHurtDamage += 0.0167f;
                npc.lifeRegen -= (int)boneHurtDamage * 2;
                damage = (int)boneHurtDamage;
                if (npc.HitSound == SoundID.NPCHit2 || npc.HitSound == SoundID.DD2_SkeletonHurt)
                {
                    npc.lifeRegen -= (int)boneHurtDamage * 2;
                    damage = (int)boneHurtDamage * 2;
                }
            }
            if (infectedRed)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 50;
                damage = 50;
            }
            if (infectedGreen)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 50;
                damage = 50;
            }
            if (infectedBlue)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 50;
                damage = 50;
            }
            if (infectedYellow)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 50;
                damage = 50;
            }
        }
        public override bool CheckDead(NPC npc)
        {
            Player player = Main.player[npc.lastInteraction];
            if (npc.lastInteraction == 255)
            {
                player = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];
            }


            if (corruptSoul && npc.type != NPCID.TargetDummy)
            {
                float damage = npc.lifeMax * 0.25f;
                if ((int)damage > 0)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -5, mod.ProjectileType("CorruptedSoul"), (int)damage, 0, player.whoAmI);
                }
            }
            if (lifeRend && npc.type != NPCID.TargetDummy)
            {
                float lifeStoled = npc.lifeMax * 0.05f;
                if ((int)lifeStoled > 0 && !player.moonLeech)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, 305, 0, 0f, player.whoAmI, (float)player.whoAmI, lifeStoled);
                }
            }
            if (infectedRed)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("RedXParasite"));
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("RedXParasite"));
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, mod.ProjectileType("XParasiteRed"), (int)(300 * (player.allDamage + player.meleeDamage - 1) * player.allDamageMult * player.meleeDamageMult), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, mod.ProjectileType("XParasiteRed"), (int)(300 * (player.allDamage + player.meleeDamage - 1) * player.allDamageMult * player.meleeDamageMult), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, ItemID.Heart);
                Item.NewItem(npc.Center, ItemID.Heart);
            }
            if (infectedGreen)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GreenXParasite"));
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("GreenXParasite"));
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, mod.ProjectileType("XParasiteGreen"), (int)(300 * (player.allDamage + player.rangedDamage - 1) * player.allDamageMult * player.rangedDamageMult), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, mod.ProjectileType("XParasiteGreen"), (int)(300 * (player.allDamage + player.rangedDamage - 1) * player.allDamageMult * player.rangedDamageMult), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, mod.ItemType("EnergyFragment"));
            }
            if (infectedBlue)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("IceXParasite"));
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("IceXParasite"));
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, mod.ProjectileType("XParasite"), (int)(300 * (player.allDamage + player.magicDamage - 1) * player.allDamageMult * player.magicDamageMult), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, mod.ProjectileType("XParasite"), (int)(300 * (player.allDamage + player.magicDamage - 1) * player.allDamageMult * player.magicDamageMult), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, ItemID.Star);
                Item.NewItem(npc.Center, ItemID.Star);
            }
            if (infectedYellow)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("XParasite"));
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("XParasite"));
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, mod.ProjectileType("XParasiteYellow"), (int)(300 * (player.allDamage + player.thrownDamage - 1) * player.allDamageMult * player.thrownDamageMult), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, mod.ProjectileType("XParasiteYellow"), (int)(300 * (player.allDamage + player.thrownDamage - 1) * player.allDamageMult * player.thrownDamageMult), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, ItemID.GoldCoin, 1 + (int)(npc.value / 10000f));
            }
            return base.CheckDead(npc);
        }
        public override bool PreNPCLoot(NPC npc)
        {
            if (npc.type == NPCID.KingSlime && !NPC.downedSlimeKing)
            {
                npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("KingSlime"), 1, false);
            }
            if (npc.type == NPCID.EyeofCthulhu && !NPC.downedBoss1)
            {
                npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("EyeOfCthulhu"), 1, false);
            }
            if (npc.type == NPCID.BrainofCthulhu && !NPC.downedBoss2)
            {
                npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("BrainOfCthulhu"), 1, false);
            }
            if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
            {
                if (npc.boss && !NPC.downedBoss2)
                {
                    npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("EaterOfWorlds"), 1, false);
                }
            }
            if (npc.type == NPCID.QueenBee && !NPC.downedQueenBee)
            {
                npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("QueenBee"), 1, false);
            }
            if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
            {
                npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("Skeletron"), 1, false);
            }
            if (npc.type == NPCID.WallofFlesh && !Main.hardMode)
            {
                npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("WallOfFlesh"), 1, false);
            }
            return base.PreNPCLoot(npc);
        }
        public override void NPCLoot(NPC npc)
        {
            Player player = Main.player[npc.lastInteraction];
            if (npc.lastInteraction == 255)
            {
                player = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];
            }
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (Main.rand.Next(500) == 0 && modPlayer.isLegend && !modPlayer.legendOwn && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
            if (Main.rand.Next(500000) == 0 && modPlayer.isSaitama && !modPlayer.SaitamaOwn)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OnePunch"), 1);
            }

            if (Main.rand.Next(30000) == 0 && !Main.expertMode && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
            if (Main.rand.Next(25000) == 0 && Main.expertMode && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }

            if (npc.type == NPCID.Clown)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Bananarang, 1);
            }
            if (npc.type == NPCID.DungeonSlime && Main.rand.Next(5) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("HoverBoots"), 1);
            }
            if (npc.type == NPCID.SkeletronHead && Main.rand.Next(2) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkellyStaff"), 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroFlail"), 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroSpear"), 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrokenHeroHammer"), 1);
            }
            if (npc.type == NPCID.Plantera && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RoseWeave"), 1);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BubbleBottle"), 750);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DukeFishRod"), 1);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MegaBubbleShield"), 1);
            }
            if (((npc.type == NPCID.Pumpking && Main.pumpkinMoon) || (npc.type == NPCID.IceQueen && Main.snowMoon)) && NPC.waveNumber > 8)
            {
                int chance = NPC.waveNumber - 8;
                if (Main.expertMode)
                {
                    chance++;
                }
                if (Main.rand.Next(5) < chance)
                {

                    if (npc.type == NPCID.Pumpking)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PumpkinGlove"), 1);
                        }
                        else
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PumpkinStaff"), 1);
                        }
                    }
                    else
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SnowFlake"), 1);
                    }
                }
            }
        }
        public override bool? CanHitNPC(NPC npc, NPC target)
        {
            if (npc.type == NPCID.BurningSphere && target.type == mod.NPCType("FireBall"))
            {
                return false;
            }
            return base.CanHitNPC(npc, target);
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            if (target.whoAmI == immunePlayer)
            {
                return false;
            }
            return base.CanHitPlayer(npc, target, ref cooldownSlot);
        }
    }
}