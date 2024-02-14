using JoostMod.Buffs;
using JoostMod.ItemDropRules.DropConditions;
using JoostMod.Items.Accessories;
using JoostMod.Items.Ammo;
using JoostMod.Items.Consumables;
using JoostMod.Items.Dyes;
using JoostMod.Items.Legendaries;
using JoostMod.Items.Materials;
using JoostMod.Items.Mounts;
using JoostMod.Items.Quest;
using JoostMod.Items.Tools.Rods;
using JoostMod.Items.Weapons.Melee;
using JoostMod.Items.Weapons.Summon;
using JoostMod.Items.Weapons.Thrown;
using JoostMod.NPCs.Bosses;
using JoostMod.Projectiles.Accessory;
using Microsoft.Xna.Framework;
using System.Data;
using System.Linq;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.ItemDropRules;
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
            if (!npc.HasBuff(ModContent.BuffType<BoneHurt>()))
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
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<Spikyballclump>());
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<AirplaneKeys>());
                        nextSlot++;
                    }
                    break;
                case NPCID.Merchant:
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<ScooterItem>());
                    nextSlot++;
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<GiantKnife>());
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
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<ShroomStaff>());
                        nextSlot++;
                    }
                    break;
                case NPCID.Steampunker:
                    if (player.ZoneDesert)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<DesertSolution>());
                        nextSlot++;
                    }
                    else if (player.ZoneSnow)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<WinterSolution>());
                        nextSlot++;
                    }
                    else
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<TemperateSolution>());
                        nextSlot++;
                    }
                    break;
                case NPCID.TravellingMerchant:
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<WonderWaffle>());
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ModContent.ItemType<GrabGlove>());
                    nextSlot++;
                    break;
                case NPCID.DyeTrader:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<TeamOutlineDye>());
                        nextSlot++;
                    }
                    if (Main.GetMoonPhase() >= MoonPhase.ThreeQuartersAtLeft && Main.GetMoonPhase() <= MoonPhase.QuarterAtLeft)
                    {
                        shop.item[nextSlot].SetDefaults(ModContent.ItemType<BlurryDye>());
                        nextSlot++;
                    }
                    if (!Main.dayTime)
                    {
                        if (Main.GetMoonPhase() == MoonPhase.Empty)
                            shop.item[nextSlot].SetDefaults(ModContent.ItemType<GhostDye>());
                        else
                            shop.item[nextSlot].SetDefaults(ModContent.ItemType<GlowInTheDarkDye>());
                        nextSlot++;
                    }
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
            var source = npc.GetSource_Death();
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
                    Projectile.NewProjectile(source, npc.Center.X, npc.Center.Y, 0, -5, ModContent.ProjectileType<CorruptedSoul>(), (int)damage, 0, player.whoAmI);
                }
            }
            if (lifeRend && npc.type != NPCID.TargetDummy)
            {
                float lifeStoled = npc.lifeMax * 0.05f;
                if ((int)lifeStoled > 0 && !player.moonLeech)
                {
                    Projectile.NewProjectile(source, npc.Center.X, npc.Center.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, player.whoAmI, (float)player.whoAmI, lifeStoled);
                }
            }
            if (infectedRed)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<RedXParasite>());
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<RedXParasite>());
                }
                else
                {
                    Projectile.NewProjectile(source, npc.Center, dir, ModContent.ProjectileType<XParasiteRed>(), (int)player.GetDamage(DamageClass.Ranged).ApplyTo(300), 3, player.whoAmI);
                    Projectile.NewProjectile(source, npc.Center, -dir, ModContent.ProjectileType<XParasiteRed>(), (int)player.GetDamage(DamageClass.Ranged).ApplyTo(300), 3, player.whoAmI);
                }
                Item.NewItem(source, npc.Center, ItemID.Heart);
                Item.NewItem(source, npc.Center, ItemID.Heart);
            }
            if (infectedGreen)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<GreenXParasite>());
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<GreenXParasite>());
                }
                else
                {
                    Projectile.NewProjectile(source, npc.Center, dir, ModContent.ProjectileType<XParasiteGreen>(), (int)player.GetDamage(DamageClass.Throwing).ApplyTo(300), 3, player.whoAmI);
                    Projectile.NewProjectile(source, npc.Center, -dir, ModContent.ProjectileType<XParasiteGreen>(), (int)player.GetDamage(DamageClass.Throwing).ApplyTo(300), 3, player.whoAmI);
                }
                Item.NewItem(source, npc.Center, ModContent.ItemType<EnergyFragment>());
            }
            if (infectedBlue)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<IceXParasite>());
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<IceXParasite>());
                }
                else
                {
                    Projectile.NewProjectile(source, npc.Center, dir, ModContent.ProjectileType<XParasiteIce>(), (int)player.GetDamage(DamageClass.Magic).ApplyTo(300), 3, player.whoAmI);
                    Projectile.NewProjectile(source, npc.Center, -dir, ModContent.ProjectileType<XParasiteIce>(), (int)player.GetDamage(DamageClass.Magic).ApplyTo(300), 3, player.whoAmI);
                }
                Item.NewItem(source, npc.Center, ItemID.Star);
                Item.NewItem(source, npc.Center, ItemID.Star);
            }
            if (infectedYellow)
            {
                Vector2 dir = Utils.ToRotationVector2(Main.rand.Next(360));
                if (npc.friendly)
                {
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<XParasite>());
                    NPC.NewNPC(source, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<XParasite>());
                }
                else
                {
                    Projectile.NewProjectile(source, npc.Center, dir, ModContent.ProjectileType<XParasiteYellow>(), (int)player.GetDamage(DamageClass.Melee).ApplyTo(300), 3, player.whoAmI);
                    Projectile.NewProjectile(source, npc.Center, -dir, ModContent.ProjectileType<XParasiteYellow>(), (int)player.GetDamage(DamageClass.Melee).ApplyTo(300), 3, player.whoAmI);
                }
                Item.NewItem(source, npc.Center, ItemID.GoldCoin, 1 + (int)(npc.value / 10000f));
            }
            return base.CheckDead(npc);
        }
        public override bool PreKill(NPC npc)
        {
            if (npc.type == NPCID.KingSlime && !NPC.downedSlimeKing)
            {
                CommonCode.DropItemForEachInteractingPlayerOnThePlayer(npc, ModContent.ItemType<KingSlime>(), Main.rand, 1, 1, 1, false);
            }
            if (npc.type == NPCID.EyeofCthulhu && !NPC.downedBoss1)
            {
                CommonCode.DropItemForEachInteractingPlayerOnThePlayer(npc, ModContent.ItemType<EyeOfCthulhu>(), Main.rand, 1, 1, 1, false);
            }
            if (npc.type == NPCID.BrainofCthulhu && !NPC.downedBoss2)
            {
                CommonCode.DropItemForEachInteractingPlayerOnThePlayer(npc, ModContent.ItemType<BrainOfCthulhu>(), Main.rand, 1, 1, 1, false);
            }
            if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
            {
                if (npc.boss && !NPC.downedBoss2)
                {
                    CommonCode.DropItemForEachInteractingPlayerOnThePlayer(npc, ModContent.ItemType<EaterOfWorlds>(), Main.rand, 1, 1, 1, false);
                }
            }
            if (npc.type == NPCID.QueenBee && !NPC.downedQueenBee)
            {
                CommonCode.DropItemForEachInteractingPlayerOnThePlayer(npc, ModContent.ItemType<QueenBee>(), Main.rand, 1, 1, 1, false);
            }
            if (npc.type == NPCID.SkeletronHead && !NPC.downedBoss3)
            {
                CommonCode.DropItemForEachInteractingPlayerOnThePlayer(npc, ModContent.ItemType<Skeletron>(), Main.rand, 1, 1, 1, false);
            }
            if (npc.type == NPCID.WallofFlesh && !Main.hardMode)
            {
                CommonCode.DropItemForEachInteractingPlayerOnThePlayer(npc, ModContent.ItemType<WallOfFlesh>(), Main.rand, 1, 1, 1, false);
            }
            return base.PreKill(npc);
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new LegendDropCondition(), ModContent.ItemType<EvilStone>(), 500));
            npcLoot.Add(ItemDropRule.ByCondition(new SDropCondition(), ModContent.ItemType<OnePunch>(), 500000));
            npcLoot.Add(new LeadingConditionRule(new EvilStoneDropCondition()).OnSuccess(ItemDropRule.NormalvsExpertNotScalingWithLuck(ModContent.ItemType<EvilStone>(), 30000, 25000)));
            /*if (npc.type == NPCID.Clown) 1.4.4 changes bananarang to no longer stack
            {
                npcLoot.Add()
            }*/
            if (npc.type == NPCID.DungeonSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HoverBoots>(), 5));
            }
            if (npc.type >= NPCID.HellArmoredBones && npc.type <= NPCID.HellArmoredBonesSword)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<EternalFlames>(), 130, 88));
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SkellyStaff>(), 2));
            }
            if (npc.type == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RoseWeave>(), 4));
            }
            if (npc.type == NPCID.Mothron)
            {
                npcLoot.Add(ItemDropRule.ExpertGetsRerolls(ModContent.ItemType<BrokenHeroFlail>(), 4, 1));
                npcLoot.Add(ItemDropRule.ExpertGetsRerolls(ModContent.ItemType<BrokenHeroSpear>(), 4, 1));
                npcLoot.Add(ItemDropRule.ExpertGetsRerolls(ModContent.ItemType<BrokenHeroHammer>(), 4, 1));
            }
            if (npc.type == NPCID.DukeFishron)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BubbleKnife>(), 4, 750, 999));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Items.Accessories.MegaBubbleShield>(), 4));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DukeFishRod>(), 4));
            }
            if (npc.type == NPCID.Pumpking)
            {
                /*
                new LeadingConditionRule(new Conditions.PumpkinMoonDropGatingChance()).OnSuccess(new OneFromRulesRule(2, new IItemDropRule[] 
                { 
                    ItemDropRule.Common(ModContent.ItemType<PumpkinGlove>()),
                    ItemDropRule.Common(ModContent.ItemType<PumpkinStaff>())
                }));
                */
                //Mod.Logger.Info("Checking Pumpking droptable...");

                foreach (var rule in npcLoot.Get())//Finally got this loot table tomfuckery to work, gave up on ice queen tho
                {
                    /*
                    Mod.Logger.Info("Rule: " + rule.ToString());
                    foreach (var a in rule.ChainedRules.ToArray())
                    {
                        Mod.Logger.Info("   Chain: " + a.ToString());
                        Mod.Logger.Info("      Chain2: " + a.RuleToChain.ToString());
                    }
                    */

                    if (rule is LeadingConditionRule lcr)
                    {
                        foreach (var cr in lcr.ChainedRules)
                        {
                            if (cr is Chains.TryIfSucceeded tic && tic.RuleToChain is OneFromRulesRule oneFromRules)
                            {
                                /*
                                foreach (var a in oneFromRules.options.ToArray())
                                {
                                    Mod.Logger.Info("onefromRules options: " + a.ToString());
                                    if (a is CommonDrop cd)
                                        Mod.Logger.Info("  Drop: " + cd.itemId);
                                }
                                */
                                //Mod.Logger.Info("Droptable Contains The Horsemans Blade: " + oneFromRules.options.Contains(ItemDropRule.Common(ItemID.TheHorsemansBlade)));
                                var og = oneFromRules.options.ToList();
                                og.Add(ItemDropRule.Common(ModContent.ItemType<PumpkinGlove>()));
                                og.Add(ItemDropRule.Common(ModContent.ItemType<PumpkinStaff>()));
                                oneFromRules.options = og.ToArray();
                            }
                        }
                    }
                }
            }
            if (npc.type == NPCID.Flocko)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.FrostMoonDropGatingChance(), ModContent.ItemType<SnowFlake>(), 20));
            }
            if (npc.type == NPCID.IceQueen)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.FrostMoonDropGatingChance(), ModContent.ItemType<SnowFlake>(), 4));
                //Mod.Logger.Info("Checking Ice Queen droptable...");
                /*
                foreach (var rule in npcLoot.Get())//This is supposed to add to the existing loot table, needs testing
                {
                    //Mod.Logger.Info("Rule: " + rule.ToString());
                    foreach (var a in rule.ChainedRules.ToArray())
                    {
                        //Mod.Logger.Info("   Chain: " + a.ToString());
                        //Mod.Logger.Info("      Chain2: " + a.RuleToChain.ToString());
                        if (a.RuleToChain is CommonDrop cd)
                        {
                            //Mod.Logger.Info("    cd: " + cd.ToString());
                            //Mod.Logger.Info("    cdItem: " + cd.itemId);
                            if (cd.itemId == ItemID.BabyGrinchMischiefWhistle)
                            {
                                cd.OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<SnowFlake>(), 4));
                            }
                        }
                    }
                    //Mod.Logger.Info("Droptable Rule is OneFromOptionsDropRule: " + (rule is OneFromOptionsDropRule o));
                    if (rule is OneFromOptionsDropRule oneFromOptions)
                    {
                        Mod.Logger.Info("Droptable Contains North Pole: " + oneFromOptions.dropIds.Contains(ItemID.NorthPole));
                        if (oneFromOptions.dropIds.Contains(ItemID.NorthPole))
                        {
                            var og = oneFromOptions.dropIds.ToList();
                            og.Add(ModContent.ItemType<SnowFlake>());
                            oneFromOptions.dropIds = og.ToArray();
                        }
                    }
                }
                */
            }
        }
        /*
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
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EvilStone>(), 1);
            }
            if (Main.rand.Next(500000) == 0 && modPlayer.isSaitama && !modPlayer.SaitamaOwn)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<OnePunch>(), 1);
            }

            if (Main.rand.Next(30000) == 0 && !Main.expertMode && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EvilStone>(), 1);
            }
            if (Main.rand.Next(25000) == 0 && Main.expertMode && !npc.SpawnedFromStatue)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<EvilStone>(), 1);
            }

            if (npc.type == NPCID.Clown)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Bananarang, 1);
            }
            if (npc.type == NPCID.DungeonSlime && Main.rand.Next(5) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<HoverBoots>(), 1);
            }
            if (npc.type == NPCID.SkeletronHead && Main.rand.Next(2) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("SkellyStaff>().Type, 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BrokenHeroFlail>(), 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BrokenHeroSpear>(), 1);
            }
            if (npc.type == NPCID.Mothron && Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BrokenHeroHammer>(), 1);
            }
            if (npc.type == NPCID.Plantera && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<RoseWeave>(), 1);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BubbleBottle>(), 750);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DukeFishRod>(), 1);
            }
            if (npc.type == NPCID.DukeFishron && Main.rand.Next(3) == 0 && !Main.expertMode)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MegaBubbleShield>(), 1);
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
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PumpkinGlove>(), 1);
                        }
                        else
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("PumpkinStaff>().Type, 1);
                        }
                    }
                    else
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SnowFlake>(), 1);
                    }
                }
            }
        }
        */
        public override bool? CanHitNPC(NPC npc, NPC target)
        {
            if (npc.type == NPCID.BurningSphere && target.type == ModContent.NPCType<Hunts.FireBall>())
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