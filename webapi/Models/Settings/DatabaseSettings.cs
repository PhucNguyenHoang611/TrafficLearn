namespace webapi.Models.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string TitlesCollectionName { get; set; } = null!;

        public string LicensesCollectionName { get; set; } = null!;

        public string LicenseTitlesCollectionName { get; set; } = null!;

        public string QuestionsCollectionName { get; set; } = null!;

        public string AnswersCollectionName { get; set; } = null!;

        public string ExaminationsCollectionName { get; set; } = null!;

        public string ExaminationQuestionsCollectionName { get; set; } = null!;

        public string NewsCollectionName { get; set; } = null!;

        public string TrafficSignsCollectionName { get; set; } = null!;

        public string TrafficSignTypesCollectionName { get; set; } = null!;

        public string TrafficFinesCollectionName { get; set; } = null!;

        public string TrafficFineTypesCollectionName { get; set; } = null!;

        public string DecreesCollectionName { get; set; } = null!;

        public string ArticlesCollectionName { get; set; } = null!;

        public string ClausesCollectionName { get; set; } = null!;

        public string PointsCollectionName { get; set; } = null!;
    }
}