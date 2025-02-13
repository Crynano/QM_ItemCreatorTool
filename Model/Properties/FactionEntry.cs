using MGSC;

namespace QM_ItemCreatorTool.Properties
{
    public class FactionEntry
    {
        public string Name { get; set; } = string.Empty;
        public int TechLevel { get; set; } = 1;
        public float Weight { get; set; } = 1;
        public float Points { get; set; } = 1;
        public RewardType RewardType { get; set; } = RewardType.None;

        public FactionEntry() { }

        public FactionEntry(string factionName, ContentDropRecord contentDropRecord)
        {
            this.Name = factionName;
            this.TechLevel = contentDropRecord.TechLevel;
            this.Weight = contentDropRecord.Weight;
            this.Points = contentDropRecord.Points;
        }

        public List<ContentDropRecord> GetContentDrop(string itemID)
        {
            return new List<ContentDropRecord>()
            {
                // Usually its just 1 entry... so...
                new ContentDropRecord() 
                {
                    TechLevel = this.TechLevel,
                    ContentIds = new List<string> { itemID },
                    Weight = this.Weight,
                    Points = this.Points,
                }
            };
        }
    }

    public enum RewardType
    {
        None,
        Reward,
        Trade,
        Units
    }
}