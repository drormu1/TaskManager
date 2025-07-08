using TaskManager.Data.Entities;

namespace TaskManager.Data.Tasks
{
    public class DevelopmentTask : TaskBase
    {
        public string FeatureName { get; set; } = string.Empty;
        public string CodeReviewStatus { get; set; } = string.Empty;
    }
}