using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.IO;

namespace JoostMod
{
    public class JoostMod : Mod
    {
        internal bool battleRodsLoaded;
        internal static ModHotKey ArmorAbilityHotKey;
        static internal JoostMod instance;

        public List<HuntInfo> hunts;

        public JoostMod()
        {

            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadSounds = true,
                AutoloadGores = true

            };

        }
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            battleRodsLoaded = ModLoader.GetMod("UnuBattleRods") != null;
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddMiniBossWithInfo", "Pinkzor", 0.1f, (Func<bool>)(() => JoostWorld.downedPinkzor), "Rarely found on the surface or underground. The Huntmaster will spawn after it is killed");
                bossChecklist.Call("AddMiniBossWithInfo", "Rogue Tomato", 0.2f, (Func<bool>)(() => JoostWorld.downedRogueTomato), "Quest from the Hunt Master, found on the surface during daytime");
                bossChecklist.Call("AddMiniBossWithInfo", "Forest's Vengeance", 0.3f, (Func<bool>)(() => JoostWorld.downedWoodGuardian), "Quest from the Hunt Master, found in the forest");
                bossChecklist.Call("AddMiniBossWithInfo", "Flowering Cactoid", 1.1f, (Func<bool>)(() => JoostWorld.downedFloweringCactoid), "Quest from the Hunt Master, found in the surface desert");
                bossChecklist.Call("AddMiniBossWithInfo", "ICU", 2.1f, (Func<bool>)(() => JoostWorld.downedICU), "Quest from the Hunt Master, found on the surface during nighttime");
                bossChecklist.Call("AddMiniBossWithInfo", "Spore Mother", 3.1f, (Func<bool>)(() => JoostWorld.downedSporeSpawn), "Quest from the Hunt Master, found in the underground jungle");
                bossChecklist.Call("AddMiniBossWithInfo", "Roc", 4.1f, (Func<bool>)(() => JoostWorld.downedRoc), "Quest from the Hunt Master, found in the sky");
                bossChecklist.Call("AddMiniBossWithInfo", "Skeleton Demolitionist", 5.1f, (Func<bool>)(() => JoostWorld.downedSkeletonDemoman), "Quest from the Hunt Master, found in the dungeon");
                bossChecklist.Call("AddMiniBossWithInfo", "Imp Lord", 5.8f, (Func<bool>)(() => JoostWorld.downedImpLord), "Quest from the Huntmaster, found in hell.");
                //14 is the value BossChecklist gives to Moonlord.
                bossChecklist.Call("AddBossWithInfo", "Alpha Cactus Worm", 5.7f, (Func<bool>)(() => JoostWorld.downedCactusWorm), "Quest from the Hunt Master, found in the underground desert. Or, use a [i:" + ItemType("CactusBait") + "] in the underground desert");
                bossChecklist.Call("AddBossWithInfo", "Jumbo Cactuar", 14.6f, (Func<bool>)(() => JoostWorld.downedJumboCactuar), "Use a [i:" + ItemType("Cactusofdoom") + "] in the desert");
                bossChecklist.Call("AddBossWithInfo", "SA-X", 15.1f, (Func<bool>)(() => JoostWorld.downedSAX), "Use an [i:" + ItemType("InfectedArmCannon") + "] anywhere");
                bossChecklist.Call("AddBossWithInfo", "Gilgamesh and Enkidu", 15.8f, (Func<bool>)(() => JoostWorld.downedGilgamesh), "Use an [i:" + ItemType("Excalipoor") + "] anywhere");
            }
            InitializeHunts();
        }
        // string:"AddHunt", string:huntName, string:questAccept, string:questComplete, int:questItem, Func<bool>:questActive, Func<bool>:questAvailable, Func<bool>:downedHunt, Action<Player>:reward
        public override object Call(params object[] args)
        {
            try
            {
                string message = args[0] as string;
                if (message == "AddHunt")
                {
                    int NPC = Convert.ToInt32(args[1]);
                    float progression = Convert.ToSingle(args[2]);
                    string questAccept = args[3] as string;
                    string questComplete = args[4] as string;
                    int questItem = Convert.ToInt32(args[5]);
                    Func<bool> questAvailable = args[6] as Func<bool>;
                    Action<Player> reward = args[7] as Action<Player>;
                    if (!Main.dedServ)
                        AddHunt(NPC, progression, questAccept, questComplete, questItem, questAvailable, reward);
                        hunts.Sort((a, b) => a.progression.CompareTo(b.progression));
                    return "Success";
                }
                else
                {
                    ErrorLogger.Log("Joostmod Call Error: Unknown Message: " + message);
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Log("Joostmod Call Error: " + e.StackTrace + e.Message);
            }
            return "Failure";
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            JoostModMessageType msgType = (JoostModMessageType)reader.ReadByte();
            switch (msgType)
            {
                case JoostModMessageType.ActiveQuest:
                    int quest = reader.ReadInt32();
                    JoostWorld.activeQuest = quest;
                    break;
                default:
                    ErrorLogger.Log("JoostMod: Unknown Message type: " + msgType);
                    break;
            }
        }
        public void InitializeHunts()
        {
            Mod mod = JoostMod.instance;
            hunts = new List<HuntInfo> {
            new HuntInfo(mod.NPCType("RogueTomato"), 0.2f, "I saw this tomato monster thing runnin' around. It shouldn't be too hard, I've fought slimes nastier than this thing. It's nastier than that big pink slime from earlier, but there's nastier slimes out there is what I'm sayin'. Anyways, it tends to bury itself, but the top of it will stick up on the surface durin' daytime", "Ya got that tomato? Hand it over here. ERGH, its head is still movin'! Here, uh, you can keep it.", mod.ItemType("RogueTomato"), () => !JoostWorld.downedRogueTomato, TomatoReward),
            new HuntInfo(mod.NPCType("WoodGuardian"), 0.3f, "Well, turns out ya angered some kind of ancient forest guardian by chopping down trees fer wood. As long as ya replant them it's fine! It'll be somewhere in the forest", "Nice work with that tree monster! Whuzzat ya got there? Some kinda seed? It looks to be moving... Oh! It just sprouted a little baby tree guy! He's cute, here ya take care of him, okay?", mod.ItemType("WoodGuardian"), () => !JoostWorld.downedWoodGuardian, WoodReward),
            new HuntInfo(NPCID.KingSlime, 1f, "Ya know how I said there are slimes nastier than that tomato? This is one of 'em. The King of slimes 'imself! He'll be found rarely nearish to the ocean. He will also show up if it starts rainin' slimes. He can also be summoned with a Slime Crown", "Good job takin' down the slime king! Here's something fer ya troubles.", mod.ItemType("KingSlime"), () => !NPC.downedSlimeKing, KingSlimeReward),
            new HuntInfo(mod.NPCType("FloweringCactoid"), 1.1f, "Ya know those Cactoid things in the desert? Well they got a leader now. I'm pretty sure it's conspiring to destroy us all! Find it and slay it!", "Good job taking down the Cactoid! Hmm, there seems to be some kinda badge in the flower. Here, you take it", mod.ItemType("FloweringCactoid"), () => !JoostWorld.downedFloweringCactoid, FloweringCactoidReward),
            new HuntInfo(NPCID.EyeofCthulhu, 2f, "Do ya feel that evil presence watching us? That's because it's a GIANT FRICKIN' EYEBALL! This thing might show up at night, otherwise use a Suspicious Looking Eye", "Good job taking down the Eyeball with a mouth! Here, take this.", mod.ItemType("EyeOfCthulhu"), () => !NPC.downedBoss1, EyeReward),
            new HuntInfo(mod.NPCType("ICU"), 2.1f, "There are far too many eye monsters in this world. I saw one that was four of 'em at once amalgamated together! It saw me too, of course, so I started thrown' my knives at it when it started shootin' lasers! I promptly went back indoors. It'll roam around at night, if it's nearby I'll help ya' out.", "This thing is frickin' disgusting. EERUGUHUH! It blinked!", mod.ItemType("ICU"), () => !JoostWorld.downedICU, ICUReward),
            new HuntInfo(NPCID.BrainofCthulhu, 3f, "Ya know those caverns in the crimson? Well deep inside there are these demonic heart things. Legend has it that if you destroy three of them they summons an ancient evil! Summon and then slay this evil! You can also use a Bloody Spine to summon it too.", "Good job taking down the Brain of Cthulhu! Strange how it also had a heart in it.", mod.ItemType("BrainOfCthulhu"), () => (WorldGen.crimson && !NPC.downedBoss2), BrainReward),
            new HuntInfo(NPCID.EaterofWorldsHead, 3f, "Ya know those chasms in the corruption? Well deep inside there are these shadow orbs. Legend has it that if you smash three of them they summons an ancient evil! Summon and then slay this evil! You can also use Worm food to summon it too.", "Good job taking down the Eater of Worlds! Good thing it doesn't actually eat worlds!", mod.ItemType("EaterOfWorlds"), () => (!WorldGen.crimson && !NPC.downedBoss2), EaterReward),
            new HuntInfo(mod.NPCType("SporeSpawn"), 3.1f, "Ya know those spores in the unnerground jungle? Well I saw what seems to be where they come from! It's this big angry plant thing with some kinda shell. Go to the unnerground jungle and kill it!", "Good job takin' it down! Looks like some kinda, uh, organ in here that creates the spores. You take it", mod.ItemType("SporeSpawn"), () => !JoostWorld.downedSporeSpawn, SpoReward),
            new HuntInfo(NPCID.QueenBee, 4f, "While down in the jungle ya may have come across one of those giant beehives. Turns out, each of 'em gots a giant Queen! Take it down before it kills us all! It can be summoned by killin' the larva within the hive or with an Abeemination", "Good job takin' down the Queen o' Bees! Here's take, uh, this special flail! It's not just a hive stuck to a handle together with honey!", mod.ItemType("QueenBee"), () => !NPC.downedQueenBee, BeeReward),
            new HuntInfo(mod.NPCType("Roc"), 4.1f, "I was climbin' up a mountain when a giant frickin' bird swooped down and grabbed me! I struggled meself out of it's talons and fell into a nearby pond. That thing is vicious! Ascend towards the heavens and slay this oversized chicken!", "Nice work groundin' that bird. Here, you should use these wings. What're ya givin' me that look fer? Just strap 'em to your back, I'm sure it'll work!", mod.ItemType("Roc"), () => !JoostWorld.downedRoc, RocReward),
            new HuntInfo(NPCID.SkeletronHead, 5f, "That old man outside the dungeon said he was cursed! Apparantly his master can be summoned at night and defeating him will lift the curse. The old man told me I'd only get meself killed, but I think that yer strong enough now to do it.", "Good job decursin' that old man! I'm sure he much appreciates it. Here, take this before going down into that skeleton infested dungeon.", mod.ItemType("Skeletron"), () => !NPC.downedBoss3, SkeletronReward),
            new HuntInfo(mod.NPCType("SkeletonDemoman"), 5.1f, "There's all sorts of dangerous things in the dungeon, but currently it seems the biggest danger is a demolitions skeleton! It plants landmines, throws grenades, and even has a giant cannon of doom! Take it out before it uses that cannon to destroy us all! Also, watch yer step.", "Damn, that thing is huge! Hang on, it's jammed. *POP* There we go! Here, try not get us all killed.", mod.ItemType("SkeletonDemoman"), () => !JoostWorld.downedSkeletonDemoman, DemomanReward),
            new HuntInfo(mod.NPCType("IdleCactusWorm"), 5.7f, "Ya know those cactus worms? They're the most delicious kind of cactus! Anyways, there seems to be some huge one in the underground desert. You ought to use that cannon to clear out a large enough space to fight it, it's gonna be mighty nasty when you wake it up!", "Hang on here, so you killed the big cacto worm, and then an even BIGGER one showed up after? Jegus, you should be glad you weren't killed too hard! Anyways, it seems there's less cactus worms now that you killed their leader. I think the cactus people will finally be able to move back into the desert now! Ya better make a house for them.", mod.ItemType("GrandCactusWorm"), () => !JoostWorld.downedCactusWorm, CactoWormReward),
            new HuntInfo(mod.NPCType("ImpLord"), 5.8f, "Go to hell! Literally! Dig down until ya find yerself surrounded by lava and demons! Among those demons are fire imps: little teleportin' buggers that shoot fire! They have a leader with wings that's especially dangerous. Watch out for his giant fire blast attack! Ya can use a sword to reflect it back at him but if you fail to it hurts a lot!", "Good job taking down the Imp Lord! Here, ya can use the tail as a whip or somethin'", mod.ItemType("ImpLord"), () => !JoostWorld.downedImpLord, ImpReward),
            new HuntInfo(NPCID.WallofFlesh, 6f, "I think it's time for ya to fight what may be the nastiest creature ever: the Wall o' Flesh! This horrific monstrosity is summoned by sacrificin' a special doll into the magma of hell. Ya will find the doll carried by some of the demons down there. Be well prepared for this one.", "Ya did it! Fantastic! But it turns out defeating the Wall o' Flesh released the ancient spirits of light and dark into the world. All sorts of nasties will appear as a result of that. Keep yer eyes peeled.", mod.ItemType("WallOfFlesh"), () => !Main.hardMode, WofReward),
            };
        }
        internal void AddHunt(int NPC, float progression, string questAccept, string questComplete, int questItem, Func<bool> questAvailable, Action<Player> reward)
        {
            hunts.Add(new HuntInfo(NPC, progression, questAccept, questComplete, questItem, questAvailable, reward));
        }
        private void TomatoReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("TomatoHead"));
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
        }
        private void WoodReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("Sapling"));
            player.QuickSpawnItem(ItemID.Wood, 150);
            player.QuickSpawnItem(ItemID.GoldCoin);
        }
        private void KingSlimeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("FireFlinger"));
            player.QuickSpawnItem(ItemID.Gel, 100);
            player.QuickSpawnItem(ItemID.SlimeStaff);
            player.QuickSpawnItem(ItemID.GoldCoin);
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
            player.QuickSpawnItem(ItemID.LifeCrystal);
        }
        private void FloweringCactoidReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("CactoidCommendation"));
            player.QuickSpawnItem(ItemID.GoldCoin, 2);
        }
        private void EyeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("EyeballStaff"));
            player.QuickSpawnItem(ItemID.Binoculars);
            player.QuickSpawnItem(ItemID.GoldCoin, 2);
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
            player.QuickSpawnItem(ItemID.LifeCrystal);
            player.QuickSpawnItem(ItemID.ManaCrystal);
        }
        private void ICUReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("ObservantStaff"));
            player.QuickSpawnItem(ItemID.GoldCoin, 3);
            player.QuickSpawnItem(ItemID.Lens, 2);
            player.QuickSpawnItem(ItemID.BlackLens, 2);
        }
        private void EaterReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("CorruptPommel"));
            player.QuickSpawnItem(ItemID.GoldCoin, 3);
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
            player.QuickSpawnItem(ItemID.LifeCrystal);
            player.QuickSpawnItem(ItemID.ManaCrystal);
        }
        private void BrainReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("CrimsonPommel"));
            player.QuickSpawnItem(ItemID.GoldCoin, 3);
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
            player.QuickSpawnItem(ItemID.LifeCrystal);
            player.QuickSpawnItem(ItemID.ManaCrystal);
        }
        private void SpoReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("Sporgan"));
            player.QuickSpawnItem(ItemID.GoldCoin, 4);
            player.QuickSpawnItem(ItemID.JungleSpores, 20);
        }
        private void BeeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("TheHive"));
            player.QuickSpawnItem(ItemID.GoldCoin, 4);
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
            player.QuickSpawnItem(ItemID.HoneyedGoggles);
            player.QuickSpawnItem(ItemID.LifeCrystal);
        }
        private void RocReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("RocWings"));
            player.QuickSpawnItem(ItemID.GoldCoin, 5);
            player.QuickSpawnItem(ItemID.Feather, 20);
        }
        private void SkeletronReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("Bonesaw"));
            player.QuickSpawnItem(ItemID.Bone, 20);
            player.QuickSpawnItem(ItemID.GoldCoin, 5);
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
            player.QuickSpawnItem(ItemID.LifeCrystal);
            player.QuickSpawnItem(ItemID.ManaCrystal);
        }
        private void DemomanReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("DoomCannon"));
            player.QuickSpawnItem(ItemID.LandMine, 3);
            player.QuickSpawnItem(ItemID.Grenade, 20);
            player.QuickSpawnItem(ItemID.GoldCoin, 6);
        }
        private void CactoWormReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("CactusBoots"));
            player.QuickSpawnItem(ItemID.GoldCoin, 6);
            player.QuickSpawnItem(ItemID.SilverCoin, 50);
            player.QuickSpawnItem(ItemID.LifeCrystal);
            player.QuickSpawnItem(ItemID.ManaCrystal);
        }
        private void ImpReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("TailWhip"));
            player.QuickSpawnItem(ItemID.GoldCoin, 7);
            player.QuickSpawnItem(ItemID.FireblossomSeeds, 15);
        }
        private void WofReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(mod.ItemType("FleshShield"));
            player.QuickSpawnItem(ItemID.GoldCoin, 10);
            player.QuickSpawnItem(ItemID.LifeCrystal);
            player.QuickSpawnItem(ItemID.ManaCrystal);
        }
        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemType("Sapling")), new int[]
            {
                ItemType("Sapling"),
                ItemType("ShieldSapling"),
                ItemType("GlowSapling"),
                ItemType("SpelunkerSapling"),
                ItemType("BowSapling"),
                ItemType("SwordSapling"),
                ItemType("StaffSapling"),
                ItemType("HatchetSapling"),
                ItemType("FishingSapling")
            });

            RecipeGroup cpgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.CobaltBar), new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup mogroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.MythrilBar), new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            });
            RecipeGroup atgroup = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemID.AdamantiteBar), new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("JoostMod:Saplings", group);
            RecipeGroup.RegisterGroup("JoostMod:AnyCobalt", cpgroup);
            RecipeGroup.RegisterGroup("JoostMod:AnyMythril", mogroup);
            RecipeGroup.RegisterGroup("JoostMod:AnyAdamantite", atgroup);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.HallowedBar, 15);
            recipe.AddIngredient(ItemID.SoulofLight, 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.RodofDiscord);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.PiggyBank);
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddIngredient(ItemID.Feather, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.MoneyTrough);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddIngredient(ItemID.Wire, 30);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.DPSMeter);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("Wood", 25); ;
            recipe.AddTile(TileID.LeafBlock);
            recipe.SetResult(ItemID.LivingLoom);
            recipe.AddRecipe();


            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.RangerEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.SorcererEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.SummonerEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.WarriorEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.SorcererEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.SummonerEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.WarriorEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.RangerEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.SummonerEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.WarriorEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.RangerEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(ItemID.SorcererEmblem);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "DesertCore", 2);
            recipe.AddIngredient(ItemID.DarkShard);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.AncientBattleArmorMaterial);

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Glowstick, 50);
            recipe.AddIngredient(ItemID.SpelunkerPotion);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.SpelunkerGlowstick, 50);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Leather);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "FireEssence", 25);
            recipe.AddIngredient(ItemID.MechanicsRod);
            recipe.AddTile(null, "ElementalForge");
            recipe.SetResult(ItemID.HotlineFishingHook);
            recipe.AddRecipe();
        }
        public override void Load()
        {
            instance = this;
            ArmorAbilityHotKey = RegisterHotKey("Armor Ability", "Z");
            if (!Main.dedServ)
            {
                //AddEquipTexture(null, EquipType.Legs, "GenjiArmorMagic_Legs", "JoostMod/Items/Armor/GenjiArmorMagic_Legs");
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/TheDecisiveBattle"), ItemType("DecisiveBattleMusicBox"), TileType("DecisiveBattleMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/VsSAX"), ItemType("SAXMusicBox"), TileType("SAXMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/ClashOnTheBigBridge"), ItemType("COTBBMusicBox"), TileType("COTBBMusicBox"));
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/DeoremMua"), ItemType("DeoremMuaMusicBox"), TileType("DeoremMuaMusicBox"));
            }
        }
        class SpawnRateMultiplierGlobalNPC : GlobalNPC
        {
            public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
            {
                if (player.GetModPlayer<JoostPlayer>().HavocPendant)
                {
                    spawnRate = (int)(spawnRate / 5f);
                    maxSpawns = (int)(maxSpawns * 5f);
                }
                if (player.GetModPlayer<JoostPlayer>().HarmonyPendant)
                {
                    spawnRate = (int)(spawnRate * 5f);
                    maxSpawns = (int)(maxSpawns / 5f);
                }

            }
        }

    }
    enum JoostModMessageType : byte
    {
        ActiveQuest
    }
    public class HuntInfo
    {
        internal int NPC;
        internal float progression;
        internal string accept;
        internal string complete;
        internal int item;
        internal Func<bool> available;
        internal Action<Player> reward;

        public HuntInfo(int NPC, float progression, string accept, string complete, int item, Func<bool> available, Action<Player> reward)
        {
            this.NPC = NPC;
            this.progression = progression;
            this.accept = accept;
            this.complete = complete;
            this.item = item;
            this.available = available;
            this.reward = reward;
        }
    }
}