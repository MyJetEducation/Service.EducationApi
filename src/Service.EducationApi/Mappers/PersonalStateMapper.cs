using System.Linq;
using Service.EducationApi.Models.TaskModels;
using Service.TutorialPersonal.Grpc.Models.State;

namespace Service.EducationApi.Mappers
{
	public static class PersonalStateMapper
	{
		public static TestScoreResponse ToModel(this TestScoreGrpcResponse response) => response != null ? new TestScoreResponse
		{
			IsSuccess = response.IsSuccess,
			Unit = response.Unit.ToModel()
		} : null;

		public static PersonalStateResponse ToModel(this PersonalStateGrpcResponse response) => response != null ? new PersonalStateResponse
		{
			Available = response.Available,
			Duration = response.Duration,
			Units = response.Units?.Select(unit => unit.ToModel())
		} : null;

		public static PersonalStateUnit ToModel(this PersonalStateUnitGrpcModel grpcModel) => grpcModel != null ? new PersonalStateUnit
		{
			Index = grpcModel.Index,
			Duration = grpcModel.Duration,
			TestScore = grpcModel.TestScore,
			HabitCount = grpcModel.HabitCount,
			SkillCount = grpcModel.SkillCount,
			Tasks = grpcModel.Tasks?.Select(task => task.ToModel())
		} : null;

		public static PersonalStateTask ToModel(this PersonalStateTaskGrpcModel grpcModel) => grpcModel != null ? new PersonalStateTask
		{
			TaskId = grpcModel.TaskId,
			TestScore = grpcModel.TestScore,
			Duration = grpcModel.Duration,
			CanRetry = grpcModel.CanRetry
		} : null;
	}
}