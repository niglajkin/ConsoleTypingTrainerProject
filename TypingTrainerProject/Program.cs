using TypingTrainerProject.App;
using TypingTrainerProject.TrainingModes;
using TypingTrainerProject.UserInterface;


var userInterface = new ConsoleUserInterface();
var allModes = new Dictionary<string, ITrainingMode> {
    { "1", new StagedTrainingMode() },
    { "2", new CustomTrainingMode() },
    { "3", new RealTextTrainingMode() }
};

var app = new TypingTrainerApp(allModes, userInterface);
app.Start();

