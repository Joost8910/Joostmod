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
using JoostMod.Items.Consumables;
using JoostMod.Items.GrappleHooks;
using Terraria.Graphics.Shaders;
using ReLogic.Content;
using JoostMod.NPCs.Hunts;
using JoostMod.Items.Rewards;

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
            //battleRodsLoaded = ModLoader.TryGetMod("UnuBattleRods", out Mod UnuBattleRods);
            /*
            if (ModLoader.TryGetMod("Fargowiltas", out Mod fargos))
            {
                // AddSummon, order or value in terms of vanilla bosses, your mod internal name, summon   
                //item internal name, inline method for retrieving downed value, price to sell for in copper
                fargos.Call("AddSummon", 5.7f, "JoostMod", "CactusBait", (Func<bool>)(() => JoostWorld.downedCactusWorm), 75000);
                fargos.Call("AddSummon", 14.6f, "JoostMod", "Cactusofdoom", (Func<bool>)(() => JoostWorld.downedJumboCactuar), 7500000);
                fargos.Call("AddSummon", 15.1f, "JoostMod", "InfectedArmCannon", (Func<bool>)(() => JoostWorld.downedSAX), 12500000);
                fargos.Call("AddSummon", 15.8f, "JoostMod", "Excalipoor", (Func<bool>)(() => JoostWorld.downedGilgamesh), 20000000);

            }
            */
            DoBossChecklistIntegration();
            InitializeHunts();
        }
        private void DoBossChecklistIntegration()
        {
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                return;
            }
            if (bossChecklist.Version < new Version(1, 6))
            {
                return;
            }
            bossChecklist.Call("LogMiniBoss", this, "Pinkzor", 0.1f, () => JoostWorld.downedPinkzor, ModContent.NPCType<NPCs.Hunts.Pinkzor>());
            bossChecklist.Call("LogMiniBoss", this, "RogueTomato", 0.2f, () => JoostWorld.downedRogueTomato, ModContent.NPCType<NPCs.Hunts.RogueTomato>());
            bossChecklist.Call("LogMiniBoss", this, "WoodGuardian", 0.3f, () => JoostWorld.downedWoodGuardian, ModContent.NPCType<NPCs.Hunts.WoodGuardian>());
            bossChecklist.Call("LogMiniBoss", this, "FloweringCactoid", 1.1f, () => JoostWorld.downedFloweringCactoid, ModContent.NPCType<NPCs.Hunts.FloweringCactoid>());
            bossChecklist.Call("LogMiniBoss", this, "ICU", 2.1f, () => JoostWorld.downedICU, ModContent.NPCType<NPCs.Hunts.ICU>());
            bossChecklist.Call("LogMiniBoss", this, "SporeSpawn", 3.1f, () => JoostWorld.downedSporeSpawn, ModContent.NPCType<NPCs.Hunts.SporeSpawn>());
            bossChecklist.Call("LogMiniBoss", this, "Roc", 4.1f, () => JoostWorld.downedRoc, ModContent.NPCType<NPCs.Hunts.Roc>());
            bossChecklist.Call("LogMiniBoss", this, "SkeletonDemoman", 5.1f, () => JoostWorld.downedSkeletonDemoman, ModContent.NPCType<NPCs.Hunts.SkeletonDemoman>());
            bossChecklist.Call("LogMiniBoss", this, "ImpLord", 6.8f, () => JoostWorld.downedImpLord, ModContent.NPCType<NPCs.Hunts.ImpLord>());

            bossChecklist.Call("LogMiniBoss", this, "StormWyvern", 7.5f, () => JoostWorld.downedStormWyvern, ModContent.NPCType<NPCs.Hunts.StormWyvernHead>(), new Dictionary<string, object>()
            {
                ["customPortrait"] = (SpriteBatch spriteBatch, Rectangle rect, Color color) => {
                    Texture2D texture = ModContent.Request<Texture2D>("JoostMod/NPCs/Hunts/StormWyvernBossLog").Value;
                    Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                    spriteBatch.Draw(texture, centered, color);
                }
            });

            bossChecklist.Call("LogBoss", this, "AlphaCactusWorm", 6.5f, () => JoostWorld.downedCactusWorm, ModContent.NPCType<IdleCactusWorm>(), new Dictionary<string, object>()
            {
                ["spawnItems"] = ModContent.ItemType<CactusBait>()
            });
            bossChecklist.Call("LogBoss", this, "JumboCactuar", 18.6f, () => JoostWorld.downedJumboCactuar, ModContent.NPCType<JumboCactuar>(), new Dictionary<string, object>()
            {
                ["spawnItems"] = ModContent.ItemType<Cactusofdoom>()
            });
            bossChecklist.Call("LogBoss", this, "SAX", 19.1f, () => JoostWorld.downedSAX, ModContent.NPCType<SAX>(), new Dictionary<string, object>()
            {
                ["spawnItems"] = ModContent.ItemType<InfectedArmCannon>(),
                ["customPortrait"] = (SpriteBatch spriteBatch, Rectangle rect, Color color) => {
                    Texture2D texture = ModContent.Request<Texture2D>("JoostMod/NPCs/Bosses/SAXBossLog").Value;
                    Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                    spriteBatch.Draw(texture, centered, color);
                }
            }); ;
            bossChecklist.Call("LogBoss", this, "GilgameshandEnkidu", 19.8f, () => JoostWorld.downedGilgamesh, new List<int>() { ModContent.NPCType<Gilgamesh>(), ModContent.NPCType<Enkidu>() }, new Dictionary<string, object>()
            {
                ["spawnItems"] = ModContent.ItemType<Excalipoor>(),
                ["customPortrait"] = (SpriteBatch spriteBatch, Rectangle rect, Color color) => {
                    Texture2D texture = ModContent.Request<Texture2D>("JoostMod/NPCs/Bosses/GilgameshAndEnkiduBossLog").Value;
                    Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + (rect.Height / 2) - (texture.Height / 2));
                    spriteBatch.Draw(texture, centered, color);
                }
            });
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
            new HuntInfo(ModContent.NPCType<RogueTomato>(), 0.2f, "I saw this tomato monster thing runnin' around. It shouldn't be too hard, I've fought slimes nastier than this thing. It's nastier than that big pink slime from earlier, but there's nastier slimes out there is what I'm sayin'. Anyways, it tends to bury itself, but the top of it will stick up on the surface durin' daytime", "Ya got that tomato? Hand it over here. ERGH, its head is still movin'! Here, uh, you can keep it.", ModContent.ItemType<Items.Quest.RogueTomato>(), () => true, () => JoostWorld.downedRogueTomato, () => true, TomatoReward),
            new HuntInfo(ModContent.NPCType<WoodGuardian>(), 0.3f, "Well, turns out ya angered some kind of ancient forest guardian by chopping down trees fer wood. As long as ya replant them it's fine! It'll be somewhere in the forest", "Nice work with that tree monster! Whuzzat ya got there? Some kinda seed? It looks to be moving... Oh! It just sprouted a little baby tree guy! He's cute, here ya take care of him, okay?", ModContent.ItemType<Items.Quest.WoodGuardian>(), () => true, () => JoostWorld.downedWoodGuardian, () => true, WoodReward),
            new HuntInfo(NPCID.KingSlime, 1f, "Ya know how I said there are slimes nastier than that tomato? This is one of 'em. The King of slimes 'imself! He'll be found rarely nearish to the ocean. He will also show up if it starts rainin' slimes. He can also be summoned with a Slime Crown", "Good job takin' down the slime king! Here's something fer ya troubles.", ModContent.ItemType<Items.Quest.KingSlime>(), () => true, () => NPC.downedSlimeKing, () => true, KingSlimeReward),
            new HuntInfo(ModContent.NPCType<FloweringCactoid>(), 1.1f, "Ya know those Cactoid things in the desert? Well they got a leader now. I'm pretty sure it's conspiring to destroy us all! Find it and slay it!", "Good job taking down the Cactoid! Hmm, there seems to be some kinda badge in the flower. Here, you take it", ModContent.ItemType<Items.Quest.FloweringCactoid>(), () => NPC.downedSlimeKing, () => JoostWorld.downedFloweringCactoid, () => true, FloweringCactoidReward),
            new HuntInfo(NPCID.EyeofCthulhu, 2f, "Do ya feel that evil presence watching us? That's because it's a GIANT FRICKIN' EYEBALL! This thing might show up at night, otherwise use a Suspicious Looking Eye", "Good job taking down the Eyeball with a mouth! Here, take this.", ModContent.ItemType<Items.Quest.EyeOfCthulhu>(), () => true, () => NPC.downedBoss1, () => true, EyeReward),
            new HuntInfo(ModContent.NPCType<ICU>(), 2.1f, "There are far too many eye monsters in this world. I saw one that was four of 'em at once amalgamated together! It saw me too, of course, so I started thrown' my knives at it when it started shootin' lasers! I promptly went back indoors. It'll roam around at night, if it's nearby I'll help ya' out.", "This thing is frickin' disgusting. EERUGUHUH! It blinked!", ModContent.ItemType<Items.Quest.ICU>(), () => NPC.downedBoss1, () => JoostWorld.downedICU, () => true, ICUReward),
            new HuntInfo(NPCID.BrainofCthulhu, 3f, "Ya know those caverns in the crimson? Well deep inside there are these demonic heart things. Legend has it that if you destroy three of them they summon an ancient evil! Summon and then slay this evil! You can also use a Bloody Spine to summon it too.", "Good job taking down the Brain of Cthulhu! Strange how it also had a heart in it.", ModContent.ItemType<Items.Quest.BrainOfCthulhu>(), () => WorldGen.crimson, () => NPC.downedBoss2, () => WorldGen.crimson, BrainReward),
            new HuntInfo(NPCID.EaterofWorldsHead, 3f, "Ya know those chasms in the corruption? Well deep inside there are these shadow orbs. Legend has it that if you smash three of them they summon an ancient evil! Summon and then slay this evil! You can also use Worm food to summon it too.", "Good job taking down the Eater of Worlds! Good thing it doesn't actually eat worlds!", ModContent.ItemType<Items.Quest.EaterOfWorlds>(), () => !WorldGen.crimson, () => NPC.downedBoss2, () => !WorldGen.crimson, EaterReward),
            new HuntInfo(ModContent.NPCType<SporeSpawn>(), 3.1f, "Ya know those spores in the unnerground jungle? Well I saw what seems to be where they come from! It's this big angry plant thing with some kinda shell. Go to the unnerground jungle and kill it!", "Good job takin' it down! Looks like some kinda, uh, organ in here that creates the spores. You take it", ModContent.ItemType<Items.Quest.SporeSpawn>(), () => NPC.downedBoss2, () => JoostWorld.downedSporeSpawn, () => true, SpoReward),
            new HuntInfo(NPCID.QueenBee, 4f, "While down in the jungle ya may have come across one of those giant beehives. Turns out, each of 'em gots a giant Queen! Take it down before it kills us all! It can be summoned by killin' the larva within the hive or with an Abeemination", "Good job takin' down the Queen o' Bees! Here's take, uh, this special flail! It's not just a hive stuck to a handle together with honey!", ModContent.ItemType<Items.Quest.QueenBee>(), () => true, () => NPC.downedQueenBee, () => true, BeeReward),
            new HuntInfo(ModContent.NPCType<Roc>(), 4.1f, "I was climbin' up a mountain when a giant frickin' bird swooped down and grabbed me! I struggled meself out of it's talons and fell into a nearby pond. That thing is vicious! Ascend towards the heavens and slay this oversized chicken!", "Nice work groundin' that bird. Here, you should use these wings. What're ya givin' me that look fer? Just strap 'em to your back, I'm sure it'll work!", ModContent.ItemType<Items.Quest.Roc>(), () => NPC.downedQueenBee, () => JoostWorld.downedRoc, () => true, RocReward),
            new HuntInfo(NPCID.SkeletronHead, 5f, "That old man outside the dungeon said he was cursed! Apparantly his master can be summoned at night and defeating him will lift the curse. The old man told me I'd only get meself killed, but I think that yer strong enough now to do it.", "Good job decursin' that old man! I'm sure he much appreciates it. Here, take this before going down into that skeleton infested dungeon.", ModContent.ItemType<Items.Quest.Skeletron>(), () => true, () => NPC.downedBoss3, () => true, SkeletronReward),
            new HuntInfo(ModContent.NPCType<SkeletonDemoman>(), 5.1f, "There's all sorts of dangerous things in the dungeon, but currently it seems the biggest danger is a demolitions skeleton! It plants landmines, throws grenades, and even has a giant cannon of doom! Take it out before it uses that cannon to destroy us all! Also, watch yer step.", "Damn, that thing is huge! Hang on, it's jammed. *POP* There we go! Here, try not get us all killed.", ModContent.ItemType<Items.Quest.SkeletonDemoman>(), () => NPC.downedBoss3, () => JoostWorld.downedSkeletonDemoman, () => true, DemomanReward),
            new HuntInfo(ModContent.NPCType<IdleCactusWorm>(), 6.5f, "Ya know those cactus worms? They're the most delicious kind of cactus! Anyways, there seems to be some huge one in the underground desert. You ought to use that cannon to clear out a large enough space to fight it, it's gonna be mighty nasty when you wake it up!", "Hang on here, so you killed the big cacto worm, and then an even BIGGER one showed up after? Jegus, you should be glad you weren't killed too hard! Anyways, it seems there's less cactus worms now that you killed their leader. I think the cactus people will finally be able to move back into the desert now! Ya better make a house for them.", ModContent.ItemType<Items.Quest.GrandCactusWorm>(), () => NPC.downedBoss3, () => JoostWorld.downedCactusWorm, () => true, CactoWormReward),
            new HuntInfo(ModContent.NPCType<ImpLord>(), 6.8f, "Go to hell! Literally! Dig down until ya find yerself surrounded by lava and demons! Among those demons are fire imps: little teleportin' buggers that shoot fire! They have a leader with wings that's especially dangerous. Watch out for his giant fire blast attack! Ya can use a sword to reflect it back at him but if you fail to it hurts a lot!", "Good job taking down the Imp Lord! Here, ya can use the tail as a whip or somethin'", ModContent.ItemType<Items.Quest.ImpLord>(), () => JoostWorld.downedCactusWorm, () => JoostWorld.downedImpLord, () => true, ImpReward),
            new HuntInfo(NPCID.WallofFlesh, 7f, "I think it's time for ya to fight what may be the nastiest creature ever: the Wall o' Flesh! This horrific monstrosity is summoned by sacrificin' a special doll into the magma of hell. Ya will find the doll carried by some of the demons down there. Be well prepared for this one.", "Ya did it! Fantastic! But it turns out defeating the Wall o' Flesh released the ancient spirits of light and dark into the world. All sorts of nasties will appear as a result of that. Keep yer eyes peeled.", ModContent.ItemType<Items.Quest.WallOfFlesh>(), () => JoostWorld.downedRogueTomato && JoostWorld.downedWoodGuardian && JoostWorld.downedFloweringCactoid && JoostWorld.downedSporeSpawn && JoostWorld.downedRoc && JoostWorld.downedSkeletonDemoman && JoostWorld.downedImpLord, () => Main.hardMode, () => true, WofReward),
            new HuntInfo(ModContent.NPCType<StormWyvernHead>(), 7.5f, "Ya know those flying wolf-snake things? What were they called again? Lindwurms? Whatever they're called, I noticed a particularly nasty-lookin' one fly over last it rained. What's nasty 'bout it? It shot a freakin' LIGHTNIN' bolt outta it's mouth! That thing is too dangerous to be left roamin' free! When it rains, take to the skies and take it down! But important tip before ya go! When it turns yellow that means it's a chargin'. When that happens: TAKE COVER. Ya may think yer fast, but ya ain't gonna dodge a lightnin' bolt!", "Good job grounding that, uh, flying wolf-snake. Wyrm? Whatever, here's ya reward. Whuzzat? They're called wyverns? Naw, that don't sound right.", ModContent.ItemType<Items.Quest.StormWyvern>(), () => Main.hardMode, () => JoostWorld.downedStormWyvern, () => Main.hardMode, StormWyvernReward)
            };
        }
        internal void AddHunt(int NPC, float progression, string questText, string completeText, int questItem, Func<bool> questAvailable, Func<bool> questCompleted, Func<bool> showQuest, Action<Player> reward, int xFrameCount = 1)
        {
            hunts.Add(new HuntInfo(NPC, progression, questText, completeText, questItem, questAvailable, questCompleted, showQuest, reward, xFrameCount));
        }
        private void TomatoReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<TomatoHead>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
        }
        private void WoodReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<Sapling>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Wood, 150);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin);
        }
        private void KingSlimeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<FireFlinger>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Gel, 100);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SlimeStaff);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
        }
        private void FloweringCactoidReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<CactoidCommendation>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 2);
        }
        private void EyeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<EyeballStaff>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Binoculars);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 2);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void ICUReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<ObservantStaff>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Lens, 2);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.BlackLens, 2);
        }
        private void EaterReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<CorruptPommel>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void BrainReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<CrimsonPommel>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void SpoReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<Sporgan>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 4);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.JungleSpores, 20);
        }
        private void BeeReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<TheHive>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 4);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.HoneyedGoggles);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
        }
        private void RocReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<RocWings>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 5);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Feather, 20);
        }
        private void SkeletronReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<Bonesaw>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Bone, 20);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 5);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void DemomanReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<DoomCannon>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LandMine, 3);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.Grenade, 20);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 6);
        }
        private void CactoWormReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<CactusBoots>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 6);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SilverCoin, 50);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void ImpReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<TailWhip>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<ImpLordFlame>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 7);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.FireblossomSeeds, 15);
        }
        private void WofReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<FleshShield>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 10);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.LifeCrystal);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.ManaCrystal);
        }
        private void StormWyvernReward(Player player)
        {
            Mod mod = JoostMod.instance;
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<StormWyvernScroll>());
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.GoldCoin, 15);
            player.QuickSpawnItem(player.GetSource_GiftOrReward(), ItemID.SoulofFlight, 20);
        }
        
        public override void Load()
        {
            instance = this;
            ArmorAbilityHotKey = KeybindLoader.RegisterKeybind(this, "Armor Ability", "Z");

            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> projShaderRef = new Ref<Effect>(this.Assets.Request<Effect>("Shaders/JuiceProjectileShaders", AssetRequestMode.ImmediateLoad).Value);

                GameShaders.Misc["TrueGungnirBeam"] = new MiscShaderData(projShaderRef, "GungnirBeamShaderPass");
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