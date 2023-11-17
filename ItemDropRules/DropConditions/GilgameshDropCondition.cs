using JoostMod.NPCs.Bosses;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace JoostMod.ItemDropRules.DropConditions
{
    public class GilgameshDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            bool doDrop = !NPC.AnyNPCs(ModContent.NPCType<Enkidu>());
            if (info.npc.type == ModContent.NPCType<Enkidu>())
            {
                doDrop = !NPC.AnyNPCs(ModContent.NPCType<Gilgamesh>()) && !NPC.AnyNPCs(ModContent.NPCType<Gilgamesh2>());
            }
            return doDrop;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
}
