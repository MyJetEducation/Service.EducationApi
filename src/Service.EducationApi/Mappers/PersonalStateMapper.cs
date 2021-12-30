using System.Linq;
using Service.EducationApi.Models.TaskModels;
using Service.TutorialPersonal.Grpc.Models.State;

namespace Service.EducationApi.Mappers
{
	public static class PersonalStateMapper
	{
		public static TestScoreResponse ToModel(this TestScoreGrpcResponse response) => new TestScoreResponse
		{
			IsSuccess = response.IsSuccess,
			Unit = response.Unit.ToModel()
		};

		public static PersonalStateResponse ToModel(this PersonalStateGrpcResponse response)
		{
			return new PersonalStateResponse
			{
				Available = response.Available,
				Duration = response.Duration,
				Units = response.Units.Select(unit => unit.ToModel())
			};
		}

		public static PersonalStateUnit ToModel(this PersonalStateUnitGrpcModel grpcModel) => new PersonalStateUnit
		{
			Index = grpcModel.Index,
			Duration = grpcModel.Duration,
			TestScore = grpcModel.TestScore,
			HabitCount = grpcModel.HabitCount,
			SkillCount = grpcModel.SkillCount,
			Tasks = grpcModel.Tasks.Select(task => task.ToModel())
		};

		public static PersonalStateTask ToModel(this PersonalStateTaskGrpcModel grpcModel) => new PersonalStateTask
		{
			TaskId = grpcModel.TaskId,
			TestScore = grpcModel.TestScore,
			Duration = grpcModel.Duration,
			CanRetry = grpcModel.CanRetry
		};
	}
}