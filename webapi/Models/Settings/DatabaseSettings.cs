namespace webapi.Models.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string TitlesCollectionName { get; set; } = null!;

        public string LicensesCollectionName { get; set; } = null!;

        public string QuestionsCollectionName { get; set; } = null!;

        public string NewsCollectionName { get; set; } = null!;

        public string TrafficSignTypesCollectionName { get; set; } = null!;

        public string TrafficFineTypesCollectionName { get; set; } = null!;

        public string DecreesCollectionName { get; set; } = null!;
    }
}