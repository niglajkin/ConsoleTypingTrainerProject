using TypingTrainerProject.TrainingModes;
using TypingTrainerProject.UserInterface;

namespace TypingTrainerProject.App;

public class TypingTrainerApp(
    List<TrainingMode> allTrainingModes,
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

            var userModeNumberChoice = int.Parse(userModeChoice);
            var trainingMode = allTrainingModes.Find(mode => mode.Number == userModeNumberChoice)!;
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

            foreach (var mode in allTrainingModes) {
                massage += $"{mode.Number}){mode.Name}. {mode.Description}.{Environment.NewLine}";
            }

            massage += "Print [exit] to stop the application.";

            return [massage];
        }
    }

    private Predicate<string>[] CorrectModeChosenCondition => [
        input => {
            var inputIsExit = input == "exit";
            var inputIsNumber = int.TryParse(input, out var inputAsNumber);
            var inputIsModeNumber = allTrainingModes.Any(mode => mode.Number == inputAsNumber);

            return inputIsExit || (inputIsNumber && inputIsModeNumber);
        }
    ];
}