using Terraria;
using Terraria.ID;
 
namespace JoostMod.NPCs
{
    public class CactusWormTail : CactusWorm
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus Worm");
        }
        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.DiggerTail);
            npc.aiStyle = -1; 
            npc.damage = 5;
            npc.defense = 10;         
            npc.knockBackResist = 0f;
            npc.width = 12;
            npc.height = 16;       
            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.noGravity = true;
            npc.dontCountMe = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
        }
        public override void Init()
        {
            base.Init();
            tail = true;
        }
        public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SucculentCactus"), 1);
            }
		}
    }
}