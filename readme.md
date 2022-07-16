# Exercise Tracker   ![GitHub commit merge status](https://img.shields.io/github/commit-status/CodeDreamer06/ExerciseTracker/main/c7607ac05699fb8a18efcded0d532822ab6d7c7a)&nbsp;  ![GitHub issues](https://img.shields.io/github/issues/Codedreamer06/ExerciseTracker)
A simple exercise logger for you to measure your progress!
## Getting Started:
Note: Use help to display this message
* **exit or 0**: stop the program
* **show**: display existing logs
* **add**: create an exercise log
* **update [id]**: change an existing log
* **remove [id]**: delete a log
## Main Program Flow UML
```mermaid
classDiagram
  class Program {
    +WorkoutController service
    +Main()
  }

  class WorkoutController {
    +Add()
    +Show()
    +Update()
    +Remove()
  }

  class WorkoutControllerUtils {
    +WorkoutRepository IWorkoutRepository
    -GetRepository(repositoryType) IWorkoutRepository
    -SetRepositoryFromInput(keyword)
    -ReplaceEmptyFields(log) Workout
  }

  class IWorkoutRepository {
    <<Interface>>
    +int GetCount()
    +Create(log)
    +List Read()
    +Workout ReadUsingRelativeId(id)
    +Update(log)
    +Delete(id)
  }

  class WeightsWorkoutRepository {
    +Execute(query)
  }

  class FlexibilityWorkoutRepository {
    +Execute(query, parameters)
  }

  class SqliteDbContext {
    -string FolderPath
    -string FileName
    -string ConnectionString

    +CreateDb()
    +CreateTable(handler, tableName)
  }

  class CardioWorkoutContext {
    +DbSet CardioWorkouts
    -OnConfiguring(options)
  }
 
  WorkoutController <|.. Program
  WorkoutControllerUtils -- WorkoutController
  CardioWorkoutRepository ..|> IWorkoutRepository
  WeightsWorkoutRepository ..|> IWorkoutRepository
  FlexibilityWorkoutRepository ..|> IWorkoutRepository
  CardioWorkoutRepository --> CardioWorkoutContext
  WeightsWorkoutRepository --> SqliteDbContext
  FlexibilityWorkoutRepository --> SqliteDbContext
```
## Utility & Model UML
```mermaid
classDiagram
  class Workout {
    +int Id
    +DateTime Start
    +DateTime End
    +TimeSpan Duration
    +string Comments
    +Workout SetDuration()
    +Workout GetDeepClone()
  }

  class NullWorkout {
    Id = -1
  }

  class WorkoutToDbDTO {
    +int Id
    +string Start
    +string End
    +double DurationInSeconds
    +string Comments
    +Workout ConvertToWorkout(log)
    Deconstruct()
  }

  class Helpers {
    +string MainMessage
    +string NoLogsMessage
    +string DateTimeFormat
    +string SqlInsert
    +string SqlRead
    +string SqlUpdate
    +string SqlDelete
    -Dictionary HeaderCharacterMap
    +DisplayTable(records, emptyMessage)
    +string CorrectSpelling(command)
  }

  class ExtensionMethods {
    +int GetNumber()
    +string RemoveKeyword()
  }
NullWorkout <|-- Workout
```
## Motivation & Features
This project is a simple command line based tool that helps you to keep track of your workouts. The purpose of this project is to gain an understanding of the repository pattern in C#. To further cement my comprehension using this pattern, I explored using two kinds of workouts - cardio and weight workouts. Cardio workouts are fetched using the Entity Framework and SQLite for weight workouts.

## Further reading & References
* EF Core docs: https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
* Implementing the repository pattern: https://www.programmingwithwolfgang.com/repository-pattern-net-core/
## Contribution
If you have any ideas,   [open an issue](https://github.com/CodeDreamer06/ExerciseTracker/issues/new)  and tell me what you think. If you'd like to contribute, please fork the repository and make changes as you'd like. Pull requests are warmly welcome.
1. Fork it
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new pull request