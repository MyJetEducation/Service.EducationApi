using System.Linq;
using Service.EducationApi.Models.TaskModels;
using Service.TutorialPersonal.Grpc.Models.State;

namespace Service.EducationApi.Mappers
{
	public static class PersonalStateMapper
	{
		public static TestScoreResponse ToModel(this TestScoreGrpcResponse response) => response != null
			? new TestScoreResponse
			{
				IsSuccess = response.IsSuccess,
				Unit = response.Unit?.ToModel()
			}
			: null;

		public static PersonalStateResponse ToModel(this PersonalStateGrpcResponse response) => response != null
			? new PersonalStateResponse
			{
				Available = response.Available,
				Duration = response.Duration,
				Units = response.Units?.Select(unit => unit.ToModel()),
				TotalProgress = response.TotalProgress.ToModel()
			}
			: null;

		public static FinishUnitResponse ToModel(this FinishUnitGrpcResponse grpcResponse) => grpcResponse != null
			? new FinishUnitResponse
			{
				Unit = grpcResponse.Unit?.ToModel(),
				TotalProgress = grpcResponse.TotalProgress.ToModel()
			}
			: null;

		private static TotalProgressResponse ToModel(this TotalProgressStateGrpcModel grpcModel) => grpcModel != null
			? new TotalProgressResponse
			{
				HabitName = grpcModel.HabitName,
				HabitProgress = grpcModel.HabitProgress,
				SkillName = grpcModel.SkillName,
				SkillProgress = grpcModel.SkillProgress,
				Achievements = grpcModel.Achievements
			}
			: null;

		private static PersonalStateUnit ToModel(this PersonalStateUnitGrpcModel grpcModel) => grpcModel != null
			? new PersonalStateUnit
			{
				Index = grpcModel.Index,
				Duration = grpcModel.Duration,
				TestScore = grpcModel.TestScore,
				HabitCount = grpcModel.HabitCount,
				SkillCount = grpcModel.SkillCount,
				Tasks = grpcModel.Tasks?.Select(task => task.ToModel())
			}
			: null;

		private static PersonalStateTask ToModel(this PersonalStateTaskGrpcModel grpcModel) => grpcModel != null
			? new PersonalStateTask
			{
				TaskId = grpcModel.TaskId,
				TestScore = grpcModel.TestScore,
				Duration = grpcModel.Duration,
				CanRetry = grpcModel.CanRetry
			}
			: null;
	}
}