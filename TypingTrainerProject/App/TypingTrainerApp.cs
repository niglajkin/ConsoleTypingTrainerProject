using TypingTrainerProject.TrainingModes;
using TypingTrainerProject.UserInterface;

namespace TypingTrainerProject.App;

public class TypingTrainerApp(
    Dictionary<string, ITrainingMode> allTrainingModes,
    IUserInterface userInterface
) {
    public void Start() {
        var appIsRunning = true;

        while (appIsRunning) {
            var userModeChoice = userInterface.GetUserInput(ChoosingModeMassage, CorrectModeChosenCondition);

            if (userModeChoice == "exit") {
                appIsRunning = false;
                continue;
            }

            var trainingMode = allTrainingModes[userModeChoice];
            var chosenExerciseNumber = userInterface.GetUserInput(
                trainingMode.GettingInputMassage,
                trainingMode.CorrectInputCondition
            );

            var exercise = trainingMode.GetExercise(chosenExerciseNumber);

            var exitWasChosen = exercise == null;
            if (!exitWasChosen) {
                userInterface.RunExercise(exercise!);
            }
        }
    }

    private string[] ChoosingModeMassage {
        get {
            var massage = $"Choose typing training type:{Environment.NewLine}";

            foreach (var (modeNumber, mode) in allTrainingModes) {
                massage += $"{modeNumber}){mode.Name}. {mode.Description}.{Environment.NewLine}";
            }

            massage += "Print [exit] to stop the application.";

            return [massage];
        }
    }

    private Predicate<string>[] CorrectModeChosenCondition =>
        [str => str == "exit" || allTrainingModes.ContainsKey(str)];
}