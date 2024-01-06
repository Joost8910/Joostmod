using Terraria.GameContent.ItemDropRules;

namespace JoostMod.ItemDropRules.DropConditions
{
    public class LegendDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            JoostPlayer modPlayer = info.player.GetModPlayer<JoostPlayer>();
            return modPlayer.isLegend && !modPlayer.legendOwn && !info.npc.SpawnedFromStatue;
        }

        public bool CanShowItemDropInUI()
        {
            return false;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class EvilStoneDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return !info.npc.SpawnedFromStatue;
        }

        public bool CanShowItemDropInUI()
        {
            return false;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
    public class SDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            JoostPlayer modPlayer = info.player.GetModPlayer<JoostPlayer>();
            return modPlayer.isSaitama && !modPlayer.SaitamaOwn;
        }

        public bool CanShowItemDropInUI()
        {
            return false;
        }

        public string GetConditionDescription()
        {
            return null;
        }
    }
}
