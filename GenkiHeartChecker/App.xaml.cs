using GenkiHeartChecker.Components;

namespace GenkiHeartChecker
{
    public partial class App : Application
    {
        public static DBService _dbService {  get; private set; }
        public App(DBService db)
        {
            InitializeComponent();
            _dbService = db;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "GenkiHeartChecker"};
        }
    }
}
