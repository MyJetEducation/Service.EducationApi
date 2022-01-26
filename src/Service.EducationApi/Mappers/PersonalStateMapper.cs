using System.Linq;
using Service.EducationApi.Models;
using Service.TutorialPersonal.Grpc.Models.State;
using Enum = System.Enum;

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
				HabitValue = grpcModel.HabitValue,
				HabitProgress = grpcModel.HabitProgress,
				SkillValue = grpcModel.SkillValue,
				SkillProgress = grpcModel.SkillProgress,
				Achievements = grpcModel.Achievements.Select(Enum.GetName).ToArray()
			}
			: null;

		private static PersonalStateUnit ToModel(this PersonalStateUnitGrpcModel grpcModel) => grpcModel != null
			? new PersonalStateUnit
			{
				Unit = grpcModel.Unit,
				TestScore = grpcModel.TestScore,
				Tasks = grpcModel.Tasks?.Select(task => task.ToModel())
			}
			: null;

		private static PersonalStateTask ToModel(this PersonalStateTaskGrpcModel grpcModel) => grpcModel != null
			? new PersonalStateTask
			{
				Task = grpcModel.Task,
				TestScore = grpcModel.TestScore,
				Retry = grpcModel.RetryInfo.ToModel()
			}
			: null;

		private static RetryInfo ToModel(this TaskRetryInfoGrpcModel grpcModel) => grpcModel != null
			? new RetryInfo
			{
				InRetry = grpcModel.InRetry,
				CanRetryByCount = grpcModel.CanRetryByCount,
				CanRetryByTime = grpcModel.CanRetryByTime
			}
			: null;
	}
}