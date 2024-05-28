using TypingTrainerProject.App;
using TypingTrainerProject.TrainingModes;
using TypingTrainerProject.UserInterface;


var userInterface = new ConsoleUserInterface();
var allModes = new List<TrainingMode> {
    new StagedTrainingMode(),
    new CustomTrainingMode(),
    new RealTextTrainingMode()
};

var app = new TypingTrainerApp(allModes, userInterface);
app.Start();