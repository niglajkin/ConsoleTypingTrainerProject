using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.UserInterface;

public interface IUserInterface {
    public string GetUserInput(string[] askingInputMassage, Predicate<string>[] correctInputConditions);

    void RunExercise(Exercise exercise);
}