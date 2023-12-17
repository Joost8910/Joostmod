using JoostMod.Items.Materials;
using JoostMod.Items.Placeable;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace JoostMod.NPCs
{
    public class CactusWormHead : CactusWorm
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm");
        }
        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.DiggerHead);
            NPC.aiStyle = -1;
            NPC.lifeMax = 25;        
            NPC.damage = 15;    
            NPC.defense = 0;         
            NPC.knockBackResist = 0f;
            NPC.width = 12;
            NPC.height = 12;       
            NPC.noGravity = true;           
            NPC.noTileCollide = true;     
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.behindTiles = true;
            NPC.value = Item.buyPrice(0, 0, 0, 75);
            NPC.npcSlots = 1f;
            NPC.netAlways = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<CactusWormBanner>();
        }
        public override void Init()
        {
            base.Init();
            head = true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SucculentCactus>()));
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
			return !spawnInfo.Player.ZoneBeach && !spawnInfo.PlayerInTown && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneHallow ? (JoostWorld.downedCactusWorm ? 0.005f : 0.025f) : 0f;
		}
    }
}