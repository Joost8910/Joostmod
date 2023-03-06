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
        public bool sap = false;
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
            sap = false;
            if (!npc.HasBuff(Mod.Find<ModBuff>("BoneHurt").Type))
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
                        shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("Spikyballclump").Type);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("Airplane").Type);
                        nextSlot++;
                    }
                    break;
                case NPCID.Merchant:
                    shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("Scooter").Type);
                    nextSlot++;
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("GiantKnife").Type);
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
                        shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("ShroomStaff>().Type);
                        nextSlot++;
                    }
                    break;
                case NPCID.Steampunker:
                    if (player.ZoneDesert)
                    {
                        shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("DesertSolution").Type);
                        nextSlot++;
                    }
                    else if (player.ZoneSnow)
                    {
                        shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("WinterSolution").Type);
                        nextSlot++;
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("TemperateSolution").Type);
                        nextSlot++;
                    }
                    break;
                case NPCID.TravellingMerchant:
                    shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("WonderWaffle").Type);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(Mod.Find<ModItem>("GrabGlove").Type);
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
            if (sap)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 20;
                damage = 1;
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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -5, Mod.Find<ModProjectile>("CorruptedSoul").Type, (int)damage, 0, player.whoAmI);
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
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("RedXParasite").Type);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("RedXParasite").Type);
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, Mod.Find<ModProjectile>("XParasiteRed").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Melee) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Melee)), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, Mod.Find<ModProjectile>("XParasiteRed").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Melee) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Melee)), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, ItemID.Heart);
                Item.NewItem(npc.Center, ItemID.Heart);
            }
            if (infectedGreen)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("GreenXParasite").Type);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("GreenXParasite").Type);
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, Mod.Find<ModProjectile>("XParasiteGreen").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Ranged) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Ranged)), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, Mod.Find<ModProjectile>("XParasiteGreen").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Ranged) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Ranged)), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, Mod.Find<ModItem>("EnergyFragment").Type);
            }
            if (infectedBlue)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("IceXParasite").Type);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("IceXParasite").Type);
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, Mod.Find<ModProjectile>("XParasite").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Magic) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Magic)), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, Mod.Find<ModProjectile>("XParasite").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Magic) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Magic)), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, ItemID.Star);
                Item.NewItem(npc.Center, ItemID.Star);
            }
            if (infectedYellow)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("XParasite").Type);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, Mod.Find<ModNPC>("XParasite").Type);
                }
                else
                {
                    Projectile.NewProjectile(npc.Center, dir, Mod.Find<ModProjectile>("XParasiteYellow").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Throwing) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Throwing)), 3, player.whoAmI);
                    Projectile.NewProjectile(npc.Center, -dir, Mod.Find<ModProjectile>("XParasiteYellow").Type, (int)(300 * (player.GetDamage(DamageClass.Generic) + player.GetDamage(DamageClass.Throwing) - 1) * player.GetDamage(DamageClass.Generic) * player.GetDamage(DamageClass.Throwing)), 3, player.whoAmI);
                }
                Item.NewItem(npc.Center, ItemID.GoldCoin, 1 + (int)(npc.value / 10000f));
            }
            return base.CheckDead(npc);
        }
        public override bool PreKill(NPC npc)
        {
            if (npc.type == NPCID.KingSlime && !NPC.downedSlimeKing)
            {
                npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("KingSlime").Type, 1, false);
            }
            if (npc.type == NPCID.EyeofCthulhu && !NPC.downedBoss1)
            {
                npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("EyeOfCthulhu").Type, 1, false);
            }
            if (npc.type == NPCID.BrainofCthulhu && !NPC.downedBoss2)
            {
                npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("BrainOfCthulhu").Type, 1, false);
            }
            if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
            {
                if (npc.boss && !NPC.downedBoss2)
                {
                    npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("EaterOfWorlds").Type, 1, false);
                }
            }
            if (npc.type == NPCID.QueenBee && !NPC.downedQueenBee)
            {
                npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("QueenBee").Type, 1, false);
            }
            if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
            {
                npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("Skeletron").Type, 1, false);
            }
            if (npc.type == NPCID.WallofFlesh && !Main.hardMode)
            {
                npc.DropItemInstanced(npc.position, npc.Size, Mod.Find<ModItem>("WallOfFlesh").Type, 1, false);
            }
            return base.PreKill(npc);
        }
        public override void OnKill(NPC npc)
        {
            Player player = Main.player[npc.lastInteraction];
            if (npc.lastInteraction == 255)
            {
                player = Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)];
            }
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            if (Main.rand.Next(500) == 0 && modPlayer.isLegend && !modPlayer.legendOwn && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("EvilStone").Type, 1);
            }
            if (Main.rand.Next(500000) == 0 && modPlayer.isSaitama && !modPlayer.SaitamaOwn)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("OnePunch").Type, 1);
            }

            if (Main.rand.Next(30000) == 0 && !Main.expertMode && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("EvilStone").Type, 1);
            }
            if (Main.rand.Next(25000) == 0 && Main.expertMode && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("EvilStone").Type, 1);
            }

            if (npc.type == NPCID.Clown)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Bananarang, 1);
            }
            if (npc.type == NPCID.DungeonSlime && Main.rand.Next(5) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("HoverBoots").Type, 1);
            }
            if (npc.type == NPCID.SkeletronHead && Main.rand.Next(2) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("SkellyStaff>().Type, 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("BrokenHeroFlail").Type, 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("BrokenHeroSpear").Type, 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("BrokenHeroHammer").Type, 1);
            }
            if (npc.type == NPCID.Plantera && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("RoseWeave").Type, 1);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("BubbleBottle").Type, 750);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("DukeFishRod").Type, 1);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("MegaBubbleShield").Type, 1);
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
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("PumpkinGlove").Type, 1);
                        }
                        else
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("PumpkinStaff>().Type, 1);
                        }
                    }
                    else
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("SnowFlake").Type, 1);
                    }
                }
            }
        }
        public override bool? CanHitNPC(NPC npc, NPC target)
        {
            if (npc.type == NPCID.BurningSphere && target.type == Mod.Find<ModNPC>("FireBall").Type)
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