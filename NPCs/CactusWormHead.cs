using Terraria;
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
            npc.CloneDefaults(NPCID.DiggerHead);
            npc.aiStyle = -1;
            npc.lifeMax = 25;        
            npc.damage = 15;    
            npc.defense = 0;         
            npc.knockBackResist = 0f;
            npc.width = 12;
            npc.height = 12;       
            npc.noGravity = true;           
            npc.noTileCollide = true;     
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.behindTiles = true;
            npc.value = Item.buyPrice(0, 0, 0, 75);
            npc.npcSlots = 1f;
            npc.netAlways = true;
            banner = npc.type;
			bannerItem = mod.ItemType("CactusWormBanner");
        }
        public override void Init()
        {
            base.Init();
            head = true;
        }
        public override void NPCLoot()
		{
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SucculentCactus"), 1);
		}
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY];
			return !spawnInfo.player.ZoneBeach && !spawnInfo.playerInTown && !spawnInfo.invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.player.ZoneDesert && !spawnInfo.player.ZoneCorrupt && !spawnInfo.player.ZoneCrimson && !spawnInfo.player.ZoneHoly ? (JoostWorld.downedCactusWorm ? 0.005f : 0.025f) : 0f;
		}
    }
}