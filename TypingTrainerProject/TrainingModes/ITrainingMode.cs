using TypingTrainerProject.DataClasses;

namespace TypingTrainerProject.TrainingModes;

public interface ITrainingMode {
    
    public string Name { get; }
    
    public string Description { get; }
    
    public string[] GettingInputMassage { get; }
    public Predicate<string>[] CorrectInputCondition { get; }

    public Exercise? GetExercise(string exerciseNumber);
}