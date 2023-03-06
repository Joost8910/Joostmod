using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.IO;
using JoostMod.NPCs.Bosses;
using JoostMod.Items;
using Microsoft.Xna.Framework;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using JoostMod.UI;
using JoostMod.Items.Materials;
using JoostMod.Items.Accessories;

namespace JoostMod
{
    public class JoostMod : Mod
    {
        internal bool battleRodsLoaded;
        internal static ModKeybind ArmorAbilityHotKey;
        static internal JoostMod instance;

        public List<HuntInfo> hunts;

        public JoostMod()
        {
            ContentAutoloadingEnabled = true;
            GoreAutoloadingEnabled = true;
            MusicAutoloadingEnabled = true;
        }
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            battleRodsLoaded = ModLoader.GetMod("UnuBattleRods") != null;
            if (bossChecklist != null)
            {
                /*
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
                */

                bossChecklist.Call("AddMiniBoss", 0.1f, ModContent.NPCType<NPCs.Hunts.Pinkzor>(), this, "Pinkzor", (Func<bool>)(() => JoostWorld.downedPinkzor), null, null, ModContent.ItemType<Items.Quest.Pinkzor>(), "Rarely found on the surface or underground. The Huntmaster will spawn after it is killed");
                bossChecklist.Call("AddMiniBoss", 0.2f, ModContent.NPCType<NPCs.Hunts.RogueTomato>(), this, "Rogue Tomato", (Func<bool>)(() => JoostWorld.downedRogueTomato), null, null, ModContent.ItemType<Items.Quest.RogueTomato>(), "Quest from the Hunt Master, found on the surface during daytime");
                bossChecklist.Call("AddMiniBoss", 0.3f, ModContent.NPCType<NPCs.Hunts.WoodGuardian>(), this, "Forest's Vengeance", (Func<bool>)(() => JoostWorld.downedWoodGuardian), null, null, ModContent.ItemType<Items.Quest.WoodGuardian>(), "Quest from the Hunt Master, found in the forest");
                bossChecklist.Call("AddMiniBoss", 1.1f, ModContent.NPCType<NPCs.Hunts.FloweringCactoid>(), this, "Flowering Cactoid", (Func<bool>)(() => JoostWorld.downedFloweringCactoid), null, null, ModContent.ItemType<Items.Quest.FloweringCactoid>(), "Quest from the Hunt Master, found in the surface desert");
                bossChecklist.Call("AddMiniBoss", 2.1f, ModContent.NPCType<NPCs.Hunts.ICU>(), this, "ICU", (Func<bool>)(() => JoostWorld.downedICU), null, null, ModContent.ItemType<Items.Quest.ICU>(), "Quest from the Hunt Master, found on the surface during nighttime");
                bossChecklist.Call("AddMiniBoss", 3.1f, ModContent.NPCType<NPCs.Hunts.SporeSpawn>(), this, "Spore Mother", (Func<bool>)(() => JoostWorld.downedSporeSpawn), null, null, ModContent.ItemType<Items.Quest.SporeSpawn>(), "Quest from the Hunt Master, found in the underground jungle");
                bossChecklist.Call("AddMiniBoss", 4.1f, ModContent.NPCType<NPCs.Hunts.Roc>(), this, "Roc", (Func<bool>)(() => JoostWorld.downedRoc), null, null, ModContent.ItemType<Items.Quest.Roc>(), "Quest from the Hunt Master, found in the sky");
                bossChecklist.Call("AddMiniBoss", 5.1f, ModContent.NPCType<NPCs.Hunts.SkeletonDemoman>(), this, "Skeleton Demolitionist", (Func<bool>)(() => JoostWorld.downedSkeletonDemoman), null, null, ModContent.ItemType<Items.Quest.SkeletonDemoman>(), "Quest from the Hunt Master, found in the dungeon");
                bossChecklist.Call("AddMiniBoss", 5.8f, ModContent.NPCType<NPCs.Hunts.ImpLord>(), this, "Imp Lord", (Func<bool>)(() => JoostWorld.downedImpLord), null, null, ModContent.ItemType<Items.Quest.ImpLord>(), "Quest from the Huntmaster, found in hell.");
                bossChecklist.Call("AddMiniBoss", 6.5f, ModContent.NPCType<NPCs.Hunts.StormWyvernHead>(), this, "Storm Wyvern", (Func<bool>)(() => JoostWorld.downedStormWyvern), null, null, ModContent.ItemType<Items.Quest.StormWyvern>(), "Quest from the Huntmaster, found in the sky while it's raining.", "JoostMod/NPCs/Hunts/StormWyvernBossLog");

                bossChecklist.Call("AddBoss", 5.7f, ModContent.NPCType<IdleCactusWorm>(), this, "Alpha Cactus Worm", (Func<bool>)(() => JoostWorld.downedCactusWorm), ModContent.ItemType<CactusBait>(), new List<int>() { ModContent.ItemType<Items.Armor.GrandCactusWormMask>(), ModContent.ItemType<Items.Placeable.DeoremMuaMusicBox>(), ModContent.ItemType<Items.Placeable.GrandCactusWormTrophy>() }, new List<int>() { ModContent.ItemType<GrandCactusWormBag>(), ModContent.ItemType<CactusWormHook>(), ModContent.ItemType<Materials.LusciousCactus>() }, "Quest from the Hunt Master, found in the underground desert. Or, use a [i:" + Find<ModItem>("CactusBait").Type + "] in the underground desert");
                bossChecklist.Call("AddBoss", 14.6f, ModContent.NPCType<JumboCactuar>(), this, "Jumbo Cactuar", (Func<bool>)(() => JoostWorld.downedJumboCactuar), ModContent.ItemType<Cactusofdoom>(), new List<int>() { ModContent.ItemType<Items.Armor.JumboCactuarMask>(), ModContent.ItemType<Items.Placeable.DecisiveBattleMusicBox>(), ModContent.ItemType<Items.Placeable.JumboCactuarTrophy>() }, new List<int>() { ModContent.ItemType<JumboCactuarBag>(), ModContent.ItemType<CactuarShield>(), ModContent.ItemType<Cactustoken>() }, "Use a [i:" + Find<ModItem>("Cactusofdoom").Type + "] in the desert");
                bossChecklist.Call("AddBoss", 15.1f, ModContent.NPCType<SAX>(), this, "SA-X", (Func<bool>)(() => JoostWorld.downedSAX), ModContent.ItemType<InfectedArmCannon>(), new List<int>() { ModContent.ItemType<Items.Armor.SAXMask>(), ModContent.ItemType<Items.Placeable.SAXMusicBox>(), ModContent.ItemType<Items.Placeable.SAXTrophy>() }, new List<int>() { ModContent.ItemType<XBag>(), ModContent.ItemType<XShield>(), ModContent.ItemType<IceCoreX>() }, "Use an [i:" + Find<ModItem>("InfectedArmCannon").Type + "] anywhere", "The SA-X disappears into the unknown", "JoostMod/NPCs/Bosses/SAXBossLog");
                bossChecklist.Call("AddBoss", 15.8f, new List<int>() { ModContent.NPCType<Gilgamesh>(), ModContent.NPCType<Enkidu>() }, this, "Gilgamesh and Enkidu", (Func<bool>)(() => JoostWorld.downedGilgamesh), ModContent.ItemType<Excalipoor>(), new List<int>() { ModContent.ItemType<Items.Armor.GilgameshMask>(), ModContent.ItemType<Items.Placeable.COTBBMusicBox>(), ModContent.ItemType<Items.Placeable.GilgameshTrophy>() }, new List<int>() { ModContent.ItemType<GilgBag>(), ModContent.ItemType<Items.Weapons.Gilgameshset>(), ModContent.ItemType<GenjiToken>() }, "Use an [i:" + Find<ModItem>("Excalipoor").Type + "] anywhere", "<Gilgamesh> Hah! I won!", "JoostMod/NPCs/Bosses/GilgameshAndEnkiduBossLog");

            }
            Mod fargos = ModLoader.GetMod("Fargowiltas");
            if (fargos != null)
            {
                // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon   
                //item internal name, inline method for retrieving downed value, price to sell for in copper
                fargos.Call("AddSummon", 5.7f, "JoostMod", "CactusBait", (Func<bool>)(() => JoostWorld.downedCactusWorm), 75000);
                fargos.Call("AddSummon", 14.6f, "JoostMod", "Cactusofdoom", (Func<bool>)(() => JoostWorld.downedJumboCactuar), 7500000);
                fargos.Call("AddSummon", 15.1f, "JoostMod", "InfectedArmCannon", (Func<bool>)(() => JoostWorld.downedSAX), 12500000);
                fargos.Call("AddSummon", 15.8f, "JoostMod", "Excalipoor", (Func<bool>)(() => JoostWorld.downedGilgamesh), 20000000);

            }
            InitializeHunts();
        }
        // string:"AddHunt", string:huntName, string:questText, string:completeText, int:questItem, Func<bool>:questActive, Func<bool>:questAvailable, Func<bool>:questCompleted, Func<bool>:showQuest, Func<bool>:downedHunt, Action<Player>:reward, int:xFrameCount
        public override object Call(params object[] args)
        {
            try
            {
                string message = args[0] as string;
                if (message == "AddHunt")
                {
                    int NPC = Convert.ToInt32(args[1]);
                    float progression = Convert.ToSingle(args[2]);
                    string questText = args[3] as string;
                    string completeText = args[4] as string;
                    int questItem = Convert.ToInt32(args[5]);
                    Func<bool> questAvailable = args[6] as Func<bool>;
                    Func<bool> questCompleted = args[7] as Func<bool>;
                    Func<bool> showQuest = args[8] as Func<bool>;
                    Action<Player> reward = args[9] as Action<Player>;
                    int xFrameCount = Convert.ToInt32(args[10]);
                    if (!Main.dedServ)
                        AddHunt(NPC, progression, questText, completeText, questItem, questAvailable, questCompleted, showQuest, reward, xFrameCount);
                    hunts.Sort((a, b) => a.progression.CompareTo(b.progression));
                    return "Success";
                }
                else
                {
                    Logger.InfoFormat("Joostmod Call Error: Unknown Message: " + message);
                }
            }
            catch (Exception e)
            {
                Logger.InfoFormat("Joostmod Call Error: " + e.StackTrace + e.Message);
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
                    JoostWorld.activeQuest.Add(quest);
                    break;
                case JoostModMessageType.SAXCore:
                    SAXCoreX sax = Main.npc[reader.ReadInt32()].ModNPC as SAXCoreX;
                    if (sax != null && sax.NPC.active)
                    {
                        sax.HandlePacket(reader);
                    }
                    break;
                case JoostModMessageType.KillNPC:
                    int b = reader.ReadInt32();
                    Main.npc[b].life = 0;
                    Main.npc[b].checkDead();
                    break;
                case JoostModMessageType.NPCpos:
                    int n = reader.ReadInt32();
                    Vector2 pos = reader.ReadVector2();
                    Vector2 vel = reader.ReadVector2();
                    Main.npc[n].position = pos;
                    Main.npc[n].velocity = vel;
                    break;
                case JoostModMessageType.Playerpos:
                    byte p = reader.ReadByte();
                    Vector2 position = reader.ReadVector2();
                    Vector2 velocity = reader.ReadVector2();
                    Main.player[p].position = position;
                    Main.player[p].velocity = velocity;
                    break;
                default:
                    Logger.InfoFormat("JoostMod: Unknown Message type: " + msgType);
                    break;
            }
        }
        public void InitializeHunts()
        {
            Mod mod = JoostMod.instance;
            hunts = new List<HuntInfo> {
            new HuntInfo(mod.Find<ModNPC>("RogueTomato").Type, 0.2f, "I saw this tomato monster thing runnin' around. It shouldn't be too hard, I've fought slimes nastier than this thing. It's nastier than that big pink slime from earlier, but there's nastier slimes out there is what I'm sayin'. Anyways, it tends to bury itself, but the top of it will stick up on the surface durin' daytime", "Ya got that tomato? Hand it over here. ERGH, its head is still movin'! Here, uh, you can keep it.", mod.Find<ModItem>("RogueTomato").Type, () => true, () => JoostWorld.downedRogueTomato, () => true, TomatoReward),
            new HuntInfo(mod.Find<ModNPC>("WoodGuardian").Type, 0.3f, "Well, turns out ya angered some kind of ancient forest guardian by chopping down trees fer wood. As long as ya replant them it's fine! It'll be somewhere in the forest", "Nice work with that tree monster! Whuzzat ya got there? Some kinda seed? It looks to be moving... Oh! It just sprouted a little baby tree guy! He's cute, here ya take care of him, okay?", mod.Find<ModItem>("WoodGuardian").Type, () => true, () => JoostWorld.downedWoodGuardian, () => true, WoodReward),
            new HuntInfo(NPCID.KingSlime, 1f, "Ya know how I said there are slimes nastier than that tomato? This is one of 'em. The King of slimes 'imself! He'll be found rarely nearish to the ocean. He will also show up if it starts rainin' slimes. He can also be summoned with a Slime Crown", "Good job takin' down the slime king! Here's something fer ya troubles.", mod.Find<ModItem>("KingSlime").Type, () => true, () => NPC.downedSlimeKing, () => true, KingSlimeReward),
            new HuntInfo(mod.Find<ModNPC>("FloweringCactoid").Type, 1.1f, "Ya know those Cactoid things in the desert? Well they got a leader now. I'm pretty sure it's conspiring to destroy us all! Find it and slay it!", "Good job taking down the Cactoid! Hmm, there seems to be some kinda badge in the flower. Here, you take it", mod.Find<ModItem>("FloweringCactoid").Type, () => NPC.downedSlimeKing, () => JoostWorld.downedFloweringCactoid, () => true, FloweringCactoidReward),
            new HuntInfo(NPCID.EyeofCthulhu, 2f, "Do ya feel that evil presence watching us? That's because it's a GIANT FRICKIN' EYEBALL! This thing might show up at night, otherwise use a Suspicious Looking Eye", "Good job taking down the Eyeball with a mouth! Here, take this.", mod.Find<ModItem>("EyeOfCthulhu").Type, () => true, () => NPC.downedBoss1, () => true, EyeReward),
            new HuntInfo(mod.Find<ModNPC>("ICU").Type, 2.1f, "There are far too many eye monsters in this world. I saw one that was four of 'em at once amalgamated together! It saw me too, of course, so I started thrown' my knives at it when it started shootin' lasers! I promptly went back indoors. It'll roam around at night, if it's nearby I'll help ya' out.", "This thing is frickin' disgusting. EERUGUHUH! It blinked!", mod.Find<ModItem>("ICU").Type, () => NPC.downedBoss1, () => JoostWorld.downedICU, () => true, ICUReward),
            new HuntInfo(NPCID.BrainofCthulhu, 3f, "Ya know those caverns in the crimson? Well deep inside there are these demonic heart things. Legend has it that if you destroy three of them they summon an ancient evil! Summon and then slay this evil! You can also use a Bloody Spine to summon it too.", "Good job taking down the Brain of Cthulhu! Strange how it also had a heart in it.", mod.Find<ModItem>("BrainOfCthulhu").Type, () => WorldGen.crimson, () => NPC.downedBoss2, () => WorldGen.crimson, BrainReward),
            new HuntInfo(NPCID.EaterofWorldsHead, 3f, "Ya know those chasms in the corruption? Well deep inside there are these shadow orbs. Legend has it that if you smash three of them they summon an ancient evil! Summon and then slay this evil! You can also use Worm food to summon it too.", "Good job taking down the Eater of Worlds! Good thing it doesn't actually eat worlds!", mod.Find<ModItem>("EaterOfWorlds").Type, () => !WorldGen.crimson, () => NPC.downedBoss2, () => !WorldGen.crimson, EaterReward),
            new HuntInfo(mod.Find<ModNPC>("SporeSpawn").Type, 3.1f, "Ya know those spores in the unnerground jungle? Well I saw what seems to be where they come from! It's this big angry plant thing with some kinda shell. Go to the unnerground jungle and kill it!", "Good job takin' it down! Looks like some kinda, uh, organ in here that creates the spores. You take it", mod.Find<ModItem>("SporeSpawn").Type, () => NPC.downedBoss2, () => JoostWorld.downedSporeSpawn, () => true, SpoReward),
            new HuntInfo(NPCID.QueenBee, 4f, "While down in the jungle ya may have come across one of those giant beehives. Turns out, each of 'em gots a giant Queen! Take it down before it kills us all! It can be summoned by killin' the larva within the hive or with an Abeemination", "Good job takin' down the Queen o' Bees! Here's take, uh, this special flail! It's not just a hive stuck to a handle together with honey!", mod.Find<ModItem>("QueenBee").Type, () => true, () => NPC.downedQueenBee, () => true, BeeReward),
            new HuntInfo(mod.Find<ModNPC>("Roc").Type, 4.1f, "I was climbin' up a mountain when a giant frickin' bird swooped down and grabbed me! I struggled meself out of it's talons and fell into a nearby pond. That thing is vicious! Ascend towards the heavens and slay this oversized chicken!", "Nice work groundin' that bird. Here, you should use these wings. What're ya givin' me that look fer? Just strap 'em to your back, I'm sure it'll work!", mod.Find<ModItem>("Roc").Type, () => NPC.downedQueenBee, () => JoostWorld.downedRoc, () => true, RocReward),
            new HuntInfo(NPCID.SkeletronHead, 5f, "That old man outside the dungeon said he was cursed! Apparantly his master can be summoned at night and defeating him will lift the curse. The old man told me I'd only get meself killed, but I think that yer strong enough now to do it.", "Good job decursin' that old man! I'm sure he much appreciates it. Here, take this before going down into that skeleton infested dungeon.", mod.Find<ModItem>("Skeletron").Type, () => true, () => NPC.downedBoss3, () => true, SkeletronReward),
            new HuntInfo(mod.Find<ModNPC>("SkeletonDemoman").Type, 5.1f, "There's all sorts of dangerous things in the dungeon, but currently it seems the biggest danger is a demolitions skeleton! It plants landmines, throws grenades, and even has a giant cannon of doom! Take it out before it uses that cannon to destroy us all! Also, watch yer step.", "Damn, that thing is huge! Hang on, it's jammed. *POP* There we go! Here, try not get us all killed.", mod.Find<ModItem>("SkeletonDemoman").Type, () => NPC.downedBoss3, () => JoostWorld.downedSkeletonDemoman, () => true, DemomanReward),
            new HuntInfo(mod.Find<ModNPC>("IdleCactusWorm").Type, 5.7f, "Ya know those cactus worms? They're the most delicious kind of cactus! Anyways, there seems to be some huge one in the underground desert. You ought to use that cannon to clear out a large enough space to fight it, it's gonna be mighty nasty when you wake it up!", "Hang on here, so you killed the big cacto worm, and then an even BIGGER one showed up after? Jegus, you should be glad you weren't killed too hard! Anyways, it seems there's less cactus worms now that you killed their leader. I think the cactus people will finally be able to move back into the desert now! Ya better make a house for them.", mod.Find<ModItem>("GrandCactusWorm").Type, () => NPC.downedBoss3, () => JoostWorld.downedCactusWorm, () => true, CactoWormReward),
            new HuntInfo(mod.Find<ModNPC>("ImpLord").Type, 5.8f, "Go to hell! Literally! Dig down until ya find yerself surrounded by lava and demons! Among those demons are fire imps: little teleportin' buggers that shoot fire! They have a leader with wings that's especially dangerous. Watch out for his giant fire blast attack! Ya can use a sword to reflect it back at him but if you fail to it hurts a lot!", "Good job taking down the Imp Lord! Here, ya can use the tail as a whip or somethin'", mod.Find<ModItem>("ImpLord").Type, () => JoostWorld.downedCactusWorm, () => JoostWorld.downedImpLord, () => true, ImpReward),
            new HuntInfo(NPCID.WallofFlesh, 6f, "I think it's time for ya to fight what may be the nastiest creature ever: the Wall o' Flesh! This horrific monstrosity is summoned by sacrificin' a special doll into the magma of hell. Ya will find the doll carried by some of the demons down there. Be well prepared for this one.", "Ya did it! Fantastic! But it turns out defeating the Wall o' Flesh released the ancient spirits of light and dark into the world. All sorts of nasties will appear as a result of that. Keep yer eyes peeled.", mod.Find<ModItem>("WallOfFlesh").Type, () => JoostWorld.downedRogueTomato && JoostWorld.downedWoodGuardian && JoostWorld.downedFloweringCactoid && JoostWorld.downedSporeSpawn && JoostWorld.downedRoc && JoostWorld.downedSkeletonDemoman && JoostWorld.downedImpLord, () => Main.hardMode, () => true, WofReward),
            new HuntInfo(mod.Find<ModNPC>("StormWyvernHead").Type, 6.5f, "Ya know those flying wolf-snake things? What were they called again? Lindwurms? Whatever they're called, I noticed a particularly nasty-lookin' one fly over last it rained. What's nasty 'bout it? It shot a freakin' LIGHTNIN' bolt outta it's mouth! That thing is too dangerous to be left roamin' free! When it rains, take to the skies and take it down! But important tip before ya go! When it turns yellow that means it's a chargin'. When that happens: TAKE COVER. Ya may think yer fast, but ya ain't gonna dodge a lightnin' bolt!", "Good job grounding that, uh, flying wolf-snake. Wyrm? Whatever, here's ya reward. Whuzzat? They're called wyverns? Naw, that don't sound right.", mod.Find<ModItem>("StormWyvern").Type, () => Main.hardMode, () => JoostWorld.downedStormWyvern, () => Main.hardMode, StormWyvernReward)
            };
        }
        internal void AddHunt(int NPC, float progression, string questText, string completeText, int questItem, Func<bool> questAvailable, Func<bool> questCompleted, Func<bool> showQuest, Action<Player> reward, int xFrameCount = 1)
        {
            hunts.Add(new HuntInfo(NPC, progression, questText, completeText, questItem, questAvailable, questCompleted, showQuest, reward, xFrameCount));
        }
        private void TomatoReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("TomatoHead").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
        }
        private void WoodReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("Sapling").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Wood, 150);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin);
        }
        private void KingSlimeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("FireFlinger").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Gel, 100);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SlimeStaff);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
        }
        private void FloweringCactoidReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("CactoidCommendation").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 2);
        }
        private void EyeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("EyeballStaff>().Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Binoculars);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 2);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void ICUReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("ObservantStaff>().Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Lens, 2);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.BlackLens, 2);
        }
        private void EaterReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("CorruptPommel").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void BrainReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("CrimsonPommel").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void SpoReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("Sporgan").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 4);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.JungleSpores, 20);
        }
        private void BeeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("TheHive").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 4);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.HoneyedGoggles);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
        }
        private void RocReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("RocWings").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 5);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Feather, 20);
        }
        private void SkeletronReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("Bonesaw").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Bone, 20);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 5);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void DemomanReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("DoomCannon").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LandMine, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Grenade, 20);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 6);
        }
        private void CactoWormReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("CactusBoots").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 6);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void ImpReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("TailWhip").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("ImpLordFlame").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 7);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.FireblossomSeeds, 15);
        }
        private void WofReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("FleshShield").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 10);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void StormWyvernReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), mod.Find<ModItem>("StormWyvernScroll").Type);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 15);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SoulofFlight, 20);
        }
        
        public override void Load()
        {
            instance = this;
            ArmorAbilityHotKey = KeybindLoader.RegisterKeybind(this, "Armor Ability", "Z");
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
        ActiveQuest,
        SAXCore,
        KillNPC,
        NPCpos,
        Playerpos
    }
    public class HuntInfo
    {
        internal int NPC;
        internal float progression;
        internal string questText;
        internal string completeText;
        internal int item;
        internal Func<bool> available;
        internal Func<bool> completed;
        internal Func<bool> showQuest;
        internal Action<Player> reward;
        internal int xFrameCount;

        public HuntInfo(int NPC, float progression, string questText, string completeText, int item, Func<bool> available, Func<bool> completed, Func<bool> showQuest, Action<Player> reward, int xFrameCount = 1)
        {
            this.NPC = NPC;
            this.progression = progression;
            this.questText = questText;
            this.completeText = completeText;
            this.item = item;
            this.available = available;
            this.completed = completed;
            this.showQuest = showQuest;
            this.reward = reward;
            this.xFrameCount = xFrameCount;
        }
    }
}